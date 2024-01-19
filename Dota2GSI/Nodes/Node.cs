using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Has this node been used to successfully read a value from json.
        /// </summary>
        private bool _successfully_retrieved_any_value = false;

        internal Node(JObject parsed_data)
        {
            _ParsedData = parsed_data;
        }

        internal T ToEnum<T>(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), str, true);
                }
                catch
                {
                }
            }

            return (T)Enum.Parse(typeof(T), "Undefined", true);
        }

        internal JToken GetJToken(string property_name)
        {
            if (_ParsedData != null)
            {
                JToken value;

                if (_ParsedData.TryGetValue(property_name, out value))
                {
                    // Successfully retrieved a property, this must be a valid node.
                    _successfully_retrieved_any_value = true;
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

        internal JArray GetJArray(string property_name)
        {
            var jtoken = GetJToken(property_name);

            if (jtoken != null)
            {
                return jtoken as JArray;
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

            return string.Empty;
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

            return ToEnum<T>(string_value);
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
                    _successfully_retrieved_any_value = true;
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

        /// <summary>
        /// Returns validity of this node.
        /// </summary>
        /// <returns>True if the node is valid, false otherwise.</returns>
        public virtual bool IsValid()
        {
            return _successfully_retrieved_any_value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Node other &&
                _ParsedData.Equals(other._ParsedData) &&
                _successfully_retrieved_any_value.Equals(other._successfully_retrieved_any_value);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 898763153;
            hashCode = hashCode * -405816372 + _ParsedData.GetHashCode();
            hashCode = hashCode * -405816372 + _successfully_retrieved_any_value.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Helper class,<br/>
    /// A Dictionary class wrapped with proper Equals and GetHashCode functionality.<br/>
    /// Can be safely cast into Dictionary.
    /// </summary>
    /// <typeparam name="Key">Key type</typeparam>
    /// <typeparam name="Value">Value type</typeparam>
    public class NodeMap<Key, Value> : Dictionary<Key, Value>
    {
        public NodeMap()
        {
        }

        public NodeMap(Dictionary<Key, Value> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string return_string = "";
            return_string += "[";
            bool first_element = true;
            foreach (var kvp in this)
            {
                if (first_element)
                {
                    first_element = false;
                }
                else
                {
                    return_string += ", ";
                }

                return_string += $"{kvp.Key}: {kvp.Value}";
            }
            return_string += "]";
            return return_string;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is NodeMap<Key, Value> other &&
                this.SequenceEqual(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 482146548;

            foreach (var kvp in this)
            {
                hashCode = hashCode * -954782166 + kvp.Key.GetHashCode();
                hashCode = hashCode * -954782166 + kvp.Value.GetHashCode();
            }

            return hashCode;
        }
    }

    /// <summary>
    /// Helper class,<br/>
    /// A List class wrapped with proper Equals and GetHashCode functionality.<br/>
    /// Can be safely cast into List.
    /// </summary>
    /// <typeparam name="Value">Value type</typeparam>
    public class NodeList<Value> : List<Value>
    {
        public NodeList()
        {
        }

        public NodeList(IEnumerable<Value> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string return_string = "";
            return_string += "[";
            bool first_element = true;
            foreach (var item in this)
            {
                if (first_element)
                {
                    first_element = false;
                }
                else
                {
                    return_string += ", ";
                }

                return_string += $"{item}";
            }
            return_string += "]";
            return return_string;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is NodeList<Value> other &&
                this.SequenceEqual(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 825543184;

            foreach (var item in this)
            {
                hashCode = hashCode * -357544921 + item.GetHashCode();
            }

            return hashCode;
        }
    }
}
