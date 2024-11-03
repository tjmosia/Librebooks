using PdfSharp.Drawing;
using PdfSharp.Pdf;
// THIS IS A SAMPLE CODE.


Console.WriteLine("Hello, World!");
PdfDocument document = new PdfDocument();
document.Info.Title = "Created with PDFsharp";
PdfPage page = document.AddPage();

XGraphics gfx = XGraphics.FromPdfPage(page);
XFont font = new XFont("Verdana", 20, XFontStyleEx.BoldItalic);

gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point),

XStringFormats.Center);

const string filename = "HelloWorld.pdf";
document.Save(filename);

