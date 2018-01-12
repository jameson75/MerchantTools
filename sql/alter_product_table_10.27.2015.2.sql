/*
   Tuesday, October 27, 20155:34:17 PM
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
EXECUTE sp_rename N'dbo.Product.AffilateAddToCartUrl', N'Tmp_AffiliateAddToCartUrl', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Product.Tmp_AffiliateAddToCartUrl', N'AffiliateAddToCartUrl', 'COLUMN' 
GO
ALTER TABLE dbo.Product SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per 