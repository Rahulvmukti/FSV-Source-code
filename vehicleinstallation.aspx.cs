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
    public partial class vehicleinstallation : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();
        db_data_admin _dbadmin = new db_data_admin();

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
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                Session["MenuName"] = "BoothUpload";
             
                ScriptManager1.RegisterPostBackControl(this.ddlDistrict);
                ScriptManager1.RegisterPostBackControl(this.ddlAssembly);
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                utypeall = usertype;
                if (utypeall.Contains("_all"))
                {
                    usertype = usertype.Replace("_all", "");
                }
                if (utypeall.StartsWith("eci"))
                {
                    usertype = usertype.Replace("eci_", "");
                }

                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtinstallationdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LoadZone();
                    LoadMasterzone();
                    LoadDistrict(usertype);
                    LoadMaterDistrict(drpAddDistrict.SelectedValue);
                    LoadPC(ddlDistrict.SelectedValue, usertype);
                    LoadMasterPC();
                    LoadBooth();
                  
                   

                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load() -- >  " + ex.Message);
            }
        }
        private void LoadBooth()
        
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _boothlist.GetInstallVehicle(ddlzone.SelectedItem.Value,ddlDistrict.SelectedItem.Value, ddlAssembly.SelectedValue != "" ? ddlAssembly.SelectedItem.Text : "");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    int res = 1;
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "<div align='center'><label class='text-center text-danger'>No Records Found</label></div>";
                }
                else
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth() -- >  " + ex.Message);
            }
        }

        private void LoadDistrict(string zone)
        {
            try
            {
                var Alldist = _boothlist.GetAllistictbyzone(zone);

                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
              

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadZone()
        {
            try
            {
                var Alldist = _boothlist.GetZone();

                ddlzone.DataSource = Alldist;
                ddlzone.DataTextField = "Zone";
                ddlzone.DataValueField= "Zone";
                ddlzone.DataBind();

                ddlzone.Items.Insert(0, new ListItem("ALL Zone", ""));
                ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadMasterzone()
        {
            try
            {
                var Alldist = _boothlist.GetZone();

                drpaddzone.DataSource = Alldist;
                drpaddzone.DataTextField = "Zone";
                drpaddzone.DataValueField = "Zone";
                drpaddzone.DataBind();

                drpaddzone.Items.Insert(0, new ListItem("ALL Zone", ""));
                drpAddDistrict.Items.Insert(0, new ListItem("ALL Area", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadMaterDistrict(string zone)
        {
            try
            {
                var Alldist = _boothlist.GetAllistictbyzone(zone);

                drpAddDistrict.DataSource = Alldist;
                drpAddDistrict.DataTextField = "district";
                drpAddDistrict.DataValueField = "district";
                drpAddDistrict.DataBind();

                drpAddDistrict.Items.Insert(0, new ListItem("ALL Area", ""));


            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadPC(string District, string usertype)
        {
            try
            {
                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedValue);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        private void LoadMasterPC()
        {
            try
            {

                var Assembly = _boothlist.GetAllacnamebyzoneandistrict(ddlzone.SelectedItem.Text, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        private void LoadPC()
        {
            try
            {

                var Assembly = _boothlist.GetAllacnamebyzoneandistrict(drpaddzone.SelectedItem.Text, drpAddDistrict.SelectedItem.Text);

                drpAddAcname.Items.Clear();
                drpAddAcname.DataSource = Assembly;
                drpAddAcname.DataTextField = "acname";
                drpAddAcname.DataValueField = "acname";
                drpAddAcname.DataBind();

                drpAddAcname.Items.Insert(0, new ListItem("ALL SubArea", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlDistrict.SelectedValue == "0")
                {
                    LoadMasterPC();


                }

                else
                {
                    LoadMasterPC();
                    ddlDistrict.Style.Add("background", "#007bff");
                    ddlDistrict.Style.Add("color", "#FFFFFF");
                }
                if (ddlDistrict.SelectedValue == "")
                {
                    ddlDistrict.Style.Add("background", "#fff");
                    ddlDistrict.Style.Add("color", "#333");
                   
                }
                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpAddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpAddDistrict.SelectedValue == "0")
                    LoadPC();
                else
                    LoadPC();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpAddAcname_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblaccode.Text = drpAddAcname.SelectedValue;
        }

       
       

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssembly.SelectedValue == "")
                {
                    ddlAssembly.Style.Add("background", "#fff");
                    ddlAssembly.Style.Add("color", "#333");
                }
                else
                {
                    ddlAssembly.Style.Add("background", "#007bff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");

                }

                LoadBooth();
            }


            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void ddlbooth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadBooth();
            }
            catch
            {
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                int streamid = 0;
                DataSet ds = new DataSet();

               

                // Convert to string in "yyyy-MM-dd" format
          


                ds = _boothlist.SaveVehicleinstallation(Convert.ToInt32(lblboothid.Value), drpaddzone.SelectedValue,drpAddDistrict.SelectedValue,drpAddAcname.SelectedValue,FromDt.Text, txtvehicleno.Text,Type.Text, txtAddOperatorName.Text,txtAddMobileNumber.Text, txtinstallationdate.Text,ddlstatus.SelectedValue, Session["username"].ToString(),txtAddCameraId.Text);
                    if (ds.Tables[0].Rows[0]["Message"].ToString()== "Update Success!!")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0]["Message"].ToString() + "','success');$j('#PopupAddCamera').hide();", true);
                        LoadBooth();
                         //lblboothid.Value = "0";
                    }
                   else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + ds.Tables[0].Rows[0]["Message"].ToString() + "','error');$j('#PopupAddCamera').show();", true);
                }
                

               
            }
            catch (Exception ex)
            {
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Submit")
            {


                GridViewRow Row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                lblTitle.Text = "EDIT BOOTH";
                //HiddenField lblIdtest = (HiddenField)Row.FindControl("lblid");
                //lblboothid.Value = lblIdtest.Value;
                Label lblIdtest = (Label)Row.FindControl("lblid");
                lblboothid.Value = lblIdtest.Text;
                
                DataSet ds = _boothlist.Getvehiclebyid(Convert.ToInt32(lblboothid.Value));
                LoadMasterzone();
                drpaddzone.SelectedValue = ds.Tables[0].Rows[0]["Zone"].ToString();
                LoadMaterDistrict(drpaddzone.SelectedValue);
                drpAddDistrict.SelectedValue = ds.Tables[0].Rows[0]["district"].ToString();
                LoadPC();
                drpAddAcname.SelectedValue = ds.Tables[0].Rows[0]["acname"].ToString();
                txtvehicleno.Text = ds.Tables[0].Rows[0]["vehicleno"].ToString();
                txtAddOperatorName.Text = ds.Tables[0].Rows[0]["driverName"].ToString();
                txtAddCameraId.Text = ds.Tables[0].Rows[0]["deviceid"].ToString();
                txtAddMobileNumber.Text = ds.Tables[0].Rows[0]["driverno"].ToString();
                ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
                Type.Text= ds.Tables[0].Rows[0]["VehicleType"].ToString();



            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    Label boothid = (Label)row.FindControl("lblid");
                    Label lblstreamname = (Label)row.FindControl("lblStreamId");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showLoader", "showLoader();", true);
                    bool result = _boothlist.DeleteBoothListMaster(Convert.ToInt32(boothid.Text), Session["username"].ToString());
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "hideLoader", "hideLoader();", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record Deleted.')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please try after sometime!!')", true);
                        return;

                    }
                    LoadBooth();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                DataSet ds = _boothlist.GetInstallVehicle(ddlzone.SelectedItem.Value, ddlDistrict.SelectedItem.Value, ddlAssembly.SelectedValue != "" ? ddlAssembly.SelectedItem.Text : "");
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
      
        
        protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlzone.SelectedValue == "0")
                {
                    LoadDistrict(ddlzone.SelectedItem.Text);


                }

                else
                {
                    LoadDistrict(ddlzone.SelectedItem.Text);
                    ddlzone.Style.Add("background", "#007bff");
                    ddlzone.Style.Add("color", "#FFFFFF");
                }
                if (ddlzone.SelectedValue == "")
                {
                    ddlzone.Style.Add("background", "#fff");
                    ddlzone.Style.Add("color", "#333");
                   
                }
                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpaddzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (drpaddzone.SelectedValue == "0")
                {
                    LoadMaterDistrict(drpaddzone.SelectedItem.Text);


                }

                else
                {
                    LoadMaterDistrict(drpaddzone.SelectedItem.Text);
                    drpaddzone.Style.Add("background", "#007bff");
                    drpaddzone.Style.Add("color", "#FFFFFF");
                }
                if (drpaddzone.SelectedValue == "")
                {
                    drpaddzone.Style.Add("background", "#fff");
                    drpaddzone.Style.Add("color", "#333");
                   
                }
                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooth();
        }
    }
   
}