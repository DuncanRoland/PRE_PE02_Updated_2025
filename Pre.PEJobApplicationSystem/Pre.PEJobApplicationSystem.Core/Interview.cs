namespace Pre.PEJobApplicationSystem.Core;

public class Interview
{
    public Recruiter Recruiter { get; }
    private string _feedback;
    public string Feedback { get; }

    public Interview(Recruiter recruiter)
    {
        Recruiter = recruiter ?? throw new ArgumentNullException(nameof(recruiter));
    }

    // setter zodat het ioverschreven kan worden
    public void AddFeedback(string feedback)
    {
        _feedback = feedback ?? throw new ArgumentNullException(nameof(feedback));
        
    }
}