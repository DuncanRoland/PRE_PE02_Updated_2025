using Pre.PEJobApplicationSystem.Core;

namespace Pre.PEJobApplicationSystem.Cons;

public class Program
{
    static void Main(string[] args)
    {
        List<Skill> skills = new List<Skill>();
        Skill csharpProgramming = new Skill("C# Programming", 5);
        Skill javascript = new Skill("JavaScript Programming", 5);

        skills.Add(csharpProgramming);
        skills.Add(javascript);

        Resume resumeDuncan = new Resume(skills);

        Candidate candidate1 = new Candidate("Duncan", "Roland", "Duncan.roland@student.howest.be", resumeDuncan);

        Console.WriteLine(candidate1.GetInfo());

        Recruiter recruiter1 = new Recruiter("Sofie", "De Smet", "Sofie@desmet.be"); 
        Console.WriteLine(recruiter1.GetInfo());
        
        var manager = new ApplicationManager();
        
        manager.AddCandidate(candidate1);
        manager.AddRecruiter(recruiter1);

        Console.WriteLine("Added candidate: " + candidate1.GetInfo());
        Console.WriteLine("Added recruiter: " + recruiter1.GetInfo());
        
      
    }
}