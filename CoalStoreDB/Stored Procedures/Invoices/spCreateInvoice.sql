CREATE PROCEDURE [dbo].[spCreateInvoice]
	@OrderId INT,
    @PaymentMethod NVARCHAR(255),
    @Status INT,
    @Amount MONEY,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO Invoices(OrderId,PaymentMethod,Status,Amount) VALUES (@OrderId,@PaymentMethod,@Status,@Amount)
        
        SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END