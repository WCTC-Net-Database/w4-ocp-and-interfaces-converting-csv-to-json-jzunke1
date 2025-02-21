namespace W4_assignment_template.Models;

public class Character
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<string> Equipment { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Class: {Class}, Level: {Level}, HP: {HP}, Equipment: {string.Join(", ", Equipment)}";
    }
}
