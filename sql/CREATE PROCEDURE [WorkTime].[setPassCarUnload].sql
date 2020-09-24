USE [dbase1]
GO
/****** Object:  StoredProcedure [Vacation].[setSettings]    Script Date: 24.09.2020 15:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-09-24
-- Description:	«апись данных по печати пропуска на парковку
-- =============================================
ALTER PROCEDURE [WorkTime].[setPassCarUnload]	
	@id_User_vs_Car int,
	@id_user int
AS
BEGIN
	SET NOCOUNT ON;
BEGIN TRY 
		INSERT INTO WorkTime.j_PassCarUnload (id_User_vs_Car,id_Editor,DateEdit)
			values (@id_User_vs_Car,@id_user,GETDATE())

		select cast(SCOPE_IDENTITY() as int) as id
END TRY
BEGIN CATCH
	SELECT -1 as id, ERROR_MESSAGE() as msg
END CATCH
	
END
