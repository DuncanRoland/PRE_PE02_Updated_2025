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

            // --- Test SetLevel on Skill ---
            var testSkill = new Skill("TestSkill", 3);
            Console.WriteLine("Initial TestSkill level: " + testSkill.Level);

            // valid
            testSkill.SetLevel(4);
            Console.WriteLine("After SetLevel(4): " + testSkill.Level);

            // invalid low
            try
            {
                testSkill.SetLevel(0);
                Console.WriteLine("SetLevel(0) did NOT throw (unexpected).");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Expected exception for SetLevel(0): " + ex.Message);
            }

            // invalid high
            try
            {
                testSkill.SetLevel(6);
                Console.WriteLine("SetLevel(6) did NOT throw (unexpected).");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Expected exception for SetLevel(6): " + ex.Message);
            }

            // Create company and job, post it
            var job = new Job("Software Developer", "Develop apps", 40000, 70000, new List<Skill>(skills));
            var company = new Company("Howest", "IT");
            company.AddJob(job);
            recruiter.PostJob(company, job);
            Console.WriteLine($"Posted job '{job.Title}' at {company.Name}");

            // Test Job.SetMinSalary / SetMaxSalary validations
            Console.WriteLine("\nTesting Job salary setters:");
            try
            {
                job.SetMinSalary(50); // invalid low
                Console.WriteLine("SetMinSalary(50) did NOT throw (unexpected).");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Expected exception for SetMinSalary(50): " + ex.Message);
            }

            try
            {
                job.SetMinSalary(150); // valid
                Console.WriteLine("SetMinSalary(150) succeeded. MinSalary: " + job.MinSalary);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Unexpected exception for SetMinSalary(150): " + ex.Message);
            }

            try
            {
                job.SetMaxSalary(1_500_000); // invalid high
                Console.WriteLine("SetMaxSalary(1500000) did NOT throw (unexpected).");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Expected exception for SetMaxSalary(1500000): " + ex.Message);
            }

            try
            {
                job.SetMaxSalary(900000); // valid
                Console.WriteLine("SetMaxSalary(900000) succeeded. MaxSalary: " + job.MaxSalary);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Unexpected exception for SetMaxSalary(900000): " + ex.Message);
            }

            // Test AddRequiredSkill
            Console.WriteLine("\nTesting AddRequiredSkill:");
            Console.WriteLine("Required skills before: " + job.RequiredSkills.Count);
            job.AddRequiredSkill(new Skill("SQL", 4));
            Console.WriteLine("Required skills after: " + job.RequiredSkills.Count);

            // Candidate applies once
            var application = candidate.ApplyForJob(job);
            manager.AddJobApplication(application);
            Console.WriteLine($"\nCandidate applied for: {application.Job.Title}");
            Console.WriteLine("Candidate AppliedJobs count: " + candidate.AppliedJobs.Count);

            // Check eligibility (should be false)
            Console.WriteLine("\nChecking candidate eligibility (expect false): " + job.IsCandidateEligible(candidate));

            // Add more applications to reach 10 and re-check eligibility
            for (int i = 0; i < 9; i++)
            {
                var app = candidate.ApplyForJob(job);
                manager.AddJobApplication(app);
            }

            Console.WriteLine("Candidate AppliedJobs count after additional applies: " + candidate.AppliedJobs.Count);
            Console.WriteLine("Checking candidate eligibility (expect true): " + job.IsCandidateEligible(candidate));

            // Recruiter reviews the application (should add an Interview with feedback "Top!")
            recruiter.ReviewApplication(application);
            Console.WriteLine("\nRecruiter reviewed application.");

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
            Console.WriteLine("\nResume skills before: " + resume.Skills.Count);
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