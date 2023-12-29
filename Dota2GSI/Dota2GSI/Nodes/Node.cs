using Newtonsoft.Json.Linq;
using System;

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
        protected Newtonsoft.Json.Linq.JObject _ParsedData;

        internal Node() : this("{}")
        {
        }

        internal Node(string json_data)
        {
            if (json_data.Equals(""))
            {
                json_data = "{}";
            }

            _ParsedData = Newtonsoft.Json.Linq.JObject.Parse(json_data);
        }

        internal Newtonsoft.Json.Linq.JToken GetJToken(string name)
        {
            Newtonsoft.Json.Linq.JToken value;

            if (_ParsedData.TryGetValue(name, out value))
            {
                return value;
            }

            return null;
        }


        internal string GetString(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null)
            {
                return value.ToString();
            }

            return "";
        }

        internal int GetInt(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null)
            {
                return Convert.ToInt32(value.ToString());
            }

            return -1;
        }

        internal long GetLong(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null)
            {
                return Convert.ToInt64(value.ToString());
            }

            return -1;
        }

        internal T GetEnum<T>(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), value.ToString(), true);
                }
                catch
                {
                }
            }

            return (T)Enum.Parse(typeof(T), "Undefined", true);
        }

        internal bool GetBool(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null)
            {
                return value.ToObject<bool>();
            }

            return false;
        }

        internal IJEnumerable<JToken> GetArray(string Name)
        {
            Newtonsoft.Json.Linq.JToken value = GetJToken(Name);

            if (value != null && value.HasValues)
            {
                return value.Values();
            }

            return new JEnumerable<JToken>();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _ParsedData.ToString();
        }
    }
}
