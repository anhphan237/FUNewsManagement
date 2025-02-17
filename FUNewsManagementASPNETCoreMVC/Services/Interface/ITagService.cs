using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITagService
    {
        void SaveTag(Tag a);
        void DeleteTag(Tag a);
        void UpdateTag(Tag a);
        List<Tag> GetTags();
        Tag GetTagById(string TagID);
    }
}
