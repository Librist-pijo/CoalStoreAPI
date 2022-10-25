CREATE PROCEDURE [dbo].[spUpdateCustomer]
	@Id INT,
	@Login NVARCHAR(255), 
    @Password NVARCHAR(50), 
    @FirstName NVARCHAR(255), 
    @LastName NVARCHAR(255),
	@AddressLine1 NVARCHAR(255),
	@AddressLine2 NVARCHAR(255),
	@PostCode NVARCHAR(255)
AS
BEGIN
 UPDATE Customers
 SET
	Login = @Login,
	Password = HASHBYTES('SHA2_512',
	@Password),
	FirstName = @FirstName,
	LastName = @LastName,
	AddressLine1 = @AddressLine1,
	AddressLine2 = @AddressLine2,
	PostCode = @PostCode
 WHERE 
	Id = @Id
END
