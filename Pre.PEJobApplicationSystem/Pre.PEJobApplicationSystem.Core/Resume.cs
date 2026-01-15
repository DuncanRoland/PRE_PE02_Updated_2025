namespace Pre.PEJobApplicationSystem.Core;

public class Resume
{
    public List<WorkExperience> Experiences { get; }
    public List<Skill> Skills { get; private set; }

    public Resume(List<Skill>? skills)
    {
        Skills = skills ?? new List<Skill>();
        Experiences = new List<WorkExperience>();
    }

    public void AddExperience(WorkExperience experience)
    {
        if (experience == null) throw new ArgumentNullException(nameof(experience));
        if (Experiences.Contains(experience)) throw new Exception("Experience already exists");
        Experiences.Add(experience);
    }

    public void AddSkill(Skill skill)
    {
        if (skill == null) throw new ArgumentNullException(nameof(skill));
        Skills.Add(skill);
    }
}