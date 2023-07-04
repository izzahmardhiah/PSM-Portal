
Thank you for using Select.Pdf Html To Pdf Converter for .NET Core - Community Edition. This is a package for .NET Core / .NET 5 / .NET 6 / .NET 4.6.1 / .NET 4.7.2.

Online demo C#: https://selectpdf.com/html-to-pdf/demo/
Online demo Vb.Net: https://selectpdf.com/html-to-pdf/demo-vb/
Online documentation: https://selectpdf.com/html-to-pdf/docs/


With Select.Pdf is very easy to convert any web page to a pdf document. The code is as simple as this:

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl("https://selectpdf.com");
            doc.Save("test.pdf");
            doc.Close();

IMPORTANT: Please note that THIS IS NOT A FREE TRIAL of our commercial library. This is a different, FREE product, that contains less features than the full library. 
If you want to test our full SelectPdf Library for .NET use one of the following urls:

https://selectpdf.com/pdf-library-for-net/
https://www.nuget.org/packages/Select.Pdf.NetCore/
https://www.nuget.org/packages/Select.Pdf/


Installation troubleshooting
============================

When SelectPdf HtmlToPdf Converter is installed into your project, a reference to Select.HtmlToPdf.dll is created. Besides this assembly, several additional files are needed by SelectPdf:
- Select.Html.dep (required when the Webkit rendering engine is used for html to pdf conversions)
- Chromium-88.0.4324.0 folder (required when the Blink rendering engine is used for html to pdf conversions) - if this is needed, install the Select.HtmlToPdf.NetCore.Blink Nuget package.


If you are using a newer NuGet client (newer Visual Studio - 2017+) and a newer project type that uses PackageReference, these files are automatically copied to your project.
If you are using older Visual Studio versions or older project types, these files will not be copied automatically to your project.

IMPORTANT: If you do not see these files into your project, you need to manually copy them from the packages folder located next to your solution. 
You will find them in \packages\Select.HtmlToPdf.NetCore.21.1.0\contentFiles\any\any\ folder. Copy all the files into your project bin folder (\bin\Release, \bin\Debug), next to SelectPdf library main assembly - Select.HtmlToPdf.dll.

IMPORTANT: To have these files included when you publish or deploy your application, set in Visual Studio:
- Build Action: Content
- Copy to Output Directory: Copy if newer


For complete product information, take a look at https://selectpdf.com.
For support, contact us at support@selectpdf.com.
