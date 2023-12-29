using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing the authentication information for GSI
    /// </summary>
    public class TeamsGroup
    {
        /// <summary>
        /// Returns the Dictionary containing all the Teams
        /// </summary>
        public readonly Dictionary<int, Dictionary<int, PlayerDetails>> All = new Dictionary<int, Dictionary<int, PlayerDetails>>();

        /// <summary>
        /// Returns the group of only Radiant players
        /// </summary>
        public Dictionary<int, PlayerDetails> Radiant { get { return All.ContainsKey((int)PlayerTeam.Radiant) ? All[(int)PlayerTeam.Radiant] : null; } }

        /// <summary>
        /// Returns the group of only Dire players
        /// </summary>
        public Dictionary<int, PlayerDetails> Dire { get { return All.ContainsKey((int)PlayerTeam.Dire) ? All[(int)PlayerTeam.Dire] : null; } }

        private Dictionary<int, PlayerDetails> allPlayers = null;

        /// <summary>
        /// Returns the Dictionary with all the players in the game
        /// </summary>
        public Dictionary<int, PlayerDetails> AllPlayers {
            get
            {
                if (allPlayers == null)
                {
                    allPlayers = new Dictionary<int, PlayerDetails>();
                    foreach(var team in All)
                    {
                        foreach(var player in team.Value)
                        {
                            if (allPlayers.ContainsKey(player.Key))
                            {
                                Console.WriteLine("[DOTA2GSI] Warning, multiple players with the same ID! ID: " + player.Key);
                                continue;
                            }

                            allPlayers.Add(player.Key, player.Value);
                        }
                    }
                }

                return allPlayers;
            }
        }

        internal TeamsGroup(JObject json)
        {
            JToken player = json.SelectToken("player");
            
            //do try catch
            foreach (JToken arr in player)
            {
                int team;
                if (int.TryParse(((JProperty)arr).Name.Replace("team", ""), out team))
                {
                    All.Add(team, new Dictionary<int, PlayerDetails>());
                    foreach (JToken token in arr.Values())
                    {
                        int index;
                        if (int.TryParse(((JProperty)token).Name.Replace("player", ""), out index))
                        {
                            All[team].Add(index, new PlayerDetails(json, $"team{team}.player{index}"));
                        }
                        else
                        {
                            Console.WriteLine("[DOTA2GSI] Warning, could not get team ID! ID: " + ((JProperty)token).Name);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("[DOTA2GSI] Warning, could not get team ID! ID: " + ((JProperty)arr).Name);
                }
            }
        }
    }
}
