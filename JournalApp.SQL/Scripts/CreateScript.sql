USE [master]
GO
/****** Object:  Database [test]    Script Date: 5/29/2018 4:28:57 PM ******/
CREATE DATABASE [JournalApplication_DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Users\tchow\test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Users\tchow\test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [test] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [test] SET ARITHABORT OFF 
GO
ALTER DATABASE [test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [test] SET  MULTI_USER 
GO
ALTER DATABASE [test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [test] SET QUERY_STORE = OFF
GO
USE [test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [test]
GO
/****** Object:  Table [dbo].[BulletPoint]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BulletPoint](
	[Content] [varchar](max) NULL,
	[BulletPointId] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NULL,
	[IsActive] [bit] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BulletPoint] PRIMARY KEY CLUSTERED 
(
	[BulletPointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Journal]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Journal](
	[JournalId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[ImagePath] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Journal] PRIMARY KEY CLUSTERED 
(
	[JournalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[JournalId] [int] NULL,
	[Title] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[BulletPointDeleteForPage]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BulletPointDeleteForPage]

@pageId AS INT,
@bulletPointId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM BulletPoint WHERE PageId = @pageId AND BulletPointId = @bulletPointId;
ELSE
UPDATE BulletPoint SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE PageId = @pageId AND BulletPointId = @bulletPointId AND IsActive = 1;

END
GO
/****** Object:  StoredProcedure [dbo].[BulletPointGetForPage]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BulletPointGetForPage]

@pageId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT PageId, BulletPointId, Content FROM BulletPoint 
WHERE PageId = @pageId AND IsActive = @isActive;

END
GO
/****** Object:  StoredProcedure [dbo].[BulletPointUpsertForPage]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BulletPointUpsertForPage]

@pageId AS INT,
@bulletPointId AS INT,
@newContent AS VARCHAR(MAX) = NULL,
@newPageId AS INT = NULL

AS
BEGIN

IF (@bulletPointId = 0)
INSERT INTO BulletPoint (Content, PageId, IsActive) VALUES (@newContent,@pageId,1);

ELSE

UPDATE BulletPoint SET Content = COALESCE(@newContent,Content),PageId = COALESCE(@newPageId,PageId)
WHERE PageId = @pageId AND BulletPointId = @bulletPointId;

END
GO
/****** Object:  StoredProcedure [dbo].[JournalDeleteForUser]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[JournalDeleteForUser]

@userId AS INT,
@journalId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM Journal WHERE UserId = @userId AND JournalId = @journalId;
ELSE
UPDATE Journal SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE UserId = @userId AND JournalId = @journalId AND IsActive = 1;

END
GO
/****** Object:  StoredProcedure [dbo].[JournalGetForUser]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[JournalGetForUser]
@userId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT JournalId, UserId, Title, Description, ImagePath  FROM Journal 
WHERE UserId = @userId AND IsActive = @isActive;

END
GO
/****** Object:  StoredProcedure [dbo].[JournalUpsertForUser]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[JournalUpsertForUser]

@userId AS INT,
@journalId AS INT,
@newTitle AS VARCHAR(MAX) = NULL,
@newDescription AS VARCHAR(MAX) = NULL,
@newImagePath AS VARCHAR(MAX) = NULL

AS
BEGIN

IF (@journalId = 0)
INSERT INTO Journal (UserId, Title, Description, ImagePath, IsActive) VALUES (@userId, @newTitle,@newDescription,@newImagePath,1);

ELSE

UPDATE Journal SET Title = COALESCE(@newTitle,Title),Description = COALESCE(@newDescription,Description), ImagePath = COALESCE(@newImagePath,ImagePath)
WHERE JournalId = @journalId AND UserId = @userId;

END
GO
/****** Object:  StoredProcedure [dbo].[PageDeleteForJournal]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PageDeleteForJournal]

@journalId AS INT,
@pageId AS INT,
@permanentDelete AS BIT = 0,
@modifiedBy AS VARCHAR(50) = NULL,
@modifiedDate AS DATETIME = NULL

AS
BEGIN

IF (@permanentDelete = 1)
DELETE FROM Page WHERE JournalId = @journalId AND PageId = @pageId;
ELSE
UPDATE Page SET IsActive = 0, ModifiedBy = COALESCE(ModifiedBy,@modifiedBy), ModifiedDate = COALESCE(ModifiedDate,@modifiedDate) 
WHERE JournalId = @journalId AND PageId = @pageId AND IsActive = 1;

END
GO
/****** Object:  StoredProcedure [dbo].[PageGetForJournal]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PageGetForJournal]
@journalId AS INT,
@isActive AS BIT = 1

AS
BEGIN

SELECT PageId, JournalId, Title  FROM Page 
WHERE JournalId = @journalId AND IsActive = @isActive;

END
GO
/****** Object:  StoredProcedure [dbo].[PageUpsertForJournal]    Script Date: 5/29/2018 4:28:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PageUpsertForJournal]

@journalId AS INT,
@pageId AS INT,
@newTitle AS VARCHAR(MAX) = NULL,
@newJournalId AS INT = NULL

AS
BEGIN

IF (@pageId = 0)
INSERT INTO Page (Title, JournalId, IsActive) VALUES (@newTitle,@journalId,1);

ELSE

UPDATE Page SET Title = COALESCE(@newTitle,Title),JournalId = COALESCE(@newJournalId,JournalId)
WHERE PageId = @pageId AND JournalId = @journalId;

END
GO
USE [master]
GO
ALTER DATABASE [test] SET  READ_WRITE 
GO
