using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.LeagueProvider
{
    /// <summary>
    /// Rules.
    /// </summary>
    public enum Rules
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Unknown rules.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Automatic rules.
        /// </summary>
        Automatic,

        /// <summary>
        /// Manual rules.
        /// </summary>
        Manual,
    }

    /// <summary>
    /// Method of choosing team.
    /// </summary>
    public enum TeamChoiceMethod
    {
        /// <summary>
        /// Invalid choice.
        /// </summary>
        Invalid = -2,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Unknown coice.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Dire choses.
        /// </summary>
        Dire,

        /// <summary>
        /// Radiant choses.
        /// </summary>
        Radiant,

        /// <summary>
        /// First pick.
        /// </summary>
        First_Pick,

        /// <summary>
        /// Second pick.
        /// </summary>
        Second_Pick
    }

    /// <summary>
    /// A class representing team selection priority.
    /// </summary>
    public class SelectionPriority : Node
    {
        /// <summary>
        /// Rules.
        /// </summary>
        public readonly Rules Rules;

        /// <summary>
        /// Previous priority team ID.
        /// </summary>
        public readonly int PreviousPriorityTeamID;

        /// <summary>
        /// Current priority team ID.
        /// </summary>
        public readonly int CurrentPriorityTeamID;

        /// <summary>
        /// Prioritized team choosing method.
        /// </summary>
        public readonly TeamChoiceMethod PriorityTeamChoice;

        /// <summary>
        /// Non-prioritized team choosing method.
        /// </summary>
        public readonly TeamChoiceMethod NonPriorityTeamChoice;

        /// <summary>
        /// Used coin toss to choose team.
        /// </summary>
        public readonly bool UsedCoinToss;

        internal SelectionPriority(JObject parsed_data = null) : base(parsed_data)
        {
            Rules = GetEnum<Rules>("rules");
            PreviousPriorityTeamID = GetInt("previous_priority_team_id");
            CurrentPriorityTeamID = GetInt("current_priority_team_id");
            PriorityTeamChoice = GetEnum<TeamChoiceMethod>("priority_team_choice");
            NonPriorityTeamChoice = GetEnum<TeamChoiceMethod>("non_priority_team_choice");
            UsedCoinToss = GetBool("used_coin_toss");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Rules: {Rules}, " +
                $"PreviousPriorityTeamID: {PreviousPriorityTeamID}, " +
                $"CurrentPriorityTeamID: {CurrentPriorityTeamID}, " +
                $"PriorityTeamChoice: {PriorityTeamChoice}, " +
                $"NonPriorityTeamChoice: {NonPriorityTeamChoice}, " +
                $"UsedCoinToss: {UsedCoinToss}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is SelectionPriority other &&
                Rules == other.Rules &&
                PreviousPriorityTeamID == other.PreviousPriorityTeamID &&
                CurrentPriorityTeamID == other.CurrentPriorityTeamID &&
                PriorityTeamChoice == other.PriorityTeamChoice &&
                NonPriorityTeamChoice == other.NonPriorityTeamChoice &&
                UsedCoinToss == other.UsedCoinToss;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 107532357;
            hashCode = hashCode * -96977732 + Rules.GetHashCode();
            hashCode = hashCode * -96977732 + PreviousPriorityTeamID.GetHashCode();
            hashCode = hashCode * -96977732 + CurrentPriorityTeamID.GetHashCode();
            hashCode = hashCode * -96977732 + PriorityTeamChoice.GetHashCode();
            hashCode = hashCode * -96977732 + NonPriorityTeamChoice.GetHashCode();
            hashCode = hashCode * -96977732 + UsedCoinToss.GetHashCode();
            return hashCode;
        }
    }
}
