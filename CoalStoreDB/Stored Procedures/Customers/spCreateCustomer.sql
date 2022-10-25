CREATE PROCEDURE [dbo].[spCreateCustomer]
   @Login NVARCHAR(255), 
    @Password NVARCHAR(50), 
    @FirstName NVARCHAR(255) = NULL, 
    @LastName NVARCHAR(255) = NULL,
	@AddressLine1 NVARCHAR(255) = NULL,
	@AddressLine2 NVARCHAR(255) = NULL,
	@PostCode NVARCHAR(255) = NULL,
	@Id INT output
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

        INSERT INTO dbo.[Customers] (Login, Password, FirstName, LastName, AddressLine1, AddressLine2, PostCode)
        VALUES(@Login, HASHBYTES('SHA2_512', @Password), @FirstName, @LastName, @AddressLine1, @AddressLine2, @PostCode)
		set @id = SCOPE_IDENTITY()

		return @id
    END TRY
    BEGIN CATCH
        return ERROR_MESSAGE() 
    END CATCH
END