using System;

namespace TextAdventureGame
{
    /// Entry point for the text adventure game application.
    class Program
    {
        /// Main method where the program starts execution.
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your player name:");
            string playerName = Console.ReadLine();

            // Initialize and start the text adventure game
            TextAdventureGame game = new TextAdventureGame(playerName);
            game.Play();
        }
    }
}