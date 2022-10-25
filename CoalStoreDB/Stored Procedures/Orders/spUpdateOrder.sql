﻿CREATE PROCEDURE [dbo].[spUpdateOrder]
	@Id INT,
	@CustomerId INT,
	@OrderDate DATETIME2(7),
	@ShippingAddress NVARCHAR(255),
	@ShippingDate DATETIME2(7),
	@Status INT
AS
BEGIN
	UPDATE Orders
	SET
		CustomerId = @CustomerId,
		OrderDate = @OrderDate,
		ShippingAddress = @ShippingAddress,
		ShippingDate = @ShippingDate,
		Status = @Status
	WHERE
		Id = @Id
END
