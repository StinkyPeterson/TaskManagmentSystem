using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled);

    public string Value { get; }
    
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email не может быть пустым");
        }

        if (!EmailRegex.IsMatch(value))
        {
            throw new ArgumentException("Неверный формат Email");
        }

        Value = value;
    }
    
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new Email(email);

    public override string ToString() => Value;
    public override bool Equals(object obj) => obj is Email other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}