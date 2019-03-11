﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using System.Web;
using System.IO;
using CipherPark.TriggerOrange.Web.Models;
using CipherPark.TriggerOrange.Web.Util;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Core;

namespace CipherPark.TriggerOrange.Web.CoreServices
{
    public static class CoreDataServices
    {               
       
        internal static List<BlogPostJsonModel> GetBlogPostsForPage(int pageSize, int firstItemIndex, string sortKey, Guid? blogId, string keywords, out int nTotalItems)
        {
            if (!string.IsNullOrWhiteSpace(keywords))
                return GetBlogPostsByKeywords(pageSize, firstItemIndex, blogId, keywords.Split(' '), sortKey, true, out nTotalItems)
                        .Select(p => new BlogPostJsonModel()
                        {
                            Id = p.Id,
                            Content = "<h1>Test Content</h1> <img src='https://www.w3schools.com/images/colorpicker.gif'/> " +  p.BlogContent,
                            Title = p.Title,
                            DateCreated = p.DateCreated.ToUnixMilliseconds(),
                            DateModified = p.DateModified.ToUnixMilliseconds(),
                            ProductPrice = p.ProductPrice,
                            ProductSellerScore = p.ProductSellerScore,
                            ProductUnitsSold = p.ProductUnitsSold,
                            ProductUrl = p.ProductUrl,
                            CoverImageId = p.CoverImageId,
                            CoverImageUrl = ReferenceDataServices.HostedImageUrl(p.CoverImageId),
                            ProductSellerLevel = SellerLevels.ScoreToLevel(p.ProductSellerScore),
                            ProductListingDate = p.ProductListingDate.GetValueOrDefault().ToUnixMilliseconds(),
                            ProductCategory = p.ProductCategory
                        })
                        .ToList();
            else
            {
                List<BlogPostJsonModel> blogPosts = new List<BlogPostJsonModel>();
                nTotalItems = 0;
                int nPageSize = pageSize;
                using (OrangeEntities db = new OrangeEntities())
                {
                    var dbPostsAll = db.BlogPosts.Where(b => blogId == null || b.BlogId == blogId);
                    var dbPosts = dbPostsAll.OrderByDescending(x => x.DateCreated)
                                                   .Skip(firstItemIndex)
                                                   .Take(pageSize)
                                                   .ToList();
                    blogPosts.AddRange(dbPosts.Select(p => new BlogPostJsonModel()
                    {
                        Id = p.Id,
                        Content = p.BlogContent,
                        Title = p.Title,
                        DateCreated = p.DateCreated.ToUnixMilliseconds(),
                        DateModified = p.DateModified.ToUnixMilliseconds(),
                        ProductPrice = p.ProductPrice,
                        ProductSellerScore = p.ProductSellerScore,
                        ProductUnitsSold = p.ProductUnitsSold,
                        ProductUrl = p.ProductUrl,
                        CoverImageId = p.CoverImageId,
                        CoverImageUrl = ReferenceDataServices.HostedImageUrl(p.CoverImageId),
                        ProductSellerLevel = SellerLevels.ScoreToLevel(p.ProductSellerScore),
                        ProductListingDate = p.ProductListingDate.GetValueOrDefault().ToUnixMilliseconds(),
                        ProductCategory = p.ProductCategory
                    }));
                    nTotalItems = dbPostsAll.Count();
                }
                return blogPosts;
            }
        }
       

