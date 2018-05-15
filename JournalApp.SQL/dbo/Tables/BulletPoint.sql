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