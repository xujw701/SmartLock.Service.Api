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
    }

    public interface IBranchCreateCommand : IBranchCommand
    {
        int CompanyId { get; }

        int? OperatedBy { get; }
        int? OperatedByAdmin { get; }
    }

    public interface IUserCommand : ICommand
    {
    }

    public interface IUserCreateCommand : IUserCommand
    {
        int CompanyId { get; }
        int BranchId { get; }
        string Username { get; }
        int UserRoleId { get; }

        int? OperatedBy { get; }
        int? OperatedByAdmin { get; }
    }

    public interface IKeyboxAssetCommand : ICommand
    {
        string Uuid { get; }
    }
}