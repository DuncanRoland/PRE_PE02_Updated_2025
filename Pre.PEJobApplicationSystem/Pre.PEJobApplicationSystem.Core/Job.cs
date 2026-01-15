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
       
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        RequiredSkills = requiredSkills;

        SetMinSalary(minSalary);
        SetMaxSalary(maxSalary);
    }

    public void SetMinSalary(decimal minSalary)
    {
        if (minSalary <= 100m || minSalary >= 1_000_000m)
            throw new ArgumentOutOfRangeException(nameof(minSalary), "Salary must be greater than 100 and less than 1,000,000.");
        MinSalary = minSalary;
    }

    public void SetMaxSalary(decimal maxSalary)
    {
        if (maxSalary <= 100m || maxSalary >= 1_000_000m)
            throw new ArgumentOutOfRangeException(nameof(maxSalary), "Salary must be greater than 100 and less than 1,000,000.");
        MaxSalary = maxSalary;
    }
    
    public void AddRequiredSkill(Skill skill)
    {
        if (skill == null) throw new ArgumentNullException(nameof(skill));
        RequiredSkills.Add(skill);
    }

    public bool IsCandidateEligible(Candidate candidate)
    {
        var apps = candidate.AppliedJobs;
        return apps.Count >= 10;
    }
}