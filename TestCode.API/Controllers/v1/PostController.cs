using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCode.Contract.v1;
using TestCode.Contract.v1.Requests;
using TestCode.Contract.v1.Responses;
using TestCode.Extension;
using TestCode.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCode.API.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;          
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Post.GetPosts)]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Post.GetPostById)]
        public async Task<IActionResult> GetPostById([FromRoute] string Id)
        {
            if(string.IsNullOrWhiteSpace(Id))
            {
                return BadRequest(new { error = "Please enter a valid post Id" });
            }

            var result = await _postService.GetPostByIdAsync(Id);
            if(result!=null)
            {
                return Ok(result);
            }

            return NotFound();
            
        }

        [Authorize]
        [HttpPost(ApiRoutes.Post.CreatePost)]
        public async Task<IActionResult> CreatePost([FromBody] PostInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                var Id = Guid.NewGuid().ToString();
                Domain.Posts.Post post = new Domain.Posts.Post()
                {
                    Id = Id,
                    UserId = _httpContextAccessor.HttpContext.GetUserId(),
                    Title = model.Title
                };

                var result = await _postService.CreatePostAsync(post);
                if (result)
                {
                    return CreatedAtAction(nameof(GetPostById), new { Id = post.Id }, post);
                    //return CreatedAtRoute(
                    //        routeName: "GetPostById",
                    //        routeValues: new { id = post.Id },
                    //        value: post);
                }
                else
                {
                    return BadRequest(new { error = "Something went wrong." });
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Something went wrong." });
            }
        }

        [Authorize]
        [HttpPut(ApiRoutes.Post.UpdatePost)]
        public async Task<IActionResult> UpdatePost([FromRoute] string Id, [FromBody] PostInputModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { error = "Post object is null" });                   
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid Post object" });
                }

                var post = await _postService.GetPostByIdAsync(Id);
                if(post == null)
                {
                    return BadRequest(new { error = "Invalid Post Id" });
                }

                post.Title = model.Title;
                await _postService.UpdatePostAsync(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Something went wrong" });
            }
        }

        [Authorize]
        [HttpDelete(ApiRoutes.Post.DeletePost)]
        public async Task<IActionResult> DeletePost([FromRoute] string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return BadRequest(new { error = "Please enter a valid post Id" });
            }

            var UserId = _httpContextAccessor.HttpContext.GetUserId();

            bool IsUserOwnPost = await _postService.UserOwnsPost(Id, UserId);
            if(IsUserOwnPost)
            {
                var result = await _postService.DeletePostAsync(Id);
                if(!result)
                {
                    return BadRequest(new { error = "Post was not deleted." });
                }
            }
            else
            {
                return BadRequest(new { error = "You do not own this Post" });
            }

            return NoContent();
        }        
    }
}
