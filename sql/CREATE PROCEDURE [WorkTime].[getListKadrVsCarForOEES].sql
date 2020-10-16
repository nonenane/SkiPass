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
-- Description:	ѕолучение списка сотрудников с пропусками
-- =============================================
ALTER PROCEDURE [WorkTime].[getListKadrVsCarForOEES]	
AS
BEGIN
	SET NOCOUNT ON;
	
select 	
	isnull(substring(isnull(ltrim(rtrim(k.lastname)),''),1,1)+'. ','')+isnull(substring(ltrim(rtrim(k.firstname)),1,1)+'. ','')+isnull(substring(ltrim(rtrim(k.secondname)),1,1)+'. ','') as fio,
	u.FullNameCar,	
	u.ShortNameCar,
	pd.Code,
	ltrim(rtrim(k.lastname)) as lastname,
	ltrim(rtrim(k.firstname)) as firstname,
	ltrim(rtrim(k.secondname)) as secondname
from 
	WorkTime.User_vs_Car u	
		inner join dbo.s_kadr  k on k.id = u.id_kadr
		left join Personnel.s_PersonalData pd on pd.id_Kadr = k.id
	
where 
	k.id_WorkStatus  =  4 and pd.Code is not null
order by 
	pd.Code asc

END
