CREATE PROCEDURE [dbo].[spCreateProduct]
	@Name NVARCHAR(255),
    @Price MONEY,
    @Stock INT,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO Products(Name,Price,Stock) VALUES (@Name, @Price, @Stock)
        
        SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
