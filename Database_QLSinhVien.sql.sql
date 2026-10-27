USE master;
GO

-- 1. Sao lưu (Backup) CSDL vào thư mục Public (tránh lỗi Access denied)
BACKUP DATABASE QLSinhVien
TO DISK = 'C:\Users\Public\QLSinhVien.bak'
WITH FORMAT, INIT, NAME = 'Full Backup QLSinhVien';
GO

-- 2. Xóa thử 1 bản ghi
USE QLSinhVien;
DELETE FROM SinhVien WHERE MaSV = 'SV001';
GO

-- 3. Phục hồi (Restore) lại CSDL từ file backup
USE master;
GO
ALTER DATABASE QLSinhVien SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

RESTORE DATABASE QLSinhVien
FROM DISK = 'C:\Users\Public\QLSinhVien.bak'
WITH REPLACE;

ALTER DATABASE QLSinhVien SET MULTI_USER;
GO

-- 4. Kiểm tra lại dữ liệu đã khôi phục nguyên vẹn
USE QLSinhVien;
SELECT * FROM SinhVien;