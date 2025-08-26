using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace KopiBudget.Domain.Entities
{
    public class User : AuditableEntity
    {
        #region Properties

        protected User()
        { }

        private User(string userName, string email, string password, string status, string firstName, string lastName, string middleName)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailException(email);

            if (!IsValidEmail(email))
                throw new InvalidEmailException(email);

            UserName = userName;
            Email = email;
            Password = password;
            Status = status;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string Email { get; set; } = string.Empty;

        public string UserName { get; private set; } = string.Empty;

        public string Password { get; private set; } = string.Empty;

        public string Status { get; private set; } = string.Empty;

        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string MiddleName { get; private set; } = string.Empty;

        public bool IsSystemGenerated { get; private set; } = false;

        // Navigation
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        public static User Register(string userName, string email, string password, string status, string firstName, string lastName, string middleName)
        {
            User user = new User(userName, email, password, status, firstName, lastName, middleName);
            return user;
        }

        public static User Seed(string userName, string email, string password, string status, string firstName, string lastName, string middleName)
        {
            User user = new User(userName, email, password, status, firstName, lastName, middleName);
            return user;
        }

        public static User Create(string userName, string email, string password, string status, string firstName, string lastName, string middleName, DateTime? createdOn, Guid? createdById, bool isSeed = false)
        {
            User user = new User(userName, email, password, status, firstName, lastName, middleName);
            if (!isSeed)
            {
                user.SetCreated(createdById!.Value, createdOn!.Value);
            }
            return user;
        }

        public void FlagAsSystemGenerated() => IsSystemGenerated = true;

        public User Update(string firstName, string lastName, string middleName, DateTime updatedOn, Guid updatedById)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            SetUpdated(updatedById, updatedOn);
            return this;
        }

        public void AssignRole(Role role)
        {
            UserRoles.Add(new UserRole { User = this, Role = role });
        }

        private static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        #endregion Properties
    }
}