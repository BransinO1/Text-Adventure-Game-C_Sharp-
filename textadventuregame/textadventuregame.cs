// Text Adventure Game
// Creates a single player text adventure game where the user can pick up items and progress through rooms
// Allows the user to save the game or load the progress into a seperate file. 
using System;
using System.Collections.Generic;
using System.IO;

namespace TextAdventureGame
{
    public class TextAdventureGame
    {
        private Player player;
        private Room startingRoom = new Room();
        private Room secondRoom = new Room();
        private Room thirdRoom = new Room();

        // Set New Player Name and Health to 100
        public TextAdventureGame(string playerName)
        {
            player = new Player { Name = playerName, Health = 100 };
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Initialize rooms using class-level fields
            startingRoom.Name = "Starting Room";
            startingRoom.Description = "You find yourself in a dimly lit room.";

            secondRoom.Name = "Second Room";
            secondRoom.Description = "This room has a mysterious aura.";

            thirdRoom.Name = "Third Room";
            thirdRoom.Description = "This room has a musty smell.";

            // Add items to rooms
            Item key = new Item("Key", "A shiny golden key.", 0.1, 100, false, "Unlocks doors");
            startingRoom.Items.Add(key);

            Item apple = new Item("Apple", "A red juicy apple.", 0.2, 10, true, "Restores 5 health points");
            startingRoom.Items.Add(apple);

            Item lantern = new Item("Lantern", "An unlit lantern.", 1.0, 50, true, "Briefly lights the space around you");
            secondRoom.Items.Add(lantern);

            Item knife = new Item("Knife", "A sharp knife.", 0.5, 25, true, "Useful for cutting things");
            thirdRoom.Items.Add(knife);

            // Set starting room for the player
            player.CurrentRoom = startingRoom;
        }
        
