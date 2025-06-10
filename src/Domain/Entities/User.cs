using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class User : Entity
{
    public string UserName { get; private set; }
    public Email Email { get; private set; }
    
    protected User(Guid id) : base(id)
    {
    }
    
    public User(Guid id, string userName, Email email) : base(id)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Имя пользователя не может быть пустым");
        }

        UserName = userName.Trim();
        
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void SetUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Имя пользователя не может быть пустым");
        }

        UserName = userName.Trim();
    }

    public void SetEmail(string email)
    {
        Email = new Email(email);
    }
}