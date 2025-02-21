using System.Text.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return new List<Character>();
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(characters, options);
        File.WriteAllText(filePath, json);
    }
}
