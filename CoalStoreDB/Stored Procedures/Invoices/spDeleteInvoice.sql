CREATE PROCEDURE [dbo].[spDeleteInvoice]
	@Id INT
AS
BEGIN
	DELETE Invoices WHERE Id = @Id
END
