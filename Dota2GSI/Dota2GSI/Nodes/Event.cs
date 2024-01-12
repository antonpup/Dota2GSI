using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Enum for types of events.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Courier was killed.
        /// </summary>
        Courier_killed,

        /// <summary>
        /// Roshan was killed.
        /// </summary>
        Roshan_killed,

        /// <summary>
        /// Aegis was picked up.
        /// </summary>
        Aegis_picked_up,

        /// <summary>
        /// Aegis was denied.
        /// </summary>
        Aegis_denied,

        /// <summary>
        /// Player was tipped.
        /// </summary>
        Tip,

        /// <summary>
        /// Bounty rune was picked up.
        /// </summary>
        Bounty_rune_pickup
    }

    /// <summary>
    /// Class representing an event.
    /// </summary>
    public class Event : Node
    {
        /// <summary>
        /// The game time when this event took place.
        /// </summary>
        public readonly int GameTime;

        /// <summary>
        /// The type of event.
        /// </summary>
        public readonly EventType EventType;

        /// <summary>
        /// The team invovled in the event.
        /// </summary>
        public readonly PlayerTeam Team;

        /// <summary>
        /// The ID of the killer player invovled in the event.
        /// </summary>
        public readonly int KillerPlayerID;

        /// <summary>
        /// The ID of the player invovled in the event.
        /// </summary>
        public readonly int PlayerID;

        /// <summary>
        /// Was the aegis snatched from the other team in the event.
        /// </summary>
        public readonly bool WasSnatched;

        /// <summary>
        /// The ID of the player that received a tip in the event.
        /// </summary>
        public readonly int TipReceiverPlayerID;

        /// <summary>
        /// The amount that was tipped in the event.
        /// </summary>
        public readonly int TipAmount;

        /// <summary>
        /// The amount that was picked up from a bounty rune in the event.
        /// </summary>
        public readonly int BountyValue;

        /// <summary>
        /// The amount of team gold after the event.
        /// </summary>
        public readonly int TeamGold;

        internal Event(JObject parsed_data = null) : base(parsed_data)
        {
            GameTime = GetInt("game_time");
            EventType = GetEnum<EventType>("event_type");

            switch (EventType)
            {
                case EventType.Courier_killed:
                    Team = GetEnum<PlayerTeam>("courier_team");
                    KillerPlayerID = GetInt("killer_player_id");
                    break;
                case EventType.Roshan_killed:
                    Team = GetEnum<PlayerTeam>("killed_by_team");
                    KillerPlayerID = GetInt("killer_player_id");
                    break;
                case EventType.Aegis_picked_up:
                    PlayerID = GetInt("player_id");
                    WasSnatched = GetBool("snatched");
                    break;
                case EventType.Aegis_denied:
                    PlayerID = GetInt("player_id");
                    break;
                case EventType.Tip:
                    PlayerID = GetInt("sender_player_id");
                    TipReceiverPlayerID = GetInt("receiver_player_id");
                    TipAmount = GetInt("tip_amount");
                    break;
                case EventType.Bounty_rune_pickup:
                    PlayerID = GetInt("player_id");
                    Team = GetEnum<PlayerTeam>("team");
                    BountyValue = GetInt("bounty_value");
                    TeamGold = GetInt("team_gold");
                    break;
                default:
                    break;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"GameTime: {GameTime}, " +
                $"EventType: {EventType}, " +
                $"Team: {Team}, " +
                $"KillerPlayerID: {KillerPlayerID}, " +
                $"PlayerID: {PlayerID}, " +
                $"WasSnatched: {WasSnatched}, " +
                $"TipReceiverPlayerID: {TipReceiverPlayerID}, " +
                $"TipAmount: {TipAmount}, " +
                $"BountyValue: {BountyValue}, " +
                $"TeamGold: {TeamGold}" +
                $"]";
        }
    }
}
