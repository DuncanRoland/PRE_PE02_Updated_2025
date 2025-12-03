using System;
using System.Collections.Generic;
using Pre.PEJobApplicationSystem.Core;

namespace Pre.PEJobApplicationSystem.Cons;

public class Program
{
    static void Main(string[] args)
    {
        var skills = new List<Skill>
        {
            new("C# Programming", 5),
            new("JavaScript Programming", 5)
        };

        var resumeDuncan = new Resume(skills);

        var candidate1 = new Candidate("Duncan", "Roland", "Duncan.roland@student.howest.be", resumeDuncan);
        var recruiter1 = new Recruiter("Sofie", "De Smet", "Sofie@desmet.be");

        var manager = new ApplicationManager();


        var candidates = new List<Candidate> { candidate1 };
        var recruiters = new List<Recruiter> { recruiter1 };
        var companies = new List<Company>();
        var jobApplications = new List<JobApplication>();

        manager.AddCandidate(candidate1);
        manager.AddRecruiter(recruiter1);

        Console.WriteLine("Added candidate: " + candidate1.GetInfo());
        Console.WriteLine("Added recruiter: " + recruiter1.GetInfo());

        var softwareDeveloper = new Job(
            "Software Developer",
            "Develop and maintain software applications.",
            40000,
            70000,
            skills);

        var howest = new Company("Howest", "Information Technology");
        companies.Add(howest);

        howest.AddJob(softwareDeveloper);

        recruiter1.PostJob(howest, softwareDeveloper);
        Console.WriteLine("Posted job: " + softwareDeveloper.Title + " at " + howest.Name);


        Console.WriteLine("\nCandidates list:");
        foreach (var c in candidates)
            Console.WriteLine("- " + c.GetInfo());

        Console.WriteLine("\nRecruiters list:");
        foreach (var r in recruiters)
            Console.WriteLine("- " + r.GetInfo());

        Console.WriteLine("\nCompanies and their jobs:");
        foreach (var comp in companies)
        {
            Console.WriteLine("- " + comp.Name);
            if (comp.Jobs.Count > 0)
            {
                foreach (var j in comp.Jobs)
                    Console.WriteLine("  * " + j.Title + ": " + j.Description);
            }
            else
            {
                Console.WriteLine("  (no jobs)");
            }
        }
        
        
        

        var jobApplication1 = new JobApplication(candidate1, softwareDeveloper);
        jobApplications.Add(jobApplication1 );
        Console.WriteLine("\nJob Applications:");
        
        
        
    }
}