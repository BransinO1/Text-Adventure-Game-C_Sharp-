namespace TextAdventureGame
{
    /// Represents a base class for game objects in a text adventure game.
    public abstract class GameObject
    {
        /// Gets or sets the name of the game object.
        public string Name { get; set; }

        /// Gets or sets the description of the game object.
        public string Description { get; set; }
    }
}