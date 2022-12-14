CREATE PROCEDURE [dbo].[spGetCategoryById]
@Id int
AS
BEGIN
	SELECT * FROM Categories WHERE Id = @Id
END
