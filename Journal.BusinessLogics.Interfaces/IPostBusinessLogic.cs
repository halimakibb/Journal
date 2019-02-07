using System.Collections.Generic;
using Journal.Entities;

namespace Journal.BusinessLogics.Interfaces
{
    public interface IPostBusinessLogic
    {
        List<Post> GetAll();
        List<Post> GetByAuthor(long userId);
        Post GetById(long postId);
        List<Post> GetByTag(List<long> tagId);
        void InsertPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);


    }
}