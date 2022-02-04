using System.Collections.Specialized;
using System.Diagnostics;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
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
                var fileExtension = pdf.FilePath;
                var memory = new MemoryStream();
                using (var stream = new FileStream($"{fileExtension}", FileMode.Open))
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
            var fileNameExtension = Guid.NewGuid();
            var pdf = new PdfModel
            {
                TextInput = viewModel.TextInput,
                FilePath = $"{FilePath}{fileNameExtension}.pdf",
            };
            
            _dbContext.PdfModels.Add(pdf);
            _dbContext.SaveChanges();

            var fullFileName = $"{FilePath}{fileNameExtension}";
            GeneratePDF(viewModel.TextInput, fullFileName);
            return RedirectToAction("Details", "PDF");
        }

        private void GeneratePDF(string textInput, string filePath)
        {
            DocumentBuilder builder = DocumentBuilder.New();
            
            var sectionBuilder =
                builder
                    .AddSection()
                    // Customize settings:
                    .SetMargins(horizontal: 30, vertical: 10)
                    .SetSize(PaperSize.A4)
                    .SetOrientation(PageOrientation.Portrait)
                    .SetNumerationStyle(NumerationStyle.Arabic);
            
            sectionBuilder
                .AddParagraph(textInput)
                .SetMarginTop(15)
                .SetFontColor(Color.Gray)
                .SetAlignment(HorizontalAlignment.Center)
                .SetOutline();
            
            builder.Build($"{filePath}.pdf");
        }
    }
}
