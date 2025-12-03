namespace Pre.PEJobApplicationSystem.Core;

public class JobApplication
{
    public Candidate Candidate { get; }
    public Job Job { get; }
    public DateTime ApplicationDate { get; private set; }
    public string Status { get; }
    public Interview Interview { get;}

    public JobApplication(Candidate candidate, Job job)
    {
        Candidate = candidate;
        Job = job;
    }

    public void UpdateStatus(string status)
    {
        
    }

    public void AddInterview(Interview interview)
    {
        
    }
    
    
}