using System.Collections.Generic;
using Journal.BusinessLogics.Interfaces;
using Journal.Entities;

namespace Journal.BusinessLogics
{
    public class PostBusinessLogic : IPostBusinessLogic
    {
        public void DeletePost(Post post)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetByAuthor(long userId)
        {
            throw new System.NotImplementedException();
        }

        public Post GetById(long postId)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetByTag(List<long> tagId)
        {
            throw new System.NotImplementedException();
        }

        public void InsertPost(Post post)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new System.NotImplementedException();
        }
    }
}