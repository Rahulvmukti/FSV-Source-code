using exam.DAL;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class IncedentReport : System.Web.UI.Page
    {
        db_data _data = new db_data();

        public int stateid
        {
            get
            {
                if (ViewState["stateid"] != null)
                    return (int)ViewState["stateid"];
                else
                    return 0;
            }
            set
            {
                ViewState["stateid"] = value;
            }
        }
        public DataSet dsReport
        {
            get
            {
                if (ViewState["dsReport"] != null)
                    return (DataSet)ViewState["dsReport"];
                else
                    return null;
            }
            set
            {
                ViewState["dsReport"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                Session["MenuName"] = "IncedentReport";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);

                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadDistrict();
                    GetIncedentList();
                }
               // ScriptManager1.RegisterPostBackControl(this.btnExportPDF);
            }
            catch (Exception ex)
            {

            }
        }

        private void GetIncedentList()
        {
           
            //ToDt.Text = FromDt.Text.Trim();
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = ToDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + ToDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + ToDt.Text.Trim().Split(new char[] { '/', '-' })[0];
             dsReport = _data.GetIncidenceDetails(0, fromdate , todate, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue);
        }

        private void LoadDistrict()
        {
            try
            {
                var Alldist = _data.GetAllDistrictByStateId(stateid);

                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));

            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  LoadDistrict()" + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Assembly = _data.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "accode";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
                GetIncedentList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  ddlDistrictOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetIncedentList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetIncedentList();
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            string content = "";
            var sb = new StringBuilder();
          //  divReport.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            content = sb.ToString();

            DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=IncedentReport" + currentTime.ToString("ddMMyyyyhhmmss") + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string headertext = "";
            if (FromDt.Text.Trim() != "" && ToDt.Text.Trim() != "")
            {
                headertext = "<h2><div style='text-align:center;'>IncedentReport From " + FromDt.Text.Trim() + " To " + ToDt.Text.Trim() + "</div></h2><br/><br/>";
            }
            else
            {
                headertext = "<h2><div style='text-align:center;'>IncedentReport</div></h2><br/><br/>";
            }

            string legendtext = "";
            legendtext = "<br/><br/><div style='text-align:right;'>This is system genereted report. Printed on " + currentTime.ToString("dd/MM/yyyy hh:mm:ss tt") + "</div>";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
          //  divReport.RenderControl(htmlTextWriter);
            StringReader stringReader = new StringReader(headertext + stringWriter.ToString() + legendtext);
            iTextSharp.text.Document Doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 100, 0);
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();

            Response.Write(Doc);
            Response.End();
        }
      
    }
}