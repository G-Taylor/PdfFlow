using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using PdfFlow.Models;

namespace PdfFlow.Controllers;

public class PDFCreation
{
    private static string GetLogo(string logoSelection)
    {
        string logoFilePath = "/Users/gtaylor038/Documents/Logos/";
        string image = "";

        switch (logoSelection)
        {
            case "1":
                image = "PwC.jpeg";
                break;
            case "2":
                image = "Lloyds.png";
                break;
            case "3":
                image = "Santander.png";
                break;
            default:
                image = "PwC.jpeg";
                break;
        }

        return $"{logoFilePath}{image}";
    }

    public static void GeneratePdf(string textInput, string filePath, PdfModel pdf)
        {
            DocumentBuilder builder = DocumentBuilder.New();
            
            // Set page style
            var sectionBuilder =
                builder
                    .AddSection()
                    // Customize settings:
                    .SetMargins(horizontal: 50, vertical: 50)
                    .SetSize(PaperSize.A4)
                    .SetOrientation(PageOrientation.Portrait)
                    .SetNumerationStyle(NumerationStyle.Arabic);

            
            string image = GetLogo(pdf.Logo);
            sectionBuilder
                .AddImage(image)
                .SetScale(ScalingMode.None)
                .SetWidth(150);

            // Set address style to the top right of the page
            sectionBuilder
                .AddParagraph()
                .AddTabSymbol()
                .AddTextToParagraph(pdf.AddressLine1)
                .SetAlignment(HorizontalAlignment.Right)
                .ToSection()
                .AddParagraph()
                .AddTabSymbol()
                .AddTextToParagraph(pdf.AddressLine2)
                .SetAlignment(HorizontalAlignment.Right)
                .ToSection()
                .AddParagraph()
                .AddTabSymbol()
                .AddTextToParagraph(pdf.Postcode)
                .SetAlignment(HorizontalAlignment.Right);
            
            // Add 'Account Holder' with name below it
            sectionBuilder
                .AddParagraph()
                .SetMarginTop(15)
                .AddTextToParagraph("Account Holder")
                .SetFontColor(Color.Black)
                .SetBold()
                .SetAlignment(HorizontalAlignment.Left)
                .ToSection()
                .AddParagraph()
                .AddTabSymbol()
                .AddTextToParagraph(pdf.Name)
                .SetAlignment(HorizontalAlignment.Left);
            
            // Set main body of text
            sectionBuilder
                .AddParagraph(pdf.TextInput)
                .SetMarginTop(15)
                .SetFontColor(Color.Gray)
                .SetAlignment(HorizontalAlignment.Left)
                .SetOutline();
            
            RepeatingAreaBuilder footer = sectionBuilder.AddFooterToBothPages(40f);
            footer
                .AddParagraph()
                .SetAlignment(HorizontalAlignment.Right)
                .AddPageNumber(customText: "Page ");
            
            // Build the pdf and save to local machine
            builder.Build($"{filePath}.pdf");
        }
}