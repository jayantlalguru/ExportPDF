using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

namespace ExportPDF
{
    public class PDFCreator
    {
        public static void DemoTableSpacing()
        {
            using (FileStream fs = new FileStream(@"D:\Work\Projects\DotNet\PracticeProjects\ExportToPDFTest\ExportToPDFTest\Pdfs\SpacingTest.pdf", FileMode.Create))
            {

                Document doc = new Document();
                PdfWriter.GetInstance(doc, fs);
                doc.Open();
                
                Paragraph paragraphTable1 = new Paragraph();
                paragraphTable1.SpacingAfter = 80f;
                PdfPTable table = new PdfPTable(3);
                PdfPCell cell = new PdfPCell(new Phrase());
                cell.Colspan = 3;
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                //table.AddCell("Col 1 Row 1");
                //table.AddCell("Col 2 Row 1");
                //table.AddCell("Col 3 Row 1");
                //table.AddCell("Col 1 Row 2"); 
                //table.AddCell("Col 2 Row 2"); 
                //table.AddCell("Col 3 Row 2"); 
                paragraphTable1.Add(table);
                doc.Add(paragraphTable1);

                Paragraph paragraphTable2 = new Paragraph();
                paragraphTable2.SpacingAfter = 50f;

                table = new PdfPTable(3);
                cell = new PdfPCell(new Phrase("This is table 2"));
                cell.Colspan = 3;
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                table.AddCell("Col 1 Row 1");
                table.AddCell("Col 2 Row 1");
                table.AddCell("Col 3 Row 1");
                table.AddCell("Col 1 Row 2");
                table.AddCell("Col 2 Row 2");
                table.AddCell("Col 3 Row 2");
                paragraphTable2.Add(table);
                doc.Add(paragraphTable2);
                doc.Close();
            }
        }
        public static void CreatePdf()
        {
            System.IO.FileStream fs = new FileStream(@"D:\Work\Projects\DotNet\PracticeProjects\ExportToPDFTest\ExportToPDFTest\Pdfs\FirstPDFdocument.pdf", FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF 
            // Writer class using the document and the filestrem in the constructor.
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            // Add meta information to the document

            document.AddAuthor("Jayant Guru");
            document.AddCreator("Sample application using iTextSharp");
            document.AddKeywords("PDF tutorial education");
            document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("The document title - PDF creation using iTextSharp");
            // Open the document to enable you to write to the document
            document.Open();
            // Add a simple and wellknown phrase to the document in a flow layout manner
            document.Add(new Paragraph("Hello World!"));
            // Close the document
            document.Close();
            // Close the writer instance
            writer.Close();
            // Always close open filehandles explicity
            fs.Close();
        }

        public static void Create(DataTable dataTable, string destinationPath)
        {
            Document document = new Document(PageSize.A0);
            //Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationPath, FileMode.Create));            
            document.Open();

            PdfPTable tableHeader = new PdfPTable(1);
            Paragraph paragraphTableHeader = new Paragraph();
            paragraphTableHeader.SpacingAfter = 80f;            
            PdfPCell cellHeader = new PdfPCell(new Phrase("              "));
            cellHeader.BorderWidth = 0F;
            //cell0.Colspan = 3;
            cellHeader.HorizontalAlignment = 1;
            tableHeader.AddCell(cellHeader);
            //table0.AddCell("Col 1 Row 1");
            //table0.AddCell("Col 2 Row 1");
            //table0.AddCell("Col 3 Row 1");
            paragraphTableHeader.Add(tableHeader);
            document.Add(paragraphTableHeader);

            //Adding Image
            PdfContentByte cb = writer.DirectContent;
            Image png = Image.GetInstance(ConfigurationManager.AppSettings["LogoPath"]);
            png.ScaleAbsolute(200, 55);
            png.SetAbsolutePosition(40, 3250);
            cb.AddImage(png);

            TwoColumnHeaderFooter twoColumnHeaderFooter = new TwoColumnHeaderFooter();
            twoColumnHeaderFooter.Title = "Transaction Detailed Report";
            twoColumnHeaderFooter.OnOpenDocument(writer, document);
            twoColumnHeaderFooter.OnStartPage(writer, document);

            Font fontH1 = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL);
            Font fontH2 = new Font(Font.FontFamily.COURIER, 7, Font.BOLD, new BaseColor(249, 249, 249));

            /*Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingAfter = 100f;
            PdfPTable table0 = new PdfPTable(3);
            PdfPCell cell0 = new PdfPCell(new Phrase());
            //cell0.Colspan = 3;
            cell0.HorizontalAlignment = 1;
            table0.AddCell(cell0);
            table0.AddCell("Col 1 Row 1");
            table0.AddCell("Col 2 Row 1");
            table0.AddCell("Col 3 Row 1");
            paragraphTable1.Add(table0);
            document.Add(paragraphTable1);*/
            
            Paragraph paragraphTable = new Paragraph();            
            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            table.WidthPercentage = 100;            
            //Set columns names in the pdf file
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[k].ColumnName, fontH2));
                cell.BorderWidth = 00.1F;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(5, 48, 117);                
                table.AddCell(cell);                
            }
            
            
            //Add values of DataTable in pdf file
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString(), fontH1));
                    cell.BorderWidth = 00.1F;
                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;                    
                    table.AddCell(cell);                    
                }
            }
            paragraphTable.Add(table);
            document.Add(paragraphTable);
            //document.Add(table);
            //Footer footer = new Footer();
            //footer.OnEndPage(writer, document);
            twoColumnHeaderFooter.OnEndPage(writer, document);
            document.Close();
        }
    }

    public class TwoColumnHeaderFooter : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;
        // we will put the final number of pages in a template
        PdfTemplate template;
        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;
        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        #region Properties
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _HeaderLeft;
        public string HeaderLeft
        {
            get { return _HeaderLeft; }
            set { _HeaderLeft = value; }
        }
        private string _HeaderRight;
        public string HeaderRight
        {
            get { return _HeaderRight; }
            set { _HeaderRight = value; }
        }
        private Font _HeaderFont;
        public Font HeaderFont
        {
            get { return _HeaderFont; }
            set { _HeaderFont = value; }
        }
        private Font _FooterFont;
        public Font FooterFont
        {
            get { return _FooterFont; }
            set { _FooterFont = value; }
        }
        #endregion
        // we override the onOpenDocument method
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
                throw de;
            }
            catch (System.IO.IOException ioe)
            {
                throw ioe;
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            Rectangle pageSize = document.PageSize;           
            if (Title != string.Empty)
            {
                //cb = new PdfContentByte(writer);
                cb.BeginText();
                cb.SetFontAndSize(bf, 15);
                cb.SetRGBColorFill(50, 50, 200);
                cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                cb.ShowText(Title);
                cb.EndText();
            }
            if (HeaderLeft + HeaderRight != string.Empty)
            {
                PdfPTable HeaderTable = new PdfPTable(2);
                HeaderTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                HeaderTable.TotalWidth = pageSize.Width - 80;
                HeaderTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);

                PdfPCell HeaderLeftCell = new PdfPCell(new Phrase(8, HeaderLeft, HeaderFont));
                HeaderLeftCell.Padding = 5;
                HeaderLeftCell.PaddingBottom = 8;
                HeaderLeftCell.BorderWidthRight = 0;
                HeaderTable.AddCell(HeaderLeftCell);
                PdfPCell HeaderRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
                HeaderRightCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                HeaderRightCell.Padding = 5;
                HeaderRightCell.PaddingBottom = 8;
                HeaderRightCell.BorderWidthLeft = 0;
                HeaderTable.AddCell(HeaderRightCell);
                cb.SetRGBColorFill(0, 0, 0);
                HeaderTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(50), cb);
            }
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = ConfigurationManager.AppSettings["Disclaimer"];// "Page " + pageN + " of ";
            float len = bf.GetWidthPoint(text, 8);
            Rectangle pageSize = document.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(100));
            cb.ShowText(text);
            cb.EndText();
            //cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));
            
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
            //    "Printed On " + PrintTime.ToString(),
            //    pageSize.GetRight(40),
            //    pageSize.GetBottom(30), 0);
            cb.EndText();
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }
    }

    /*public partial class Footer : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            Paragraph footer = new Paragraph(ConfigurationManager.AppSettings["Disclaimer"], FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));
            footer.Alignment = Element.ALIGN_LEFT;
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = 1000;
            footerTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            footerTbl.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
            PdfPCell cell = new PdfPCell(footer);
            cell.Border = 1;
            cell.PaddingLeft = 10;

            footerTbl.AddCell(cell);
            footerTbl.WriteSelectedRows(0, -1, 0, 70, writer.DirectContent);
        }
    }*/
}
