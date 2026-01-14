using System.Text.RegularExpressions;

namespace Pre.PEJobApplicationSystem.Core;

public abstract class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; private set; }

    private string _email = string.Empty;

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new Exception("This is not a valid email address.");
            _email = value;
        }
    }

    // Read-only computed full name
    public string FullName => $"{FirstName} {LastName}".Trim();

    public Person(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    private void SetAddress(string address) => Address = address?.Trim() ?? string.Empty;

    //abstract method
    public abstract string GetInfo();

    /*public string GetInfo()
    {
        string fullName = GetFullName();

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            throw new Exception("This is not a valid email address.");

        return $"{GetType().Name} - Name: {fullName}, Email: {Email}";
    }*/
}