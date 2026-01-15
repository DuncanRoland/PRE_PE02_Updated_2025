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

            // Existing add methods (keep for backward compatibility)
            manager.AddCandidate(candidate);
            manager.AddRecruiter(recruiter);

            // --- Ensure interface methods are exercised ---
            manager.RegisterCandidate(candidate); // RegisterCandidate
            var company = new Company("Howest", "IT");
            manager.RegisterCompany(company); // RegisterCompany

            var job = new Job("Software Developer", "Develop apps", 40000, 70000, new List<Skill>(skills));

            // Show candidate->job match score (uses Candidate.GetMatchScore which calls the extension)
            var matchScore = candidate.GetMatchScore(job);
            Console.WriteLine($"Match score for {candidate.FullName} and job '{job.Title}': {matchScore}");

            
            // PostJob via manager (uses company reference and job parameters)
            manager.PostJob(company, job.Title, job.Description, job.MinSalary, job.MaxSalary,
                new List<Skill>(job.RequiredSkills));
            Console.WriteLine($"Manager.PostJob: company '{company.Name}' now has {company.Jobs.Count} job(s).");

            // Create an application and apply via manager.Apply
            var managerApplication = new JobApplication(candidate, job);
            manager.Apply(managerApplication);
            Console.WriteLine("Manager.Apply: added a job application via manager.Apply.");

            // MatchCandidateToJobs (uses jobs from first company and extension method)
            var matches = manager.MatchCandidateToJobs(candidate);
            Console.WriteLine($"MatchCandidateToJobs returned {matches.Count} match(es):");
            foreach (var m in matches)
            {
                Console.WriteLine($" - {m.Title} at {company.Name}");
            }

            // --- rest of original tests follow ---
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

            // Use company and job already posted above
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

            // Candidate applies once (original flow)
            var application = candidate.ApplyForJob(job);
            manager.AddJobApplication(application);
            Console.WriteLine($"\nCandidate applied for: {application.Job.Title}");
            Console.WriteLine("Candidate AppliedJobs count: " + candidate.AppliedJobs.Count);

            // Test JobApplication methods: UpdateStatus, ApplicationDate, AddInterview
            Console.WriteLine("\nTesting JobApplication:");

            Console.WriteLine("Initial status: " + (application.Status ?? "(null)"));
            application.UpdateStatus("Pending");
            Console.WriteLine("Updated status: " + application.Status);

            Console.WriteLine("ApplicationDate <= now: " + (application.ApplicationDate <= DateTime.Now));

            var interview = new Interview(recruiter);
            interview.AddFeedback("Top!");
            application.AddInterview(interview);
            Console.WriteLine("Interview added: " + (application.Interview != null));

            var feedback = application.Interview?.Feedback ?? "(no feedback)";
            Console.WriteLine("Interview feedback: " + feedback);

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

            // --- NEW: test ApplicationManager.GetJobApplicationsForJob and GetApplicationForJob ---
            var appsForThisJob = manager.GetJobApplicationsForJob(job);
            Console.WriteLine($"\nManager reports {appsForThisJob.Count} job applications for job '{job.Title}':");
            for (int i = 0; i < appsForThisJob.Count; i++)
            {
                var ja = appsForThisJob[i];
                var candName = ja.Candidate != null ? ja.Candidate.FullName : "(unknown)";
                Console.WriteLine($" - #{i + 1}: Candidate: {candName}, Status: {(ja.Status ?? "(null)")}");
            }

            var firstForJob = manager.GetJobApplicationsForJob(job);
            Console.WriteLine("\nFirst application for job (GetApplicationForJob): " +
                              (firstForJob == null
                                  ? "(none)"
                                  : $"{job.Title}"));

            // Recruiter reviews the application (should add an Interview with feedback "Top!")
            recruiter.ReviewApplication(application);
            Console.WriteLine("\nRecruiter reviewed application.");

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