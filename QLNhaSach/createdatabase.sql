use master
go

create database dbQLCuaHangSach
go

use dbQLCuaHangSach
go

create table tblSach  (
id int identity(1,1) primary key,
tenSach nvarchar(50),
giaBan int,
soLuong int,
)
go

create table tblLoaiSach  (
id int identity(1,1) primary key,
tenLoai nvarchar(50),
)
go

create table tblTacGia  (
id int identity(1,1) primary key,
ten nvarchar(50),
ngaySinh date,
moTa nvarchar(200),
)
go

create table tblKhachHang (
id int identity(1,1) primary key,
tenKhachHang nvarchar(50),
diaChi nvarchar(200),
dienThoai varchar(20),
email nvarchar(50),
)
go

create table tblHoaDon  (
id int identity(1,1) primary key,
ngayTao datetime,
tenNhanVien nvarchar(50),
tongTien int,
)
go

create table TheLoaiSach (
id int identity(1,1) primary key,
id_tblSach int references tblSach(id),
id_tblLoaiSach int references tblLoaiSach(id),
)
go

create table PhieuNhapSach (
id int identity(1,1) primary key,
id_tblSach int references tblSach(id),
soLuong int,
ngay datetime,
)
go

create table ChiTietHoaDonBanSach (
id int identity(1,1) primary key,
id_tblHoaDon int references tblHoaDon(id),
id_tblKhachHang int references tblKhachHang(id),
id_tblSach int references tblSach(id),
soLuong int,
)
go

create table BaoCaoTon (
id int identity(1,1) primary key,
id_tblSach int references tblSach(id),
chiPhi int,
ngay datetime,
moTa nvarchar(200),
)
go

create table BaoCaoCongNo (
id int identity(1,1) primary key,
id_tblKhachHang int references tblKhachHang(id),
noTien int,
ngayNo datetime,
moTa nvarchar(200),
)
go