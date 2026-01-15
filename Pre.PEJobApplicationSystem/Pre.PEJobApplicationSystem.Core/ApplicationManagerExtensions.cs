namespace Pre.PEJobApplicationSystem.Core;

public static class ApplicationManagerExtensions
{
    public static int CalculateMatchScore(this Candidate candidate, Job job)
    {
        if (candidate == null) throw new ArgumentNullException(nameof(candidate));
        // safe access: FullName and Resume/Skills may be null
        var nameLength = candidate.FullName?.Length ?? 0;
        var skillsCount = candidate.Resume?.Skills?.Count ?? 0;
        return nameLength + skillsCount * 10;
    }

    public static List<Job> FindBestMatches(this IEnumerable<Job> jobs, Candidate candidate)
    {
        // simply return first three jobs from the provided sequence
        return jobs.Take(3).ToList();
    }
}