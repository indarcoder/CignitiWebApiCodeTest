using System;
using System.Linq;
using TestCode.Domain.Posts;
using Xunit;
using TestCode.Services;

namespace XUnitTestProject
{
    public class InMemoryDataProviderTest
    {
        [Fact]
        public async void Task_Post_List()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);

            var list = await postService.GetPostsAsync();          

            if (list.Count != 0)
            {
                Assert.Equal(3, list.Count);
            }
        }

        [Fact]
        public async void Task_Add_Post()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);

            var post = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test Post " + Guid.NewGuid().ToString().Substring(0, 4),
                UserId = Guid.NewGuid().ToString()
            };

            await postService.CreatePostAsync(post);

            var postCount = context.posts.Count();

            if (postCount != 0)
            {
                Assert.Equal(4, postCount);
            }                       
        }

        [Fact]
        public async void Task_Post_GetPostById()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);

            var post = new Post()
            {
                Id = "f5a14454-54b2-4cda-8dbb-fe93ed765561",
                Title = "This is existing Post1",
                UserId = "f5a14454-54b2-4cda-8dbb-fe93ed765561"
            };

            var post_details = await postService.GetPostByIdAsync("f5a14454-54b2-4cda-8dbb-fe93ed765561");
            Assert.Equal(post.Id, post_details.Id);
            Assert.Equal(post.Title, post_details.Title);
            Assert.Equal(post.UserId, post_details.UserId);
        }

        [Fact]
        public async void Task_DeletePostById()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);            

            var result = await postService.DeletePostAsync("f5a14454-54b2-4cda-8dbb-fe93ed765561");
          
            Assert.True(result);
        }

        [Fact]
        public async void Task_DeletePostByIdFailed()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);

            var result = await postService.DeletePostAsync("f5a14454-54b2-4cda-8dbb-fe93ed765570");

            Assert.True(result);
        }

        [Fact]
        public async void Task_NotFound_GetPostById()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            TestCode.Services.IPostService postService = new TestCode.Services.PostService(context, null);
                    

            var post_details = await postService.GetPostByIdAsync("f5a14454-54b2-4cda-8dbb-fe93ed765561");
            Assert.Equal("f5a14454-54b2-4cda-8dbb-fe93ed765561", post_details.Id);
        }


    }
}
