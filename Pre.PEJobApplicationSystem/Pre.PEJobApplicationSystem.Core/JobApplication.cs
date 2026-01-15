namespace Pre.PEJobApplicationSystem.Core;

public class JobApplication
{
    public Candidate Candidate { get; }
    public Job Job { get; }
    public DateTime ApplicationDate { get; private set; }

    private string _status;
    public string Status => _status;

    private Interview _interview;
    public Interview Interview => _interview;

    public JobApplication(Candidate candidate, Job job)
    {
        Candidate = candidate ?? throw new ArgumentNullException(nameof(candidate));
        Job = job ?? throw new ArgumentNullException(nameof(job));
        ApplicationDate = DateTime.Now;
    }

    public void UpdateStatus(string status)
    {
        _status = status;
    }

    public void AddInterview(Interview interview)
    {
        _interview = interview;
    }
}