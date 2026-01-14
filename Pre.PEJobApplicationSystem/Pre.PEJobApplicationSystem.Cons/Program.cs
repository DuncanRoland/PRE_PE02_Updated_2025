using System;
using System.Collections.Generic;
using System.Reflection;
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

        //var howest = new Company("Howest", "Information Technologies");
        var howest = new Company("Howest", "IT");
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
        jobApplications.Add(jobApplication1);
        manager.AddJobApplication(jobApplication1);

        // Call the method under test
        recruiter1.ReviewApplication(jobApplication1);
        Console.WriteLine("\nJob application reviewed by recruiter.");

        // Reflection-based attempt to find interviews and their feedback (robust to property/field naming)
        Console.WriteLine("\nInterviews found for the application:");
        bool foundAny = false;

        // Check public properties
        foreach (var prop in jobApplication1.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var val = prop.GetValue(jobApplication1);
            if (val == null) continue;

            if (val is Interview singleInterview)
            {
                foundAny = true;
                PrintInterviewFeedback(singleInterview);
            }
            else if (val is System.Collections.IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is Interview interviewItem)
                    {
                        foundAny = true;
                        PrintInterviewFeedback(interviewItem);
                    }
                }
            }
        }

        // Check public fields as well (in case interviews are stored in a field)
        foreach (var field in jobApplication1.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            var val = field.GetValue(jobApplication1);
            if (val == null) continue;

            if (val is Interview singleInterview)
            {
                foundAny = true;
                PrintInterviewFeedback(singleInterview);
            }
            else if (val is System.Collections.IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is Interview interviewItem)
                    {
                        foundAny = true;
                        PrintInterviewFeedback(interviewItem);
                    }
                }
            }
        }

        if (!foundAny)
        {
            Console.WriteLine("  (no interviews found or unable to read interview details)");
        }
    }

    static void PrintInterviewFeedback(Interview interview)
    {
        // Try to get a public property named Feedback (or a field) to display the value
        var t = interview.GetType();
        var fbProp = t.GetProperty("Feedback", BindingFlags.Public | BindingFlags.Instance);
        if (fbProp != null)
        {
            var fbVal = fbProp.GetValue(interview) as string;
            Console.WriteLine("  - Feedback: " + fbVal);
            return;
        }

        var fbField = t.GetField("feedback", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (fbField != null)
        {
            var fbVal = fbField.GetValue(interview) as string;
            Console.WriteLine("  - Feedback: " + fbVal);
            return;
        }

        // Fallback: print the interview's type name
        Console.WriteLine("  - Interview instance of type: " + t.Name);
    }
}