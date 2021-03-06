/*
   Wednesday, November 04, 20157:04:52 PM
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
CREATE TABLE dbo.ProductAnalytics
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
	SampleWindowEndDate datetime2(7) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ProductAnalytics ADD CONSTRAINT
	PK_ProductAnalytics PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ProductAnalytics SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProductAnalytics', 'Object', 'CONTROL') as Contr_Per 