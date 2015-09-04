USE [WiseEyeV5Express]
GO

ALTER TABLE [dbo].[Shifts6] DROP CONSTRAINT [DF__Shifts6__Enable__73DA2C14]
GO

/****** Object:  Table [dbo].[Shifts6]    Script Date: 9/4/2015 3:26:20 PM ******/
DROP TABLE [dbo].[Shifts6]
GO

/****** Object:  Table [dbo].[Shifts6]    Script Date: 9/4/2015 3:26:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shifts6](
	[ShiftID] [int] IDENTITY(1,1) NOT NULL,
	[ShiftCode] [nvarchar](20) NULL,
	[Onduty] [nvarchar](50) NULL,
	[Offduty] [nvarchar](50) NULL,
	[KyHieuCC] [nvarchar](4) NULL,
	[DayCount] [int] NULL,
	[OnTimeIn] [int] NULL,
	[OnTimeOut] [int] NULL,
	[CutIn] [int] NULL,
	[CutOut] [int] NULL,
	[OnLunch] [nvarchar](5) NULL,
	[OffLunch] [nvarchar](5) NULL,
	[WorkingTime] [real] NULL,
	[Workingday] [real] NULL,
	[StartNT] [nvarchar](5) NULL,
	[EndNT] [nvarchar](5) NULL,
	[LateGrace] [int] NULL,
	[EarlyGrace] [int] NULL,
	[AfterOT] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[Enable] [bit] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Shifts6] ADD  CONSTRAINT [DF__Shifts6__Enable__73DA2C14]  DEFAULT ((1)) FOR [Enable]
GO


