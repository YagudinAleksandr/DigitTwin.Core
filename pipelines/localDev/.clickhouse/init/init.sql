CREATE DATABASE IF NOT EXISTS DigitTwin_db;
CREATE USER IF NOT EXISTS admin IDENTIFIED WITH plaintext_password BY 'admin123';
GRANT ALL ON DigitTwin_db.* TO admin;