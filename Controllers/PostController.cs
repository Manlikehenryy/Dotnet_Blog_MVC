using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mvc.Data;
using mvc.Models;


namespace mvc.Controllers
{
    
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHost;

        public PostController(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            _db = db;
            _webHost = webHost;
        }

        public IActionResult SinglePost(int? Id){
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var post = _db.Posts.Find(Id);

            if (post == null)
            {
                return NotFound();
            }

            // IEnumerable<Comment> Comments = _db.Comments.FromSqlRaw($"SELECT * FROM dbo.Comments WHERE PostId = {Id}").ToList();
            IEnumerable<Comment> Comments = _db.Comments.Where(comment => comment.PostId == Id).ToList();

            ViewData["Category"] = post.Category;
            ViewData["FilePath"] = post.FilePath;
            ViewData["Content"] = post.Content;
            ViewData["PostTitle"] = post.Title;
            ViewData["CreatedAt"] = post.CreatedAt;
            ViewData["Author"] = post.Author;
            ViewData["PostId"] = post.Id;
            return View(Comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult SinglePost(int PostId, string Author, string Comment_){
           
           try
           {
             if (PostId != null || Author != null || Comment_ != null)
             {
                _db.Comments.Add(
                new Comment{
                   PostId = PostId,
                   Author = Author,
                   Comment_ = Comment_,
                   CreatedAt = DateTime.UtcNow
                }
            );
            _db.SaveChanges();

            TempData["success"] = "Comment added successfully";
             }

             TempData["error"] = "Missing required field(s)";

            return RedirectToAction("SinglePost");
           }
           catch (Exception)
           {
            TempData["error"] = "Something went wrong, please try again later";
            return RedirectToAction("SinglePost");
           }
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index(){
            // _db.Posts.FromSqlRaw("");
            // _db.Database.ExecuteSqlRaw("");
        // _db.Posts.Where((x)=>x.Contains(SearchString)).ToList();
        IEnumerable<Post> posts = _db.Posts.Where((x)=>x.Author == "").ToList();
        return View(posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,Post post)
        {
            try
            {

            if (file == null)
            {
               TempData["error"] = "Upload an image";
                return View(post); 
            }
           
            
            if (ModelState.IsValid)
            {
               
               string UploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

            if (!Directory.Exists(UploadsFolder))
            {
                Directory.CreateDirectory(UploadsFolder);
            }

            string FileName = Path.GetFileName(file.FileName);
            string FileSavePath = Path.Combine(UploadsFolder, FileName);

            using (FileStream Stream = new FileStream(FileSavePath, FileMode.Create))
            {
              await file.CopyToAsync(Stream);
             
            }

             
            
            post.FilePath = $"uploads/{FileName}";
            post.CreatedAt = DateTime.UtcNow;
            
            _db.Posts.Add(post);
      
         
            _db.SaveChanges();
            TempData["success"] = "Post created successfully";
             
             return RedirectToAction("Create");
            }

             TempData["error"] = "Missing required field(s)";
             return View(post);
            }
            catch (Exception e)
            {
                // e.Message
                TempData["error"] = "Something went wrong, please try again later";
                return View(post); 
            }
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}