using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class SuperAdmin
    {
        public int SuperAdminId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private SuperAdmin(SuperAdminCreateCommand command)
        {
            Username = command.Username;
            Password = command.Password;
        }

        private SuperAdmin(SuperAdminSnapshot snapshot)
        {
            SuperAdminId = snapshot.SuperAdminId;
            Username = snapshot.Username;
            Password = snapshot.Password;
            Token = snapshot.Token;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static SuperAdmin CreateFrom(SuperAdminCreateCommand command)
        {
            return new SuperAdmin(command);
        }

        public static SuperAdmin CreateFrom(SuperAdminSnapshot snapshot)
        {
            return new SuperAdmin(snapshot);
        }
    }
}
