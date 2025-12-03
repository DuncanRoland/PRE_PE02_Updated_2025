namespace Pre.PEJobApplicationSystem.Core.Interfaces;

public interface IApplicationManager
{
    public void RegisterCandidate(Candidate candidate);
    public void RegisterCompany(Company company);

    public void PostJob(Company company, string title, string description, decimal minSalary, decimal maxSalary,
        List<Skill> requiredSkills);

    public void Apply(JobApplication jobApplication);

    public List<Job> MatchCandidateToJobs(Candidate candidate);
}