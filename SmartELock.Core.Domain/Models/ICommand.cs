namespace SmartELock.Core.Domain.Models
{
    public interface ICommand
    {
    }

    public interface IPermissionCommand
    {
        int? OperatedBy { get; }
        int? OperatedByAdmin { get; }
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

    public interface IBranchCreateCommand : IBranchCommand, IPermissionCommand
    {
        int CompanyId { get; }
    }

    public interface IUserCommand : ICommand
    {
    }

    public interface IUserCreateCommand : IUserCommand, IPermissionCommand
    {
        int CompanyId { get; }
        int BranchId { get; }
        string Username { get; }
        int UserRoleId { get; }
    }

    public interface IUserUpdateCommand : IUserCommand, IPermissionCommand
    {
        int UserId { get; }
    }

    public interface IKeyboxAssetCommand : ICommand
    {
        string Uuid { get; }
    }

    public interface IKeyboxCommand : ICommand, IPermissionCommand
    {
        int KeyboxId { get; }
        string Uuid { get; set; }
    }

    public interface IKeyboxCreateCommand : IKeyboxCommand
    {
    }

    public interface IKeyboxPropertyCreateUpdateCommand : IKeyboxCommand
    {
        int CompanyId { get; }
        int BranchId { get; }
    }

    public interface IKeyboxAssignToCommand : IKeyboxCommand
    {
        int TargetUserId { get; }
    }
}