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
        int? OperatedBy { get; }
        int? OperatedByAdmin { get; }
    }

    public interface IUserCreateCommand : IUserCommand
    {
        int CompanyId { get; }
        int BranchId { get; }
        string Username { get; }
        int UserRoleId { get; }
    }

    public interface IUserUpdateCommand : IUserCommand
    {
        int UserId { get; }
    }

    public interface IKeyboxAssetCommand : ICommand
    {
        string Uuid { get; }
    }

    public interface IKeyboxCommand : ICommand
    {
        int? OperatedBy { get; }
        int? OperatedByAdmin { get; }
    }

    public interface IKeyboxCreateCommand : IKeyboxCommand, IKeyboxAssetCommand
    {
    }

    public interface IKeyboxPropertyCommand : IKeyboxCommand
    {
        int KeyboxId { get; }
    }

    public interface IKeyboxPropertyCreateUpdateCommand : IKeyboxPropertyCommand
    {
        int CompanyId { get; }
        int BranchId { get; }
    }

    public interface IKeyboxAssignToCommand : IKeyboxCommand
    {
        int KeyboxId { get; }
        int TargetUserId { get; }
    }
}