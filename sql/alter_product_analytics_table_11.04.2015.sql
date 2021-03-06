/*
   Wednesday, November 04, 20157:37:26 PM
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
ALTER TABLE dbo.Product SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ProductAnalytics
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	MarketPlace nvarchar(100) NOT NULL,
	HighPrice money NOT NULL,
	LowPrice money NOT NULL,
	AveragePrice money NOT NULL,
	MedianPrice money NOT NULL,
	DateCreated datetime2(7) NOT NULL,
	DateModified datetime2(7) NOT NULL,
	SampleWindowStartDate datetime2(7) NOT NULL,
	SampleWindowEndDate datetime2(7) NOT NULL,
	ProductId bigint NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ProductAnalytics SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_ProductAnalytics ON
GO
IF EXISTS(SELECT * FROM dbo.ProductAnalytics)
	 EXEC('INSERT INTO dbo.Tmp_ProductAnalytics (Id, MarketPlace, HighPrice, LowPrice, AveragePrice, MedianPrice, DateCreated, DateModified, SampleWindowStartDate, SampleWindowEndDate)
		SELECT Id, MarketPlace, HighPrice, LowPrice, AveragePrice, MedianPrice, DateCreated, DateModified, SampleWindowStartDate, SampleWindowEndDate FROM dbo.ProductAnalytics WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_ProductAnalytics OFF
GO
ALTER TABLE dbo.AnalyticsSampleSet
	DROP CONSTRAINT FK_AnalyticsSampleSet_ProductAnalytics
GO
DROP TABLE dbo.ProductAnalytics
GO
EXECUTE sp_rename N'dbo.Tmp_ProductAnalytics', N'ProductAnalytics', 'OBJECT' 
GO
ALTER TABLE dbo.ProductAnalytics ADD CONSTRAINT
	PK_ProductAnalytics PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_ProductAnalytics ON dbo.ProductAnalytics
	(
	MarketPlace
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.ProductAnalytics ADD CONSTRAINT
	FK_ProductAnalytics_Product FOREIGN KEY
	(
	ProductId
	) REFERENCES dbo.Product
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
ALTER TABLE dbo.AnalyticsSampleSet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.AnalyticsSampleSet', 'Object', 'CONTROL') as Contr_Per 