-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CreateReservation
	-- Add the parameters for the stored procedure here
	@BookCopyID integer,
	@DateReserved Date,
	@DateReturned Date,
	@UserId varchar(40)
AS
BEGIN
	insert into Reservation 
(BookCopyID, DateReserved, DateReturned, UserId)
values(@BookCopyID, @DateReserved, @DateReturned,@UserId);

END
GO
