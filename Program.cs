using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace W4_assignment_template
{
    class Program
    {
        static IFileHandler fileHandler;
        static List<Character> characters;
        static string filePath;

        static void Main()
        {
            ChooseFileFormat();

            try
            {
                characters = fileHandler.ReadCharacters(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                characters = new List<Character>();
            }

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Display Characters");
                Console.WriteLine("2. Find Character");
                Console.WriteLine("3. Add Character");
                Console.WriteLine("4. Level Up Character");
                Console.WriteLine("5. Switch File Format (CSV/JSON)");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllCharacters();
                        break;
                    case "2":
                        FindCharacter();
                        break;
                    case "3":
                        AddCharacter();
                        break;
                    case "4":
                        LevelUpCharacter();
                        break;
                    case "5":
                        SwitchFileFormat();
                        break;
                    case "6":
                        try
                        {
                            fileHandler.WriteCharacters(filePath, characters);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error writing file: {ex.Message}");
                        }
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ChooseFileFormat()
        {
            Console.WriteLine("Choose file format (1 for CSV, 2 for JSON): ");
            string formatChoice = Console.ReadLine();

            if (formatChoice == "1")
            {
                filePath = "input.csv";
                fileHandler = new CsvFileHandler();
            }
            else if (formatChoice == "2")
            {
                filePath = "input.json";
                fileHandler = new JsonFileHandler();
            }
            else
            {
                Console.WriteLine("Invalid choice. Defaulting to CSV.");
                filePath = "input.csv";
                fileHandler = new CsvFileHandler();
            }
        }

        static void SwitchFileFormat()
        {
            try
            {
                fileHandler.WriteCharacters(filePath, characters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }

            ChooseFileFormat();

            try
            {
                characters = fileHandler.ReadCharacters(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                characters = new List<Character>();
            }
        }

        static void DisplayAllCharacters()
        {
            foreach (var character in characters)
            {
                Console.WriteLine($"Name: {character.Name}, Class: {character.Class}, Level: {character.Level}, HP: {character.HP}, Equipment: {string.Join(", ", character.Equipment)}");
            }
        }

        static void FindCharacter()
        {
            Console.Write("Enter the name of the character to find: ");
            string nameToFind = Console.ReadLine();
            var character = characters.Find(c => c.Name.Equals(nameToFind, StringComparison.OrdinalIgnoreCase));
            if (character != null)
            {
                Console.WriteLine($"Name: {character.Name}, Class: {character.Class}, Level: {character.Level}, HP: {character.HP}, Equipment: {string.Join(", ", character.Equipment)}");
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }

        static void AddCharacter()
        {
            Console.Write("Enter character name: ");
            string name = Console.ReadLine();

            Console.Write("Enter character class: ");
            string characterClass = Console.ReadLine();

            Console.Write("Enter character level: ");
            int level;
            while (!int.TryParse(Console.ReadLine(), out level) || level < 1)
            {
                Console.Write("Invalid input. Enter a valid character level: ");
            }

            Console.Write("Enter character HP: ");
            int hp;
            while (!int.TryParse(Console.ReadLine(), out hp) || hp < 1)
            {
                Console.Write("Invalid input. Enter a valid character HP: ");
            }

            Console.Write("Enter character equipment (separated by '|'): ");
            List<string> equipment = Console.ReadLine().Split('|').Select(e => e.Trim()).ToList();

            Character newCharacter = new Character
            {
                Name = name,
                Class = characterClass,
                Level = level,
                HP = hp,
                Equipment = equipment
            };

            characters.Add(newCharacter);
            Console.WriteLine("Character added successfully.");
        }

        static void LevelUpCharacter()
        {
            Console.Write("Enter the name of the character to level up: ");
            string nameToLevelUp = Console.ReadLine();

            var character = characters.Find(c => c.Name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase));
            if (character != null)
            {
                character.Level++;
                Console.WriteLine($"Character {character.Name} leveled up to level {character.Level}!");
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }
    }
}
