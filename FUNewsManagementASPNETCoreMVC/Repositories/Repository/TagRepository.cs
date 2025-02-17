using BusinessObjects;
using DataAccessObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class TagRepository : ITagRepository
    {
        public void DeleteTag(Tag a) => TagDAO.DeleteTag(a);

        public Tag GetTagById(string tagID) => TagDAO.GetTagById(tagID);

        public List<Tag> GetTags() => TagDAO.GetTags();

        public void SaveTag(Tag a) => TagDAO.SaveTag(a);

        public void UpdateTag(Tag a) => TagDAO.UpdateTags(a); 
    }
}
