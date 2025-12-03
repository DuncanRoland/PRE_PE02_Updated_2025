namespace Pre.PEJobApplicationSystem.Core;

public class Recruiter : Person
{
    public Recruiter(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
    }

    public void PostJob(Company company, Job job)
    {
        company.Jobs =
        [
            job
        ];
    }

    public static void ReviewApplication(JobApplication application)
    {
        if (application.Status == "Pending")
        {
            application.UpdateStatus("Top!");
        }
        
    }
}