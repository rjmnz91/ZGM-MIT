using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace AVE
{
    public partial class ImprimirTicket : CLS.Cls_Session 
    {

        float startY = 100;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.LocalReport.ReportPath = string.Format("{0}.rdlc", Request.QueryString["Tipo"]);
                ReportViewer1.LocalReport.Refresh();

                dsCabecera.ConnectionString = ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                dsCabecera.SelectCommand = string.Format("select * from VW_AVE_TICKET_CABECERA where id_Ticket = '{0}'", Request.QueryString["idTicket"].ToString() );
                dsCabecera.DataBind();

                dsLineas.ConnectionString = ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;
                dsLineas.SelectCommand = string.Format("SELECT * FROM PR_VIEW_TICKET_{0}", Session.SessionID.ToString());
                dsLineas.DataBind();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PrinterSettings prn = new PrinterSettings();
            PrintDirect(ReportViewer1.LocalReport, 8, startY, prn.PrinterName);
        }

        public void PrintDirect(LocalReport LocalReport, Double WidthInInCM, Double HeightInInCM, String PrinterName)
        {
            String deviceInfo = "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>" + WidthInInCM + "cm</PageWidth>" +
              "  <PageHeight>" + HeightInInCM + "cm</PageHeight>" +
              "  <MarginTop>0.0cm</MarginTop>" +
              "  <MarginLeft>0.0cm</MarginLeft>" +
              "  <MarginRight>0.0cm</MarginRight>" +
              "  <MarginBottom>0.0cm</MarginBottom>" +
              "</DeviceInfo>";

            IList<Stream> m_streams;

            m_streams = new List<Stream>();
            Byte[] bytes;
            String fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(),"f.emf");
            bytes = LocalReport.Render("Image", deviceInfo);
            if (File.Exists(fileName))
                File.Delete(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            MemoryStream ms = new MemoryStream(bytes);
            m_streams.Add(ms);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if ((m_streams == null) || (m_streams.Count == 0))
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = PrinterName;

            PaperSize pkCustomSize = new System.Drawing.Printing.PaperSize("Custom Paper Size", Convert.ToInt32((WidthInInCM / 2.54) * 100), Convert.ToInt32((HeightInInCM / 2.54) * 100));
            printDoc.DefaultPageSettings.PaperSize = pkCustomSize;
            printDoc.DefaultPageSettings.Margins.Top = 0;
            printDoc.DefaultPageSettings.Margins.Bottom = 0;
            printDoc.DefaultPageSettings.Margins.Left = 0;
            printDoc.DefaultPageSettings.Margins.Right = 0;

            if (!printDoc.PrinterSettings.IsValid)
            {
                String msg = String.Format("Can't find printer \"{0}\".", PrinterName);
                //MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrinterSettings.DefaultPageSettings.PaperSize = printDoc.DefaultPageSettings.PaperSize;
            printDoc.PrinterSettings.DefaultPageSettings.Margins = printDoc.DefaultPageSettings.Margins;
            if (m_streams.Count > 0)
                ViewState["pageImage"] = new Metafile(m_streams[0]);

            printDoc.PrintPage += new PrintPageEventHandler(this.PrintPage);

            printDoc.Print();


        }
        private void PrintPage(Object sender, PrintPageEventArgs ev)
        {

            Metafile pageImage = (Metafile)ViewState["pageImage"];

            if (pageImage == null)
                return;

            ev.Graphics.PageUnit = GraphicsUnit.Pixel;

            ev.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            ev.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            float a = (ev.PageSettings.PrintableArea.Width / 100) * ev.Graphics.DpiX;
            float b = ((ev.PageSettings.PrintableArea.Height / 100) * ev.Graphics.DpiY);
            float scale = 1500;
            scale = 0;
            RectangleF srcRect = new RectangleF(0, startY, pageImage.Width, b - scale);
            RectangleF destRect = new RectangleF(0, 0, a, b);
            ev.Graphics.DrawImage(pageImage, destRect, srcRect, GraphicsUnit.Pixel);
            startY = startY + b - scale;
            float marignInPixel = (0.5f / 2.54f) * ev.Graphics.DpiY;
            ev.HasMorePages = (startY + marignInPixel < pageImage.Height);

            ev.PageSettings.PaperSize.Height += (int)startY;

        }

    }
}