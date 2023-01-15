CREATE PROCEDURE [dbo].[spUpdateCustomerPassword]
	@Id INT,
	@OldPassword NVARCHAR(255),
	@NewPassword NVARCHAR(255)
AS
BEGIN
 UPDATE Customers
 SET
	Password = HASHBYTES('SHA2_512', @NewPassword)
 WHERE 
	Id = @Id AND Password = HASHBYTES('SHA2_512', @OldPassword)
END
