using Pre.PEJobApplicationSystem.Core.Interfaces;

namespace Pre.PEJobApplicationSystem.Core;

public class ApplicationManager : IApplicationManager
{
    private readonly List<Candidate> _candidates;
    public  List<Recruiter> Recruiters { get;}
    public readonly List<Company> _companies;
    protected List<JobApplication> _jobApplications;
    
    public ApplicationManager()
    {
        _candidates = [];
        Recruiters = [];
        _companies = [];
        _jobApplications = [];
    }

    public void AddCandidate(Candidate candidate)
    {
        _candidates.Add(candidate);
    }

    public void AddRecruiter(Recruiter recruiter)
    {
        Recruiters.Add(recruiter);
    }

    public void AddJobApplication(JobApplication jobApplication)
    {
        _jobApplications.Add(jobApplication);
    }

    public List<JobApplication> GetJobApplicationsForJob(Job job)
    {
        var result = new List<JobApplication>();
        // simple loop, no LINQ
        foreach (var ja in _jobApplications)
        {
            if (ja.Job == job)
            {
                result.Add(ja);
            }
        }
        return result;
    }

    //interface methods
    public void RegisterCandidate(Candidate candidate)
    {
        _candidates.Add(candidate);
    }

    public void RegisterCompany(Company company)
    {
        _companies.Add(company);
    }

    public void PostJob(Company company, string title, string description, decimal minSalary, decimal maxSalary,
        List<Skill> requiredSkills)
    {
        // Create a new Job and add it to the given company.
        // Assumes a suitable Job constructor or public setters exist.
        var job = new Job(title, description, minSalary, maxSalary, requiredSkills);
        company.AddJob(job);
    }

    public void Apply(JobApplication jobApplication)
    {
        _jobApplications.Add(jobApplication);
    }

    public List<Job> MatchCandidateToJobs(Candidate candidate)
    {
        var jobs = new List<Job>();
        if (_companies.Count == 0) return jobs;

        // Use jobs from the first company to populate the list (no LINQ)
        //jobs.AddRange(_companies[0].Jobs);
        
        //geen linq
        foreach (var job in _companies[0].Jobs)
        {
            jobs.Add(job);
        }

        return jobs;
    }
}