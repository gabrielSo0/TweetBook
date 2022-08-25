using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contract.v1;
using Tweetbook.Contract.v1.Requests;
using Tweetbook.Contract.v1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.v1
{
    public class PostsController : Controller
    {
        
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = await _postService.GetPostById(postId);

            if(post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid postId, [FromBody]UpdatePostRequest request)
        {

            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postService.UpdatePost(post);

            if(updated)
                return Ok(post);
        
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId)
        {
            var deleted = await _postService.DeletePost(postId);

            if(deleted)
                return NoContent();
            
            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Name = postRequest.Name };

            await _postService.CreatePostAsync(post);

            var baseURL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseURL + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(locationUri, response);
        }
    }
}