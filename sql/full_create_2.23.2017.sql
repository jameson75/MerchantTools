USE [Orange]
GO
ALTER TABLE [dbo].[SalesData] DROP CONSTRAINT [FK_SalesData_Product]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[LongRunningTaskMessage] DROP CONSTRAINT [FK_LongRunningTaskMessages_LongRunningTasks]
GO
ALTER TABLE [dbo].[AnalyticsReportItem] DROP CONSTRAINT [FK_AnalyticsReportItem_AnalyticsReport]
GO
ALTER TABLE [dbo].[AnalyticsReportInput] DROP CONSTRAINT [FK_AnalyticsReportInput_AnalyticsReport]
GO
/****** Object:  FullTextIndex     Script Date: 2/23/2017 2:48:41 AM ******/
DROP FULLTEXT INDEX ON [dbo].[Product]

GO
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
GO
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[VerifiedSimilarProducts]
GO
/****** Object:  Table [dbo].[Sourcing]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[Sourcing]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[Schedule]
GO
/****** Object:  Table [dbo].[SalesData]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[SalesData]
GO
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[ProductMeta]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[LongRunningTaskMessage]
GO
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[LongRunningTask]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[Category]
GO
/****** Object:  Table [dbo].[AnalyticsReportItem]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[AnalyticsReportItem]
GO
/****** Object:  Table [dbo].[AnalyticsReportInput]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[AnalyticsReportInput]
GO
/****** Object:  Table [dbo].[AnalyticsReport]    Script Date: 2/23/2017 2:48:41 AM ******/
DROP TABLE [dbo].[AnalyticsReport]
GO
/****** Object:  FullTextCatalog [ProductKeywordsFTS]    Script Date: 2/23/2017 2:48:41 AM ******/
GO
DROP FULLTEXT CATALOG [ProductKeywordsFTS]
GO
/****** Object:  FullTextCatalog [ProductKeywordsFTS]    Script Date: 2/23/2017 2:48:41 AM ******/
CREATE FULLTEXT CATALOG [ProductKeywordsFTS]WITH ACCENT_SENSITIVITY = ON
AS DEFAULT

GO
/****** Object:  Table [dbo].[AnalyticsReport]    Script Date: 2/23/2017 2:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AnalyticsReport](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[ReportType] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AnalyticsReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AnalyticsReportInput]    Script Date: 2/23/2017 2:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyticsReportInput](
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[ReportId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ProductReferenceId] ASC,
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnalyticsReportItem]    Script Date: 2/23/2017 2:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyticsReportItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[CategoryReferenceId] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Price] [money] NOT NULL,
	[LargeImageUrl] [nvarchar](max) NOT NULL,
	[UnitsSold] [bigint] NOT NULL,
	[WatchCount] [bigint] NOT NULL,
	[SourceCount] [int] NOT NULL,
	[MarketPriceLow] [money] NOT NULL,
	[MarketPriceHigh] [money] NOT NULL,
	[SourcePriceLow] [money] NULL,
	[SourcePriceHigh] [money] NULL,
	[SourceUrl] [money] NULL,
	[CompetitorCount] [money] NOT NULL,
	[SoldInLast30] [int] NOT NULL,
	[SoldInLast15] [int] NOT NULL,
	[SoldInLast5] [int] NOT NULL,
	[ReportId] [bigint] NOT NULL,
 CONSTRAINT [PK_AnalyticsReportItem_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[SalesData]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[Schedule]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[Sourcing]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 2/23/2017 2:48:41 AM ******/
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
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 2/23/2017 2:48:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
(
	[ParentCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     Script Date: 2/23/2017 2:48:41 AM ******/
CREATE FULLTEXT INDEX ON [dbo].[Product](
[Keywords] LANGUAGE 'English')
KEY INDEX [PK_Product]ON ([ProductKeywordsFTS], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = MANUAL, STOPLIST = SYSTEM)


GO
ALTER TABLE [dbo].[AnalyticsReportInput]  WITH CHECK ADD  CONSTRAINT [FK_AnalyticsReportInput_AnalyticsReport] FOREIGN KEY([ReportId])
REFERENCES [dbo].[AnalyticsReport] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnalyticsReportInput] CHECK CONSTRAINT [FK_AnalyticsReportInput_AnalyticsReport]
GO
ALTER TABLE [dbo].[AnalyticsReportItem]  WITH CHECK ADD  CONSTRAINT [FK_AnalyticsReportItem_AnalyticsReport] FOREIGN KEY([ReportId])
REFERENCES [dbo].[AnalyticsReport] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnalyticsReportItem] CHECK CONSTRAINT [FK_AnalyticsReportItem_AnalyticsReport]
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
ALTER TABLE [dbo].[SalesData]  WITH NOCHECK ADD  CONSTRAINT [FK_SalesData_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[SalesData] CHECK CONSTRAINT [FK_SalesData_Product]
GO
