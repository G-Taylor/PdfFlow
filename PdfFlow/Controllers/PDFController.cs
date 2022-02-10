using System.Collections.Specialized;
using System.Diagnostics;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using Gehtsoft.PDFFlow.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders.Physical;
using PdfFlow.Data;
using PdfFlow.Models;
using PdfFlow.ViewModels;
using static PdfFlow.Controllers.PDFCreation;


namespace PdfFlow.Controllers
{
    public class PDFController : Controller
    {

        private ApplicationDbContext _dbContext;

        public PDFController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /*
         * Loads the Enter Details page, and populates the Logo
         * dropdown with data from the LogoModels table
         */
        [HttpGet]
        public IActionResult Details()
        {
            var viewModel = new DetailsFormViewModel()
            {
                Logos = _dbContext.LogoModels.ToList()
            };
            return View(viewModel);
        }
        
        /*
         * Get Request for the API, that takes an integer Id value.
         * The database is queried for an entry with that Id, and if one is found then the associated PDF
         * is returned and opened in the browser
         */
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
                var fileLocation = pdf.FilePath;
                var memory = new MemoryStream();
                using (var stream = new FileStream($"{fileLocation}", FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, "application/pdf");
            }
        }
        
        /*
         * Post Request for application via the Enter Details page.
         * Takes data from input fields, saves it to the DB, and then generates a PDF
         */
        [HttpPost]
        public ActionResult Details(DetailsFormViewModel viewModel)
        {
            var uniqueId = Guid.NewGuid();
            var pdf = new PdfModel
            {
                Name = viewModel.Name,
                AddressLine1 = viewModel.AddressLine1,
                AddressLine2 = viewModel.AddressLine2,
                Postcode = viewModel.Postcode,
                TextInput = viewModel.TextInput,
                FilePath = $"{uniqueId}.pdf",
                Logo = viewModel.Logo
            };
            
            _dbContext.PdfModels.Add(pdf);
            _dbContext.SaveChanges();
            
            GeneratePdf(pdf);
            return RedirectToAction("Details", "PDF");
        }
    }
}
