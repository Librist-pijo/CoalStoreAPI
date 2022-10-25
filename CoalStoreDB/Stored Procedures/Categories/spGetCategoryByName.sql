CREATE PROCEDURE [dbo].[spGetCategoryByName]
@Name NVARCHAR(255)
AS
BEGIN
	SELECT * FROM Categories WHERE Name = @Name
END
