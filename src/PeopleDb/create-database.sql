CREATE DATABASE [people-db]
GO

USE [people-db];
GO


CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,                -- Primary Key
    LoginId INT NOT NULL,                       -- Login ID
    UserName NVARCHAR(100) NOT NULL,            -- User Name (Required, max length 100)
    Biogram NVARCHAR(500) NULL,                 -- Biogram (Optional, max length 500)
    FirstName NVARCHAR(100) NULL,               -- First Name (Optional, max length 100)
    LastName NVARCHAR(100) NULL,                -- Last Name (Optional, max length 100)
    DateOfBirth DATETIME NULL                   -- Date of Birth (Optional)
);

-- Create the Following relationship
CREATE TABLE UserFollows (
    Id INT PRIMARY KEY IDENTITY,               -- Primary Key
    UserId INT NOT NULL,                       -- User ID (The user who is following someone)
    FollowedUserId INT NOT NULL,                  -- Following ID (The user being followed)
    CONSTRAINT FK_UserFollow_User FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,  -- Cascade delete for user
    CONSTRAINT FK_UserFollow_FollowedUser FOREIGN KEY (FollowedUserId) REFERENCES Users(Id) ON DELETE CASCADE	 -- Prevent cascading delete for the followed user
);
GO


INSERT INTO Users (LoginId, UserName, Biogram, FirstName, LastName, DateOfBirth)
VALUES 
    (101, 'john_doe', 'Loves coding and hiking.', 'John', 'Doe', '1990-05-15'),
    (102, 'jane_smith', 'Avid reader and traveler.', 'Jane', 'Smith', '1985-03-22'),
    (103, 'mike_brown', 'Tech enthusiast and blogger.', 'Mike', 'Brown', '1992-07-10'),
    (104, 'susan_lee', 'Coffee lover and artist.', 'Susan', 'Lee', '1988-09-25'),
    (105, 'david_jones', 'Entrepreneur and cyclist.', 'David', 'Jones', '1995-12-05');

-- Insert exemplary data into UserFollowing table
-- Relationships indicating who is following whom

GO
INSERT INTO UserFollows (UserId, FollowedUserId)
VALUES 
    (1, 2), -- John follows Jane
    (1, 3), -- John follows Mike
    (2, 4), -- Jane follows Susan
    (3, 1), -- Mike follows John
    (3, 4), -- Mike follows Susan
    (4, 5), -- Susan follows David
    (5, 1); -- David follows John

