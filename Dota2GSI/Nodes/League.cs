using Dota2GSI.Nodes.LeagueProvider;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// The series type.
    /// </summary>
    public enum SeriesType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Best of 2.
        /// </summary>
        BO2,

        /// <summary>
        /// Best of 3.
        /// </summary>
        BO3,

        /// <summary>
        /// Best of 5.
        /// </summary>
        BO5
    }

    /// <summary>
    /// A class representing leagues information.
    /// </summary>
    public class League : Node
    {
        /// <summary>
        /// The league series type.
        /// </summary>
        public readonly SeriesType SeriesType;

        /// <summary>
        /// The league selection priority.
        /// </summary>
        public readonly SelectionPriority SelectionPriority;

        /// <summary>
        /// The league ID.
        /// </summary>
        public readonly int LeagueID;

        /// <summary>
        /// The league match ID.
        /// </summary>
        public readonly long MatchID;

        /// <summary>
        /// The league name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The league tier.
        /// </summary>
        public readonly int Tier;

        /// <summary>
        /// The league region.
        /// </summary>
        public readonly int Region;

        /// <summary>
        /// The league URL.
        /// </summary>
        public readonly string Url;

        /// <summary>
        /// The league description.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// The league notes.
        /// </summary>
        public readonly string Notes;

        /// <summary>
        /// The start timestamp of the league.
        /// </summary>
        public readonly int StartTimestamp;

        /// <summary>
        /// The end timestamp of the league.
        /// </summary>
        public readonly int EndTimestamp;

        /// <summary>
        /// The pro circuit points of the league.
        /// </summary>
        public readonly int ProCircuitPoints;

        /// <summary>
        /// The image bits of the league.
        /// </summary>
        public readonly int ImageBits;

        /// <summary>
        /// The status of the league.
        /// </summary>
        public readonly int Status;

        /// <summary>
        /// The most recent activity of the league.
        /// </summary>
        public readonly int MostRecentActivity;

        /// <summary>
        /// The registration period of the league.
        /// </summary>
        public readonly int RegistrationPeriod;

        /// <summary>
        /// The base prize pool of the league.
        /// </summary>
        public readonly int BasePrizePool;

        /// <summary>
        /// The total prize pool of the league.
        /// </summary>
        public readonly int TotalPrizePool;

        /// <summary>
        /// The note ID of the league.
        /// </summary>
        public readonly int LeagueNoteID;

        /// <summary>
        /// The Radiant team information of the league.
        /// </summary>
        public readonly LeagueTeam RadiantTeam;

        /// <summary>
        /// The Dire team information of the league.
        /// </summary>
        public readonly LeagueTeam DireTeam;

        /// <summary>
        /// The ID of the series.
        /// </summary>
        public readonly int SeriesID;

        /// <summary>
        /// The start time.
        /// </summary>
        public readonly int StartTime;

        /// <summary>
        /// The first team ID.
        /// </summary>
        public readonly int FirstTeamID;

        /// <summary>
        /// The second team ID.
        /// </summary>
        public readonly int SecondTeamID;

        /// <summary>
        /// The streams for the league.
        /// </summary>
        public readonly List<Stream> Streams = new List<Stream>();

        internal League(JObject parsed_data = null) : base(parsed_data)
        {
            SeriesType = GetEnum<SeriesType>("series_type");
            SelectionPriority = new SelectionPriority(GetJObject("selection_priority"));
            LeagueID = GetInt("league_id");
            MatchID = GetLong("match_id");
            Name = GetString("name");
            Tier = GetInt("tier");
            Region = GetInt("region");
            Url = GetString("url");
            Description = GetString("description");
            Notes = GetString("notes");
            StartTimestamp = GetInt("start_timestamp");
            EndTimestamp = GetInt("end_timestamp");
            ProCircuitPoints = GetInt("pro_circuit_points");
            ImageBits = GetInt("image_bits");
            Status = GetInt("status");
            MostRecentActivity = GetInt("most_recent_activity");
            RegistrationPeriod = GetInt("registration_period");
            BasePrizePool = GetInt("base_prize_pool");
            TotalPrizePool = GetInt("total_prize_pool");
            LeagueNoteID = GetInt("league_node_id");
            RadiantTeam = new LeagueTeam(GetJObject("radiant"));
            DireTeam = new LeagueTeam(GetJObject("dire"));
            SeriesID = GetInt("series_id");
            StartTime = GetInt("start_time");
            FirstTeamID = GetInt("team_id_1");
            SecondTeamID = GetInt("team_id_2");

            var streams = GetJObject("streams");
            if (streams != null)
            {
                foreach (var property in streams.Properties())
                {
                    if (property.Value.Type == JTokenType.Object)
                    {
                        Streams.Add(new Stream(property.Value as JObject));
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"SeriesType: {SeriesType}, " +
                $"SelectionPriority: {SelectionPriority}, " +
                $"LeagueID: {LeagueID}, " +
                $"MatchID: {MatchID}, " +
                $"Name: {Name}, " +
                $"Tier: {Tier}, " +
                $"Region: {Region}, " +
                $"Url: {Url}, " +
                $"Description: {Description}, " +
                $"Notes: {Notes}, " +
                $"StartTimestamp: {StartTimestamp}, " +
                $"EndTimestamp: {EndTimestamp}, " +
                $"ProCircuitPoints: {ProCircuitPoints}, " +
                $"ImageBits: {ImageBits}, " +
                $"Status: {Status}, " +
                $"MostRecentActivity: {MostRecentActivity}, " +
                $"RegistrationPeriod: {RegistrationPeriod}, " +
                $"BasePrizePool: {BasePrizePool}, " +
                $"TotalPrizePool: {TotalPrizePool}, " +
                $"LeagueNoteID: {LeagueNoteID}, " +
                $"RadiantTeam: {RadiantTeam}, " +
                $"DireTeam: {DireTeam}, " +
                $"SeriesID: {SeriesID}, " +
                $"StartTime: {StartTime}, " +
                $"FirstTeamID: {FirstTeamID}, " +
                $"SecondTeamID: {SecondTeamID}, " +
                $"Streams: {Streams}" +
                $"]";
        }
    }
}
