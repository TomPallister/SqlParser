-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertResults]
	-- Add the parameters for the stored procedure here
	@Database NVARCHAR(MAX),
	@Server NVARCHAR(MAX),
	@ObjectName NVARCHAR(MAX),
	@Errors NVARCHAR(MAX),
	@Exception NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO TestResults([Database], [Server], [ObjectName], [Errors], [Exception])
	VALUES(@Database, @Server, @ObjectName, @Errors, @Exception)
END