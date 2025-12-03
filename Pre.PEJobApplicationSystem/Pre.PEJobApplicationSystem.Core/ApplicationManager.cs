using Pre.PEJobApplicationSystem.Core.Interfaces;

namespace Pre.PEJobApplicationSystem.Core;

public class ApplicationManager : IApplicationManager
{
    private List<Candidate> _candidates;
    public List<Recruiter> Recruiters { get;}
    public List<Company> _companies;

    protected List<JobApplication> _jobApplications;

    public ApplicationManager()
    {
    }

    public void AddCandidate(Candidate candidate)
    {
        
    }

    public void AddRecruiter(Recruiter recruiter)
    {
        
    }

    public void AddJobApplication(JobApplication jobApplication)
    {
        
    }

    public List<JobApplication> GetJobApplicationsForJob(Job job)
    {
        return null;
    }
    
    

    //interface methods
    public void RegisterCandidate(Candidate candidate)
    {
        throw new NotImplementedException();
    }

    public void RegisterCompany(Company company)
    {
        throw new NotImplementedException();
    }

    public void PostJob(Company company, string title, string description, decimal minSalary, decimal maxSalary,
        List<Skill> requiredSkills)
    {
        throw new NotImplementedException();
    }

    public void Apply(JobApplication jobApplication)
    {
        throw new NotImplementedException();
    }

    public List<Job> MatchCandidateToJobs(Candidate candidate)
    {
        throw new NotImplementedException();
    }
}