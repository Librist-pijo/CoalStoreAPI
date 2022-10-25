CREATE PROCEDURE [dbo].[spDeleteCategory]
	@Id INT
AS
BEGIN
	DELETE Categories WHERE Id = @Id
END
