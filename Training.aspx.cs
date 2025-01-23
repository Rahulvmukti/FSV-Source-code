using exam.DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.services;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace exam
{
    public partial class Training : System.Web.UI.Page
    {
         
        
       
         

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Session.Timeout = 240;
                }
                Session["MenuName"] = "Training";
                if (!IsPostBack)
                {
                     
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_gridauto() -- >  " + ex.Message);
            }
        }
        
    }


}