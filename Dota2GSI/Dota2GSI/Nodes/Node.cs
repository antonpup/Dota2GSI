using System;

namespace Dota2GSI.Nodes
{
    public class Node
    {
        protected Newtonsoft.Json.Linq.JObject _ParsedData;

        internal Node(string json_data)
        {
            if (json_data.Equals(""))
            {
                json_data = "{}";
            }
            _ParsedData = Newtonsoft.Json.Linq.JObject.Parse(json_data);
        }

        internal string GetString(string Name)
        {
            Newtonsoft.Json.Linq.JToken value;
            
            if(_ParsedData.TryGetValue(Name, out value))
                return value.ToString();
            else
                return "";
        }

        internal int GetInt(string Name)
        {
            Newtonsoft.Json.Linq.JToken value;
            
            if(_ParsedData.TryGetValue(Name, out value))
                return Convert.ToInt32(value.ToString());
            else
                return -1;
        }

        internal long GetLong(string Name)
        {
            Newtonsoft.Json.Linq.JToken value;

            if (_ParsedData.TryGetValue(Name, out value))
                return Convert.ToInt64(value.ToString());
            else
                return -1;
        }

        internal T GetEnum<T>(string Name)
        {
            Newtonsoft.Json.Linq.JToken value;
            
            if(_ParsedData.TryGetValue(Name, out value) && !String.IsNullOrWhiteSpace(value.ToString()))
                return (T)Enum.Parse(typeof(T), value.ToString(), true);
            else
                return (T)Enum.Parse(typeof(T), "Undefined", true);
        }

        internal bool GetBool(string Name)
        {
            Newtonsoft.Json.Linq.JToken value;
            
            if(_ParsedData.TryGetValue(Name, out value) && value.ToObject<bool>())
                return value.ToObject<bool>();
            else
                return false;
        }
    }
}
