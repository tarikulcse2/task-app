USE [databasename]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 08-Dec-19 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](300) NOT NULL,
	[Description] [varchar](max) NULL,
	[Date] [date] NULL,
	[FromTime] [varchar](10) NULL,
	[ToTime] [varchar](10) NULL,
	[Location] [varchar](50) NULL,
	[Notify] [int] NOT NULL,
	[Label] [varchar](10) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifierId] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08-Dec-19 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[DateOfBirth] [date] NULL,
	[ImageUrl] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_Notify]  DEFAULT ((0)) FOR [Notify]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_CreatorId]  DEFAULT ((0)) FOR [CreatorId]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_ModifierId]  DEFAULT ((0)) FOR [ModifierId]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
