CREATE PROCEDURE [dbo].[spDeleteOrdersProducts]
    @Id int 
AS
BEGIN
    	DELETE OrdersProducts WHERE Id = @Id
END
