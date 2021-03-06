USE [Orange]
GO
/****** Object:  FullTextCatalog [ProductKeywordsFTS]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE FULLTEXT CATALOG [ProductKeywordsFTS]WITH ACCENT_SENSITIVITY = ON
AS DEFAULT

GO
/****** Object:  Table [dbo].[ActiveUserReport]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveUserReport](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ReportId] [bigint] NOT NULL,
 CONSTRAINT [PK_ActiveUserReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Blog]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Blog_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BlogPost]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPost](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BlogContent] [nvarchar](max) NOT NULL,
	[CoverImageId] [bigint] NOT NULL,
	[ProductUnitsSold] [bigint] NOT NULL,
	[ProductPrice] [money] NOT NULL,
	[ProductSellerScore] [bigint] NOT NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[ProductReferenceId] [nvarchar](50) NOT NULL,
	[ProductKeywords] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[BlogId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ParentReferenceId] [nvarchar](50) NULL,
	[ReferenceId] [nvarchar](50) NOT NULL,
	[PathLevel] [tinyint] NOT NULL,
	[SiteId] [tinyint] NULL,
	[Path] [nvarchar](500) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HostedImages]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HostedImages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PhysicalPath] [nvarchar](512) NULL,
	[EmbeddedContent] [varbinary](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateLastModified] [datetime2](7) NOT NULL,
	[IsContentEmbedded] [bit] NOT NULL,
 CONSTRAINT [PK_HostedImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 12/25/2018 11:46:07 AM ******/
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
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 12/25/2018 11:46:07 AM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReferenceId] [varchar](50) NOT NULL,
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
	[StartTime] [datetime2](7) NULL,
	[SellerId] [nvarchar](100) NULL,
	[SellerScore] [int] NULL,
	[Location] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMeta](
	[Id] [bigint] NOT NULL,
	[MarketCount] [int] NULL,
	[MarketPriceLow] [money] NULL,
	[MarketPriceHigh] [money] NULL,
	[MarketPriceDescription] [nvarchar](255) NULL,
	[SoldInLastDay] [int] NULL,
	[SoldInLastFiveDays] [int] NULL,
	[SoldInLastFifteenDays] [int] NULL,
	[SoldInLastThirtyDays] [int] NULL,
	[SellThroughRate] [decimal](18, 4) NULL,
	[SourceCount] [int] NULL,
	[SourcePriceLow] [money] NULL,
	[SourcePriceHigh] [money] NULL,
	[SourcePriceDescription] [nvarchar](255) NULL,
	[SourceUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductMeta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductStaging]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductStaging](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReferenceId] [varchar](50) NOT NULL,
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
	[StartTime] [datetime2](7) NULL,
	[SellerId] [nvarchar](100) NULL,
	[SellerScore] [int] NULL,
	[Location] [nvarchar](100) NULL,
 CONSTRAINT [PK_ProductStaging] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PublishedList]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublishedList](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReportId] [bigint] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_PublicList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Report](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[RunDate] [datetime2](7) NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnalyticsReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportItem]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[CategoryReferenceId] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[Price] [money] NOT NULL,
	[LargeImageUrl] [nvarchar](max) NOT NULL,
	[UnitsSold] [bigint] NULL,
	[WatchCount] [bigint] NULL,
	[SourceCount] [int] NULL,
	[MarketPriceLow] [money] NULL,
	[MarketPriceHigh] [money] NULL,
	[SourcePriceLow] [money] NULL,
	[SourcePriceHigh] [money] NULL,
	[SourceUrl] [nvarchar](max) NULL,
	[CompetitorCount] [int] NULL,
	[SoldInLastThirtyDays] [int] NULL,
	[SoldInLastFifteenDays] [int] NULL,
	[SoldInLastFiveDays] [int] NULL,
	[SoldInLastDay] [int] NULL,
	[ReportId] [bigint] NOT NULL,
	[SellerScore] [bigint] NULL,
	[SellerId] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
	[Caption] [nvarchar](max) NULL,
	[HostedImageId] [bigint] NULL,
 CONSTRAINT [PK_AnalyticsReportItem_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesData]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SalePrice] [money] NOT NULL,
	[SaleDate] [datetime2](7) NOT NULL,
	[ProductReferenceId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SalesData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 12/25/2018 11:46:07 AM ******/
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
/****** Object:  Table [dbo].[ShortRunningTask]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShortRunningTask](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[ExecutionTime] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ContextId] [nvarchar](255) NULL,
	[ContextName] [nvarchar](255) NULL,
	[ErrorMessage] [text] NULL,
 CONSTRAINT [PK_ShortRunningTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sourcing]    Script Date: 12/25/2018 11:46:07 AM ******/
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
	[SourceProductReferenceId] [nvarchar](50) NOT NULL,
	[MarketProductReferenceId] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerifiedSimilarProducts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[SimilarProductReferenceId] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_VerifiedSimilarProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
(
	[ParentReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category_Site]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_Site] ON [dbo].[Category]
(
	[Site] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Category_SiteId]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_SiteId] ON [dbo].[Category]
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_FK_CategoryId]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_FK_CategoryId] ON [dbo].[Product]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_Price]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_Price] ON [dbo].[Product]
(
	[Price] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Product_ReferenceId]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_ReferenceId] ON [dbo].[Product]