        public void Play()
        {
            Console.WriteLine($"Welcome, {player.Name}, to the Text Adventure Game!");

            bool gameOver = false;

            while (!gameOver)
            {
                Console.WriteLine();
                Console.WriteLine(player.CurrentRoom.Description);

                // Display items in the room
                if (player.CurrentRoom.Items.Count > 0)
                {
                    Console.WriteLine("You see the following items:");
                    foreach (var item in player.CurrentRoom.Items)
                    {
                        Console.WriteLine($"- {item.Name}: {item.Description} (Weight: {item.Weight}, Value: {item.Value})");
                    }
                }

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Pick up an item");
                Console.WriteLine("2. Move to another room");
                Console.WriteLine("3. Save game");
                Console.WriteLine("4. Load game");
                Console.WriteLine("5. Display inventory");
                Console.WriteLine("6. Quit");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if (player.CurrentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Which item would you like to pick up?");
                            for (int i = 0; i < player.CurrentRoom.Items.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {player.CurrentRoom.Items[i].Name}");
                            }

                            int itemIndex;
                            if (int.TryParse(Console.ReadLine(), out itemIndex) && itemIndex > 0 && itemIndex <= player.CurrentRoom.Items.Count)
                            {
                                Item item = player.CurrentRoom.Items[itemIndex - 1];
                                player.Inventory.Add(item);
                                player.CurrentRoom.Items.Remove(item);
                                Console.WriteLine($"You picked up: {item.Name}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no items to pick up.");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Which room would you like to move to?");
                        Console.WriteLine("1. Starting Room");
                        Console.WriteLine("2. Second Room");
                        Console.WriteLine("3. Third Room");

                        string roomChoice = Console.ReadLine();
                        switch (roomChoice)
                        {
                            case "1":
                                player.CurrentRoom = startingRoom;
                                break;
                            case "2":
                                player.CurrentRoom = secondRoom;
                                break;
                            case "3":
                                player.CurrentRoom = thirdRoom;
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }

                        // Display the new room's description and items after moving
                        Console.WriteLine();
                        Console.WriteLine(player.CurrentRoom.Description);
                        if (player.CurrentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("You see the following items:");
                            foreach (var item in player.CurrentRoom.Items)
                            {
                                Console.WriteLine($"- {item.Name}: {item.Description} (Weight: {item.Weight}, Value: {item.Value})");
                            }
                        }
                        break;
                    case "3":
                        SaveGame();
                        break;
                    case "4":
                        Console.WriteLine("Enter the filename to load your game:");
                        string loadFileName = Console.ReadLine();
                        LoadGame(loadFileName);
                        break;
                    case "5":
                        DisplayInventory();
                        break;
                    case "6":
                        gameOver = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Thanks for playing!");
        }

        // Function to Diplay the Player's Current Inventory
        private void DisplayInventory()
        {
            Console.WriteLine($"Inventory of {player.Name}:");
            if (player.Inventory.Count > 0)
            {
                foreach (var item in player.Inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description} (Weight: {item.Weight}, Value: {item.Value})");
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty.");
            }
        }

        private int GetChoice(int min, int max)
        {
            int choice = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= min && choice <= max)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            return choice;
        }

        public void SaveGame()
        {
            Console.WriteLine("Enter the filename to save your game:");
            string fileName = Console.ReadLine();

            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    // Write player data
                    writer.WriteLine($"Player Name: {player.Name}");
                    writer.WriteLine($"Player Health: {player.Health}");
                    writer.WriteLine($"Current Room: {player.CurrentRoom.Name}");

                    // Write player inventory
                    writer.WriteLine($"Inventory Count: {player.Inventory.Count}");
                    foreach (var item in player.Inventory)
                    {
                        writer.WriteLine($"Item Name: {item.Name}");
                        writer.WriteLine($"Item Description: {item.Description}");
                        writer.WriteLine($"Item Weight: {item.Weight}");
                        writer.WriteLine($"Item Value: {item.Value}");
                        writer.WriteLine($"Is Consumable: {item.IsConsumable}");
                        writer.WriteLine($"Effect: {item.Effect}");
                    }
                }

                Console.WriteLine($"Game saved successfully to {fileName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public void LoadGame(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    // Read player name and health
                    string playerName = ReadNextLine(reader, "Player Name");
                    int playerHealth = int.Parse(ReadNextLine(reader, "Player Health"));

                    // Read current room
                    string currentRoomName = ReadNextLine(reader, "Current Room");
                    Room currentRoom = new Room
                    {
                        Name = currentRoomName,
                        Description = "Loaded room description"
                    };

                    // Read inventory count
                    int inventoryCount = int.Parse(ReadNextLine(reader, "Inventory Count"));

                    // Clear existing inventory
                    player.Inventory.Clear();

                    // Read each item in the inventory
                    for (int i = 0; i < inventoryCount; i++)
                    {
                        string itemName = ReadNextLine(reader, "Item Name");
                        string itemDescription = ReadNextLine(reader, "Item Description");
                        double itemWeight = double.Parse(ReadNextLine(reader, "Item Weight"));
                        int itemValue = int.Parse(ReadNextLine(reader, "Item Value"));
                        bool itemIsConsumable = bool.Parse(ReadNextLine(reader, "Is Consumable"));
                        string itemEffect = ReadNextLine(reader, "Effect");

                        // Create new Item object with all required parameters
                        Item newItem = new Item(itemName, itemDescription, itemWeight, itemValue, itemIsConsumable, itemEffect);
                        player.Inventory.Add(newItem);
                    }

                    // Update player object with loaded data
                    player.Name = playerName;
                    player.Health = playerHealth;
                    player.CurrentRoom = currentRoom;

                    Console.WriteLine($"Game loaded successfully from {fileName}.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the loading process
                Console.WriteLine($"Failed to load game: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        // Helper method to read the next line from the StreamReader and validate its format
        private string ReadNextLine(StreamReader reader, string expectedLabel)
        {
            string line = reader.ReadLine();
            if (line.StartsWith(expectedLabel + ": "))
            {
                // Extract the value part of the line (skipping the label and ": " characters)
                return line.Substring(expectedLabel.Length + 2); // +2 to skip ": "
            }
            else
            {
                // If the line does not match the expected format, throw a FormatException
                throw new FormatException($"Expected '{expectedLabel}' but got '{line}'");
            }
        }
    }
}