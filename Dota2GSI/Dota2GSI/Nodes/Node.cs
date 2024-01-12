using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// The base Node for GSI states.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The json data for this Node.
        /// </summary>
        protected JObject _ParsedData;

        internal Node(JObject parsed_data)
        {
            _ParsedData = parsed_data;
        }

        internal JToken GetJToken(string property_name)
        {
            if (_ParsedData != null)
            {
                JToken value;

                if (_ParsedData.TryGetValue(property_name, out value))
                {
                    return value;
                }
            }

            return null;
        }

        internal JObject GetJObject(string property_name)
        {
            var jtoken = GetJToken(property_name);

            if (jtoken != null)
            {
                return jtoken as JObject;
            }

            return null;
        }


        internal string GetString(string property_name)
        {
            var value = GetJToken(property_name);

            if (value != null)
            {
                return value.ToString();
            }

            return "";
        }

        internal int GetInt(string property_name)
        {
            var value = GetJToken(property_name);

            if (value != null)
            {
                return Convert.ToInt32(value.ToString());
            }

            return -1;
        }

        internal long GetLong(string property_name)
        {
            var value = GetJToken(property_name);

            if (value != null)
            {
                return Convert.ToInt64(value.ToString());
            }

            return -1;
        }

        internal float GetFloat(string property_name)
        {
            var value = GetJToken(property_name);

            if (value != null)
            {
                return Convert.ToSingle(value.ToString());
            }

            return -1;
        }

        internal T GetEnum<T>(string property_name)
        {
            var string_value = GetString(property_name);

            if (!string.IsNullOrWhiteSpace(string_value))
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), string_value, true);
                }
                catch
                {
                }
            }

            return (T)Enum.Parse(typeof(T), "Undefined", true);
        }

        internal bool GetBool(string Name)
        {
            var value = GetJToken(Name);

            if (value != null)
            {
                return value.ToObject<bool>();
            }

            return false;
        }

        internal IJEnumerable<JToken> GetArray(string Name)
        {
            JToken value = GetJToken(Name);

            if (value != null && value.HasValues)
            {
                return value.Values();
            }

            return new JEnumerable<JToken>();
        }

        internal void GetMatchingTokens(JObject data, Regex regex, JTokenType type, Action<Match, JToken> match_callback)
        {
            if (data == null)
            {
                return;
            }

            foreach (var property in data.Properties())
            {
                string property_name = property.Name;

                if (regex.IsMatch(property_name) && property.Value.Type == type)
                {
                    var match = regex.Match(property_name);
                    var matched_token = property.Value;

                    match_callback(match, matched_token);
                }
            }
        }

        internal void GetMatchingObjects(JObject data, Regex regex, Action<Match, JObject> match_callback)
        {
            GetMatchingTokens(data, regex, JTokenType.Object, (Match match, JToken token) =>
            {
                match_callback(match, token as JObject);
            });
        }

        internal void GetMatchingIntegers(JObject data, Regex regex, Action<Match, int> match_callback)
        {
            GetMatchingTokens(data, regex, JTokenType.Integer, (Match match, JToken token) =>
            {
                match_callback(match, Convert.ToInt32(token.ToString()));
            });
        }

        internal void GetMatchingStrings(JObject data, Regex regex, Action<Match, string> match_callback)
        {
            GetMatchingTokens(data, regex, JTokenType.String, (Match match, JToken token) =>
            {
                match_callback(match, token.ToString());
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _ParsedData.ToString();
        }
    }

    /// <summary>
    /// Struct representing 2D vectors.
    /// </summary>
    public struct Vector2D
    {
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// Default constructor with given X and Y coordinates.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Equates this Vector2D object to another object.
        /// </summary>
        /// <param name="obj">The other object to compare against.</param>
        /// <returns>True if the two objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector2D other &&
                   X == other.X &&
                   Y == other.Y;
        }

        /// <summary>
        /// Calculates unique hash code for this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
