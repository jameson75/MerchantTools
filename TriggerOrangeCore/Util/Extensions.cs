using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using CipherPark.Amazon.Api;
using CipherPark.Ebay.Api.Finding;
using CipherPark.Ebay.Api.Trading;
using CipherPark.Walmart.Api;
using CipherPark.BestBuy.Api;

namespace CipherPark.TriggerOrange.Core
{
    public static class EbayApiExtension
    {
        public static string FindPath(this Ebay.Api.Trading.Category category, IEnumerable<Ebay.Api.Trading.Category> allCategories)
        {
            if (category.CategoryLevel <= 1)
                return category.CategoryName;
            else
            {
                List<string> parts = new List<string>();
                for (int i = 0; i < category.CategoryParentIDs.Length; i++)
                    parts.Add(allCategories.First(c => c.CategoryID == category.CategoryParentIDs[i]).CategoryName);
                parts.Add(category.CategoryName);
                return string.Join("/", parts);
            }
        }
    }

    public static class AmazonApiExtension
    {
        public static string LargeImageUrl(this ListedItem item)
        {
            if (item.LargeImage != null)
                return item.LargeImage.URL;
            else if (item.ImageSets?.FirstOrDefault()?.LargeImage != null)
                return item.ImageSets[0].LargeImage.URL;
            else
                return null;
        }

        public static string MediumImageUrl(this ListedItem item)
        {
            if (item.MediumImage != null)
                return item.MediumImage.URL;
            else if (item.ImageSets?.FirstOrDefault()?.MediumImage != null)
                return item.ImageSets[0].MediumImage.URL;
            else
                return null;
        }
    }

    public static class WalmartApiExtension
    {
        public static string OnlineAvailability(this Item item)
        {
            return item.AvailableOnline ? "Available Online" : "In Store Only";
        }

        public static string CategoryId(this Item item)
        {
            return item.CategoryNode?.Split(new[] { '_' }).Last();
        }

        public static string ParentId(this Walmart.Api.Category category)
        {
            string[] ids = category.Id.Split(new[] { '_' });
            if (ids.Length > 1)
                return ids[ids.Length - 2];
            else
                return null;
        }

        public static int Level(this Walmart.Api.Category category)
        {
            string[] ids = category.Id.Split(new[] { '_' });
            return ids.Length - 1;
        }

        public static IEnumerable<Walmart.Api.Category> Flatten(this IEnumerable<Walmart.Api.Category> categories, int limit = 0)
        {
            List<Walmart.Api.Category> flattenedList = new List<Walmart.Api.Category>();
            FlattenToList(categories, flattenedList, limit);
            return flattenedList.AsEnumerable();
        }

        private static void FlattenToList(IEnumerable<Walmart.Api.Category> input, List<Walmart.Api.Category> output, int limit)
        {
            foreach (var category in input)
            {
                output.Add(category);
                if(limit <= 0 || category.Level() < limit)
                    FlattenToList(category.Children, output, limit);
            }
        }
    }

    public static class BestBuyApiExtension
    {
        public static string CategoryId(this BestBuy.Api.Product product)
        {          
            return product.CategoryPath?.Categories?.LastOrDefault().Id;          
        }

        public static int Level(this BestBuy.Api.Category category)
        {
            return category.Path.Categories.Length - 1;
        }

        public static string ParentId(this BestBuy.Api.Category category)
        {
            return category.Path.Categories.Length > 1 ? category.Path.Categories[category.Path.Categories.Length - 2].Id : null;
        }

        public static string DisplayPath(this BestBuy.Api.Category category)
        {
            return string.Join("/", category.Path.Categories.Select(c => c.Name).ToArray());
        }
    }

    public static class DbSetExtensions
    {
        public static void Delete<TEntity>(this System.Data.Entity.DbSet<TEntity> dbSet, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            dbSet.RemoveRange(dbSet.Where(predicate));           
        }
    }

