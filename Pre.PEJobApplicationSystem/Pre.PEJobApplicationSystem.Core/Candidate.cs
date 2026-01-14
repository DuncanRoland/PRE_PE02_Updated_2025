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
        var application = new JobApplication(this, job);
        AppliedJobs.Add(application);
        return application;
 
    }

    public override string GetInfo()
    {
        return $"{GetType().Name} - Name: {FullName}, Email: {Email}";
    }
}