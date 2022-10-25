CREATE PROCEDURE [dbo].[spDeleteOrder]
	@Id INT
AS
BEGIN
	DELETE Orders WHERE Id = @Id
END

