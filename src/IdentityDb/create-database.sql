CREATE DATABASE [credentials-db]
GO

USE [credentials-db];
GO


CREATE TABLE UserCredentials (
    Id INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL
);
GO

-- Insert sample data
INSERT INTO UserCredentials (UserName, Password)
VALUES 
    ('user1', 'password1'),
    ('user2', 'password2'),
    ('user3', 'password3');
GO