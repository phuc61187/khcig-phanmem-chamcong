USE [WiseEyeV5Express]
GO

/****** Object:  Table [dbo].[CIO]    Script Date: 9/9/2015 2:12:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CIO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserEnrollNumber] [int] NOT NULL,
	[NgayCong] [datetime] NOT NULL,
	[HaveINOUT] [int] NOT NULL,
	[GioVao] [datetime] NULL,
	[GioRa] [datetime] NULL,
	[Vao] [int] NULL,
	[Ra] [int] NULL,
	[MayVao] [int] NULL,
	[MayRa] [int] NULL,
	[BDLV] [int] NULL,
	[KTLVTrongCa] [int] NULL,
	[KTLV] [int] NULL,
	[BDLVCa3] [int] NULL,
	[KTLVCa3] [int] NULL,
	[Tre] [int] NULL,
	[Som] [int] NULL,
	[VaoSauCa] [int] NULL,
	[RaTruocCa] [int] NULL,
	[SoPhutXacNhanNgoaiGio] [int] NOT NULL,
	[ChoPhepTre] [bit] NOT NULL,
	[ChoPhepSom] [bit] NOT NULL,
	[VaoTuDo] [bit] NOT NULL,
	[RaTuDo] [bit] NOT NULL,
	[CongTrongGio] [real] NOT NULL,
	[CongNgoaiGio] [real] NOT NULL,
	[TruCongTre] [real] NOT NULL,
	[TruCongSom] [real] NOT NULL,
	[ChamCongTay] [real] NOT NULL,
	[DinhMucCong] [real] NOT NULL,
	[TongCong] [real] NULL,
	[GhiChu] [nvarchar](1000) NULL,
	[LyDo] [nvarchar](500) NULL,
	[TheoDoiGioGocMayCC] [nvarchar](2000) NULL,
	[TinhCongThuCong] [bit] NOT NULL,
 CONSTRAINT [PK__CIO__3214EC27284DF453] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__Vao__2A363CC5]  DEFAULT ((-1)) FOR [Vao]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__Ra__2B2A60FE]  DEFAULT ((-1)) FOR [Ra]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__MayVao__2C1E8537]  DEFAULT ((-1)) FOR [MayVao]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__MayRa__2D12A970]  DEFAULT ((-1)) FOR [MayRa]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__BDLV__2E06CDA9]  DEFAULT ((-1)) FOR [BDLV]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__KTLVTrongCa__2EFAF1E2]  DEFAULT ((-1)) FOR [KTLVTrongCa]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__KTLV__2FEF161B]  DEFAULT ((-1)) FOR [KTLV]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__BDLVCa3__30E33A54]  DEFAULT ((-1)) FOR [BDLVCa3]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__KTLVCa3__31D75E8D]  DEFAULT ((-1)) FOR [KTLVCa3]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__TreVR__32CB82C6]  DEFAULT ((0)) FOR [Tre]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__SomVR__33BFA6FF]  DEFAULT ((0)) FOR [Som]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__VaoSauCa__34B3CB38]  DEFAULT ((0)) FOR [VaoSauCa]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__RaTruocCa__35A7EF71]  DEFAULT ((0)) FOR [RaTruocCa]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__SoPhutXacNh__46D27B73]  DEFAULT ((0)) FOR [SoPhutXacNhanNgoaiGio]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__CoTreVR__369C13AA]  DEFAULT ((0)) FOR [ChoPhepTre]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__CoSomVR__379037E3]  DEFAULT ((0)) FOR [ChoPhepSom]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__VaoTuDo__00FF1D08]  DEFAULT ((0)) FOR [VaoTuDo]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__RaTuDo__01F34141]  DEFAULT ((0)) FOR [RaTuDo]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__CongTrongGi__3E3D3572]  DEFAULT ((0)) FOR [CongTrongGio]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__CongNgoaiGi__3F3159AB]  DEFAULT ((0)) FOR [CongNgoaiGio]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__TruCongTre__43F60EC8]  DEFAULT ((0)) FOR [TruCongTre]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__TruCongSom__44EA3301]  DEFAULT ((0)) FOR [TruCongSom]
GO

ALTER TABLE [dbo].[CIO] ADD  DEFAULT ((0)) FOR [ChamCongTay]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__DinhMucCong__45DE573A]  DEFAULT ((0)) FOR [DinhMucCong]
GO

ALTER TABLE [dbo].[CIO] ADD  CONSTRAINT [DF__CIO__TongCong__02E7657A]  DEFAULT ((0)) FOR [TongCong]
GO

ALTER TABLE [dbo].[CIO] ADD  DEFAULT ((0)) FOR [TinhCongThuCong]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Int, quy doi thanh so phut trong ngay. VD: 60 ~ 1g sang, 120 ~ 2g sang' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CIO', @level2type=N'COLUMN',@level2name=N'Vao'
GO

