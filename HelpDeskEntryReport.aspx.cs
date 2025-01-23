using exam.DAL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SelectPdf;
using System;
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
    public partial class HelpDeskEntryReport : System.Web.UI.Page
    {
        db_data _data = new db_data();
        public string district = ConfigurationManager.AppSettings["district"].ToString();
        public string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
        public int pageitemcount = Convert.ToInt32(ConfigurationManager.AppSettings["pageitemcount"]);
        public static int totalDatacount = 0;
        public string pcname = ConfigurationManager.AppSettings["pcname"].ToString();
        public string assemblyname = ConfigurationManager.AppSettings["assemblyname"].ToString();
        public string usernametotal = ConfigurationManager.AppSettings["usertotal"].ToString();
        public int usernametotalen = Convert.ToInt32(ConfigurationManager.AppSettings["usertotalen"].ToString());
        public bool hrflag = Convert.ToBoolean(ConfigurationManager.AppSettings["hrflag"].ToString());
        public string usertype
        {
            get
            {
                if (ViewState["usertype"] != null)
                    return (string)ViewState["usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["usertype"] = value;
            }
        }
        public string _usertype
        {
            get
            {
                if (ViewState["_usertype"] != null)
                    return (string)ViewState["_usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["_usertype"] = value;
            }
        }
        public string utypeall
        {
            get
            {
                if (ViewState["utypeall"] != null)
                    return (string)ViewState["utypeall"];
                else
                    return "live";
            }
            set
            {
                ViewState["utypeall"] = value;
            }
        }
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

                Session["MenuName"] = "HelpDeskReports";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);
                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadDistrict();
                    GetStopVehicleReasonList();
                }
                ScriptManager1.RegisterPostBackControl(this.btnExportPDF);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetStopVehicleReasonList()
        {
            DataSet ds = new DataSet();
            ToDt.Text = FromDt.Text.Trim();
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string fromtime = "00:00:00";
            string totime = "23:59:59";
            int selectedDistrictValue = Convert.ToInt32(ddlDistrict.SelectedIndex.ToString());

            int vehiclevalue = Convert.ToInt32(ddlVehicle.SelectedIndex.ToString());
            ds = _data.GetStopVehicleList(fromdate + " " + fromtime, todate + " " + totime, selectedDistrictValue, ddlAssembly.SelectedItem.Value, vehiclevalue, 0);
            //dsReport = ds;
            DataTable dt1 = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt1 = ds.Tables[0].AsEnumerable()
               .GroupBy(r => new { District = r["District"], Accode = r["acname"], VehicleNo = r["VehicleNo"], streamname = r["streamname"], ShiftName = r["ShiftName"], Dt = r["Dt"] })
               .Select(g => g.OrderBy(r => r["StartTime"]).First())
               .CopyToDataTable();
            }
            else
            {
                dt1 = ds.Tables[0].Clone();
            }
            dt1.TableName = "GroupTable";

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt1.Copy());
            dataSet.Tables.Add(ds.Tables[0].Copy());
            dsReport = dataSet;



            
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

                ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select District", "0"));
                ddlAssembly.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Assembly", ""));

                ddlVehicle.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Vehicle", ""));

            }
            catch (Exception ex)
            {
                Common.Log("HelpDeskEntryReport -- >  LoadDistrict()" + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Assembly = _data.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlVehicle.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Assembly", ""));
                ddlVehicle.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location", "0"));
                GetStopVehicleReasonList();
            }
            catch (Exception ex)
            {
                Common.Log("HelpDeskEntryReport -- >  ddlDistrictOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var vehicle = _data.GetAllVehicleByAcCode(ddlAssembly.SelectedValue);

                ddlVehicle.Items.Clear();
                ddlVehicle.DataSource = vehicle;
                ddlVehicle.DataTextField = "location";
                ddlVehicle.DataValueField = "location";
                ddlVehicle.DataBind();
                ddlVehicle.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location", "0"));

                GetStopVehicleReasonList();
            }
            catch (Exception ex)
            {
                Common.Log("HelpDeskEntryReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStopVehicleReasonList();
            }
            catch (Exception ex)
            {
                Common.Log("HelpDeskEntryReport -- >  ddlVehicle_SelectedIndexChanged()" + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetStopVehicleReasonList();
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
                StringBuilder sb = new StringBuilder();
            HtmlTextWriter hw = new HtmlTextWriter(new StringWriter(sb));
            divReport.RenderControl(hw);
            string content = sb.ToString();

            // Prepare the additional HTML parts
            DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string headerText = !string.IsNullOrEmpty(FromDt.Text.Trim()) ?
                $"<h2><div style='text-align:center;'>Help Desk Report on {FromDt.Text.Trim()}</div></h2><br/><br/>" :
                "<h2><div style='text-align:center;'>Help Desk Report</div></h2><br/><br/>";
            string legendText = $"<br/><br/><div style='text-align:right;'>This is system generated report. Printed on {currentTime.ToString("dd/MM/yyyy hh:mm:ss tt")}</div>";

            // Combine the HTML parts
            string fullHtml = headerText + content + legendText;

            // Initialize the SelectPdf converter
            HtmlToPdf converter = new HtmlToPdf();

            // Customize the conversion options if needed
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.WebPageWidth = 1024; // Adjust as needed
            converter.Options.WebPageHeight = 0; // Auto height

            // Convert the HTML string to PDF
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(fullHtml);

            // Prepare the response
            byte[] pdf = doc.Save();
            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment;filename=Help Desk Report{currentTime:ddMMyyyyhhmmss}.pdf");
            Response.BinaryWrite(pdf);
            Response.End();
        } 
    }
}