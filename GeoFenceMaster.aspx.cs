using exam.DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;  
using System.Configuration;
using System.Data.SqlClient; 

namespace exam
{
    public partial class GeoFenceMaster : System.Web.UI.Page
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
        
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
              
                Session["MenuName"] = "IncidenceMaster";  
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString(); 
                Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                Session["userid"] = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                utypeall = usertype; 
                if (!IsPostBack)
                { 
                    LoadDistrict(usertype);
                    LoadPC(ddlDistrict.SelectedValue, usertype);
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
                ds = _boothlist.GetGeoFenceDetails(ddlDistrict.SelectedValue,ddlAssembly.SelectedValue, strm_txtBox.Text);
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

        private void LoadDistrict(string user_type)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByuserid(Convert.ToInt32(Session["userid"])); 
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
         
        private void LoadPC(string District, string usertype)
        {
            try
            {
                var Assembly = _boothlist.GetAllacnameByuserid(ddlDistrict.SelectedValue, Convert.ToInt32(Session["userid"]));
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
                int streamid = 0;
                DataSet ds = new DataSet();  
                    ds = _boothlist.AddGeoFenceDetails(Convert.ToInt32(lblboothid.Value), txtIncidencedtls.Text,"");
                   
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','success');$j('#PopupAddCamera').hide();", true);
                        LoadBooth(); 
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','error');$j('#PopupAddCamera').show();", true);
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
                DataSet ds = _boothlist.GetGeoFenceDetails("","","");
         
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    Label boothid = (Label)row.FindControl("lblid");
                    Label lblstreamname = (Label)row.FindControl("lblStreamId");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showLoader", "showLoader();", true);
                    bool result = _boothlist.DeleteIncidenceDetail(Convert.ToInt32(boothid.Text), Session["username"].ToString());
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
                LoadBooth();
            }
            catch (Exception ex)
            {

            }
        } 

        [System.Web.Services.WebMethod]
        public static string DeleteClick(int id)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            string query = "DeleteIncidenceDetail";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
             int i = cmd.ExecuteNonQuery();
            con.Close(); 
            return i > 0 ? "Deleted" : "Failed";

        } 
        protected void btnaddbooth_Click(object sender, EventArgs e)
        { 
            lblboothid.Value = "0";
          
            txtIncidencedtls.Text = "";
        }

        protected void VehicleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


    
}