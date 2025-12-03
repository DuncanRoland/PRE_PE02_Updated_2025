namespace Pre.PEJobApplicationSystem.Core;

public abstract class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; private set; }
    public string Email { get; set; }

    protected Person(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    public string GetFullName() => $"Full name: {FirstName} {LastName}";

    public string GetInfo()
    {
        return GetFullName();
    }
}