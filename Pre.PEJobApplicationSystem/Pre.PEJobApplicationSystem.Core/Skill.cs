namespace Pre.PEJobApplicationSystem.Core;

public class Skill
{
    public string Name { get; }
    public int Level { get; private set; }

    public Skill(string name, int level)
    {
        Name = name;
        Level = level;
    }
}