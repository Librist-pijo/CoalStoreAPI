CREATE PROCEDURE [dbo].[spGetInvoiceById]
	@Id INT
AS
BEGIN
	SELECT * FROM Invoices WHERE Id = @Id
END
