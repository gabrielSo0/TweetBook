using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Data;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);

            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<Post> GetPostById(Guid postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        }
        public async Task<bool> UpdatePost(Post postToUpdate)
        {
            _context.Posts.Update(postToUpdate);
            var updated = await _context.SaveChangesAsync();;

            return updated > 0;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var post = await GetPostById(postId);

            if(post == null)
                return false;

            _context.Posts.Remove(post);

            var deleted = await _context.SaveChangesAsync();
            
            return deleted > 0;
        }
    }
}