using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<bool> CreatePostAsync(Post post);
        Task<Post> GetPostById(Guid postId);
        Task<bool> UpdatePost(Post postToUpdate);
        Task<bool> DeletePost(Guid postId);

    }
}