(
	[ReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_UnitsSold]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_UnitsSold] ON [dbo].[Product]
(
	[UnitsSold] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_WatchCount]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_WatchCount] ON [dbo].[Product]
(
	[WatchCount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_SalesData_ProductReferenceId]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_SalesData_ProductReferenceId] ON [dbo].[SalesData]
(
	[ProductReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Sourcing_MarketProductReferenceId]    Script Date: 12/25/2018 11:46:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_Sourcing_MarketProductReferenceId] ON [dbo].[Sourcing]
(
	[MarketProductReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     Script Date: 12/25/2018 11:46:07 AM ******/
CREATE FULLTEXT INDEX ON [dbo].[Product](
[Keywords] LANGUAGE 'English')
KEY INDEX [PK_Product]ON ([ProductKeywordsFTS], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = MANUAL, STOPLIST = SYSTEM)


GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD  CONSTRAINT [FK_BlogPost_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
GO
ALTER TABLE [dbo].[BlogPost] CHECK CONSTRAINT [FK_BlogPost_Blog]
GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD  CONSTRAINT [FK_BlogPost_HostedImages] FOREIGN KEY([CoverImageId])
REFERENCES [dbo].[HostedImages] ([Id])
GO
ALTER TABLE [dbo].[BlogPost] CHECK CONSTRAINT [FK_BlogPost_HostedImages]
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
ALTER TABLE [dbo].[ProductMeta]  WITH CHECK ADD  CONSTRAINT [FK_ProductMeta_Product] FOREIGN KEY([Id])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductMeta] CHECK CONSTRAINT [FK_ProductMeta_Product]
GO
ALTER TABLE [dbo].[ProductStaging]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductStaging_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductStaging] CHECK CONSTRAINT [FK_ProductStaging_Category]
GO
ALTER TABLE [dbo].[PublishedList]  WITH CHECK ADD  CONSTRAINT [FK_ProductList_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([Id])
GO
ALTER TABLE [dbo].[PublishedList] CHECK CONSTRAINT [FK_ProductList_Report]
GO
ALTER TABLE [dbo].[ReportItem]  WITH CHECK ADD  CONSTRAINT [FK_ReportItem_HostedImages] FOREIGN KEY([HostedImageId])
REFERENCES [dbo].[HostedImages] ([Id])
GO
ALTER TABLE [dbo].[ReportItem] CHECK CONSTRAINT [FK_ReportItem_HostedImages]
GO
ALTER TABLE [dbo].[ReportItem]  WITH CHECK ADD  CONSTRAINT [FK_ReportItem_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([Id])
GO
ALTER TABLE [dbo].[ReportItem] CHECK CONSTRAINT [FK_ReportItem_Report]
GO
/****** Object:  StoredProcedure [dbo].[SelectListView]    Script Date: 12/25/2018 11:46:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eugene Adams
-- Create date: 3/7/2017
-- Description:	Generate a live report view.
-- =============================================
CREATE PROCEDURE [dbo].[SelectListView] 
	-- Add the parameters for the stored procedure here	
	@listId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Category.[Site],
		   Category.Name AS CategoryName,
		   Category.ReferenceId AS CategoryReferenceId,
		   Category.Id AS CategoryId,
		   Product.Id AS ProductId,
		   Product.LargeImageUrl,
		   Product.Name AS ProductName,
		   Product.ReferenceId AS ProductReferenceId,
		   Product.ProductUrl,
		   Product.WatchCount,
		   Product.UnitsSold,
		   Product.Price,
		   ProductMeta.MarketCount,
		   ProductMeta.MarketPriceHigh,
		   ProductMeta.MarketPriceLow,
		   ProductMeta.SourceCount,
		   ProductMeta.SourcePriceHigh,
		   ProductMeta.SourcePriceLow,
		   ProductMeta.SourceUrl,
		   ProductMeta.SoldInLastDay,
		   ProductMeta.SoldInLastFiveDays,
		   ProductMeta.SoldInLastFifteenDays,
		   ProductMeta.SoldInLastThirtyDays,		   
		   ListId
	FROM Product LEFT JOIN ProductMeta ON (ProductMeta.Id = Product.Id)
				 JOIN ProductListItem ON (ProductListItem.ProductId = Product.Id)
				 JOIN Category ON (Product.CategoryId = Category.Id)
	WHERE ProductListItem.ListId = @listId
END

GO
