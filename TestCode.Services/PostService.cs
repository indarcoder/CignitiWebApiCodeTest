using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCode.Data;
using TestCode.Domain.Posts;

namespace TestCode.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;

            var NumberOfItems = _dataContext.posts.Count();
            if(NumberOfItems == 0)
            {                
                _dataContext.posts.Add(new Post()
                {
                    Id = "f5a14454-54b2-4cda-8dbb-fe93ed765561",
                    Title = "This is existing Post1",
                    UserId = "f5a14454-54b2-4cda-8dbb-fe93ed765561"
                });
                _dataContext.posts.Add(new Post()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "This is existing Post2",
                    UserId = "f5a14454-54b2-4cda-8dbb-fe93ed765561"
                });
                _dataContext.posts.Add(new Post()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "This is existing Post3",
                    UserId = "f5a14454-54b2-4cda-8dbb-fe93ed765561"
                });
                _dataContext.SaveChanges();
            }
        }
        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.posts.ToListAsync();
        }
        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<Post> GetPostByIdAsync(string postId)
        {
            return await _dataContext.posts.SingleOrDefaultAsync(x => x.Id == postId);
        }
        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.posts.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> DeletePostAsync(string postId)
        {
            var post = await GetPostByIdAsync(postId);
            if (post == null)
                return false;


            _dataContext.posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();


            return deleted > 0;
        }
        public async Task<bool> UserOwnsPost(string postId, string userId)
        {
            var post = await _dataContext.posts.SingleOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return false;
            }
            if (post.UserId != userId)
            {
                return false;
            }
            return true;

        }
    }
}