        internal static void UpdateTaskSchedule(string taskName, string siteName, bool enabled, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, DateTime startTime)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                var scheduleName = $"{taskName}+{siteName}";
                var schedule = db.Schedules.FirstOrDefault(s => s.Name == scheduleName);
                if (schedule == null)
                {
                    schedule = new Schedule();
                    db.Schedules.Add(schedule);
                }
                schedule.Name = scheduleName;
                schedule.Enabled = enabled;
                schedule.ActiveSunday = Sunday;
                schedule.ActiveMonday = Monday;
                schedule.ActiveTuesday = Tuesday;
                schedule.ActiveWednesday = Wednesday;
                schedule.ActiveThursday = Thursday;
                schedule.ActiveFriday = Friday;
                schedule.ActiveSaturday = Saturday;
                schedule.StartTime = startTime;
                db.SaveChanges();
            }
        }      

        internal static string GetTaskError(long taskId)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                return db.LongRunningTasks.First(t => t.Id == taskId).ErrorMessage;
            }
        }   
      


        internal static List<MessageViewModel> GetMessages(long taskId, long sinceId)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                return db.LongRunningTaskMessages.Where(m => m.TaskId == taskId && m.Id > sinceId)
                                                 .OrderBy(m => m.Id)
                                                 .ToList() //we convert to list here to avoid exception from calling IconFileForMessageType inside EF context.
                                                 .Select(m => new MessageViewModel()
                                                 {
                                                     Id = m.Id,
                                                     Text = $"{m.Context} {m.MessageType}: {m.Message}",
                                                     MessageType = m.MessageType,
                                                     IconName = IconFileForMessageType(m.MessageType)
                                                 }).ToList();
            }
        }      
       

        public static long DownloadImageToLocalHost(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);
            string baseFileName = System.IO.Path.GetFileNameWithoutExtension(uri.Segments.Last());
            string fileExt = System.IO.Path.GetExtension(uri.Segments.Last());

            //Download image data.
            var imageBytes = WebApiServices.DownloadImage(imageUrl);

            //Store data locally.
            var directory = ReferenceDataServices.ImageStorePhysicalPath;
            var path = System.IO.Path.Combine(directory, $"{baseFileName}_{Guid.NewGuid().ToString("N")}{fileExt}");
            System.IO.Directory.CreateDirectory(directory);
            System.IO.File.WriteAllBytes(path, imageBytes);

            using (var db = new OrangeEntities())
            {
                DateTime dateNow = DateTime.Now;
                var h = new HostedImage()
                {
                    DateCreated = dateNow,
                    DateLastModified = dateNow,
                    PhysicalPath = path,                   
                };
                db.HostedImages.Add(h);         
                db.SaveChanges();
                return h.Id;
            }               
        }

        public static long CopyHostedImage(long id)
        {
            using (var db = new OrangeEntities())
            {
                var original = db.HostedImages.First(x => x.Id == id);
                DateTime dateNow = DateTime.Now;
                var newImage = new HostedImage()
                {
                    DateCreated = dateNow,
                    DateLastModified = dateNow,
                    IsContentEmbedded = original.IsContentEmbedded,
                };
                if(original.IsContentEmbedded)
                {
                    newImage.EmbeddedContent = new byte[original.EmbeddedContent.Length];
                    Buffer.BlockCopy(original.EmbeddedContent, 0, newImage.EmbeddedContent, 0, original.EmbeddedContent.Length);
                }
                else
                {
                    var fileName = Path.GetFileNameWithoutExtension(original.PhysicalPath);
                    var baseFileName = fileName.Substring(fileName.IndexOf("_"));
                    var fileExt = System.IO.Path.GetExtension(original.PhysicalPath);
                    var directory = ReferenceDataServices.ImageStorePhysicalPath;
                    var path = System.IO.Path.Combine(directory, $"{baseFileName}_{Guid.NewGuid().ToString("N")}{fileExt}");
                    newImage.PhysicalPath = original.PhysicalPath;
                }
                db.HostedImages.Add(newImage);
                db.SaveChanges();
                return newImage.Id;
            }
        }

        public static void DeleteHostedImage(long id)
        {
            using (var db = new OrangeEntities())
            {
                var h = db.HostedImages.First(x => x.Id == id);                
                //NOTE: We only want to delete the physical file if this is the last entity that references it.
                if (!h.IsContentEmbedded &&
                     db.HostedImages.Count(x => x.PhysicalPath == h.PhysicalPath) == 1)
                {
                    string imageFilename = Path.GetFileName(h.PhysicalPath);
                    string fullPath = $"{ReferenceDataServices.ImageStorePhysicalPath}/{imageFilename}";
                    System.IO.File.Delete(fullPath);
                }
                db.HostedImages.Remove(h);
                db.SaveChanges();
            }
        }

        private static readonly string[] FullTextTSQLReservedWords = new[]
        {
            "AND", "&", "AND NOT", "&!", "OR", "|"
        };

        private static IEnumerable<BlogPost> GetBlogPostsByKeywords(int pageSize, int firstItemIndex, Guid? p0, string[] searchTerms, string sortKey, bool enableInflectionalSearch, out int totalMatches)
        {
            if (searchTerms != null)
            {
                using (OrangeEntities db = new OrangeEntities())
                {
                    string query = $"SELECT * FROM BlogPost WHERE (BlogId = @p0 OR @p0 IS NULL) AND CONTAINS ((Title, ProductKeywords), @p1)";

                    System.Text.StringBuilder containsParamBuilder = new System.Text.StringBuilder();
                    for (int i = 0; i < searchTerms.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerms[i]) &&
                            !FullTextTSQLReservedWords.Contains(searchTerms[i].ToUpper()))
                        {
                            //Join each search term with AND - (Meaning any record returned must contain all expressions).
                            if (containsParamBuilder.Length > 0)
                                containsParamBuilder.Append(" AND");
                            //If the search term consists of more than one token or contains a keyword recognized by the Full Text "CONTAINS" function,
                            //We surround the term with double quotes.
                            if (searchTerms[i].Contains(" "))
                                containsParamBuilder.Append($" \"{searchTerms[i]}\"");
                            //Otherwise, if variant search is enabled we search also search for variants of the the search term.
                            else if (enableInflectionalSearch)
                                containsParamBuilder.Append($" FORMSOF(INFLECTIONAL, \"{searchTerms[i]}\")");
                            //Otherwise
                            else
                                containsParamBuilder.Append($" {searchTerms[i]}");
                        }
                    }
                    if (containsParamBuilder.Length > 0)
                    {
                        string p1 = containsParamBuilder.ToString();                       
                        var results = db.Database.SqlQuery<BlogPost>(query, p0, p1);                                                 
                        totalMatches = results.Count();
                        return results.OrderByDescending(x => x.DateCreated)
                                      .Skip(firstItemIndex)
                                      .Take(pageSize)
                                      .ToList();
                    }
                }
            }
            totalMatches = 0;
            return new List<BlogPost>();
        }


        private static string IconFileForMessageType(string messageType)
        {
            switch (messageType)
            {
                case LongRunningTaskMessageType.Error:
                    return "Icon_Message_Error.gif";
                case LongRunningTaskMessageType.Info:
                    return "Icon_Message_Success.gif";
                case LongRunningTaskMessageType.Warning:
                    return "Icon_Message_Warning.gif";
                default:
                    return null;
            }
        }        
    }

    public enum ChangeResult
    {
        None = 0,
        Added,
        Removed,
        Updated,
        NoChange,            
        ContextNotFound,
    }
}