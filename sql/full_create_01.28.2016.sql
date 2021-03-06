USE [Orange]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 01/28/2016 23:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ParentCategoryId] [nvarchar](50) NULL,
	[CategoryId] [nvarchar](50) NOT NULL,
	[PathLevel] [tinyint] NOT NULL,
	[Path] [nvarchar](500) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LongRunningTasks]    Script Date: 01/28/2016 23:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LongRunningTasks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ExecutedBy] [nvarchar](50) NOT NULL,
	[StartTimestamp] [datetime2](7) NOT NULL,
	[EndTimestamp] [datetime2](7) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ErrorMessage] [text] NULL,
 CONSTRAINT [PK_LongRunningTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 01/28/2016 23:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[ValidFrom] [datetime2](7) NOT NULL,
	[Frequency] [int] NOT NULL,
	[ActiveSunday] [bit] NOT NULL,
	[ActiveMonday] [bit] NOT NULL,
	[ActiveTuesday] [bit] NOT NULL,
	[ActiveWednesday] [bit] NOT NULL,
	[ActiveThursday] [bit] NOT NULL,
	[ActiveFriday] [bit] NOT NULL,
	[ActiveSaturday] [bit] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[LastTriggered] [datetime2](7) NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 01/28/2016 23:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [varchar](50) NOT NULL,
	[ParentProductId] [varchar](50) NULL,
	[Name] [varchar](255) NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[RegularPrice] [money] NULL,
	[Upc] [varchar](50) NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[LongDescription] [nvarchar](max) NULL,
	[SmallImageUrl] [nvarchar](max) NULL,
	[LargeImageUrl] [nvarchar](max) NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[AddToCartUrl] [nvarchar](max) NOT NULL,
	[AffiliateAddToCartUrl] [nvarchar](max) NOT NULL,
	[OnlineAvailability] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CustomerRating] [nvarchar](50) NULL,
	[DiscountDescription] [nvarchar](50) NULL,
	[ShippingCost] [nvarchar](50) NULL,
	[FreeShipToStore] [bit] NULL,
	[FreeShipping] [bit] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LongRunningTaskMessages]    Script Date: 01/28/2016 23:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LongRunningTaskMessages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Context] [nvarchar](50) NOT NULL,
	[MessageType] [nvarchar](50) NOT NULL,
	[Message] [ntext] NOT NULL,
	[TaskId] [bigint] NOT NULL,
 CONSTRAINT [PK_LongRunningTaskMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_LongRunningTaskMessages_LongRunningTasks]    Script Date: 01/28/2016 23:24:33 ******/
ALTER TABLE [dbo].[LongRunningTaskMessages]  WITH CHECK ADD  CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[LongRunningTasks] ([Id])
GO
ALTER TABLE [dbo].[LongRunningTaskMessages] CHECK CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks]
GO
/****** Object:  ForeignKey [FK_Product_Category]    Script Date: 01/28/2016 23:24:33 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
