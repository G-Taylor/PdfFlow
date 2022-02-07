using PdfFlow.Models;

namespace PdfFlow.ViewModels;

public class DetailsFormViewModel
{
    public string Name { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Postcode { get; set; }
    public string TextInput { get; set; }
    public string FilePath { get; set; }
    public string Message { get; set; }
    public string Logo { get; set; }

    public IEnumerable<LogoModel> Logos { get; set; }
}