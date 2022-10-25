CREATE PROCEDURE [dbo].[spDeleteProduct]
	@Id INT
AS
BEGIN
	DELETE Products WHERE Id = @Id
END

