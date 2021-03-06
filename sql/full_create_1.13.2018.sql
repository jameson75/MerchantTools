USE Orange
GO

/****** Object:  FullTextCatalog [ProductKeywordsFTS]    Script Date: 1/13/2018 7:11:33 AM ******/
CREATE FULLTEXT CATALOG [ProductKeywordsFTS]WITH ACCENT_SENSITIVITY = ON
AS DEFAULT

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/13/2018 7:11:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AnalyticsReport]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AnalyticsReport](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[RunDate] [datetime2](7) NOT NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_AnalyticsReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AnalyticsReportItem]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyticsReportItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductReferenceId] [nvarchar](100) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductUrl] [nvarchar](max) NOT NULL,
	[CategoryReferenceId] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
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
 CONSTRAINT [PK_AnalyticsReportItem_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/13/2018 7:11:34 AM ******/
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
	[Path] [nvarchar](500) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LongRunningTask]    Script Date: 1/13/2018 7:11:34 AM ******/
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
	[TargetName] [nvarchar](255) NULL,
	[ContextId] [bigint] NULL,
 CONSTRAINT [PK_LongRunningTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LongRunningTaskMessage]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[ProductList]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductList](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProductList_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductListItem]    Script Date: 1/13/2018 7:11:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductListItem](
	[ProductId] [bigint] NOT NULL,
	[ListId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductMeta]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[SalesData]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[Schedule]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[Sourcing]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Table [dbo].[VerifiedSimilarProducts]    Script Date: 1/13/2018 7:11:34 AM ******/
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
/****** Object:  Index [RoleNameIndex]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category_ParentCategory]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_ParentCategory] ON [dbo].[Category]
(
	[ParentReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category_Site]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_Site] ON [dbo].[Category]
(
	[Site] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_FK_CategoryId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_FK_CategoryId] ON [dbo].[Product]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_Price]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_Price] ON [dbo].[Product]
(
	[Price] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Product_ReferenceId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_ReferenceId] ON [dbo].[Product]
(
	[ReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_UnitsSold]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_UnitsSold] ON [dbo].[Product]
(
	[UnitsSold] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_WatchCount]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_WatchCount] ON [dbo].[Product]
(
	[WatchCount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_SalesData_ProductReferenceId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_SalesData_ProductReferenceId] ON [dbo].[SalesData]
(
	[ProductReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Sourcing_MarketProductReferenceId]    Script Date: 1/13/2018 7:11:34 AM ******/
CREATE NONCLUSTERED INDEX [IX_Sourcing_MarketProductReferenceId] ON [dbo].[Sourcing]
(
	[MarketProductReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  FullTextIndex     Script Date: 1/13/2018 7:11:34 AM ******/
CREATE FULLTEXT INDEX ON [dbo].[Product](
[Keywords] LANGUAGE 'English')
KEY INDEX [PK_Product]ON ([ProductKeywordsFTS], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = MANUAL, STOPLIST = SYSTEM)


GO
ALTER TABLE [dbo].[AnalyticsReportItem]  WITH CHECK ADD  CONSTRAINT [FK_AnalyticsReportItem_AnalyticsReport] FOREIGN KEY([ReportId])
REFERENCES [dbo].[AnalyticsReport] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnalyticsReportItem] CHECK CONSTRAINT [FK_AnalyticsReportItem_AnalyticsReport]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
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
ALTER TABLE [dbo].[ProductListItem]  WITH CHECK ADD  CONSTRAINT [FK_ProductListItem_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductListItem] CHECK CONSTRAINT [FK_ProductListItem_Product]
GO
ALTER TABLE [dbo].[ProductListItem]  WITH CHECK ADD  CONSTRAINT [FK_ProductListItem_ProductList] FOREIGN KEY([ListId])
REFERENCES [dbo].[ProductList] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductListItem] CHECK CONSTRAINT [FK_ProductListItem_ProductList]
GO
ALTER TABLE [dbo].[ProductMeta]  WITH CHECK ADD  CONSTRAINT [FK_ProductMeta_Product] FOREIGN KEY([Id])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductMeta] CHECK CONSTRAINT [FK_ProductMeta_Product]
GO
/****** Object:  StoredProcedure [dbo].[SelectListView]    Script Date: 1/13/2018 7:11:34 AM ******/
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
