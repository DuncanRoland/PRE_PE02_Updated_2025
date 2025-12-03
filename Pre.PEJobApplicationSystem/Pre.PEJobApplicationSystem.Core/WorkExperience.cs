namespace Pre.PEJobApplicationSystem.Core;

public class WorkExperience
{
    public string CompanyName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    private string _description;

    public WorkExperience(string companyName, DateTime startDate, DateTime endDate, string description)
    {
        _description = description;
        CompanyName = companyName;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int GetExperienceInYears()
    {
        return 0;
    }
}