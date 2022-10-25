CREATE PROCEDURE [dbo].[spGetProductByName]
@Name NVARCHAR(255)
AS
BEGIN
	SELECT * FROM Products WHERE Name = @Name
END
