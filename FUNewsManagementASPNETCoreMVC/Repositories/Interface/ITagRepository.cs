using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ITagRepository
    {
        void SaveTag(Tag a);
        void DeleteTag(Tag a);
        void UpdateTag(Tag a);
        List<Tag> GetTags();
        Tag GetTagById(string tagID);
    }
}
