namespace Pre.PEJobApplicationSystem.Core;

public class Resume
{
    public List<WorkExperience> Experiences { get; }
    public List<Skill> Skills { get; private set; }

    public Resume(List<Skill> skills)
    {
        Skills = skills;
    }

    public void AddExperience(WorkExperience experience)
    {
    }

    public void AddSkill(Skill skill)
    {
    }
}