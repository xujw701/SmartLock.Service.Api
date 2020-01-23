namespace SmartELock.Core.Domain.Models
{
    public interface ICommand
    {
    }

    public interface ISuperAdminCommand : ICommand
    {
        string Username { get; }
    }

    public interface ICompanyCommand : ICommand
    {
        string CompanyName { get; }
    }

    public interface IBranchCommand : ICommand
    {
        int BranchId { get; }
    }
}