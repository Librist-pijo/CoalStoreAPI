CREATE PROCEDURE [dbo].[spGetCustomers]
AS
BEGIN  
    SELECT Id, Login, Password, FirstName, LastName, AddressLine1, AddressLine2, PostCode FROM Customers
END
