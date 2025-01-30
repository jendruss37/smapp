CREATE DATABASE [post-db]
GO

USE [post-db];
GO


CREATE TABLE Posts (
    Id INT PRIMARY KEY IDENTITY,
    Content NVARCHAR(MAX) NOT NULL,
    UserId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);




INSERT INTO Posts (Content, UserId)
VALUES 
    ('Hello, world!', 1), -- Original post with no parent
    ('This is my first post.', 2), -- Original post with no parent
    ('I love this platform!', 3), -- Original post with no parent
    ('Can anyone recommend good books?', 1), -- Original post with no parent
    ('Happy to be here!', 4), -- Original post with no parent
    ('Reply to Hello, world!', 2), -- Reply to Post 1
    ('Another reply to Hello, world!', 3), -- Another reply to Post 1
    ('Reply to Can anyone recommend good books?', 3); -- Reply to Post 4
GO
