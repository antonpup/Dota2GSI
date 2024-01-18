using Dota2GSI.Nodes.BuildingsProvider;
using Dota2GSI.Nodes.DraftProvider;
using Dota2GSI.Nodes.EventsProvider;
using Dota2GSI.Nodes.MinimapProvider;
using Dota2GSI.Nodes.NeutralItemsProvider;

namespace Dota2GSI.Nodes.Helpers
{
    /// <summary>
    /// Class representing full team details composed from GameState data.
    /// </summary>
    public class FullTeamDetails
    {
        /// <summary>
        /// This team's team.
        /// </summary>
        public readonly PlayerTeam Team = PlayerTeam.Undefined;

        /// <summary>
        /// This team's players.
        /// </summary>
        public readonly NodeMap<int, FullPlayerDetails> Players = new NodeMap<int, FullPlayerDetails>();

        /// <summary>
        /// This team's draft.
        /// </summary>
        public readonly DraftDetails Draft = new DraftDetails();

        /// <summary>
        /// This team's neutral items.
        /// </summary>
        public readonly TeamNeutralItems NeutralItems = new TeamNeutralItems();

        /// <summary>
        /// This team's buildings.
        /// </summary>
        public readonly BuildingLayout Buildings = new BuildingLayout();

        /// <summary>
        /// This team's minimap elements.<br/>
        /// Key is element ID.<br/>
        /// Value is minimap element.
        /// </summary>
        public readonly NodeMap<int, MinimapElement> MinimapElements = new NodeMap<int, MinimapElement>();

        /// <summary>
        /// This team's recent events.
        /// </summary>
        public readonly NodeList<Event> Events = new NodeList<Event>();

        /// <summary>
        /// Is this team a winner?
        /// </summary>
        public bool IsWinner = false;

        internal FullTeamDetails(PlayerTeam team, GameState game_state)
        {
            Team = team;

            foreach (var team_player_kvp in game_state.Player.GetForTeam(team))
            {
                Players.Add(team_player_kvp.Key, new FullPlayerDetails(team_player_kvp.Key, game_state));
            }

            Draft = game_state.Draft.GetForTeam(team);
            NeutralItems = game_state.NeutralItems.GetForTeam(team);
            Buildings = game_state.Buildings.GetForTeam(team);
            MinimapElements = game_state.Minimap.GetForTeam(team);
            Events = game_state.Events.GetForTeam(team);
            IsWinner = (game_state.Map.WinningTeam == team);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Team: {Team}, " +
                $"Players: {Players}, " +
                $"Draft: {Draft}, " +
                $"NeutralItems: {NeutralItems}, " +
                $"Buildings: {Buildings}, " +
                $"MinimapElements: {MinimapElements}, " +
                $"Events: {Events}, " +
                $"IsWinner: {IsWinner}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is FullTeamDetails other &&
                Team.Equals(other.Team) &&
                Players.Equals(other.Players) &&
                Draft.Equals(other.Draft) &&
                NeutralItems.Equals(other.NeutralItems) &&
                Buildings.Equals(other.Buildings) &&
                MinimapElements.Equals(other.MinimapElements) &&
                Events.Equals(other.Events) &&
                IsWinner.Equals(other.IsWinner);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 327356736;
            hashCode = hashCode * -578827851 + Team.GetHashCode();
            hashCode = hashCode * -578827851 + Players.GetHashCode();
            hashCode = hashCode * -578827851 + Draft.GetHashCode();
            hashCode = hashCode * -578827851 + NeutralItems.GetHashCode();
            hashCode = hashCode * -578827851 + Buildings.GetHashCode();
            hashCode = hashCode * -578827851 + MinimapElements.GetHashCode();
            hashCode = hashCode * -578827851 + Events.GetHashCode();
            hashCode = hashCode * -578827851 + IsWinner.GetHashCode();
            return hashCode;
        }
    }
}
