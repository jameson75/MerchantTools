/*
   Thursday, October 15, 20154:33:29 PM
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
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Category', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Category', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Category', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Product
	(
	Id bigint NOT NULL,
	ProductId varchar(50) NOT NULL,
	ParentProductId varchar(50) NULL,
	Name varchar(50) NOT NULL,
	DiscountPrice money NOT NULL,
	RegularPrice money NOT NULL,
	Upc varchar(50) NOT NULL,
	ShortDescription nvarchar(MAX) NOT NULL,
	LongDescription nvarchar(MAX) NOT NULL,
	SmallImageUrl nvarchar(MAX) NOT NULL,
	LargeImageUrl nvarchar(MAX) NOT NULL,
	ProductUrl nvarchar(MAX) NOT NULL,
	Category bigint NOT NULL,
	AddToCartUrl nvarchar(MAX) NOT NULL,
	AffilateAddToCartUrl nvarchar(MAX) NOT NULL,
	OnlineAvailability nvarchar(50) NOT NULL,
	CustomerRating nvarchar(50) NULL,
	DiscountDescription nvarchar(50) NULL,
	ShippingCost nvarchar(50) NULL,
	FreeShipToStore bit NULL,
	FreeShipping bit NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Product ADD CONSTRAINT
	FK_Product_Category FOREIGN KEY
	(
	Category
	) REFERENCES dbo.Category
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Product SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per 