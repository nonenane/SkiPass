USE [dbase1]
GO
/****** Object:  StoredProcedure [WorkTime].[getPosts]    Script Date: 24.09.2020 16:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Butakov I.A.
-- Create date: 01-10-2014
-- Description:	Получение списка должностей
-- =============================================
ALTER PROCEDURE [WorkTime].[getPosts]
	@id_dep int,
	@isWorkTime bit  = null
AS
BEGIN
	SET NOCOUNT ON;
	

--declare @id_dep int = 1


IF(@isWorkTime is not null)
	BEGIN
		select distinct
				sp.id,
				isnull(ltrim(rtrim(sp.cName)),'') as cName
			from 
				dbo.Departments_vs_Posts dvp
					left join dbo.s_Posts sp on dvp.id_Posts = sp.id 
			where 
				 dvp.id_Departments = @id_dep
	END
ELSE
	BEGIN


			select 
				* 
			from 
				(
			select 
				0 as id, 
				'Все должности' as cName
			union
			select distinct
				sp.id,
				isnull(ltrim(rtrim(sp.cName)),'') as cName
			from 
				dbo.Departments_vs_Posts dvp
				left join dbo.s_Posts sp on dvp.id_Posts = sp.id 
			where 
				((@id_dep!=0 and dvp.id_Departments = @id_dep)
				or 
				(@id_dep=0))
				and 
				sp.isWorkTime = 1
			) t
			order by t.id
	END
END
