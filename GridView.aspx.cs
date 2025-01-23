using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;
using System.Data;
using System.Configuration;
using exam.services;
using DocumentFormat.OpenXml.Spreadsheet;

namespace exam
{
    public partial class GridView : System.Web.UI.Page
    {
        LoadTopSelections objloadtop = new LoadTopSelections();

        db_data _boothgrid = new db_data();
        db_list_lq c1 = new db_list_lq();
        //public int pageitemcount = 6;
        //public static int totalDatacount = 0;
        public string pcname = ConfigurationManager.AppSettings["pcname"].ToString();
        public string assemblyname = ConfigurationManager.AppSettings["assemblyname"].ToString();
        string PrevNextBtnEnable = ConfigurationManager.AppSettings["PrevNextBtnEnable"].ToString();
        // registrationDAL _regDal = new registrationDAL();
        public int pageitemcount
        {
            get
            {
                if (ViewState["pageitemcount"] != null)
                    return (int)ViewState["pageitemcount"];
                else
                    return 6;
            }
            set
            {
                ViewState["pageitemcount"] = value;
            }
        }
        public int gridcolumns
        {
            get
            {
                if (ViewState["gridcolumns"] != null)
                    return (int)ViewState["gridcolumns"];
                else
                    return 3;
            }
            set
            {
                ViewState["gridcolumns"] = value;
            }
        }
        public int PageNumber
        {
            get
            {

                if (ViewState["PageNumber"] != null)
                    return (int)ViewState["PageNumber"];
                else
                    return 1;
            }
            set
            {
                ViewState["PageNumber"] = value;
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


        public string username
        {
            get
            {
                if (ViewState["username"] != null)
                    return (string)ViewState["username"];
                else
                    return "1";
            }
            set
            {
                ViewState["username"] = value;
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
        public int gridtypeval
        {
            get
            {
                if (ViewState["gridtypeval"] != null)
                    return (int)ViewState["gridtypeval"];
                else
                    return 6;
            }
            set
            {
                ViewState["gridtypeval"] = value;
            }
        }
        public int totalDatacount
        {
            get
            {
                if (ViewState["totalDatacount"] != null)
                    return (int)ViewState["totalDatacount"];
                else
                    return 6;
            }
            set
            {
                ViewState["totalDatacount"] = value;
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
        public string usercode
        {
            get
            {
                if (ViewState["usercode"] != null)
                    return (string)ViewState["usercode"];
                else
                    return "";
            }
            set
            {
                ViewState["usercode"] = value;
            }
        }
        public void setusertype()
        {

        }


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
                Session["MenuName"] = "GridView";
                if (!IsPostBack)
                {

                    DataSet ds = _boothgrid.GetUserData(Page.User.Identity.Name);
                    usercode = ds.Tables[0].Rows[0]["usercode"].ToString();
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    username = ds.Tables[0].Rows[0]["username"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    utypeall = usertype;
                    if (Request.QueryString.Count > 0 && Request.QueryString["d"] != null)
                    {
                        string str = Common.Decode(Request.QueryString["d"].ToString());
                        _usertype = str.StartsWith("000") ? "online" : usertype;
                        _usertype = str.StartsWith("002") ? "offline" : _usertype;

                        usertype = "sch_" + str.Split('$')[1].ToString();
                        Dashboard.Visible = true;
                    }
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                    }
                    int t = Convert.ToInt32(ddlTimer.SelectedValue);
                    if (t == 0)
                    {
                        Timer1.Enabled = false;
                    }
                    LoadBooth(usertype);
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_gridauto() -- >  " + ex.Message);
            }
        }



        private void LoadBooth(string usertype)
        {
            var dsNew = new DataSet();
            try
            {
                setusertype();
                pageitemcount = gridtypeval;

                int startRow = ((PageNumber - 1) * pageitemcount);
                DataSet ds = new DataSet();
                IQueryable<dbData> alldata = Enumerable.Empty<dbData>().AsQueryable();
                bool isgrid = false;
                int? ident = c1.getUserIdentifier(Convert.ToInt32(Session["userid"]));
                if (ident == 4)
                    isgrid = true;

                if (usertype.ToLower().Equals("ceo") || usertype.ToLower().Equals("eci"))
                {
                    dsNew = _boothgrid.GetOnlineMapBoothListNew(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, "", -1, -1, "", strm_txtBox.Text.Trim(), "deviceid");
                    DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                    var districtlist = dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).Distinct().ToArray();
                    var assemblylist = dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).Distinct().ToArray();
                    var data = dsNew.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")));
                    data = data.Where(x => assemblylist.Contains(x.Field<string>("acname")));

                    data = data.OrderBy(x => x.Field<string>("status")).ThenBy(x => x.Field<string>("district"));
                    int totalCount = data.Count();
                    int totalPages = (totalCount + pageitemcount - 1) / pageitemcount;


                    startRow = ((PageNumber - 1) * pageitemcount);
                    listview1.DataSource = data.Skip(startRow).Take(pageitemcount).ToList().CopyToDataTable();
                    listview1.DataBind();
                    totalDatacount = data.Count();
                    BindPager(totalDatacount, this.PageNumber, pageitemcount);
                }

                else
                {
                    //DataSet alldata1 = new DataSet();
                    //if (_usertype == "live")
                    //{
                    //    alldata1 = c1.GetBoothList_1(usertype, true, stateid, startRow, startRow + pageitemcount - 1, "", "");
                    //}
                    //else
                    //{
                    //    if (utypeall.StartsWith("eci"))
                    //    {
                    //        // ds = _boothlist.GetBoothListECI(usertype, false, stateid);
                    //    }
                    //    else
                    //    {
                    //        // ds = _boothlist.GetBoothList(usertype, false, stateid, startRow, startRow + pageitemcount - 1);
                    //        alldata1 = c1.GetBoothList_1(usertype, false, stateid, startRow, startRow + pageitemcount - 1, "", "");
                    //    }
                    //}

                    //totalDatacount = alldata1.Tables[0].Rows.Count;


                    if (_usertype == "online")
                    {
                        string selectedDistrict = LoadDistrict_2(usertype);
                        string selectedacname = "";
                        //string formattedDistrict = selectedDistrict.Replace("'", "''"); // Escaping single quotes
                        //  DataTable filteredData = alldata1.Tables[0].Select($"lastseen >= #{cutoffTime}# AND sFlag = 3 AND district='{formattedDistrict}'").CopyToDataTable();
                        ddlDistrict.Enabled = false;
                        if (ddlAssembly.SelectedIndex > 0)
                        {
                            selectedacname = ddlAssembly.SelectedItem.Text;
                        }
                        //try
                        //{
                        //    string selassembly = "";
                        //    var a = usertype.Split('_');
                        //    if (a.Length >= 2)
                        //        selassembly = a[3];

                        //    if (selassembly != "")
                        //    {
                        //        ddlAssembly.SelectedIndex = ddlAssembly.Items.IndexOf(ddlAssembly.Items.FindByText(selassembly));
                        //    }
                        //    selectedacname = ddlDistrict.Items.FindByValue(selassembly)?.Text;
                        //}
                        //catch
                        //{
                        //}
                       
                        //else
                        //{
                        //    ddlAssembly.SelectedIndex = 1;
                        //    selectedacname = ddlAssembly.SelectedItem.Text;
                        //}
                        int statusflag = 0;
                        if (usertype.ToLower().Equals("ceo") || usertype.ToLower().Equals("eci"))
                        {
                            statusflag = 3;
                        }
                        dsNew = _boothgrid.GetMapBoothListNew_Grid(userid, selectedDistrict, selectedacname, statusflag, "RUNNING", -1, -1, "", strm_txtBox.Text.Trim(), "", PageNumber, pageitemcount);
                        //DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                        //var districtlist = dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).Distinct().ToArray();
                        //var assemblylist = dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).Distinct().ToArray();
                        //var data = dsNew.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")));
                        //data = data.Where(x => assemblylist.Contains(x.Field<string>("acname")));

                        //data = data.OrderBy(x => x.Field<string>("status")).ThenBy(x => x.Field<string>("district"));
                        startRow = PageNumber;// ((PageNumber - 1) * pageitemcount);
                        listview1.DataSource = dsNew.Tables[0];// data.Skip(startRow).Take(pageitemcount).ToList().CopyToDataTable();
                        listview1.DataBind();
                        totalDatacount = Convert.ToInt32(dsNew.Tables[1].Rows[0]["Total"].ToString());
                        int totalCount = totalDatacount;
                        int totalPages = (totalCount + pageitemcount - 1) / pageitemcount;
                        BindPager(totalDatacount, this.PageNumber, pageitemcount);

                        //string selectedDistrict = LoadDistrict_2(usertype);
                        //string selectedacname = "";
                        ////string formattedDistrict = selectedDistrict.Replace("'", "''"); // Escaping single quotes
                        ////  DataTable filteredData = alldata1.Tables[0].Select($"lastseen >= #{cutoffTime}# AND sFlag = 3 AND district='{formattedDistrict}'").CopyToDataTable();
                        //ddlDistrict.Enabled = false;
                        //DateTime cutoffTime = DateTime.Now.AddMinutes(-1); // Example cutoff time, replace with your logic
                        //string filterExpression = $"lastseen >= #{cutoffTime}# AND sFlag = 3 AND district='{selectedDistrict}' AND ISNULL(isdelete, '')=0 ";
                        //if (ddlAssembly.SelectedIndex > 0)
                        //{
                        //    selectedacname = ddlAssembly.SelectedItem.Text;
                        //    filterExpression += $" AND acname='{selectedacname}'";
                        //}
                        //DataRow[] filteredRows = alldata1.Tables[0].Select(filterExpression);
                        //if (filteredRows.Length > 0)
                        //{
                        //    DataTable filteredData = filteredRows.CopyToDataTable(); 
                        //    int totalCount = filteredData.Rows.Count;
                        //    int totalPages = (totalCount + pageitemcount - 1) / pageitemcount;
                        //    startRow = (PageNumber - 1) * pageitemcount; 
                        //    DataRow[] pagedRows = filteredData.Rows.Cast<DataRow>().Skip(startRow).Take(pageitemcount).ToArray();

                        //    DataTable pagedData = filteredData.Clone(); // Create a new table with the same structure
                        //    foreach (DataRow row in pagedRows)
                        //    {
                        //        pagedData.ImportRow(row); // Copy rows to the new table
                        //    }

                        //    listview1.DataSource = pagedData;
                        //    listview1.DataBind();

                        //    BindPager(totalCount, PageNumber, pageitemcount);
                        //}
                        //else
                        //{
                        //    // Handle case where no rows match the filter criteria
                        //    Console.WriteLine("No matching rows found.");
                        //}

                        // Use string interpolation to include variables in the filter expression
                        //DataTable filteredData = alldata1.Tables[0].Select($"lastseen >= #{cutoffTime}# AND sFlag = 3 AND district='{formattedDistrict}'").CopyToDataTable();


                    }
                    else
                    {
                        int statusflag = 0;
                        if (usertype.ToLower().Equals("ceo") || usertype.ToLower().Equals("eci"))
                        {
                            statusflag = 3;
                        }
                        dsNew = _boothgrid.GetMapBoothListNew_Grid(userid, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, statusflag, "", -1, -1, "", strm_txtBox.Text.Trim(), "", PageNumber, pageitemcount);
                        //DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                        //var districtlist = dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).Distinct().ToArray();
                        //var assemblylist = dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).Distinct().ToArray();
                        //var data = dsNew.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")));
                        //data = data.Where(x => assemblylist.Contains(x.Field<string>("acname")));

                        //data = data.OrderBy(x => x.Field<string>("status")).ThenBy(x => x.Field<string>("district"));
                        startRow = PageNumber;// ((PageNumber - 1) * pageitemcount);
                        listview1.DataSource = dsNew.Tables[0];// data.Skip(startRow).Take(pageitemcount).ToList().CopyToDataTable();
                        listview1.DataBind();
                        totalDatacount = Convert.ToInt32(dsNew.Tables[1].Rows[0]["Total"].ToString());
                        int totalCount = totalDatacount;
                        int totalPages = (totalCount + pageitemcount - 1) / pageitemcount;
                        BindPager(totalDatacount, this.PageNumber, pageitemcount);
                    }


                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_grid() -- >  " + ex.Message);
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            double getPageCount = (double)((decimal)totalDatacount / (decimal)pageitemcount);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = PageNumber >= pageCount ? 1 : PageNumber + 1;
            loaddata();
        }
        private void loaddata()
        {
            LoadBooth(usertype);
            //Timer1.Enabled = true;
        }


        private void LoadDistrict(string usertype)
        {
            try
            {
                DataSet ds_dist = new DataSet();
                if (utypeall.StartsWith("sch_") || utypeall.StartsWith("dst_"))
                {
                    ddlDistrict.Items.Clear();
                    ddlDistrict.Items.Insert(0, new ListItem(usertype.Split('_')[1], usertype.Split('_')[1]));
                }
                else
                {
                    objloadtop.LoadDistrict(usertype, utypeall, stateid, out ds_dist);

                    ddlDistrict.DataSource = ds_dist;
                    ddlDistrict.DataTextField = "District";
                    ddlDistrict.DataValueField = "SelValue";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
                    ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
                }
                string seldist = "";
                var a = usertype.Split('_');
                if (a.Length >= 2)
                    seldist = a[1];

                if (seldist != "")
                {
                    ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(seldist));
                }
                else
                {
                    // ddlDistrict.SelectedIndex = Session["ddldistgrid"] != null ? Convert.ToInt32(Session["ddldistgrid"]) : 0;
                }
                if (Request.QueryString.Count > 0 && Request.QueryString["d"] != null)
                {
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
                    LoadAssembly();
                    if (usercode == "Assembly_Level")
                    {
                        try
                        {
                            DataTable dtAccess = (DataTable)Session["userAssemblyAccess"];

                            ddlAssembly.SelectedValue = dtAccess.Rows[0]["acname"].ToString();
                            ddlAssembly.Enabled = false;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
        private string LoadDistrict_2(string usertype)
        {
            string selectedDistrictText = ""; // Variable to store the selected district text

            try
            {

                DataSet ds_dist = new DataSet();
                objloadtop.LoadDistrict(usertype, utypeall, stateid, out ds_dist);

                ddlDistrict.DataSource = ds_dist;
                ddlDistrict.DataTextField = "District";
                ddlDistrict.DataValueField = "SelValue";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("ALL Area", ""));
                string seldist = "";
                var a = usertype.Split('_');
                if (a.Length >= 2)
                    seldist = a[1];

                if (seldist != "")
                {
                    ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(seldist));
                }

                selectedDistrictText = ddlDistrict.Items.FindByValue(seldist)?.Text;

                // Set the selected index
                ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(selectedDistrictText));
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }

            return selectedDistrictText; // Return the selected district text
        }




        private void LoadAssembly()
        {
            try
            {

                var Assembly = _boothgrid.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));

                //LoadSchool(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text, usertype);
            }
            catch (Exception ex)
            {
                Common.Log("LoadSchool_list() -- >  " + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;

                LoadAssembly();
                LoadBooth(usertype);
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
                PageNumber = 1;

                LoadBooth(usertype);
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
                PageNumber = 1;

                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        public void BindPager(int totalRecordCount, int currentPageIndex, int pageSize)
        {
            //pageSize = 1;
            int totalLinkInPage = 10;
            int totalPageCount = (int)Math.Ceiling((decimal)totalRecordCount / pageSize);
            List<ListItem> pageLinkContainer = new List<ListItem>();
            //if (totalPageCount > 1)
            //{
            int startPageLink = Math.Max(currentPageIndex - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
            int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

            if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
            {
                lastPageLink = Math.Min(currentPageIndex + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
                startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
            }
            for (int i = startPageLink; i <= lastPageLink; i++)
            {
                pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPageIndex != i));
            }

            if (PrevNextBtnEnable == "Y")
            {
                prev.Text = "PREV";
                next.Text = "NEXT";
            }

            prev.Enabled = currentPageIndex > 1;
            next.Enabled = currentPageIndex <= totalPageCount;
            rptPages.DataSource = pageLinkContainer;
            rptPages.DataBind();

        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
            this.PageNumber = pageIndex;
            loaddata();
        }



        protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnPage = (LinkButton)e.Item.FindControl("btn_page_camera");
                    if (int.Parse(btnPage.Text) == PageNumber)
                    {
                        btnPage.Font.Underline = true;
                        btnPage.CssClass = "btn btn-primary btncustom";
                    }
                    else
                    {
                        btnPage.Font.Underline = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void next_Click(object sender, EventArgs e)
        {
            if (PrevNextBtnEnable == "Y")
            {
                PageNumber = PageNumber + 1;

            }
            else
            {
                PageNumber = PageNumber <= 5 ? 10 : (PageNumber + 4);
            }


            loaddata();

        }

        protected void prev_Click(object sender, EventArgs e)
        {
            if (PrevNextBtnEnable == "Y")
            {
                PageNumber = PageNumber - 1;
            }
            else
            {
                PageNumber = PageNumber > 5 ? (PageNumber - 5) : 1;
            }

            loaddata();

        }
        protected void ddlgrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dimention = ddlgrid.SelectedValue.Split('x');
            gridcolumns = Convert.ToInt32(dimention[0]);
            gridtypeval = Convert.ToInt32(dimention[0]) * Convert.ToInt32(dimention[1]);

            loaddata();
        }

        protected void ddlTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(ddlTimer.SelectedValue);
            if (t == 0)
            {
                Timer1.Enabled = false;
            }
            else
            {
                Timer1.Enabled = true;
                Timer1.Interval = Convert.ToInt32(ddlTimer.SelectedValue);
            }
            loaddata();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            PageNumber = 1;
            LoadBooth(usertype);
        }

        protected void ddllocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddllocationType_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void Dashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}