    public static class LinqExtensions
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, Comparer<TKey>.Default);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                TSource min = sourceIterator.Current;
                TKey minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    TSource candidate = sourceIterator.Current;
                    TKey candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        public static TKey Median<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.Median(selector, Comparer<TKey>.Default);
        }

        public static TKey Median<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            var orderedSource = source.OrderBy(selector);
            int nItems = orderedSource.Count();
            int i = nItems / 2;
            if (nItems % 2 == 0 && typeof(TKey).IsNumeric())
            {
                int j = i - 1;
                object e1 = orderedSource.Select(selector).ElementAt(i);
                object e2 = orderedSource.Select(selector).ElementAt(j);
                return (TKey)Convert.ChangeType((Convert.ToDouble(e1) + Convert.ToDouble(e2)) / 2, typeof(TKey));
            }
            else
                return orderedSource.Select(selector).ElementAt(i);
        }
    }

    public static class ProductLinqExtensions
    { 
        public static IOrderedQueryable<Data.Product> OrderProductBy(this IQueryable<Data.Product> sqlQuery, string sortKey)
        {
            switch(sortKey)
            {
                case SearchSortKey.UnitsSold:
                    return sqlQuery.OrderByDescending(p => p.UnitsSold);
                case SearchSortKey.PriceHighToLow:
                    return sqlQuery.OrderByDescending(p => p.Price);
                case SearchSortKey.PriceLowToHigh:
                    return sqlQuery.OrderBy(p => p.Price);
                case SearchSortKey.SellerScore:
                    return sqlQuery.OrderBy(p => p.SellerScore);
                default:
                    return sqlQuery.OrderByDescending(p => p.WatchCount);
            }
        }

        public static IOrderedEnumerable<Data.Product> OrderProductBy(this IEnumerable<Data.Product> sqlQuery, string sortKey)
        {
            switch (sortKey)
            {
                case SearchSortKey.UnitsSold:
                    return sqlQuery.OrderByDescending(p => p.UnitsSold);
                case SearchSortKey.PriceHighToLow:
                    return sqlQuery.OrderByDescending(p => p.Price);
                case SearchSortKey.PriceLowToHigh:
                    return sqlQuery.OrderBy(p => p.Price);
                case SearchSortKey.SellerScore:                   
                    return sqlQuery.OrderBy(p => p.SellerScore);
                default:
                    return sqlQuery.OrderByDescending(p => p.WatchCount);
            }
        }   

        public static IQueryable<Data.Product> FilterProductWhere(this IQueryable<Data.Product> sqlQuery, ProductSearchFilter filter)
        {
            if (filter != null)
            {
                if (filter.PriceHigh != null)
                    sqlQuery = sqlQuery.Where(p => p.Price <= filter.PriceHigh);

                if (filter.PriceLow != null)
                    sqlQuery = sqlQuery.Where(p => p.Price >= filter.PriceLow);

                if (filter.UnitsHigh != null)
                    sqlQuery = sqlQuery.Where(p => p.UnitsSold <= filter.UnitsHigh);

                if (filter.UnitsLow != null)
                    sqlQuery = sqlQuery.Where(p => p.UnitsSold >= filter.UnitsLow);

                if (filter.SellerRankHigh != null)
                    sqlQuery = sqlQuery.Where(p => p.SellerScore <= filter.SellerRankHigh);

                if (filter.SellerRankLow != null)
                    sqlQuery = sqlQuery.Where(p => p.SellerScore >= filter.SellerRankLow);
            }
            return sqlQuery;
        }

        public static IEnumerable<Data.Product> FilterProductWhere(this DbRawSqlQuery<Data.Product> sqlQuery, ProductSearchFilter filter)
        {
            IEnumerable<Data.Product> eResult = sqlQuery.AsEnumerable();

            if (filter != null)
            {
                if (filter.PriceHigh != null)
                    eResult = eResult.Where(p => p.Price <= filter.PriceHigh);

                if (filter.PriceLow != null)
                    eResult = eResult.Where(p => p.Price >= filter.PriceLow);

                if (filter.UnitsHigh != null)
                    eResult = eResult.Where(p => p.UnitsSold <= filter.UnitsHigh);

                if (filter.UnitsLow != null)
                    eResult = eResult.Where(p => p.UnitsSold >= filter.UnitsLow);

                if (filter.SellerRankHigh != null)
                    eResult = eResult.Where(p => p.SellerScore <= filter.SellerRankHigh);

                if (filter.SellerRankLow != null)
                    eResult = eResult.Where(p => p.SellerScore >= filter.SellerRankLow);
            }
            return eResult;
        }
    }    
   

    public static class NumericTypeExtension
    {
        public static bool IsNumeric(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("dataType");
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))                    
                        return Nullable.GetUnderlyingType(type).IsNumeric();                    
                    return false;
            }
            return false;
        }
    }

    public static class EbayDateTimeExtension
    {
        public static string ToEbayGMTString(this DateTime d)
        {
            return d.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}
