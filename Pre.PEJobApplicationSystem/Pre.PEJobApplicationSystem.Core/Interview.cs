namespace Pre.PEJobApplicationSystem.Core;

public class Interview
{
    public Recruiter Recruiter { get; }
    public string Feedback { get; set; }

    public Interview(Recruiter recruiter)
    {
        Recruiter = recruiter;
    }

    // setter zodat het ioverschreven kan worden
    public void AddFeedback(string feedback)
    {
        Feedback = feedback;
        
    }
}