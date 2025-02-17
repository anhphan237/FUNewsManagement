using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class TagDAO
    {
        public static List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            try
            {
                using var db = new FUNewsManagementContext();
                tags = db.Tags.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return tags;
        }

        public static void SaveTag(Tag a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Tags.Add(a);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateTags(Tag a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Entry<Tag>(a).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteTag(Tag a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Remove(a);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Tag GetTagById(string tagID)
        {
            using var db = new FUNewsManagementContext();
            return db.Tags.FirstOrDefault(c => c.TagId.Equals(tagID));
        }
    }
}
