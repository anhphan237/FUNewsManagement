using BusinessObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService()
        {
            _tagRepository = new TagRepository();
        }

        public void DeleteTag(Tag a) => _tagRepository.DeleteTag(a);

        public Tag GetTagById(string TagID) => _tagRepository.GetTagById(TagID);

        public List<Tag> GetTags() => _tagRepository.GetTags();

        public void SaveTag(Tag a) => _tagRepository.SaveTag(a);

        public void UpdateTag(Tag a) => _tagRepository.UpdateTag(a);
    }
}
