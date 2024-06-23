namespace TextAdventureGame
{
    public class Item : GameObject
    {
        public double Weight { get; set; } // Weight of the item
        public int Value { get; set; } // Value of the item
        public bool IsConsumable { get; set; } // Indicates if the item can be consumed
        public string Effect { get; set; } // Effect of the item if used or consumed

        // Constructor to easily create items
        public Item(string name, string description, double weight, int value, bool isConsumable, string effect)
        {
            Name = name;
            Description = description;
            Weight = weight;
            Value = value;
            IsConsumable = isConsumable;
            Effect = effect;
        }
    }
}