namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing ability attributes
    /// </summary>
    public class Attributes : Node
    {
        /// <summary>
        /// Amount of levels to spend
        /// </summary>
        public readonly int Level;

        internal Attributes(string json_data) : base(json_data)
        {
            Level = GetInt("level");
        }
    }
}
