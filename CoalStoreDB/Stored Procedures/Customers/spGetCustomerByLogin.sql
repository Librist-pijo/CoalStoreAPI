CREATE PROCEDURE [dbo].[spGetCustomerByLogin]
	@Login NVARCHAR(255)
AS
BEGIN
	SELECT 
		Id,
		Login,
		--Password,
		FirstName,
		LastName,
		AddressLine1,
		AddressLine2,
		PostCode
		FROM Customers
		WHERE Login = @Login
END

