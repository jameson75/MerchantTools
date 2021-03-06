/*
   Wednesday, November 04, 20157:31:50 PM
   User: 
   Server: localhost\MSSQLSERVER08R2
   Database: Orange
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE NONCLUSTERED INDEX IX_ProductAnalytics ON dbo.ProductAnalytics
	(
	MarketPlace
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.ProductAnalytics SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_AnalyticsSampleSet
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	ProductName nvarchar(500) NOT NULL,
	Category nvarchar(200) NOT NULL,
	CurrentPrice money NOT NULL,
	ProductUrl nvarchar(MAX) NOT NULL,
	AnalyticsId bigint NOT NULL,
	ListingId nvarchar(50) NULL,
	ProductId nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_AnalyticsSampleSet SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_AnalyticsSampleSet ON
GO
IF EXISTS(SELECT * FROM dbo.AnalyticsSampleSet)
	 EXEC('INSERT INTO dbo.Tmp_AnalyticsSampleSet (Id, ProductName, Category, CurrentPrice, ProductUrl, ListingId, ProductId)
		SELECT Id, ProductName, Category, CurrentPrice, ProductUrl, ListingId, ProductId FROM dbo.AnalyticsSampleSet WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_AnalyticsSampleSet OFF
GO
DROP TABLE dbo.AnalyticsSampleSet
GO
EXECUTE sp_rename N'dbo.Tmp_AnalyticsSampleSet', N'AnalyticsSampleSet', 'OBJECT' 
GO
ALTER TABLE dbo.AnalyticsSampleSet ADD CONSTRAINT
	PK_AnalyticsSampleSet PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AnalyticsSampleSet ADD CONSTRAINT
	FK_AnalyticsSampleSet_ProductAnalytics FOREIGN KEY
	(
	AnalyticsId
	) REFERENCES dbo.ProductAnalytics
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'CONTROL') as Contr_Per 