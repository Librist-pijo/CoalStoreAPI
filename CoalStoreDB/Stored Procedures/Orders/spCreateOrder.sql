CREATE PROCEDURE [dbo].[spCreateOrder]
	@CustomerId INT,
    @OrderDate DATETIME2(7),
    @ShippingAddress NVARCHAR(255),
    @ShippingDate DATETIME2(7) = NULL,
    @Status INT,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO Orders(CustomerId,OrderDate,ShippingAddress,ShippingDate,Status) VALUES (@CustomerId, @OrderDate, @ShippingAddress, @ShippingDate, @Status) 
        
        SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END