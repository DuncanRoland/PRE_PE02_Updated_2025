using Pre.PEJobApplicationSystem.Core;

namespace Pre.PEJobApplicationSystem.Cons
{
    internal class Program
    {
        private static void Main()
        {
            // Setup basic objects
            var skills = new List<Skill> { new Skill("C# Programming", 5), new Skill("JavaScript Programming", 5) };
            var resume = new Resume(skills);

            var candidate = new Candidate("Duncan", "Roland", "Duncan.roland@student.howest.be", resume);
            var recruiter = new Recruiter("Sofie", "De Smet", "Sofie@desmet.be");

            var manager = new ApplicationManager();
            manager.AddCandidate(candidate);
            manager.AddRecruiter(recruiter);

            Console.WriteLine("Candidate added: " + candidate.GetInfo());
            Console.WriteLine("Recruiter added: " + recruiter.GetInfo());

            // Create company and job, post it
            var job = new Job("Software Developer", "Develop apps", 40000, 70000, skills);
            var company = new Company("Howest", "IT");
            company.AddJob(job);
            recruiter.PostJob(company, job);
            Console.WriteLine($"Posted job '{job.Title}' at {company.Name}");

            // Candidate applies
            var application = candidate.ApplyForJob(job);
            manager.AddJobApplication(application);
            Console.WriteLine($"Candidate applied for: {application.Job.Title}");
            Console.WriteLine("Candidate AppliedJobs count: " + candidate.AppliedJobs.Count);

            // Recruiter reviews the application (should add an Interview with feedback "Top!")
            recruiter.ReviewApplication(application);
            Console.WriteLine("Recruiter reviewed application.");

            // Simple attempt to read any interview feedback (if Interview collection/property is public)
            if (application is not null)
            {
                var interviewsProp = application.GetType().GetProperty("Interviews");
                if (interviewsProp?.GetValue(application) is IEnumerable<Interview> interviews)
                {
                    foreach (var it in interviews)
                        Console.WriteLine("Interview feedback: " +
                                          (it.GetType().GetProperty("Feedback")?.GetValue(it) ??
                                           "(no feedback property)"));
                }
            }

            // Resume tests: AddSkill and AddExperience (duplicate check)
            Console.WriteLine("Resume skills before: " + resume.Skills.Count);
            resume.AddSkill(new Skill("Go Programming", 3));
            Console.WriteLine("Resume skills after: " + resume.Skills.Count);

            try
            {
                var exp = new WorkExperience("Contoso", new DateTime(2018, 1, 1), new DateTime(2020, 1, 1),
                    "Developer");
                resume.AddExperience(exp);
                Console.WriteLine("Added work experience. Count: " + resume.Experiences.Count);

                // adding same experience again should throw per spec
                resume.AddExperience(exp);
                Console.WriteLine("Duplicate experience added (unexpected).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expected exception on duplicate experience: " + ex.Message);
            }

            // Minimal WorkExperience.GetExperienceInYears test
            var testExp = new WorkExperience("Acme", new DateTime(2015, 6, 1), new DateTime(2018, 6, 1), "Engineer");
            Console.WriteLine("Experience in years: " + testExp.GetExperienceInYears());

            // Final lists
            Console.WriteLine("\nFinal summary:");
            Console.WriteLine("- Candidates: 1");
            Console.WriteLine("- Recruiters: 1");
            Console.WriteLine($"- Company {company.Name} jobs: {company.Jobs.Count}");
        }
    }
}