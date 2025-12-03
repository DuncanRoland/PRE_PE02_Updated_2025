namespace Pre.PEJobApplicationSystem.Core;

public class Candidate : Person
{
    public Resume Resume { get; set; }
    public List<JobApplication> AppliedJobs { get; }

    public Candidate(string firstName, string lastName, string email, Resume resume) :
        base(firstName, lastName, email)
    {
        Resume = resume;
    }

    public JobApplication ApplyForJob(Job job)
    {
        return null;
    }
}