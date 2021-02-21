using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCode.Domain.Posts;

namespace TestCode.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(string postId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(string postId);
        Task<bool> UserOwnsPost(string postId, string userId);
    }
}
