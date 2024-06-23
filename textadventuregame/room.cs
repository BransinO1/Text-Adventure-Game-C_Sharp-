using System.Collections.Generic;

namespace TextAdventureGame
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
