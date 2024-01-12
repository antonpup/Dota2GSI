using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing league team information.
    /// </summary>
    public class LeagueTeam : Node
    {
        /// <summary>
        /// The team ID.
        /// </summary>
        public readonly int TeamID;

        /// <summary>
        /// The team tag.
        /// </summary>
        public readonly string TeamTag;

        /// <summary>
        /// The team name.
        /// </summary>
        public readonly string TeamName;

        /// <summary>
        /// The series wins.
        /// </summary>
        public readonly int SeriesWins;

        internal LeagueTeam(JObject parsed_data = null) : base(parsed_data)
        {
            TeamID = GetInt("team_id");
            TeamTag = GetString("team_tag");
            TeamName = GetString("name");
            SeriesWins = GetInt("series_wins");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"TeamID: {TeamID}, " +
                $"TeamTag: {TeamTag}, " +
                $"TeamName: {TeamName}, " +
                $"SeriesWins: {SeriesWins}" +
                $"]";
        }
    }
}
