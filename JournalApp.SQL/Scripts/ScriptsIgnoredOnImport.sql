
USE [JournalApplication_DEV]
GO

/****** Object:  Table [dbo].[BulletPoint]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Journal]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Page]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET IDENTITY_INSERT [dbo].[BulletPoint] ON
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'i love postman', 1, 2, 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'Test3', 3, 2, 1, N'T', CAST(N'2018-05-07T11:12:40.510' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'new content', 5, 2, 0, N'Test', CAST(N'2018-05-07T11:13:06.540' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'new bullet from postman for page 2', 7, 2, 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'populating..', 8, 1, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'populating..', 9, 1, 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'populating..', 10, 3, 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[BulletPoint] ([Content], [BulletPointId], [PageId], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]) VALUES (N'populating.....', 11, 5, 0, N'Test', CAST(N'2018-05-07T13:40:47.963' AS DateTime), NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[BulletPoint] OFF
GO

SET IDENTITY_INSERT [dbo].[Journal] ON
GO

INSERT [dbo].[Journal] ([JournalId], [UserId], [Title], [Description], [ImagePath], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 11, N'Test Journal Title 1', N'Test Journal Description 1', N'Image Path 1', 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[Journal] ([JournalId], [UserId], [Title], [Description], [ImagePath], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 11, N'Test Journal Title 2', N'Test Journal Description2', N'Image Path 2', 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[Journal] ([JournalId], [UserId], [Title], [Description], [ImagePath], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 12, N'Test Journal Title 3', N'Test Journal Description3', N'Image Path 3', 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[Journal] ([JournalId], [UserId], [Title], [Description], [ImagePath], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, NULL, N'postman title', N'postman description', N'postman image path', 1, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[Journal] ([JournalId], [UserId], [Title], [Description], [ImagePath], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, 11, N'postman title3', N'test desp', N'postman image path2', 0, NULL, NULL, NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[Journal] OFF
GO

SET IDENTITY_INSERT [dbo].[Page] ON
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (1, 1, N'Page 1 title', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (2, 1, N'Page 2 title', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (3, 1, N'Page 3 title', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (4, 2, N'Page 4 title', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (5, 1, N'Page 5 title', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (6, 0, N'update from postmin', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (7, 4, N'update from postmin1', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (8, 4, N'update from postmin1', NULL, NULL, NULL, NULL, 1)
GO

INSERT [dbo].[Page] ([PageId], [JournalId], [Title], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (9, 4, N'update from postmin1', NULL, NULL, NULL, NULL, 0)
GO

SET IDENTITY_INSERT [dbo].[Page] OFF
GO

/****** Object:  StoredProcedure [dbo].[BulletPointDeleteForPage]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[BulletPointGetForPage]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[BulletPointUpsertForPage]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[JournalDeleteForUser]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[JournalGetForUser]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[JournalUpsertForUser]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[PageDeleteForJournal]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[PageGetForJournal]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[PageUpsertForJournal]    Script Date: 5/15/2018 5:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
