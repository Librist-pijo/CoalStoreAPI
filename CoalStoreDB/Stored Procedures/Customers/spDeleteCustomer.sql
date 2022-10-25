CREATE PROCEDURE [dbo].[spDeleteCustomer]
	@Id INT
AS
BEGIN
 DELETE Customers WHERE Id = @Id
END
