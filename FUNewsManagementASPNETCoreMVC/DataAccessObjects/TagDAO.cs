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
        public static Tag GetTagById(string tagID)
        {
            using var db = new FUNewsManagementContext();
            return db.Tags.FirstOrDefault(c => c.TagId.Equals(tagID));
        }
    }
}
