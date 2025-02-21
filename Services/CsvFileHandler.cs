using System.Globalization;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        var characters = new List<Character>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return characters;
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var values = line.Split(',');

            if (values.Length < 5)
            {
                Console.WriteLine("Invalid data format.");
                continue;
            }

            var character = new Character
            {
                Name = values[0],
                Class = values[1],
                Level = int.Parse(values[2], CultureInfo.InvariantCulture),
                HP = int.Parse(values[3], CultureInfo.InvariantCulture),
                Equipment = values[4].Split(';').Select(e => e.Trim()).ToList()
            };

            characters.Add(character);
        }

        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        var lines = new List<string>();

        foreach (var character in characters)
        {
            var line = $"{character.Name},{character.Class},{character.Level},{character.HP},{string.Join(";", character.Equipment)}";
            lines.Add(line);
        }

        File.WriteAllLines(filePath, lines);
    }
}
