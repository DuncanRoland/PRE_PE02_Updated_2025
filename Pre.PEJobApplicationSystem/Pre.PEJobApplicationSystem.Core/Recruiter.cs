namespace Pre.PEJobApplicationSystem.Core;

public class Recruiter : Person
{
    public Recruiter(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
    }

    public void PostJob(Company company, Job job)
    {
        if (company == null) throw new InvalidOperationException("Recruiter has no company set.");
        company.AddJob(job);
    }

    public void ReviewApplication(JobApplication application)
    {
        if (application == null) throw new ArgumentNullException(nameof(application));

        var interview = new Interview(this);
        interview.AddFeedback("Top!");
        application.AddInterview(interview);

    }

    public override string GetInfo()
    {
        return $"{GetType().Name} - Name: {FullName}, Email: {Email}";
    }
}