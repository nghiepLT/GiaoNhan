USE [KpiManager]
GO
/****** Object:  Table [dbo].[bdCar]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdCar](
	[IDCar] [int] IDENTITY(1,1) NOT NULL,
	[CarSignal] [nvarchar](200) NULL,
	[IdTypeCar] [int] NULL,
	[IdTaiXe] [int] NULL,
	[ThoiGianDangKiem] [datetime] NULL,
 CONSTRAINT [PK_tbCar] PRIMARY KEY CLUSTERED 
(
	[IDCar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdDichVudangkiembaoduong]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdDichVudangkiembaoduong](
	[DichvudangkiembaoduongID] [int] NOT NULL,
	[NgayTao] [datetime] NULL,
	[IDCar] [int] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_bdDichVudangkiembaoduong] PRIMARY KEY CLUSTERED 
(
	[DichvudangkiembaoduongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdDotbaoduong]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdDotbaoduong](
	[IdDotBaoDuong] [int] IDENTITY(1,1) NOT NULL,
	[IDCar] [int] NULL,
	[NgayBD] [datetime] NULL,
	[NgayKT] [datetime] NULL,
	[SokmDau] [int] NULL,
	[SoKmCuoi] [int] NULL,
	[SoKmHientai] [int] NULL,
 CONSTRAINT [PK_bdDotbaoduong] PRIMARY KEY CLUSTERED 
(
	[IdDotBaoDuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdEmailSend]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdEmailSend](
	[IdEmail] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbEmailSend] PRIMARY KEY CLUSTERED 
(
	[IdEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdHanhtrinhbaotri]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdHanhtrinhbaotri](
	[IDHanhTrinhBaoTri] [int] IDENTITY(1,1) NOT NULL,
	[Sokm] [int] NULL,
	[NgayTao] [datetime] NULL,
	[IDDotbaoduong] [int] NULL,
 CONSTRAINT [PK_tbHanhtrinhbaotri] PRIMARY KEY CLUSTERED 
(
	[IDHanhTrinhBaoTri] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdSendMail]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdSendMail](
	[SendmailId] [int] NOT NULL,
	[NgayGui] [datetime] NULL,
	[IdCar] [int] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_bdSendMail] PRIMARY KEY CLUSTERED 
(
	[SendmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdService]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdService](
	[IdService] [int] IDENTITY(1,1) NOT NULL,
	[NameService] [nvarchar](500) NULL,
	[IdCar] [int] NULL,
	[Ngaytao] [datetime] NULL,
	[TongTien] [float] NULL,
	[Type] [int] NULL,
	[Ghichu] [nvarchar](500) NULL,
	[DichvudangkiembaoduongID] [int] NULL,
 CONSTRAINT [PK_bdService] PRIMARY KEY CLUSTERED 
(
	[IdService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdTaixe]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdTaixe](
	[IdTaixe] [int] IDENTITY(1,1) NOT NULL,
	[TenTaiXe] [nvarchar](200) NULL,
	[MaTheTai] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbTaixe] PRIMARY KEY CLUSTERED 
(
	[IdTaixe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bdTypeCar]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bdTypeCar](
	[TypeCar] [int] IDENTITY(1,1) NOT NULL,
	[NameType] [nvarchar](200) NULL,
	[DinhMucBaoDuong] [int] NULL,
	[Ghichubaoduong] [int] NULL,
 CONSTRAINT [PK_bdTypeCar] PRIMARY KEY CLUSTERED 
(
	[TypeCar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbConfig]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbConfig](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[TG40] [int] NULL,
	[TG80] [int] NULL,
	[TimeTracking] [float] NULL,
	[TimeDelivery] [int] NULL,
	[TimeAccountant] [int] NULL,
	[ApiUrl] [nvarchar](500) NULL,
 CONSTRAINT [PK_tbConfig] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbDepartment]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDepartment](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbDepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbDriver]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDriver](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbDriver] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbEmployee]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbEmployee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](200) NULL,
	[PermissionID] [int] NULL,
 CONSTRAINT [PK_tbEmployee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbMenu]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbMenu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](200) NULL,
	[MenuURL] [nvarchar](200) NULL,
	[Controller] [nvarchar](200) NULL,
	[Action] [nvarchar](200) NULL,
	[Icon] [nvarchar](200) NULL,
	[STT] [int] NULL,
 CONSTRAINT [PK_tbMenu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbNCC]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbNCC](
	[NCCID] [int] IDENTITY(1,1) NOT NULL,
	[NCCName] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbNCC] PRIMARY KEY CLUSTERED 
(
	[NCCID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbPermission]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPermission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbPermission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbPermission_Menu]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPermission_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
	[STT] [int] NOT NULL,
 CONSTRAINT [PK_tbPermission_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbProducts]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbProducts](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbProducts] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbReceived]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbReceived](
	[ReceivedID] [int] IDENTITY(1,1) NOT NULL,
	[STT] [int] NULL,
	[UserID] [int] NULL,
	[NCCID] [int] NULL,
	[DateStart] [datetime] NOT NULL,
	[DateEnd] [datetime] NULL,
	[Type] [int] NULL,
	[Kpi] [int] NULL,
	[SLNhap] [int] NULL,
	[SlKiemTra] [int] NULL,
	[Products] [nvarchar](500) NULL,
	[ParentId] [int] NULL,
	[IsSent] [int] NULL,
	[Time] [int] NULL,
	[TypeTracking] [int] NULL,
	[Code] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbReceived] PRIMARY KEY CLUSTERED 
(
	[ReceivedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbReceivedDetail]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbReceivedDetail](
	[ReceivedDetailID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[ReceivedID] [int] NULL,
	[Count] [int] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_tbReceivedDetail] PRIMARY KEY CLUSTERED 
(
	[ReceivedDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbReceivedMessage]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbReceivedMessage](
	[ReceivedMessageID] [int] IDENTITY(1,1) NOT NULL,
	[IdReceived] [int] NULL,
	[UserReceived] [nvarchar](200) NULL,
	[Time] [int] NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_ReceivedMessage] PRIMARY KEY CLUSTERED 
(
	[ReceivedMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbRole]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbRoleMenu]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbRoleMenu](
	[RoleMenuID] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[MenuId] [int] NULL,
 CONSTRAINT [PK_tbRoleMenu] PRIMARY KEY CLUSTERED 
(
	[RoleMenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbSapXepConfig]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSapXepConfig](
	[SapXepConfigID] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](500) NULL,
	[NgayCapNhat] [datetime] NULL,
 CONSTRAINT [PK_tbSapXepConfig] PRIMARY KEY CLUSTERED 
(
	[SapXepConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbSapXepDetail]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSapXepDetail](
	[SapXepDetail] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](500) NULL,
	[NgayCapNhat] [datetime] NULL,
 CONSTRAINT [PK_tbSapXepDetail] PRIMARY KEY CLUSTERED 
(
	[SapXepDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbTheTai]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbTheTai](
	[ThetaiID] [int] IDENTITY(1,1) NOT NULL,
	[MaThe] [nvarchar](50) NULL,
	[MaPhieu] [nvarchar](500) NULL,
	[DateCreate] [datetime] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[Count] [int] NULL,
	[UserId] [int] NULL,
	[KPI] [int] NULL,
	[Luotdi] [datetime] NULL,
	[Luotve] [datetime] NULL,
 CONSTRAINT [PK_tbTheTai] PRIMARY KEY CLUSTERED 
(
	[ThetaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbTheTaiChiTiet]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbTheTaiChiTiet](
	[TheTaiChiTietID] [int] IDENTITY(1,1) NOT NULL,
	[ThetaiID] [int] NULL,
	[MaPhieu] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[MaThe] [nvarchar](50) NULL,
	[DateEnd] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
	[DateCreate] [datetime] NULL,
	[GhiChuKetThuc] [nvarchar](500) NULL,
	[TienPhatSinh] [int] NULL,
	[SoKMPhatSinh] [int] NULL,
 CONSTRAINT [PK_tbTheTaiChiTiet] PRIMARY KEY CLUSTERED 
(
	[TheTaiChiTietID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbTracking]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbTracking](
	[TrackingID] [int] IDENTITY(1,1) NOT NULL,
	[TrackingCode] [nvarchar](200) NULL,
	[TrackingDate] [datetime] NOT NULL,
	[UserID] [int] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[DateCreate] [datetime] NULL,
	[Count] [int] NOT NULL,
	[KPI] [int] NULL,
	[CountStep] [int] NOT NULL,
 CONSTRAINT [PK_tbTracking] PRIMARY KEY CLUSTERED 
(
	[TrackingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbUSer]    Script Date: 14/03/2023 2:31:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUSer](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NULL,
	[UserPassword] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[PermissionID] [int] NOT NULL,
	[EmployeeName] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[Code] [nvarchar](200) NULL,
	[DepartmentID] [int] NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_tbUSer] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bdCar] ON 

INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (3, N'51A-744.93', 2, 3, CAST(N'2023-03-17 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (4, N'51A-749.04', 2, 4, CAST(N'2023-03-09 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (5, N'51B-514.80', 3, 5, CAST(N'2023-03-01 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (6, N'51B-514.75', 3, 2, CAST(N'2023-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (1004, N'51B-51475', 1003, 16, CAST(N'2023-12-28 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (1005, N'51B-51480', 1003, 19, CAST(N'2024-01-16 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (1006, N'51A-74493', 1004, 18, CAST(N'2023-05-24 00:00:00.000' AS DateTime))
INSERT [dbo].[bdCar] ([IDCar], [CarSignal], [IdTypeCar], [IdTaiXe], [ThoiGianDangKiem]) VALUES (1007, N'51A-74904', 1004, 17, CAST(N'2024-01-28 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[bdCar] OFF
SET IDENTITY_INSERT [dbo].[bdDotbaoduong] ON 

INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3002, 3, CAST(N'2023-02-01 00:00:00.000' AS DateTime), NULL, 250000, NULL, 4000)
INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3003, 5, CAST(N'2023-01-30 00:00:00.000' AS DateTime), NULL, 450000, NULL, 19600)
INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3004, 1004, CAST(N'2023-02-13 00:00:00.000' AS DateTime), NULL, 0, NULL, 216)
INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3005, 1005, CAST(N'2023-02-13 00:00:00.000' AS DateTime), NULL, 0, NULL, 285)
INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3006, 1006, CAST(N'2023-01-07 00:00:00.000' AS DateTime), NULL, 216000, NULL, 1036)
INSERT [dbo].[bdDotbaoduong] ([IdDotBaoDuong], [IDCar], [NgayBD], [NgayKT], [SokmDau], [SoKmCuoi], [SoKmHientai]) VALUES (3007, 1007, CAST(N'2022-10-29 00:00:00.000' AS DateTime), NULL, 280000, NULL, 4531)
SET IDENTITY_INSERT [dbo].[bdDotbaoduong] OFF
SET IDENTITY_INSERT [dbo].[bdHanhtrinhbaotri] ON 

INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3004, 500, CAST(N'2023-02-10 15:09:30.587' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3005, 700, CAST(N'2023-02-10 15:39:30.747' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3006, 700, CAST(N'2023-02-10 15:55:57.827' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3007, 3000, CAST(N'2023-02-10 16:05:20.750' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3011, 10000, CAST(N'2023-02-10 16:54:52.960' AS DateTime), 3003)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3012, 19600, CAST(N'2023-02-10 16:56:42.090' AS DateTime), 3003)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3014, 3500, CAST(N'2023-02-10 17:28:37.023' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3015, 4000, CAST(N'2023-02-13 10:03:08.443' AS DateTime), 3002)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3016, 216, CAST(N'2023-02-13 10:20:14.307' AS DateTime), 3004)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3017, 285, CAST(N'2023-02-13 10:20:42.260' AS DateTime), 3005)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3018, 1036, CAST(N'2023-02-13 10:22:24.030' AS DateTime), 3006)
INSERT [dbo].[bdHanhtrinhbaotri] ([IDHanhTrinhBaoTri], [Sokm], [NgayTao], [IDDotbaoduong]) VALUES (3019, 4531, CAST(N'2023-02-13 10:22:59.993' AS DateTime), 3007)
SET IDENTITY_INSERT [dbo].[bdHanhtrinhbaotri] OFF
INSERT [dbo].[bdSendMail] ([SendmailId], [NgayGui], [IdCar], [Type]) VALUES (0, CAST(N'2023-02-10 10:38:10.707' AS DateTime), 3, 2)
SET IDENTITY_INSERT [dbo].[bdService] ON 

INSERT [dbo].[bdService] ([IdService], [NameService], [IdCar], [Ngaytao], [TongTien], [Type], [Ghichu], [DichvudangkiembaoduongID]) VALUES (1, N'thay đồ cho xe', 5, CAST(N'2023-02-07 09:08:42.657' AS DateTime), 200000, 1, N'Thay kiếng cho xe', NULL)
INSERT [dbo].[bdService] ([IdService], [NameService], [IdCar], [Ngaytao], [TongTien], [Type], [Ghichu], [DichvudangkiembaoduongID]) VALUES (2, N'Thay bánh', 3, CAST(N'2023-02-08 00:00:00.000' AS DateTime), 500000, 1, N'', NULL)
INSERT [dbo].[bdService] ([IdService], [NameService], [IdCar], [Ngaytao], [TongTien], [Type], [Ghichu], [DichvudangkiembaoduongID]) VALUES (1002, N'Rửa xe', 5, CAST(N'2023-02-09 00:00:00.000' AS DateTime), 120000, 1, N'Rửa xe định kỳ', NULL)
INSERT [dbo].[bdService] ([IdService], [NameService], [IdCar], [Ngaytao], [TongTien], [Type], [Ghichu], [DichvudangkiembaoduongID]) VALUES (1003, N'- Thay nhớt và kiểm tra lọc nhớt
- Rửa xe', 3, CAST(N'2023-02-09 00:00:00.000' AS DateTime), 500000, 2, N'', NULL)
SET IDENTITY_INSERT [dbo].[bdService] OFF
SET IDENTITY_INSERT [dbo].[bdTaixe] ON 

INSERT [dbo].[bdTaixe] ([IdTaixe], [TenTaiXe], [MaTheTai]) VALUES (16, N'TRẦN VŨ ANH KHOA', N'GN04')
INSERT [dbo].[bdTaixe] ([IdTaixe], [TenTaiXe], [MaTheTai]) VALUES (17, N'TRẦN NGỌC QUỐC KHÁNH', N'GN15')
INSERT [dbo].[bdTaixe] ([IdTaixe], [TenTaiXe], [MaTheTai]) VALUES (18, N'NGUYỄN CÔNG THÀNH', N'GN13')
INSERT [dbo].[bdTaixe] ([IdTaixe], [TenTaiXe], [MaTheTai]) VALUES (19, N'NGUYỄN VĂN BẢO', N'GN17')
SET IDENTITY_INSERT [dbo].[bdTaixe] OFF
SET IDENTITY_INSERT [dbo].[bdTypeCar] ON 

INSERT [dbo].[bdTypeCar] ([TypeCar], [NameType], [DinhMucBaoDuong], [Ghichubaoduong]) VALUES (1003, N'16 CHỔ', 20000, 400)
INSERT [dbo].[bdTypeCar] ([TypeCar], [NameType], [DinhMucBaoDuong], [Ghichubaoduong]) VALUES (1004, N'7 CHỔ', 5000, 200)
SET IDENTITY_INSERT [dbo].[bdTypeCar] OFF
SET IDENTITY_INSERT [dbo].[tbConfig] ON 

INSERT [dbo].[tbConfig] ([ConfigID], [TG40], [TG80], [TimeTracking], [TimeDelivery], [TimeAccountant], [ApiUrl]) VALUES (2, 20, 40, 40, 40, 180, N'http://banhangnk-2021.nguyenkimvn.com.vn/')
SET IDENTITY_INSERT [dbo].[tbConfig] OFF
SET IDENTITY_INSERT [dbo].[tbDepartment] ON 

INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (1, N'Nhập  kho')
INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (2, N'Xuất kho')
INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (3, N'Giao nhận')
INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (4, N'Trung chuyển')
INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (5, N'Xếp hàng')
INSERT [dbo].[tbDepartment] ([DepartmentID], [DepartmentName]) VALUES (6, N'Kế toán')
SET IDENTITY_INSERT [dbo].[tbDepartment] OFF
SET IDENTITY_INSERT [dbo].[tbDriver] ON 

INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (1, N'GN02')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (2, N'GN03')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (3, N'GN05')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (4, N'GN06')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (5, N'GN07')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (6, N'GN08')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (7, N'GN09')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (8, N'GN12')
INSERT [dbo].[tbDriver] ([DriverID], [Code]) VALUES (9, N'GN14')
SET IDENTITY_INSERT [dbo].[tbDriver] OFF
SET IDENTITY_INSERT [dbo].[tbEmployee] ON 

INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (3, N'Soái', 2)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (4, N'Phước', 2)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (5, N'Phong', 5)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (6, N'Phạm Thanh Hải', 3)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (7, N'Nguyễn Thu Hồng', 3)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (8, N'Tài Văn A', 4)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (10, N'Khôi', 5)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (11, N'Trí', 6)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (12, N'Tuân', 6)
INSERT [dbo].[tbEmployee] ([EmployeeID], [EmployeeName], [PermissionID]) VALUES (13, N'Minh khôi', 4)
SET IDENTITY_INSERT [dbo].[tbEmployee] OFF
SET IDENTITY_INSERT [dbo].[tbMenu] ON 

INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1, N'Import Excel', N'import-excel', N'Import', N'Index', N'fas fa-file-excel', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (4, N'Xuất kho', N'xuat-kho', N'Tracking', N'Index', N'fas fa-table ', 2)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (5, N'Nhập kho', N'nhap-hang', N'Received', N'Index', N'fas fa-th-list', 2)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1006, N'Thống kê', N'thong-ke', N'Report', N'ReportByDay', N'fas fa-chart-bar', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1008, N'Quản lý user', N'quan-ly-user', N'Users', N'GetListUser', N'fas fa-user-alt', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1009, N'Thiết lập', N'thiet-lap', N'Config', N'Index', N'fas fa-cog', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1010, N'Giao nhận', N'giao-nhan', N'TheTai', N'QuetTheTai', N'fas fa-th-list', 2)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1012, N'Kết thúc giao nhận', N'ket-thuc-giao-nhan', N'TheTai', N'KetThucGiaoNhan', N'fas fa-th-list', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1013, N'Thống kê giao nhận', N'thong-ke-giao-nhan', N'Report', N'ThongKeThongKeGiaoNhan', N'fas fa-th-list', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1014, N'Phân quyền', N'phan-quyen', N'Role', N'RoleList', N'fas fa-th-list', 1)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1015, N'Trung chuyển', N'trung-chuyen', N'Received', N'TrungChuyen', N'fas fa-th-list', 2)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (1016, N'Xếp hàng', N'xep-hang', N'Received', N'XepHang', N'fas fa-th-list', 2)
INSERT [dbo].[tbMenu] ([MenuId], [MenuName], [MenuURL], [Controller], [Action], [Icon], [STT]) VALUES (2015, N'Thống kê phiếu', N'thong-ke-phieu', N'Report', N'Thongkephieu', N'fas fa-th-list', 2)
SET IDENTITY_INSERT [dbo].[tbMenu] OFF
SET IDENTITY_INSERT [dbo].[tbNCC] ON 

INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (1, N'FPT
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (2, N'DGW
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (3, N'PSD
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (4, N'ELITE
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (5, N'ADG
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (6, N'VIEN
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (7, N'QUOC
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (8, N'KHAC
')
INSERT [dbo].[tbNCC] ([NCCID], [NCCName]) VALUES (9, N'NCCLK
')
SET IDENTITY_INSERT [dbo].[tbNCC] OFF
SET IDENTITY_INSERT [dbo].[tbPermission] ON 

INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (1, N'Admin')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (2, N'Nhập kho')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (3, N'Xuất kho')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (4, N'Giao nhận')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (5, N'Trung chuyển')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (6, N'Xếp hàng')
INSERT [dbo].[tbPermission] ([PermissionID], [PermissionName]) VALUES (1005, N'Kế toán')
SET IDENTITY_INSERT [dbo].[tbPermission] OFF
SET IDENTITY_INSERT [dbo].[tbPermission_Menu] ON 

INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2, 1, 1006, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (3, 1, 3, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (6, 2, 5, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (7, 3, 4, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (8, 5, 5, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (9, 6, 5, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (1008, 1, 2, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (1009, 4, 1010, 2)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (1010, 2, 1006, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (1011, 3, 1006, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (1012, 5, 1006, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2009, 1, 1, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2011, 1, 1008, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2012, 1, 1009, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2014, 4, 1011, 2)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2017, 4, 1012, 1)
INSERT [dbo].[tbPermission_Menu] ([ID], [PermissionID], [MenuID], [STT]) VALUES (2018, 4, 1013, 4)
SET IDENTITY_INSERT [dbo].[tbPermission_Menu] OFF
SET IDENTITY_INSERT [dbo].[tbProducts] ON 

INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (1, N'Laptop')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (2, N'PC')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (3, N'LCD')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (4, N'SEWKS')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (5, N'MAYIN')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (6, N'MUC')
INSERT [dbo].[tbProducts] ([ProductID], [ProductName]) VALUES (7, N'LKIEN')
SET IDENTITY_INSERT [dbo].[tbProducts] OFF
SET IDENTITY_INSERT [dbo].[tbReceived] ON 

INSERT [dbo].[tbReceived] ([ReceivedID], [STT], [UserID], [NCCID], [DateStart], [DateEnd], [Type], [Kpi], [SLNhap], [SlKiemTra], [Products], [ParentId], [IsSent], [Time], [TypeTracking], [Code]) VALUES (66, NULL, 1011, NULL, CAST(N'2023-02-18 15:12:19.983' AS DateTime), NULL, 2, NULL, 1, NULL, N'', NULL, NULL, NULL, 1, N'X230218109-L')
SET IDENTITY_INSERT [dbo].[tbReceived] OFF
SET IDENTITY_INSERT [dbo].[tbRole] ON 

INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (2, N'Xuất kho')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (3, N'Nhập kho')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (4, N'Nhập-xuất kho')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (5, N'Giao nhận')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (6, N'admin')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (7, N'Trung chuyển')
INSERT [dbo].[tbRole] ([RoleId], [RoleName]) VALUES (8, N'Xếp hàng')
SET IDENTITY_INSERT [dbo].[tbRole] OFF
SET IDENTITY_INSERT [dbo].[tbRoleMenu] ON 

INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (19, 2, 4)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (20, 2, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (21, 3, 5)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (22, 3, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (23, 4, 4)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (24, 4, 5)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (25, 4, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (26, 5, 1010)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (27, 5, 1012)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (28, 5, 1013)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (29, 6, 1)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (32, 6, 1009)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (34, 6, 1014)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (46, 6, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (47, 6, 1013)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (49, 6, 1008)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (62, 7, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (63, 7, 1015)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (64, 8, 1006)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (65, 8, 1016)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (1002, 6, 2015)
INSERT [dbo].[tbRoleMenu] ([RoleMenuID], [RoleId], [MenuId]) VALUES (1003, 5, 2015)
SET IDENTITY_INSERT [dbo].[tbRoleMenu] OFF
SET IDENTITY_INSERT [dbo].[tbSapXepConfig] ON 

INSERT [dbo].[tbSapXepConfig] ([SapXepConfigID], [Position], [NgayCapNhat]) VALUES (33, N'1028,1018,1035', CAST(N'2023-03-13 17:35:45.250' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbSapXepConfig] OFF
SET IDENTITY_INSERT [dbo].[tbSapXepDetail] ON 

INSERT [dbo].[tbSapXepDetail] ([SapXepDetail], [Position], [NgayCapNhat]) VALUES (13, N'1028_0,1018_0,1035_0', CAST(N'2023-03-13 17:35:45.250' AS DateTime))
INSERT [dbo].[tbSapXepDetail] ([SapXepDetail], [Position], [NgayCapNhat]) VALUES (14, N'1028_0,1018_0,1035_0', CAST(N'2023-03-14 09:12:29.843' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbSapXepDetail] OFF
SET IDENTITY_INSERT [dbo].[tbTheTai] ON 

INSERT [dbo].[tbTheTai] ([ThetaiID], [MaThe], [MaPhieu], [DateCreate], [DateStart], [DateEnd], [Count], [UserId], [KPI], [Luotdi], [Luotve]) VALUES (95, N'GN03', N'X230314018-L|1,X230314008-L|20,', CAST(N'2023-03-14 14:06:52.400' AS DateTime), CAST(N'2023-03-14 14:06:52.400' AS DateTime), CAST(N'2023-03-14 14:09:16.640' AS DateTime), 21, 2029, -696, NULL, CAST(N'2023-03-14 14:10:21.080' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbTheTai] OFF
SET IDENTITY_INSERT [dbo].[tbTheTaiChiTiet] ON 

INSERT [dbo].[tbTheTaiChiTiet] ([TheTaiChiTietID], [ThetaiID], [MaPhieu], [Status], [MaThe], [DateEnd], [Description], [DateCreate], [GhiChuKetThuc], [TienPhatSinh], [SoKMPhatSinh]) VALUES (93, 95, N'X230314018-L', 1, N'GN03', CAST(N'2023-03-14 14:10:12.200' AS DateTime), N'Test ghi chú', CAST(N'2023-03-14 14:06:58.617' AS DateTime), NULL, 0, 200)
INSERT [dbo].[tbTheTaiChiTiet] ([TheTaiChiTietID], [ThetaiID], [MaPhieu], [Status], [MaThe], [DateEnd], [Description], [DateCreate], [GhiChuKetThuc], [TienPhatSinh], [SoKMPhatSinh]) VALUES (94, 95, N'X230314008-L', 1, N'GN03', CAST(N'2023-03-14 14:09:49.963' AS DateTime), N'', CAST(N'2023-03-14 14:07:06.677' AS DateTime), NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[tbTheTaiChiTiet] OFF
SET IDENTITY_INSERT [dbo].[tbUSer] ON 

INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (3, N'hongnt', N'E10ADC3949BA59ABBE56E057F20F883E', N'Nguyễn thu hồng', 3, N'NGUYỄN THU HỒNG', 1, N'', NULL, 4)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (4, N'khoinm', N'E10ADC3949BA59ABBE56E057F20F883E', N'Nguyễn minh khôi', 3, N'NGUYỄN MINH KHÔI', 1, N'', NULL, 2)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (5, N'ThanhHai', N'E10ADC3949BA59ABBE56E057F20F883E', N'Phan thanh hải', 3, N'Thanh Hải', 1, NULL, NULL, 2)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (6, N'admin', N'E10ADC3949BA59ABBE56E057F20F883E', N'admin', 1, N'Admin', 1, N'', NULL, 6)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (8, N'phuocph', N'E10ADC3949BA59ABBE56E057F20F883E', N'Nhập kho Phước', 2, N'PHẠM HỮU PHƯỚC', 1, N'', NULL, 3)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (10, N'PHONG', N'E10ADC3949BA59ABBE56E057F20F883E', N'Trung chuyển Phong', 5, N'Phong', 1, N'', NULL, 7)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (12, N'KHOI', N'E10ADC3949BA59ABBE56E057F20F883E', N'Trung chuyển Khôi', 5, N'Khôi', 1, N'', NULL, 7)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1011, N'tritb', N'E10ADC3949BA59ABBE56E057F20F883E', N'Trí Xếp hàng', 6, N'TRẦN BỈNH TRÍ', 1, N'', NULL, 8)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1012, N'tuantt', N'E10ADC3949BA59ABBE56E057F20F883E', N'TUẤN Xếp hàng', 6, N'TRẦN THANH TUẤN', 1, N'', NULL, 8)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1014, N'ltnghiep', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 3, N'Thành Nghiệp', 0, NULL, NULL, 2)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1018, N'GN01', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'Nguyen van A', 1, N'GN01', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1019, N'TheTai', N'8E9CC9FC7D9C8A94A0B6BB77AE0B626E', NULL, 4, N'Giao nhận tài khoản', 1, N'', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1020, N'trungtt', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'TRIỆU TRÍ TRUNG', 1, N'GN02', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1021, N'TTLan', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 1005, N'Trần Thi Lan', 0, N'KTCT07', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1024, N'Khoa', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 2, N'sadsd', 1, N'', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1025, N'KTCT05', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 1005, N'KTCT05', 1, N'KTCT05', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1026, N'KTCT10', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 1005, N'KTCT10', 1, N'KTCT10', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1027, N'Test', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 2, N'Test', 1, N'', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1028, N'sangtk', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'TẤT KIM SANG', 1, N'GN03', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1029, N'haipt', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 3, N'PHAN THANH HẢI', 1, N'', NULL, 2)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1030, N'phonghh', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 5, N'HUỲNH HOÀNG PHONG', 1, N'', NULL, 7)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1031, N'soaihk', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 5, N'HỨA KỲ SOÁI', 1, N'', NULL, 2)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1032, N'phucnh', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 6, N'NGUYỄN HỮU PHÚC', 1, N'', NULL, 8)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1033, N'taidd', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 6, N'ĐOÀN ĐỨC TÀI', 1, N'', NULL, 8)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1035, N'trinh', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'NGUYỄN HỮU TRÍ', 1, N'GN05', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1036, N'tuanlq', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'LƯU QUANG TUẤN', 1, N'GN08', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1037, N'hoangtt', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'TÔ THANH HOÀNG', 1, N'GN09', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1038, N'phuocvlb', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'VÕ LÊ BÁ PHƯỚC', 1, N'GN06', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1039, N'khoitnn', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'TRANG NGUYỄN NGỌC KHÔI', 1, N'GN07', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1040, N'tienvm', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'VŨ MINH TIẾN', 1, N'GN12', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1041, N'khoatva', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'TRẦN VŨ ANH KHOA', 1, N'GN04', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1042, N'thanhnc', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'NGUYỄN CÔNG THÀNH', 1, N'GN13', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1043, N'khanhtnq', N'A2C332470622A6186B243FD195F3865D', NULL, 4, N'TRẦN NGỌC QUỐC KHÁNH', 1, N'GN15', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1044, N'khoaddb', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'ĐINH ĐỒNG BẢO KHOA', 1, N'GN18', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (1045, N'thanhtt', N'2B0C83E9104CCFD01299CDAD386E241E', NULL, 4, N'TRẦN TRÍ THÀNH', 1, N'GN14', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (2029, N'minhtn', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'Từ Ngọc Minh', 1, N'', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (2030, N'baonv', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'NGUYỄN VĂN BẢO', 1, N'GN17', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (2031, N'binhpc', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'PHAN CÔNG BÌNH', 1, N'GN16', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (2032, N'testgn', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'Giao nhận test', 1, N'GN999', NULL, NULL)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (2033, N'hungpm', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 4, N'PHẠM MINH HÙNG', 1, N'GN10', NULL, 5)
INSERT [dbo].[tbUSer] ([UserID], [UserName], [UserPassword], [Description], [PermissionID], [EmployeeName], [Status], [Code], [DepartmentID], [RoleId]) VALUES (3032, N'trungnt', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, 2, N'NGUYỄN THÀNH TRUNG', 1, N'', NULL, 3)
SET IDENTITY_INSERT [dbo].[tbUSer] OFF
