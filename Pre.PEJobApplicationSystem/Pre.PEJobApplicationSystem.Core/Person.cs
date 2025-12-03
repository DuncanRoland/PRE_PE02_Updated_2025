using System.Text.RegularExpressions;

namespace Pre.PEJobApplicationSystem.Core;

public abstract partial class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; private set; }
    public static string Email { get; set; }

    protected Person(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string GetFullName() => $"{FirstName} {LastName}";
    

    public string GetInfo()
    {
        string fullName = GetFullName();
        
        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            throw new Exception("This is not a valid email address.");
        
        return $"{GetType().Name} - Name: {fullName}, Email: {Email}";
    }
}