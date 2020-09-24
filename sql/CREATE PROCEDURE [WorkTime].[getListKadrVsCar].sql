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
-- Description:	ѕолучение списка сотрудников дл€ пропусков
-- =============================================
ALTER PROCEDURE [WorkTime].[getListKadrVsCar]
	@id_workstatus int,
	@id_personnelType int
AS
BEGIN
	SET NOCOUNT ON;
	

DECLARE @table table (id_User_vs_Car int, datePrint datetime)

insert into @table(id_User_vs_Car,datePrint)
select p.id_User_vs_Car,max(p.DateEdit) as datePrint  from WorkTime.j_PassCarUnload p group by p.id_User_vs_Car

select 	
	k.id,
	isnull(ltrim(rtrim(k.lastname))+' ','')+isnull(substring(ltrim(rtrim(k.firstname)),1,1)+'. ','')+isnull(substring(ltrim(rtrim(k.secondname)),1,1)+'. ','') as fio,
	k.id_departments,
	ltrim(rtrim(d.name)) as nameDep,
	k.id_posts,
	ltrim(rtrim(p.cName)) as namePost,
	u.FullNameCar,	
	u.ShortNameCar,
	u.DateEdit,
	ltrim(rtrim(l.FIO)) as nameEditor,
	k.dateUnemploy,
	cast(0 as bit) as isSelect,
	tt.datePrint as datePrintPass,
	k.comment,
	pd.Code,
	u.id as id_User_vs_Car
from 
	dbo.s_kadr  k
		left join Personnel.s_PersonalData pd on pd.id_Kadr = k.id
		left join dbo.departments d on d.id = k.id_departments
		left join dbo.s_Posts p on p.id = k.id_posts
		left join WorkTime.User_vs_Car u on u.id_kadr  = k.id
		left join dbo.ListUsers l on l.id = u.id_Editor
		left join @table tt on tt.id_User_vs_Car = u.id
where 
	k.id_WorkStatus  = @id_workstatus and k.id_PersonnelType = @id_personnelType

END
