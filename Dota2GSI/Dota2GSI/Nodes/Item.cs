namespace Dota2GSI.Nodes
{
    public class Item : Node
    {
        public readonly string Name;
        public readonly string ContainsRune;
        public readonly bool CanCast;
        public readonly int Cooldown;
        public readonly bool IsPassive;
        public readonly int Charges;


        internal Item(string json_data) : base(json_data)
        {
            Name = GetString("name");
            ContainsRune = GetString("contains_rune");
            CanCast = GetBool("can_cast");
            Cooldown = GetInt("cooldown");
            IsPassive = GetBool("passive");
            Charges = GetInt("charges");
        }
    }
}
