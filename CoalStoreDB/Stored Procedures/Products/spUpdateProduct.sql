CREATE PROCEDURE [dbo].[spUpdateProduct]
	@Id INT,
	@Name NVARCHAR(255),
	@Price MONEY,
	@Stock INT
AS
BEGIN
	UPDATE Products
	SET
		Name = @Name,
		Price = @Price,
		Stock = @Stock
	WHERE
		Id = @Id
END
