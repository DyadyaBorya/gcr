SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "GCR"
:setvar DefaultFilePrefix "GCR"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\"

GO

/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Dropping DF__webpages___IsCon__1920BF5C...';


GO
ALTER TABLE [dbo].[webpages_Membership] DROP CONSTRAINT [DF__webpages___IsCon__1920BF5C];


GO
PRINT N'Dropping DF__webpages___Passw__1A14E395...';


GO
ALTER TABLE [dbo].[webpages_Membership] DROP CONSTRAINT [DF__webpages___Passw__1A14E395];


GO
PRINT N'Dropping fk_UserId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_UserId];


GO
PRINT N'Dropping fk_RoleId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_RoleId];


GO
PRINT N'Dropping [dbo].[UserProfile]...';


GO
DROP TABLE [dbo].[UserProfile];


GO
PRINT N'Dropping [dbo].[webpages_Membership]...';


GO
DROP TABLE [dbo].[webpages_Membership];


GO
PRINT N'Dropping [dbo].[webpages_OAuthMembership]...';


GO
DROP TABLE [dbo].[webpages_OAuthMembership];


GO
PRINT N'Dropping [dbo].[webpages_Roles]...';


GO
DROP TABLE [dbo].[webpages_Roles];


GO
PRINT N'Dropping [dbo].[webpages_UsersInRoles]...';


GO
DROP TABLE [dbo].[webpages_UsersInRoles];


GO
PRINT N'Update complete.';


GO
