CREATE PROCEDURE [dbo].[spCreateCategory]
	@Name NVARCHAR(255),
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO Categories(Name) VALUES (@Name)
        
        SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
