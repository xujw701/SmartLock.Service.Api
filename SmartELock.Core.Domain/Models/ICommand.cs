namespace SmartELock.Core.Domain.Models
{
    public interface ICommand
    {
    }

    public interface ISuperAdminCommand : ICommand
    {
        string Username { get; }
    }
}