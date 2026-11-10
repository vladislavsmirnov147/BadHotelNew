IF DB_ID(N'BadHotelDb') IS NULL
BEGIN
    CREATE DATABASE [BadHotelDb];
END;
GO

USE [BadHotelDb];
GO

IF OBJECT_ID(N'dbo.Groups', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Groups]
    (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(100) NOT NULL UNIQUE
    );
END;
GO

IF OBJECT_ID(N'dbo.Workers', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Workers]
    (
        [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        [Type] NVARCHAR(50) NOT NULL,
        [Name] NVARCHAR(100) NOT NULL,
        [Energy] INT NOT NULL,
        [GroupId] INT NOT NULL,

        CONSTRAINT [FK_Workers_Groups]
            FOREIGN KEY ([GroupId])
            REFERENCES [dbo].[Groups]([Id]),

        CONSTRAINT [CK_Workers_Energy]
            CHECK ([Energy] BETWEEN 0 AND 100)
    );
END;
GO