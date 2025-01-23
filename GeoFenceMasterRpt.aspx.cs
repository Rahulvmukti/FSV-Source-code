using exam.DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class GeoFenceMasterRpt : System.Web.UI.Page
    {
        db_data _data = new db_data(); 
       
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

                Session["MenuName"] = "GpsDistanceRpt"; 
                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                    LoadDistrict();
                    GetgpsdailyReport();
                } 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetgpsdailyReport()
        { 
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            DataSet ds = _data.GetgeofenceReport(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue,0);
           if(ds.Tables.Count>0)
            {
                dsReport = ds;
            }
            else
            {
                dsReport = null;
            }
           
        }

        private void LoadDistrict()
        {
            try
            {
                var Alldist = _data.GetAllDistrictByStateId(1);

                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
                ddlAssembly.Items.Insert(0, new ListItem("Select Assembly", ""));  
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
                var Assembly = _data.GetAllAssemblyByDistrict(1, ddlDistrict.SelectedItem.Text); 
                ddlAssembly.Items.Clear(); 
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind(); 
                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
                GetgpsdailyReport();
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
                GetgpsdailyReport();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }
         
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetgpsdailyReport();
        } 
    }
}