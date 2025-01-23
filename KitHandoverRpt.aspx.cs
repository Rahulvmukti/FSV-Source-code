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
    public partial class KitHandoverRpt : System.Web.UI.Page
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
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                Session["MenuName"] = "KITHANDOVERReport";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);

                if (!IsPostBack)
                {
                    
                    GetStopCameraList();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetStopCameraList()
        {
           
            DataSet ds = new DataSet();
            string ddlstring = "";  

            if (ddlStatus.SelectedItem.Text == "All")
            {
                ddlstring = "";  
            }
            ds = _data.GetKITHANDOVERRpt(strm_txtBox.Text, ddlstring);
            dsReport = ds;
        }

       

      



        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GetStopCameraList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  ddlcamara_SelectedIndexChanged()" + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetStopCameraList();
        }
         

    }
}