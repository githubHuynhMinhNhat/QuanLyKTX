USE [master]
GO
/****** Object:  Database [QUANLIKTX]    Script Date: 11/12/2022 8:29:00 CH ******/
CREATE DATABASE [QUANLIKTX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QUANLIKTX', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QUANLIKTX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QUANLIKTX_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QUANLIKTX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QUANLIKTX] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QUANLIKTX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QUANLIKTX] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QUANLIKTX] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QUANLIKTX] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QUANLIKTX] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QUANLIKTX] SET ARITHABORT OFF 
GO
ALTER DATABASE [QUANLIKTX] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QUANLIKTX] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QUANLIKTX] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QUANLIKTX] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QUANLIKTX] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QUANLIKTX] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QUANLIKTX] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QUANLIKTX] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QUANLIKTX] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QUANLIKTX] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QUANLIKTX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QUANLIKTX] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QUANLIKTX] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QUANLIKTX] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QUANLIKTX] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QUANLIKTX] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QUANLIKTX] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QUANLIKTX] SET RECOVERY FULL 
GO
ALTER DATABASE [QUANLIKTX] SET  MULTI_USER 
GO
ALTER DATABASE [QUANLIKTX] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QUANLIKTX] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QUANLIKTX] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QUANLIKTX] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QUANLIKTX] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QUANLIKTX] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QUANLIKTX', N'ON'
GO
ALTER DATABASE [QUANLIKTX] SET QUERY_STORE = OFF
GO
USE [QUANLIKTX]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [char](10) NOT NULL,
	[MaNV] [char](10) NULL,
	[MaSV] [char](10) NULL,
	[Maphong] [char](10) NULL,
	[NgayLap] [date] NULL,
	[SoDien] [int] NOT NULL,
	[SoNuoc] [int] NOT NULL,
	[GiaDien] [int] NOT NULL,
	[GiaNuoc] [int] NOT NULL,
	[TrangThai] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HopDong]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDong](
	[MaHopDong] [char](10) NOT NULL,
	[MaNV] [char](10) NULL,
	[MaSV] [char](10) NULL,
	[Maphong] [char](10) NULL,
	[NgayLap] [date] NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
	[TrangThai] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHopDong] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nha]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nha](
	[MaNha] [char](10) NOT NULL,
	[TenNha] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNha] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [char](10) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[SoDT] [char](20) NULL,
	[Email] [char](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[MaPhong] [char](10) NOT NULL,
	[Manha] [char](10) NULL,
	[Toida] [int] NULL,
	[GiaPhong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSV] [char](10) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[MaLop] [char](10) NULL,
	[SoDT] [char](20) NULL,
	[Email] [char](50) NULL,
	[MaPhong] [char](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 11/12/2022 8:29:00 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[Taikhoan] [char](20) NOT NULL,
	[MatKhau] [char](20) NULL,
	[MaNV] [char](10) NULL,
	[Quyen] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Taikhoan] ASC
)WITH (PAD_INDEX = OFF,
STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon01  ', N'QL01      ', N'DTH205902 ', N'A101      ', CAST(N'2022-10-25' AS Date), 100, 100, 1660, 8000, N'Chưa Thanh Toán')
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon02  ', N'QL01      ', N'DTH205902 ', N'A101      ', CAST(N'2022-11-25' AS Date), 100, 100, 1660, 8000, N'Đã Thanh Toán')
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon03  ', N'QL01      ', N'DTH205927 ', N'A101      ', CAST(N'2022-11-25' AS Date), 100, 100, 1660, 6000, N'Đã Thanh Toán')
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon04  ', N'QL01      ', N'DTH205927 ', N'A101      ', CAST(N'2022-09-25' AS Date), 100, 100, 1660, 8000, N'Đã Thanh Toán')
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon05  ', N'QL01      ', N'DTH205927 ', N'A101      ', CAST(N'2022-09-25' AS Date), 100, 100, 1660, 8000, N'Chưa Thanh Toán')
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaNV], [MaSV], [Maphong], [NgayLap], [SoDien], [SoNuoc], [GiaDien], [GiaNuoc], [TrangThai]) VALUES (N'HoaDon06  ', N'QL01      ', N'DTH205927 ', N'A101      ', CAST(N'2022-09-25' AS Date), 100, 100, 1660, 8000, N'Đã Thanh Toán')
GO
INSERT [dbo].[HopDong] ([MaHopDong], [MaNV], [MaSV], [Maphong], [NgayLap], [NgayBatDau], [NgayKetThuc], [TrangThai]) VALUES (N'HOPDONG01 ', N'QL01      ', N'DTH205927 ', N'A101      ', CAST(N'2022-11-25' AS Date), CAST(N'2022-12-01' AS Date), CAST(N'2023-01-01' AS Date), N'Còn Thời Hạn')
INSERT [dbo].[HopDong] ([MaHopDong], [MaNV], [MaSV], [Maphong], [NgayLap], [NgayBatDau], [NgayKetThuc], [TrangThai]) VALUES (N'HOPDONG02 ', N'QL01      ', N'DTH205902 ', N'B101      ', CAST(N'2022-11-25' AS Date), CAST(N'2022-12-01' AS Date), CAST(N'2023-01-01' AS Date), N'Còn Thời Hạn')
INSERT [dbo].[HopDong] ([MaHopDong], [MaNV], [MaSV], [Maphong], [NgayLap], [NgayBatDau], [NgayKetThuc], [TrangThai]) VALUES (N'HOPDONG03 ', N'QTV01     ', N'DTH205902 ', N'C101      ', CAST(N'2022-12-09' AS Date), CAST(N'2022-12-01' AS Date), CAST(N'2023-01-01' AS Date), N'Hết Thời Hạn')
GO
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NA        ', N'Nhà A ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NB        ', N'Nhà B ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NC        ', N'Nhà C ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'ND        ', N'Nhà D ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NE        ', N'Nhà E ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NI        ', N'Nhà I ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NG        ', N'Nhà G ')
INSERT [dbo].[Nha] ([MaNha], [TenNha]) VALUES (N'NH        ', N'Nhà H ')
GO
INSERT [dbo].[NhanVien] ([MaNV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [ChucVu], [SoDT], [Email]) VALUES (N'QL01      ', N'Nguyễn Thị Hương', N'Nữ', CAST(N'1999-09-22' AS Date), N'Long Xuyên, An Giang', N'Quản Lí Khu Nhà', N'08642343463         ', N'lthuong@agu.edu.vn                                ')
INSERT [dbo].[NhanVien] ([MaNV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [ChucVu], [SoDT], [Email]) VALUES (N'QL02      ', N'Nguyễn Thị Như Ngọc', N'Nữ', CAST(N'1973-03-12' AS Date), N'Long Xuyên, An Giang', N'Quản Lí Khu Nhà', N'08642312866         ', N'ntnngoc@agu.edu.vn                                ')
INSERT [dbo].[NhanVien] ([MaNV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [ChucVu], [SoDT], [Email]) VALUES (N'QTV01     ', N'Võ Quốc Vương', N'Nam', CAST(N'1989-01-01' AS Date), N'Long Xuyên, An Giang', N'Giám Đốc', N'08642342323         ', N'vvuong@agu.edu.vn                                 ')
INSERT [dbo].[NhanVien] ([MaNV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [ChucVu], [SoDT], [Email]) VALUES (N'QTV02     ', N'Võ Quốc Vương', N'Nam', CAST(N'1989-01-01' AS Date), N'Long Xuyên, An Giang', N'Giám Đốc', N'0864234232          ', N'vvuong@agu.edu.vn                                 ')
GO
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'A101      ', N'NA        ', 8, 200000)
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'A201      ', N'NA        ', 4, 250000)
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'B101      ', N'NB        ', 8, 200000)
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'C101      ', N'NC        ', 8, 200000)
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'E101      ', N'NE        ', 8, 200000)
INSERT [dbo].[Phong] ([MaPhong], [Manha], [Toida], [GiaPhong]) VALUES (N'E102      ', N'NE        ', 8, 200000)
GO
INSERT [dbo].[SinhVien] ([MaSV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [MaLop], [SoDT], [Email], [MaPhong]) VALUES (N'DNH200000 ', N'Nguyễn Thị A', N'Nữ', CAST(N'2002-09-11' AS Date), N'Kiên Giang', N'DH20NH1   ', N'0392249911          ', N'ntha_20nh@student.agu.edu.vn                      ', N'A201      ')
INSERT [dbo].[SinhVien] ([MaSV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [MaLop], [SoDT], [Email], [MaPhong]) VALUES (N'DTH111111 ', N'Huỳnh Quốc Huy', N'Nam', CAST(N'2003-02-11' AS Date), N'Kiên Giang', N'DH22NV    ', N'039234567           ', N'hqhuy_22th@student.agu.edu.vn                     ', N'C101      ')
INSERT [dbo].[SinhVien] ([MaSV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [MaLop], [SoDT], [Email], [MaPhong]) VALUES (N'DTH205902 ', N'HUỲNH MINH NHẬT', N'Nam', CAST(N'2002-01-01' AS Date), N'AN GIANG', N'DH21TH2   ', N'01234567891         ', N'nhnhat_21th@student.agu.edu.vn                    ', N'A201      ')
INSERT [dbo].[SinhVien] ([MaSV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [MaLop], [SoDT], [Email], [MaPhong]) VALUES (N'DTH205927 ', N'LÂM HUỲNH PHÚ', N'Nam', CAST(N'2002-09-20' AS Date), N'LẤP VÒ, ĐÔNG THÁP', N'DH21TH2   ', N'0392249910          ', N'lhphu_21th@student.agu.edu.vn                     ', N'A101      ')
INSERT [dbo].[SinhVien] ([MaSV], [HoTen], [GioiTinh], [NgaySinh], [DiaChi], [MaLop], [SoDT], [Email], [MaPhong]) VALUES (N'DTH205928 ', N'LÂM HUỲNH PHÚ', N'Nam', CAST(N'2002-09-20' AS Date), N'LẤP VÒ, ĐÔNG THÁP', N'DH21TH2   ', N'0392249910          ', N'lhphu_21th@student.agu.edu.vn                     ', N'A201      ')
GO
INSERT [dbo].[TaiKhoan] ([Taikhoan], [MatKhau], [MaNV], [Quyen]) VALUES (N'Adminstrator        ', N'123456              ', N'QTV01     ', N'Admin')
INSERT [dbo].[TaiKhoan] ([Taikhoan], [MatKhau], [MaNV], [Quyen]) VALUES (N'Qlnnhungoc          ', N'123456123           ', N'QL02      ', N'Cơ Bản')
INSERT [dbo].[TaiKhoan] ([Taikhoan], [MatKhau], [MaNV], [Quyen]) VALUES (N'Qlnthuong           ', N'123456              ', N'QL01      ', N'Cơ Bản')
INSERT [dbo].[TaiKhoan] ([Taikhoan], [MatKhau], [MaNV], [Quyen]) VALUES (N'taikhoan1           ', N'123456              ', N'QL02      ', N'Cơ Bản')
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([Maphong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD FOREIGN KEY([Maphong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD FOREIGN KEY([Manha])
REFERENCES [dbo].[Nha] ([MaNha])
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD CHECK  (([NgayBatDau]<[NgayKetThuc]))
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD CHECK  (([Email] like '%[A-Z,a-z]%@%[a-z]%.%[a-z]%'))
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD CHECK  (([Email] like '%[A-Z,a-z]%@%[a-z]%.%[a-z]%'))
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD CHECK  (([SoDT] like '%0%[0-9]%'))
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD CHECK  ((len([MatKhau])>=(6) AND len([MatKhau])<=(20)))
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD CHECK  ((len([TaiKhoan])>=(6) AND len([TaiKhoan])<=(20)))
GO
USE [master]
GO
ALTER DATABASE [QUANLIKTX] SET  READ_WRITE 
GO
