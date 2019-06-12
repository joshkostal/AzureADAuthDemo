using FabrikamBackend.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FabrikamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET api/blog
        [HttpGet]
        public List<(string, string, string)> GetBlog()
        {
            var dataAccess = new DatabaseAccess();
            return dataAccess.GetBlogPosts();
        }
        
        // POST api/blog
        [HttpPost]
        [Authorize]
        public string PostBlog()
        {
            string title = Request.Form["title"];
            string image = Request.Form["image"];
            string body = Request.Form["body"];

            var dataAccess = new DatabaseAccess();
            if (dataAccess.AddBlogPost(title, image, body))
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }
    }
}