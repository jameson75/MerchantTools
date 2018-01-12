USE Orange 
GO
SELECT Category.Id, Category.CategoryId, ParentCategory.Name AS ParentCategory, Category.Name AS SubCategory, Category.PathLevel, MAX(Product.WatchCount) AS MaxWatchCount, MIN(Product.WatchCount) AS MinWatchCount, AVG(Product.WatchCount) AS AvgWatchCount,
	    MAX(Product.UnitsSold) AS MaxUnitsSold, MIN(Product.UnitsSold) AS MinUnitsSold
FROM Category JOIN Product ON (Category.Id = Product.CategoryId) JOIN
	 Category AS ParentCategory ON (Category.ParentCategoryId = ParentCategory.CategoryId)
WHERE Category.PathLevel = 1 AND Category.Site LIKE 'eBay' AND ParentCategory.Site LIKE 'eBay'
GROUP BY Category.Id, Category.Name, Category.CategoryId, ParentCategory.Name, Category.PathLevel
ORDER BY AvgWatchCount DESC
GO
