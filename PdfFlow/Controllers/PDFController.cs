using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfFlow.Data;
using PdfFlow.Models;
using PdfFlow.ViewModels;


namespace PdfFlow.Controllers
{
    public class PDFController : Controller
    {

        private ApplicationDbContext _dbContext;
        private static readonly string FilePath = "/Users/gtaylor038/Downloads/";
        
        public PDFController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }
        
        [HttpGet("/api/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var pdf = _dbContext.PdfModels.Find(id);
            if(pdf == null)
            {
                return NotFound("No entry found for this id...");
            }
            else
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream($"{FilePath}{id}.pdf", FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, "application/pdf");
            }
        }
        
        [HttpPost]
        public ActionResult Details(DetailsFormViewModel viewModel)
        {
            return Ok();
        }

    }
}
