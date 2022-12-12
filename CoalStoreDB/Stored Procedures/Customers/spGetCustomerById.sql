CREATE PROCEDURE [dbo].[spGetCustomerById]
	@Id Int
	AS
BEGIN
	SELECT 
		Id,
		Login,
		Password,
		FirstName,
		LastName,
		AddressLine1,
		AddressLine2,
		PostCode
		FROM Customers
		WHERE Id = @Id
END
