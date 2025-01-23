using exam.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class InventoryUpdation : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            Session["MenuName"] = "InventoryUpdation";
          
            DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
            usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
            stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
            Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
          
            stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);
            if (!this.IsPostBack)
            {
                LoadDistrict_master(usertype);
                this.BindGrid();
                this.LoadDistrict();
                this.LoadMaterial();
                LoadPC(ddlDistrict.SelectedValue, usertype);
            }
        }

        private void BindGrid()
        {
            db_data dbContext = new db_data();

            try
            {
                var dt = dbContext.GetallInventoryData(ddlDistrict.SelectedItem.Value,ddlAssembly.SelectedItem.Value).Tables[0];

                InventoryUpdationGrid.DataSource = dt;
                InventoryUpdationGrid.DataBind();

            }
            catch (Exception ex)
            {
                Common.Log("ShiftMaster() -- >  BindGrid()" + ex.Message);
            }
        }

        private void LoadDistrict()
        {
            try
            {
                db_data dbContext = new db_data();
                var Alldist = dbContext.GetAllDistrictByStateId(stateid);

                DistrictDropDown.DataSource = Alldist;
                DistrictDropDown.DataTextField = "district";
                DistrictDropDown.DataValueField = "district";
                DistrictDropDown.DataBind();

                DistrictDropDown.Items.Insert(0, new ListItem("Select District", "0"));
                AssemblyDropDownList.Items.Insert(0, new ListItem("Select Assembly", ""));
                VehicleDropDownList.Items.Insert(0, new ListItem("Select Vehicle", "0"));
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  LoadDistrict()" + ex.Message);
            }
        }

        private void LoadMaterial()
        {
            try
            {
                db_data dbContext = new db_data();
                var Alldist = dbContext.GetAllMaterial();

                ddlMaterial.DataSource = Alldist;
                ddlMaterial.DataTextField = "name";
                ddlMaterial.DataValueField = "name";
                ddlMaterial.DataBind();

                ddlMaterial.Items.Insert(0, new ListItem("Select Material", "0"));
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  LoadDistrict()" + ex.Message);
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                InventoryUpdationGrid.PageIndex = e.NewPageIndex;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                Common.Log("ShiftMaster -- >  OnPageIndexChanging()" + ex.Message);
            }
        }

        protected void InventoryUpdationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                db_data dbContext = new db_data();
                if (e.CommandName == "DeleteInventoryUpdation")
                {
                    //headerText.InnerText = "Delete Inventory Update";
                    //Change.CommandName = "DeleteConfirm";
                    //Change.CommandArgument = e.CommandArgument.ToString();
                    //Change.Text = "Delete";
                   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "modal.style.display='block'; addUpdate.style.display='none'; dlt.style.display='block';", true);
                }
                else if (e.CommandName == "EditInventoryUpdation")
                {
                    GridViewRow Row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    Label lblIdtest = (Label)Row.FindControl("lblid");
                    lblboothid.Value = lblIdtest.Text;
                    var row = dbContext.GetInventoryUpdationById(Convert.ToInt32(lblboothid.Value)).Tables[0].Rows[0];

                    
                    DistrictDropDown.SelectedItem.Selected = false;
                    DistrictDropDown.Items.FindByValue(row.Field<string>("districtname")).Selected = true;
                    DistrictDropDownOnSelectedIndexChanged(DistrictDropDown, null);

                    AssemblyDropDownList.SelectedItem.Selected = false;
                    AssemblyDropDownList.Items.FindByText(row.Field<string>("acname")).Selected = true;
                    AssemblyDropDownOnSelectedIndexChanged(DistrictDropDown, null);

                    VehicleDropDownList.SelectedItem.Selected = false;
                    VehicleDropDownList.Items.FindByText(row.Field<string>("vehicleNo")).Selected = true;

                    ddlMaterial.SelectedItem.Selected = false;
                    ddlMaterial.Items.FindByValue(row.Field<string>("material")).Selected = true;

                    oldsrno.Text = row.Field<string>("oldsnno");
                    newserno.Text = row.Field<string>("newsnno");
                   
                    StartDate.Text = row.Field<DateTime>("startdate").ToString("dd/MM/yyyy");
                    EndDate.Text= row.Field<DateTime>("enddate").ToString("dd/MM/yyyy");
                   

                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "modal.style.display = 'block'; addUpdate.style.display='block'; dlt.style.display='none'; ", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void AddInventoryUpdation(object sender, EventArgs e)
        {
            try
            {
                //headerText.InnerText = "Add InventoryUpdation";
                //Change.CommandName = "AddConfirm";
                //Change.Text = "Save";

                DistrictDropDown.SelectedIndex = 0;
                AssemblyDropDownList.SelectedIndex = 0;
                VehicleDropDownList.SelectedIndex = 0;
                ddlMaterial.SelectedIndex = 0;
                oldsrno.Text = "";
                newserno.Text = "";

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "modal.style.display = 'block'; addUpdate.style.display='block'; dlt.style.display='none'; ", true);
            }
            catch (Exception ex)
            {
                Common.Log("ShiftMaster -- >  deleteShiftConfirmed()" + ex.Message);
            }
        }
       

        protected void DistrictDropDownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                db_data dbContext = new db_data();
                var Assembly = dbContext.GetAllAssemblyByDistrict(stateid, DistrictDropDown.SelectedItem.Text);

                AssemblyDropDownList.Items.Clear();
                VehicleDropDownList.Items.Clear();
                AssemblyDropDownList.DataSource = Assembly;
                AssemblyDropDownList.DataTextField = "acname";
                AssemblyDropDownList.DataValueField = "accode";
                AssemblyDropDownList.DataBind();

                AssemblyDropDownList.Items.Insert(0, new ListItem("Select Assembly", ""));
                VehicleDropDownList.Items.Insert(0, new ListItem("Select Vehicle", "0"));
               // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "modal.style.display = 'block'; addUpdate.style.display='block'; dlt.style.display='none'; ", true);

            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  DistrictDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }
        private void Loadvehicalddl()
        {
            try
            {
                db_data dbContext = new db_data();
                var vehicle = dbContext.GetShiftWizeVehicleByAcCode(AssemblyDropDownList.SelectedItem.Value);

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
        protected void AssemblyDropDownOnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Loadvehicalddl();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }
        protected void InventoryUpdationGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (Session["userType"].ToString().ToLower() == "master_admin")
                {
                    ((DataControlField)InventoryUpdationGrid.Columns
                   .Cast<DataControlField>()
                   .Where(fld => fld.HeaderText == "Delete")
                   .SingleOrDefault()).Visible = true;

                    ((DataControlField)InventoryUpdationGrid.Columns
                   .Cast<DataControlField>()
                   .Where(fld => fld.HeaderText == "Edit")
                   .SingleOrDefault()).Visible = true;
                }
                else
                {
                    ((DataControlField)InventoryUpdationGrid.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "Delete")
                .SingleOrDefault()).Visible = false;
                    ((DataControlField)InventoryUpdationGrid.Columns
                  .Cast<DataControlField>()
                  .Where(fld => fld.HeaderText == "Edit")
                  .SingleOrDefault()).Visible = false;
                }
            }
        }

        
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlDistrict.SelectedValue == "0")
                {
                    LoadPC("", usertype);
                }

                else
                {
                    LoadPC(ddlDistrict.SelectedValue, usertype);
                    ddlDistrict.Style.Add("background", "#007bff");
                    ddlDistrict.Style.Add("color", "#FFFFFF");
                }
                if (ddlDistrict.SelectedValue == "")
                {
                    ddlDistrict.Style.Add("background", "#fff");
                    ddlDistrict.Style.Add("color", "#333");
                    ddlAssembly.Style.Add("background", "#fff");
                    ddlAssembly.Style.Add("color", "#333");
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        private void LoadPC(string District, string usertype)
        {
            try
            {
                db_data dbContext = new db_data();
                var Assembly = dbContext.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedValue);
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

                BindGrid();
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
                BindGrid();
            }
            catch
            {
            }
        }
        protected void btnaddbooth_Click(object sender, EventArgs e)
        {
            lblboothid.Value = "0";
            VehicleDropDownList.SelectedIndex = -1;
            DistrictDropDown.SelectedIndex = 0;
            AssemblyDropDownList.SelectedIndex = 0;
            lblmsg.Text = "";
            StartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            EndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            oldsrno.Text = "";
            newserno.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
         {
            try
            {
                int streamid = 0;
                db_data dbContext = new db_data();
                DataSet ds = new DataSet();
               ds= dbContext.AddInventoryUpdation(Convert.ToInt32(lblboothid.Value), DistrictDropDown.SelectedValue, AssemblyDropDownList.SelectedItem.Text, VehicleDropDownList.SelectedValue, ddlMaterial.SelectedValue, oldsrno.Text, newserno.Text, AssemblyDropDownList.SelectedValue, Repair_ReplaceDD.SelectedValue.Equals("0") ? 0 : 1, StartDate.Text, EndDate.Text,usertype,"", txtremarks.Text.Trim());
             //   this.BindGrid();

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "successAlert", "showSuccessAlert('" + ds.Tables[0].Rows[0]["msg"].ToString() + "');", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','error');$('#PopupAddCamera').show();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadDistrict_master(string user_type)
        {
            try
            {
                db_data _boothlist = new db_data();
                var Alldist = _boothlist.GetAllDistrictByStateId(stateid);
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
        [System.Web.Services.WebMethod]
        public static string DeleteClick(int id)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            
            string query = string.Empty;
            query = "DELETE FROM [dbo].[inventoryupdatation] WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.Text;
           
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i > 0 ? "Deleted" : "Failed";

        }
    }
}