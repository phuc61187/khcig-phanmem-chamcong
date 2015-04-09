CREATE TABLE [dbo].[XacNhanPhuCap5] (
  [LoaiPhuCap] int NULL,
  [HSPCNgay] int NULL,
  [HSPCTangCuongNgay] int NULL,
  [HSPCDem] int NULL,
  [HSPCTangCuongDem] int NULL,
  [UserEnrollNumber] int NULL,
  [Ngay] datetime NULL,
  [Duyet] bit CONSTRAINT [DF__XacNhanPh__Duyet__5649C92D] DEFAULT 1 NULL
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'0 : ngay thuong (su dung HSPCTangCuongNgay HSPCDem HSPCTangCuongDem)
1 : ngay nghi (su dung HSPCNgay HSPCDem)
2 : ngay le (su dung HSPCNgay HSPCDem)
3 : tuy chinh 1 (su dung HSPCNgay HSPCDem)
4 : tuy chinh 2 (su dung HSPCNgay TangCuongNgay HSPCDem TangCuongDem)', 'schema', 'dbo', 'table', 'XacNhanPhuCap5', 'column', 'LoaiPhuCap'
GO