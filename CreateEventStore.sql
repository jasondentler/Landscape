CREATE DATABASE LandscapeExampleEventStore
GO

USE LandscapeExampleEventStore
GO

/****** Object:  Table [dbo].[Events]    Script Date: 07/07/2011 11:38:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Events](
	[EventSourceid] [uniqueidentifier] NOT NULL,
	[Version] [int] NOT NULL,
	[TypeName] [nvarchar](max) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events_1] PRIMARY KEY NONCLUSTERED 
(
	[EventSourceid] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[EventSources](
	[Id] [uniqueidentifier] NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_EventSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


