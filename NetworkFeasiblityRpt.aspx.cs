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
    public partial class NetworkFeasiblityRpt : System.Web.UI.Page
    {
        db_data _data = new db_data();

         
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

                Session["MenuName"] = "NetworkFeasiblityRpt";
               
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
            ds = _data.GetNetworkFeasiblityRpt();
            dsReport = ds;
        } 

    }
}