using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dota2GSI.Utils
{
    /// <summary>
    /// Class that serializes and deserializes ACF file format.
    /// </summary>
    public class ACF
    {
        /// <summary>
        /// The items of this ACF element.
        /// </summary>
        public Dictionary<string, string> Items => _items;

        /// <summary>
        /// The children of this ACF element.
        /// </summary>
        public Dictionary<string, ACF> Children => _children;

        private Dictionary<string, string> _items = new Dictionary<string, string>();
        private Dictionary<string, ACF> _children = new Dictionary<string, ACF>();

        private static Dictionary<char, char> _escape_characters = new Dictionary<char, char>() {
            { 'r', '\r' },
            { 'n', '\n' },
            { 't', '\t' },
            { '\'', '\'' },
            { '"', '"' },
            { '\\', '\\' },
            { 'b', '\b' },
            { 'f', '\f' },
            { 'v', '\v' }
        };

        public ACF()
        {
        }

        public ACF(string filename)
        {
            if (File.Exists(filename))
            {
                using (FileStream file_stream = new FileStream(filename, FileMode.Open))
                {
                    ParseStream(new StreamReader(file_stream));
                }
            }
        }

        internal ACF(StreamReader stream)
        {
            ParseStream(stream);
        }

        private void ParseStream(StreamReader stream)
        {
            bool seeking_brace = false;

            if ((char)stream.Peek() == '{')
            {
                // Consume {
                stream.Read();
                seeking_brace = true;
            }

            while (!stream.EndOfStream)
            {
                if (seeking_brace && (char)stream.Peek() == '}')
                {
                    // Consume }
                    stream.Read();
                    break;
                }

                // Attempt to read the item key
                object key = ReadValue(stream);

                if (key != null && key is string str_key)
                {
                    object value = ReadValue(stream);

                    if (value != null)
                    {
                        if (value is string str_value)
                        {
                            _items.Add(str_key.ToLowerInvariant(), str_value);
                        }
                        else if (value is ACF acf_value)
                        {
                            _children.Add(str_key.ToLowerInvariant(), acf_value);
                        }
                    }
                }

                // Skip over any whitespace characters to get to next value
                while (char.IsWhiteSpace((char)stream.Peek()))
                {
                    stream.Read();
                }
            }
        }

        private object ReadValue(StreamReader stream)
        {
            object return_value = null;

            // Skip over any whitespace characters to get to next value
            while (char.IsWhiteSpace((char)stream.Peek()))
            {
                stream.Read();
            }

            char peekchar = (char)stream.Peek();

            if (peekchar.Equals('{'))
            {
                return_value = new ACF(stream);
            }
            else if (peekchar.Equals('/'))
            {
                // Comment, read until end of line
                stream.ReadLine();
            }
            else
            {
                return_value = ReadString(stream);
            }

            return return_value;
        }

        private string ReadString(StreamReader instream)
        {
            StringBuilder builder = new StringBuilder();

            bool isQuote = ((char)instream.Peek()).Equals('"');

            if (isQuote)
            {
                instream.Read();
            }

            for (char chr = (char)instream.Read(); !instream.EndOfStream; chr = (char)instream.Read())
            {

                if (isQuote && chr.Equals('"') ||
                    !isQuote && char.IsWhiteSpace(chr)) // Arrived at end of string.
                {
                    break;
                }

                if (chr.Equals('\\')) // Fix up escaped characters.
                {
                    // Read next character.
                    char escape = (char)instream.Read();

                    if (_escape_characters.ContainsKey(escape))
                    {
                        builder.Append(_escape_characters[escape]);
                    }
                }
                else
                {
                    builder.Append(chr);
                }
            }

            return builder.ToString();
        }

        public string BuildString(int indent_amount = 0)
        {
            int longest_key_value = 0;

            foreach (var item_kvp in _items)
            {
                if (item_kvp.Key.Length > longest_key_value)
                {
                    longest_key_value = item_kvp.Key.Length;
                }
            }

            string indentation = "";
            for (int i = 0; i < indent_amount; i++)
            {
                indentation += "    ";
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item_kvp in _items)
            {
                // Indent beginning
                stringBuilder.Append(indentation);
                stringBuilder.Append($"\"{item_kvp.Key}\"");
                // Pretty print
                for (int i = item_kvp.Key.Length; i < longest_key_value; i++)
                {
                    stringBuilder.Append(' ');
                }
                // Separator between the key and value
                stringBuilder.Append("    ");
                stringBuilder.Append($"\"{item_kvp.Value}\"");
                stringBuilder.AppendLine();
            }

            foreach (var child_kvp in _children)
            {
                // Indent beginning
                stringBuilder.Append(indentation);
                stringBuilder.AppendLine($"\"{child_kvp.Key}\"");
                // Opening {
                stringBuilder.Append(indentation);
                stringBuilder.AppendLine("{");
                stringBuilder.Append(child_kvp.Value.BuildString(indent_amount + 1));
                // Closing }
                stringBuilder.Append(indentation);
                stringBuilder.AppendLine("}");
            }

            return stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return BuildString();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ACF other &&
                _items.SequenceEqual(other._items) &&
                _children.SequenceEqual(other._children);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 610350854;

            foreach (var item in _items)
            {
                hashCode = hashCode * -379045661 + item.GetHashCode();
            }

            foreach (var child in _children)
            {
                hashCode = hashCode * -379045661 + child.GetHashCode();
            }

            return hashCode;
        }
    }

    /// <summary>
    /// A class for handling Steam games
    /// </summary>
    public static class SteamUtils
    {
        /// <summary>
        /// Retrieves a path to a specified AppID
        /// </summary>
        /// <param name="game_id">The game's AppID</param>
        /// <returns>Path to the location of AppID's install</returns>
        public static string GetGamePath(int game_id)
        {
            try
            {
                string steam_path = "";

                if (OperatingSystem.IsWindows())
                {
                    try
                    {
                        steam_path = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null);
                        if (string.IsNullOrWhiteSpace(steam_path))
                        {
                            steam_path = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "InstallPath", null);
                        }
                    }
                    catch (Exception)
                    {
                        steam_path = "";
                    }
                }

                if (String.IsNullOrWhiteSpace(steam_path))
                {
                    return null;
                }

                string libraries_file = Path.Combine(steam_path, "SteamApps", "libraryfolders.vdf");
                if (File.Exists(libraries_file))
                {
                    ACF lib_data = new ACF(libraries_file);
                    var library_folders = lib_data.Children["libraryfolders"];

                    foreach (var library_entry_kvp in library_folders.Children)
                    {
                        var library_entry = library_entry_kvp.Value;
                        string library_path = library_entry.Items["path"];

                        var manifest_file = Path.Combine(library_path, "steamapps", $"appmanifest_{game_id}.acf");
                        if (File.Exists(manifest_file))
                        {
                            ACF manifest_data = new ACF(manifest_file);
                            string install_dir = manifest_data.Children["appstate"].Items["installdir"];
                            string appid_path = Path.Combine(library_path, "steamapps", "common", install_dir);
                            if (Directory.Exists(appid_path))
                            {
                                return appid_path;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}