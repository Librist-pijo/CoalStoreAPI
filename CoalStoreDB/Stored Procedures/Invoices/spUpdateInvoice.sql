CREATE PROCEDURE [dbo].[spUpdateInvoice]
	@Id INT,
	@OrderId INT,
	@PaymentMethod NVARCHAR(255),
	@Amount MONEY,
	@State INT
AS
BEGIN
	UPDATE Invoices
	SET
		OrderId = @OrderId,
		PaymentMethod = @PaymentMethod,
		Amount = @Amount,
		State = @State
	WHERE
		Id = @Id
END
