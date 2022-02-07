using System.ComponentModel.DataAnnotations;

namespace PdfFlow.Models;

public class PdfModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    public string TextInput { get; set; }
    public string FilePath { get; set; }
    
    [Required]
    [StringLength(100)]
    public string AddressLine1 { get; set; }
    
    [Required]
    [StringLength(100)]
    public string AddressLine2 { get; set; }
    
    [Required]
    [StringLength(10)]
    public string Postcode { get; set; }

    public string Logo { get; set; }
}