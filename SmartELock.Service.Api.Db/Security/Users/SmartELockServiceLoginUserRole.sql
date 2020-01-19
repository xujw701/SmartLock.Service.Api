CREATE ROLE [SmartELockServiceLoginUserRole] AUTHORIZATION [dbo];
GO

GRANT CONNECT TO [SmartELockServiceLoginUserRole]
GO

ALTER ROLE [SmartELockServiceLoginUserRole] ADD MEMBER [SmartELockServiceUser]
GO
