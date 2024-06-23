using System.Collections.Generic;

namespace TextAdventureGame
{
    /// Represents a player in a text adventure game.
    public class Player
    {
        /// Gets or sets the Name of the player.
        public string Name { get; set; }
        /// Gets or sets the Health of the player.
        public int Health { get; set; }
        /// Gets or sets the Current Room of the player.
        public Room CurrentRoom { get; set; }
        /// Gets or sets the Inventory of the player.
        public List<Item> Inventory { get; set; } = new List<Item>();
    }
}
