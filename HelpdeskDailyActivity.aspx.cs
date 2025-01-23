using exam.DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;  
using System.Configuration;
using System.Data.SqlClient; 

namespace exam
{
    public partial class HelpdeskDailyActivity : System.Web.UI.Page
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
        public string username
        {
            get
            {
                if (ViewState["username"] != null)
                    return (string)ViewState["username"];
                else
                    return "username";
            }
            set
            {
                ViewState["username"] = value;
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

        public int userid
        {
            get
            {
                if (ViewState["userid"] != null)
                    return (int)ViewState["userid"];
                else
                    return 0;
            }
            set
            {
                ViewState["userid"] = value;
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
              
                Session["MenuName"] = "HelpdeskactivityMaster";  
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                username = ds.Tables[0].Rows[0]["username"].ToString();
                utypeall = usertype; 
                if (!IsPostBack)
                { 
                    LoadDistrict(userid); 
                    LoadMasterDistrict(userid); 
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
                ds = _boothlist.GetHelpdeskactivity(ddlDistrict.SelectedValue,ddlAssembly.SelectedValue, username, "GetData");
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

        private void LoadDistrict(int userid)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByuserid(userid); 
                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind(); 
                ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }

        }
        private void LoadMasterDistrict(int userid)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByuserid(userid);
                drpAddDistrict.DataSource = Alldist;
                drpAddDistrict.DataTextField = "district";
                drpAddDistrict.DataValueField = "district";
                drpAddDistrict.DataBind(); 
                drpAddDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
                drpAddAcname.Items.Insert(0, new ListItem("ALL SubArea", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadPC(string District, int userid)
        {
            try
            {
                var Assembly = _boothlist.GetAllacnameByuserid(ddlDistrict.SelectedValue,userid);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "accode";
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
                var Assembly = _boothlist.GetAllacnameByuserid(drpAddDistrict.SelectedValue, userid);
                VehicleDropDownList.Items.Clear();
                drpAddAcname.Items.Clear();
                drpAddAcname.DataSource = Assembly;
                drpAddAcname.DataTextField = "acname";
                drpAddAcname.DataValueField = "acname";
                drpAddAcname.DataBind();

                drpAddAcname.Items.Insert(0, new ListItem("ALL SubArea", ""));
                VehicleDropDownList.Items.Insert(0, new ListItem("Select Vehicle", "0"));
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
                    LoadPC("", userid); 
                }

                else
                {
                    LoadPC(ddlDistrict.SelectedValue, userid);
                    ddlDistrict.Style.Add("background", "#007bff");
                    ddlDistrict.Style.Add("color", "#FFFFFF");
                }
                if (ddlDistrict.SelectedValue == "")
                {
                    ddlDistrict.Style.Add("background", "#fff");
                    ddlDistrict.Style.Add("color", "#333");
                    ddlAssembly.Style.Add("background", "#007bff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");
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
                    LoadMasterPC();
                else
                    LoadMasterPC();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpAddAcname_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblaccode.Text = drpAddAcname.SelectedValue;
            try
            {
                Loadvehicalddl();
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        private void Loadvehicalddl()
        {
            try
            {
                db_data dbContext = new db_data();
                var vehicle = dbContext.GetVehicleByAcCode(drpAddAcname.SelectedItem.Value);
                VehicleDropDownList.Items.Clear();
                VehicleDropDownList.DataSource = vehicle;
                VehicleDropDownList.DataTextField = "location";
                VehicleDropDownList.DataValueField = "location";
                VehicleDropDownList.DataBind();
                VehicleDropDownList.Items.Insert(0, new ListItem("Select location", "0"));

            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }


        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssembly.SelectedValue == "")
                {
                    ddlAssembly.Style.Add("background", "#fff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");
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
                DataSet ds = new DataSet();  
                     
                    if(lblboothid.Value=="0")
                {
                    ds = _boothlist.AddHelpdeskactivity(Convert.ToInt32(lblboothid.Value), drpAddDistrict.SelectedItem.Text, drpAddAcname.SelectedItem.Text, VehicleDropDownList.SelectedItem.Text, txtIncidencedtls.Text, username, "SAVE");
                }
               else
                {
                    ds = _boothlist.AddHelpdeskactivity(Convert.ToInt32(lblboothid.Value), drpAddDistrict.SelectedItem.Text, drpAddAcname.SelectedItem.Text, VehicleDropDownList.SelectedItem.Text, txtIncidencedtls.Text, username, "Update");
                }

                if (ds.Tables[0].Rows.Count>0)
                {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','success');$j('#PopupAddCamera').hide();", true);
                        LoadBooth();
                      
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
                Label lblIdtest = (Label)Row.FindControl("lblid");
                lblboothid.Value = lblIdtest.Text; 
                DataSet ds = _boothlist.GetHelpdeskactivitybyid(Convert.ToInt32(lblboothid.Value));
                drpAddDistrict.SelectedValue = ds.Tables[0].Rows[0]["District"].ToString();
                LoadMasterPC();
                drpAddAcname.SelectedValue = ds.Tables[0].Rows[0]["acname"].ToString();
                Loadvehicalddl();
                VehicleDropDownList.SelectedValue = ds.Tables[0].Rows[0]["location"].ToString();
                txtIncidencedtls.Text= ds.Tables[0].Rows[0]["activity"].ToString(); 
            }

            //if (e.CommandName == "Delete")
            //{
            //    try
            //    {
            //        GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //        Label boothid = (Label)row.FindControl("lblid");
            //        Label lblstreamname = (Label)row.FindControl("lblStreamId");
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showLoader", "showLoader();", true);
            //        bool result = _boothlist.DeleteIncidenceDetail(Convert.ToInt32(boothid.Text), Session["username"].ToString());
            //        if (result)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "hideLoader", "hideLoader();", true);
            //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record Deleted.')", true);
            //        }
            //        else
            //        {
            //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please try after sometime!!')", true);
            //            return;

            //        }
            //        LoadBooth();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                 var dt = _boothlist.GetHelpdeskactivity(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, username, "GetData");
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        } 

         
        protected void btnaddbooth_Click(object sender, EventArgs e)
        { 
            lblboothid.Value = "0";
            VehicleDropDownList.SelectedIndex = 0;
            drpAddDistrict.SelectedIndex = 0;
            drpAddAcname.SelectedIndex = 0;
            lblmsg.Text = ""; 
            txtIncidencedtls.Text = "";
        } 
    }


    
}