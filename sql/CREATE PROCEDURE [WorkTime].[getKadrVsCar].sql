USE [dbase1]
GO
/****** Object:  StoredProcedure [WorkTime].[getPosts]    Script Date: 24.09.2020 16:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-09-24
-- Description:	ѕолучение сотрудников дл€ пропусков
-- =============================================
ALTER PROCEDURE [WorkTime].[getKadrVsCar]
	@id_kadr int
AS
BEGIN
	SET NOCOUNT ON;
	
select 	
	k.id,
	isnull(ltrim(rtrim(k.lastname))+' ','')+isnull(substring(ltrim(rtrim(k.firstname)),1,1)+'. ','')+isnull(substring(ltrim(rtrim(k.secondname)),1,1)+'. ','') as fio,
	u.FullNameCar,	
	u.ShortNameCar
from 
	dbo.s_kadr  k
		left join WorkTime.User_vs_Car u on u.id_kadr  = k.id
where 
	k.id = @id_kadr

END
