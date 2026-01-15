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

    public void SetLevel(int level)
    {
        if (level < 1 || level > 5)
            throw new ArgumentOutOfRangeException("The number must be between 1 and 5.");
        Level = level;
    }
}