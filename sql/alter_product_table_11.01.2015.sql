/*
   Monday, November 02, 201511:00:56 AM
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
CREATE TABLE dbo.Tmp_Product
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	ProductId varchar(50) NOT NULL,
	ParentProductId varchar(50) NULL,
	Name varchar(255) NOT NULL,
	DiscountPrice money NOT NULL,
	RegularPrice money NULL,
	Upc varchar(50) NULL,
	ShortDescription nvarchar(MAX) NULL,
	LongDescription nvarchar(MAX) NULL,
	SmallImageUrl nvarchar(MAX) NULL,
	LargeImageUrl nvarchar(MAX) NULL,
	ProductUrl nvarchar(MAX) NOT NULL,
	Category bigint NOT NULL,
	AddToCartUrl nvarchar(MAX) NOT NULL,
	AffiliateAddToCartUrl nvarchar(MAX) NOT NULL,
	OnlineAvailability nvarchar(100) NOT NULL,
	DateCreated datetime2(7) NOT NULL,
	DateModified datetime2(7) NOT NULL,
	CustomerRating nvarchar(50) NULL,
	DiscountDescription nvarchar(50) NULL,
	ShippingCost nvarchar(50) NULL,
	FreeShipToStore bit NULL,
	FreeShipping bit NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Product SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Product ON
GO
IF EXISTS(SELECT * FROM dbo.Product)
	 EXEC('INSERT INTO dbo.Tmp_Product (Id, ProductId, ParentProductId, Name, DiscountPrice, RegularPrice, Upc, ShortDescription, LongDescription, SmallImageUrl, LargeImageUrl, ProductUrl, Category, AddToCartUrl, AffiliateAddToCartUrl, OnlineAvailability, CustomerRating, DiscountDescription, ShippingCost, FreeShipToStore, FreeShipping)
		SELECT Id, ProductId, ParentProductId, Name, DiscountPrice, RegularPrice, Upc, ShortDescription, LongDescription, SmallImageUrl, LargeImageUrl, ProductUrl, Category, AddToCartUrl, AffiliateAddToCartUrl, OnlineAvailability, CustomerRating, DiscountDescription, ShippingCost, FreeShipToStore, FreeShipping FROM dbo.Product WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Product OFF
GO
DROP TABLE dbo.Product
GO
EXECUTE sp_rename N'dbo.Tmp_Product', N'Product', 'OBJECT' 
GO
ALTER TABLE dbo.Product ADD CONSTRAINT
	PK_Product PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

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
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per 