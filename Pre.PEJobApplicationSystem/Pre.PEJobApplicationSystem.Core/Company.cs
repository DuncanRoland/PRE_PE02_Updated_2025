namespace Pre.PEJobApplicationSystem.Core;

public class Company
{
    public string Name { get; private set; }

    public string Industry { get; private set; }
    
    public List<Job> Jobs { get; }

    public Company(string name, string industry)
    {
        Name = name;
        SetIndustry(industry);
        Jobs = new List<Job>();
    }

    public void SetIndustry(string industry)
    {
        if (industry != "IT" && industry != "Other")
            throw new ArgumentException("Industry must be 'IT' or 'Other'.");
        Industry = industry;
    }

    // made it public since i'm not sure how to test otherwise
    public void AddJob(Job job)
    {
        if (job == null) throw new ArgumentNullException(nameof(job));
        Jobs.Add(job);
    }
}