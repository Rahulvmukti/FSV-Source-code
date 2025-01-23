using exam.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace exam
{
    public partial class MultiMappedCameraList : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();

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

                Session["MenuName"] = "MultiMapCameraList";
                if (!IsPostBack)
                {
                    PageNumber = 1;
                    DataSet ds = _boothlist.GETMultimappedCamera();
                    GridView1.DataSource = ds.Tables[0].DefaultView;
                    GridView1.DataBind();
                    DataSet ds1 = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds1.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds1.Tables[0].Rows[0]["stateid"]);
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

      
        protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnPage = (LinkButton)e.Item.FindControl("btn_page_camera");
                    // System.Web.UI.HtmlControls.HtmlAnchor lipager = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("pager");
                    if (int.Parse(btnPage.Text) == PageNumber)
                    {
                        btnPage.Font.Underline = true;
                        btnPage.CssClass = "btn btn-primary btncustom";
                        //btnPage.Attributes.Add("class", "btn-primary");
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
        //protected void next_Click(object sender, EventArgs e)
        //{
        //    PageNumber = PageNumber + 1;

        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();

        //}

        //protected void prev_Click(object sender, EventArgs e)
        //{
        //    PageNumber = PageNumber - 1;

        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();

        //}
        //protected void Page_Changed(object sender, EventArgs e)
        //{
        //    int pageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
        //    this.PageNumber = pageIndex;
        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();
        //}

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                DataSet ds = _boothlist.GETMultimappedCamera();
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

   
    
      

     
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataSet ds = _boothlist.GETMultimappedCamera();
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();
        }

      

  
     
    }
}