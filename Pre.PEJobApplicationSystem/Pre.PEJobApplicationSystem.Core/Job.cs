namespace Pre.PEJobApplicationSystem.Core;

public class Job
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal MinSalary { get; private set; }
    public decimal MaxSalary { get; private set; }
    public List<Skill> RequiredSkills { get; private set; }

    public Job(string title, string description, decimal minSalary, decimal maxSalary, List<Skill> requiredSkills)
    {
        Title = title;
        Description = description;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        RequiredSkills = requiredSkills;
    }

    public void AddRequiredSkill(Skill skill)
    {
        
    }

    public bool IsCandidateEligible(Candidate candidate)
    {
        return false;
    }
}