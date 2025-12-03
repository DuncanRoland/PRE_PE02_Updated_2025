namespace Pre.PEJobApplicationSystem.Core;

public class Company
{
    public string Name { get; private set; }
    public string Industry { get; private set; }
    public List<Job> Jobs { get; }

    public Company(string name, string industry)
    {
        Name = name;
        Industry = industry;
    }

    protected void AddJob(Job job)
    {
        
    }
}