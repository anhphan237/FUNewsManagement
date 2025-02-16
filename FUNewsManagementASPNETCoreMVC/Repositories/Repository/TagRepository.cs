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
        public Tag GetTagById(string tagID) => TagDAO.GetTagById(tagID);
    }
}
