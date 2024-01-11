using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing events information.
    /// </summary>
    public class Events : List<Event>
    {
        internal Events(JObject parsed_data = null) : base()
        {
            if (parsed_data != null)
            {
                if (parsed_data.Type == JTokenType.Array)
                {
                    foreach(JToken element in parsed_data.Children())
                    {
                        if (element.Type == JTokenType.Object)
                        {
                            Add(new Event(element as JObject));
                        }
                    }
                }
            }
        }
    }
}
