USE [Orange]
GO
ALTER TABLE [dbo].[SalesData] DROP CONSTRAINT [FK_SalesData_Product]
GO
ALTER TABLE [dbo].[ProductListItem] DROP CONSTRAINT [FK_ProductListItem_List]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[LongRunningTaskMessage] DROP CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks]
GO
/****** Object:  FullTextIndex     Script Date: 2/11/2017 4:51:31 PM ******/
DROP FULLTEXT INDEX ON [dbo].[Product]

GO
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
GO
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[VerifiedSimilarProducts]
GO
/****** Object:  Table [dbo].[Sourcing]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[Sourcing]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[Schedule]
GO
/****** Object:  Table [dbo].[SalesData]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[SalesData]
GO
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[ProductMeta]
GO
/****** Object:  Table [dbo].[ProductListItem]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[ProductListItem]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[LongRunningTaskMessage]
GO
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[LongRunningTask]
GO
/****** Object:  Table [dbo].[List]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[List]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/11/2017 4:51:31 PM ******/
DROP TABLE [dbo].[Category]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/11/2017 4:51:31 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[List]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ListType] [nvarchar](50) NULL,
 CONSTRAINT [PK_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LongRunningTask](
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LongRunningTaskMessage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Context] [nvarchar](50) NOT NULL,
	[MessageType] [nvarchar](50) NOT NULL,
	[Message] [ntext] NOT NULL,
	[TaskId] [bigint] NOT NULL,
 CONSTRAINT [PK_LongRunningTaskMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] NOT NULL,
	[ProductId] [varchar](50) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Keywords] [nvarchar](max) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[OnlineAvailability] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[AddToCartUrl] [nvarchar](max) NULL,
	[AffiliateAddToCartUrl] [nvarchar](max) NULL,
	[Price] [money] NULL,
	[Upc] [varchar](50) NULL,
	[SmallImageUrl] [nvarchar](max) NULL,
	[LargeImageUrl] [nvarchar](max) NULL,
	[ShippingCost] [nvarchar](50) NULL,
	[FreeShipping] [bit] NULL,
	[UnitsSold] [bigint] NULL,
	[WatchCount] [bigint] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductListItem]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductListItem](
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[ListId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ProductReferenceId] ASC,
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMeta](
	[Id] [bigint] NOT NULL,
	[SourceCount] [int] NULL,
	[MarketPriceLow] [money] NULL,
	[MarketPriceHigh] [money] NULL,
	[SourcePriceLow] [money] NULL,
	[SourcePriceHigh] [money] NULL,
	[SourceUrl] [nvarchar](max) NULL,
	[MarketCount] [int] NULL,
 CONSTRAINT [PK_ProductMeta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesData]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[SalePrice] [money] NOT NULL,
	[SaleDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SalesData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 2/11/2017 4:51:31 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sourcing]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sourcing](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[DetailPageUrl] [nvarchar](max) NOT NULL,
	[SourceSite] [nvarchar](50) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[SourceProductId] [nvarchar](50) NOT NULL,
	[ProductProductId] [nvarchar](50) NOT NULL,
	[ProductSite] [nvarchar](50) NOT NULL,
	[PriceDescription] [nvarchar](50) NOT NULL,
	[CustomerId] [nvarchar](200) NOT NULL,
	[EstimatedPricePerUnit] [money] NULL,
	[AffiliateUrl] [nvarchar](max) NULL,
	[WholesalerName] [nvarchar](100) NULL,
 CONSTRAINT [PK_ProductSourcing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 2/11/2017 4:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerifiedSimilarProducts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OriginalProduct] [bigint] NOT NULL,
	[ProductId] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_VerifiedSimilarProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 2/11/2017 4:51:31 PM ******/
CREATE NONCLUSTERED INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
(
	[ParentCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     Script Date: 2/11/2017 4:51:31 PM ******/
CREATE FULLTEXT INDEX ON [dbo].[Product](
[Keywords] LANGUAGE 'English')
KEY INDEX [PK_Product]ON ([ProductKeywordsFTS], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = MANUAL, STOPLIST = SYSTEM)


GO
ALTER TABLE [dbo].[LongRunningTaskMessage]  WITH CHECK ADD  CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[LongRunningTask] ([Id])
GO
ALTER TABLE [dbo].[LongRunningTaskMessage] CHECK CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks]
GO
ALTER TABLE [dbo].[Product]  WITH NOCHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[ProductListItem]  WITH CHECK ADD  CONSTRAINT [FK_ProductListItem_List] FOREIGN KEY([ListId])
REFERENCES [dbo].[List] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductListItem] CHECK CONSTRAINT [FK_ProductListItem_List]
GO
ALTER TABLE [dbo].[SalesData]  WITH NOCHECK ADD  CONSTRAINT [FK_SalesData_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[SalesData] CHECK CONSTRAINT [FK_SalesData_Product]
GO
