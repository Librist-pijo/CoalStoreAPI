﻿CREATE PROCEDURE [dbo].[spGetOrderById]
	@Id INT
AS
BEGIN
	SELECT * FROM Orders WHERE Id = @Id
END