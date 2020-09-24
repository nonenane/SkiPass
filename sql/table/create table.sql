CREATE TABLE [WorkTime].[User_vs_Car](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_kadr]		int				not null,
	[FullNameCar]	varchar(1024)	not null,
	[ShortNameCar]	varchar(1024)	not null,
	[id_Editor]		int				null,
	[DateEdit]		datetime		null,
 CONSTRAINT [PK_User_vs_Car] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [WorkTime].[User_vs_Car] ADD CONSTRAINT FK_User_vs_Car_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [WorkTime].[User_vs_Car] ADD CONSTRAINT FK_User_vs_Car_id_id_kadr FOREIGN KEY (id_kadr)  REFERENCES [dbo].s_kadr (id)
GO


CREATE TABLE [WorkTime].[j_PassCarUnload](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_User_vs_Car]		int				not null,
	[id_Editor]		int				null,
	[DateEdit]		datetime		null,
 CONSTRAINT [PK_j_PassCarUnload] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [WorkTime].[j_PassCarUnload] ADD CONSTRAINT FK_j_PassCarUnload_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [WorkTime].[j_PassCarUnload] ADD CONSTRAINT FK_j_PassCarUnload_id_id_kadr FOREIGN KEY (id_User_vs_Car)  REFERENCES [WorkTime].[User_vs_Car] (id)
GO