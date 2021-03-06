/*
   Tuesday, October 27, 20151:42:40 PM
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
CREATE TABLE dbo.Tmp_Category
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	Name nvarchar(255) NOT NULL,
	ParentCategoryId nvarchar(50) NULL,
	CategoryId nvarchar(50) NOT NULL,
	PathLevel tinyint NOT NULL,
	Path nvarchar(500) NOT NULL,
	Supplier nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Category SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Category ON
GO
IF EXISTS(SELECT * FROM dbo.Category)
	 EXEC('INSERT INTO dbo.Tmp_Category (Id, Name, ParentCategoryId, CategoryId, PathLevel, Path, Supplier)
		SELECT Id, Name, ParentCategoryId, CategoryId, PathLevel, Path, Supplier FROM dbo.Category WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Category OFF
GO
ALTER TABLE dbo.Product
	DROP CONSTRAINT FK_Product_Category
GO
DROP TABLE dbo.Category
GO
EXECUTE sp_rename N'dbo.Tmp_Category', N'Category', 'OBJECT' 
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	PK_Category PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.Category', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Category', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Category', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Product ADD CONSTRAINT
	FK_Product_Category FOREIGN KEY
	(
	Category
	) REFERENCES dbo.Category
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.Product SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per 