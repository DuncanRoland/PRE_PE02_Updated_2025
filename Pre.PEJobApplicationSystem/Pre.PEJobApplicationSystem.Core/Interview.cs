namespace Pre.PEJobApplicationSystem.Core;

public class Interview
{
    public Recruiter Recruiter { get; }
    public string Feedback { get; }

    public Interview(Recruiter recruiter)
    {
        Recruiter = recruiter;
    }

    public void AddFeedback(string feedback)
    {
        
    }
}