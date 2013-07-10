/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
Declare @cnt Int = 0
Select @cnt = Count(*) from [dbo].[webpages_Roles] 

If @cnt = 0
Begin
	:r LookupData.sql
	:r DefaultData.sql

End
GO