using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        // IEnumerable<Post> Posts = _db.Posts.FromSqlRaw("SELECT * FROM dbo.Posts ORDER BY Id DESC").ToList();
        IEnumerable<Post> Posts = _db.Posts.OrderByDescending(post => post.Id).ToList();
    
   

        return View(Posts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
