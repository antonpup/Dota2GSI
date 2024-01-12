using System.Collections.Generic;

namespace Dota2GSI.Nodes
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
        public readonly Dictionary<int, FullPlayerDetails> Players = new Dictionary<int, FullPlayerDetails>();

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
        public readonly Dictionary<int, MinimapElement> MinimapElements = new Dictionary<int, MinimapElement>();

        /// <summary>
        /// This team's recent events.
        /// </summary>
        public readonly List<Event> Events = new List<Event>();

        /// <summary>
        /// Is this team a winner?
        /// </summary>
        public bool IsWinner = false;

        internal FullTeamDetails(PlayerTeam team, GameState game_state)
        {
            Team = team;

            foreach(var team_player_kvp in game_state.Player.GetForTeam(team))
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
    }
}
