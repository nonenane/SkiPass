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
-- Description:	«апись данных по пропуску сотрудника на сто€нку
-- =============================================
CREATE PROCEDURE [WorkTime].[setUserVsCar]	
	@id_kadr int,
	@fullName varchar(1024),
	@shortName varchar(1024),
	@id_user int,
	@result int,
	@isDel bit
AS
BEGIN
	SET NOCOUNT ON;
BEGIN TRY 
	IF @isDel = 0
		BEGIN
			IF NOT EXISTS (select id from WorkTime.User_vs_Car where id_kadr  = @id_kadr)
				BEGIN
					INSERT INTO WorkTime.User_vs_Car (id_kadr,FullNameCar,ShortNameCar,id_Editor,DateEdit)
						VALUES (@id_kadr,@fullName,@shortName,@id_user,GETDATE())
					
					select Cast(SCOPE_IDENTITY() as int ) as id
						
				END	
			ELSE
				BEGIN
					UPDATE 
						WorkTime.User_vs_Car 
					SET 
						FullNameCar = @fullName,
						ShortNameCar = @shortName,
						DateEdit = GETDATE(),
						id_Editor = @id_user
					WHERE
						id_kadr = @id_kadr

					Select @id_kadr as id
				END
		END
	ELSE
		BEGIN
			
			DELETE FROM WorkTime.j_PassCarUnload where id_User_vs_Car in (select id from WorkTime.User_vs_Car where id_kadr  = @id_kadr)
			DELETE FROM WorkTime.User_vs_Car WHERE id_kadr = @id_kadr

			Select @id_kadr as id
		END
END TRY
BEGIN CATCH
	SELECT -1 as id, ERROR_MESSAGE() as msg
END CATCH
	
END
