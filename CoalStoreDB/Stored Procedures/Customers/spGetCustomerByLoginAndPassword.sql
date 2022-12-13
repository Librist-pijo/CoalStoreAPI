CREATE PROCEDURE [dbo].[spGetCustomerByLoginAndPassword]
	@Login NVARCHAR(255),
	@Password NVARCHAR(255)
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
		WHERE Login = @Login AND Password = HASHBYTES('SHA2_512', @Password)
END
RETURN 0