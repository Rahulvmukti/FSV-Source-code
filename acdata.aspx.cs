using System; 
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
using System.Configuration;
using exam.DAL;
using System.Web.UI.HtmlControls;

namespace exam
{
    public partial class acdata : System.Web.UI.Page
    { 

        db_data _boothlist = new db_data();
        db_data_admin _dbadmin = new db_data_admin();      
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
        public string useridentifer
        {
            get
            {
                if (ViewState["useridentifer"] != null)
                    return (string)ViewState["useridentifer"];
                else
                    return "";
            }
            set
            {
                ViewState["useridentifer"] = value;
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
         
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Default.aspx");
                }

                if (!IsPostBack)
                {

                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    int userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    Session["userid"] = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString(); 
                    useridentifer = ds.Tables[0].Rows[0]["identifier"].ToString();
                    Session["usertypeDataSet"] = ds.Tables[0].Rows[0]["usercode"].ToString();
                    if (usertype == "District_Level")
                    {
                        DataSet ds1 = GetDistrictList(userid);
                        if (ds1.Tables[0].Rows.Count > 0)
                        { 
                            Session["DistrictDataSet"] = ds1.Tables[0].Rows[0]["district"].ToString(); ;
                        }

                    } 
                    utypeall = usertype; 
                     
                    _usertype = usertype; 
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadBooth(usertype);
                    }
                    Timer1.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                loaddata();
            }
            catch (Exception ex)
            {

            }
        } 
        private void LoadBooth(string usertype)
        
        {
            try
            { 
                
                DataSet ds = new DataSet();
                int UserID =Convert.ToInt32(Session["userid"].ToString());

                DataSet dspc = _boothlist.GetStaticacname("", "", UserID);
                DateTime chklastseen = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

                listview1.DataSource = dspc;
                listview1.DataBind();
                 
                  
                 
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_list() -- >  " + ex.Message);
            }
        }
        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            loaddata();

        } 
      
        private void loaddata()
        {
            LoadBooth(usertype);
        } 
        public DataSet GetDistrictList(int userid)
        {
            db_data _data = new db_data();
            DataSet ds = new DataSet();
            ds = _data.Getdistrictid(userid);
            return ds; 
        }
        protected void listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnDistrict = (HiddenField)e.Item.FindControl("hdnDistrict");
                var divRow = e.Item.FindControl("divRow") as HtmlGenericControl; // Make sure you have runat="server" and an ID on your div

                if (hdnDistrict != null)
                {
                    hdnDistrict.Value = Eval("district").ToString();
                }

                // Assuming you have an 'online' data field in your data source
                string onlineStatus = DataBinder.Eval(e.Item.DataItem, "online").ToString();
                if (divRow != null)
                {
                    if (onlineStatus == "3")
                    {
                        // Assuming you have defined CSS classes for .bg-green and .bg-red
                        divRow.Attributes["class"] = "row bg-green";
                    }
                    else
                    {
                        divRow.Attributes["class"] = "row bg-red";
                    }
                }
            }
        }

        protected string GetRowColor(object dataItem)
        {
            // Assuming "online" is a property of your dataItem
            if (DataBinder.Eval(dataItem, "online").ToString() == "3")
            {
                return "bg-green"; // Apply green background
            }
            else
            {
                return "bg-red"; // Apply red background
            }
        }

    }

}