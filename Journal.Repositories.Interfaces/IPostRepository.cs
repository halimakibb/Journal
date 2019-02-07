using System.Collections.Generic;
using Journal.Entities;

namespace Journal.Repositories.Interfaces
{
    public interface IPostRepository
    {
         List<Post> GetAll();
         Post GetById(long postId);
         List<Post> GetByAuthor(long userId);
         List<Post> GetByTag(string tagId);
         void InsertPost(Post post);
         void UpdatePost(Post post);
         void DeletePost(Post post);
         
    }
}