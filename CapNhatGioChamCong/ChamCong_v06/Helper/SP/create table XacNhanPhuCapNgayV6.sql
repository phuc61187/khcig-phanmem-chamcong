USE [WiseEyeV5Express]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] DROP CONSTRAINT [DF__XacNhanPh__PhuCa__7740A8A4]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] DROP CONSTRAINT [DF__XacNhanPh__TinhP__764C846B]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] DROP CONSTRAINT [DF__XacNhanPh__TinhP__75586032]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] DROP CONSTRAINT [DF__XacNhanPh__TinhP__74643BF9]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] DROP CONSTRAINT [DF__XacNhanPh__TinhP__737017C0]
GO

/****** Object:  Table [dbo].[XacNhanPhuCapNgayV6]    Script Date: 9/18/2015 4:45:48 PM ******/
DROP TABLE [dbo].[XacNhanPhuCapNgayV6]
GO

/****** Object:  Table [dbo].[XacNhanPhuCapNgayV6]    Script Date: 9/18/2015 4:45:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[XacNhanPhuCapNgayV6](
	[Ngay] [datetime] NULL,
	[UserEnrollNumber] [int] NOT NULL,
	[TinhPCTC] [bit] NOT NULL,
	[TinhPCNgayNghi] [bit] NOT NULL,
	[TinhPCNgayLe] [bit] NOT NULL,
	[TinhPCThuCong] [bit] NOT NULL,
	[PhuCapTay] [real] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] ADD  DEFAULT ((0)) FOR [TinhPCTC]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] ADD  DEFAULT ((0)) FOR [TinhPCNgayNghi]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] ADD  DEFAULT ((0)) FOR [TinhPCNgayLe]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] ADD  DEFAULT ((0)) FOR [TinhPCThuCong]
GO

ALTER TABLE [dbo].[XacNhanPhuCapNgayV6] ADD  DEFAULT ((0)) FOR [PhuCapTay]
GO

