USE [dbase1]
GO
/****** Object:  StoredProcedure [Vacation].[setSettings]    Script Date: 24.09.2020 15:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2019-11-28
-- Description:	Запись настроек
-- =============================================
CREATE PROCEDURE [WorkTime].[setSettings]	
	@id_prog int,
	@id_value varchar(4),
	@value varchar(64)
AS
BEGIN
	SET NOCOUNT ON;

	IF Exists ( select value from dbo.prog_config where id_prog = @id_prog and id_value= @id_value)
		UPDATE dbo.prog_config set value = @value where id_prog = @id_prog and id_value= @id_value
	else
		INSERT INTO dbo.prog_config (id_prog,id_value,type_value,value_name,value,comment)
		values (@id_prog,@id_value,'N','',@value,' ')
		

	
END
