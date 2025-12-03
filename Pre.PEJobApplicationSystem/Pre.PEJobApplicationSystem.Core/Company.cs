namespace Pre.PEJobApplicationSystem.Core;

public class Company
{
    public string Name { get; private set; }
    public string Industry { get; private set; }
    // added setter because I otherwise don't see how to add jobs to the list
    public List<Job> Jobs { get; set; }

    public Company(string name, string industry)
    {
        Name = name;
        Industry = industry;
    }

    // made it public since i'm not sure how to test otherwise
    public void AddJob(Job job)
    {
        Jobs.Add(job);
    }
}