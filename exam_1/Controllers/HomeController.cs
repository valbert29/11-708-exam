using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using exam_1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace exam_1.Controllers
{
    public class HomeController : Controller
    {
        DbCnxt db;
        IHostingEnvironment _appEnvironment;

        public HomeController(DbCnxt context, IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        public IActionResult DownloadPage()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(IFormFileCollection files, string ShortDes, string LongDes)
        {
            foreach(var file in files)
            {
                if (file != null)
                {
                    var path = "/Files/" + file.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var newFile = new FileU { Name = file.FileName, ShortDescription = ShortDes, FullDescription = LongDes, Link = "/Files/"+file.FileName};
                    db.Files.Add(newFile);
                }
            }            
            db.SaveChanges();
            return RedirectToAction("DownloadPage");
        }

        public async Task<IActionResult> AllFiles()
        {
            IQueryable<FileU> files = db.Files;
            return View(await files.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFile = await db.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFile == null)
            {
                return NotFound();
            }

            return View(userFile);
        }

        public FileResult Download(int Id)
        {
            var file = db.Files.Find(Id);
            var filename = file.Link;
            var path = _appEnvironment.WebRootPath + filename;
            // Объект Stream
            var name = filename.Replace("/Files/", "");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/txt";
            return File(fs, file_type, name);
        }

        public void CheckPass(int Id, string tx)
        {
            var file = db.Files.Find(Id);
            if (file.Pass == tx)
            {
                Download(Id);
            }
        }
    }
}
