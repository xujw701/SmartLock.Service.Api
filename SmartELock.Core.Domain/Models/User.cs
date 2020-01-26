using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public int CompanyId { get; private set; }
        public int BranchId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }
        public bool Individual { get; private set; }
        public int UserRoleId { get; private set; }
        public int? ResPortraitId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private User(UserCreateCommand command)
        {
            CompanyId = command.CompanyId;
            BranchId = command.BranchId;
            FirstName = command.FirstName;
            LastName = command.LastName;
            Email = command.Email;
            Phone = command.Phone;
            Username = command.Username;
            Password = command.Password;
            Individual = command.Individual;
            UserRoleId = command.UserRoleId;
        }

        private User(UserSnapshot snapshot)
        {
            UserId = snapshot.UserId;
            CompanyId = snapshot.CompanyId;
            BranchId = snapshot.BranchId;
            FirstName = snapshot.FirstName;
            LastName = snapshot.LastName;
            Email = snapshot.Email;
            Phone = snapshot.Phone;
            Username = snapshot.Username;
            Password = snapshot.Password;
            Token = snapshot.Token;
            Individual = snapshot.Individual;
            UserRoleId = snapshot.UserRoleId;
            ResPortraitId = snapshot.ResPortraitId;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static User CreateFrom(UserCreateCommand command)
        {
            return new User(command);
        }

        public static User CreateFrom(UserSnapshot snapshot)
        {
            return new User(snapshot);
        }
    }
}
