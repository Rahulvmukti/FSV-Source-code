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
    public partial class UploadOperator : System.Web.UI.Page
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
                Session["MenuName"] = "UploadOperator"; 
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                Session["username"] = ds.Tables[0].Rows[0]["username"].ToString(); 

                if (!IsPostBack)
                { 
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
                ds = _boothlist.GetBoothWiseCamOperator(strm_txtBox.Text.Trim());

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
       
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                DataSet ds = _boothlist.GetMapBoothListNew_Master("","", strm_txtBox.Text, "");
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        
        protected void btnupload_Click(object sender, EventArgs e)
        
        {
            string filename = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "").Replace("AM", "").Replace("PM", "") + ".xlsx";
            if (FileUploadbooth.PostedFile.ContentLength == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please Choose excel file!!','error');$('#loaderdiv').hide();", true);
                return;
            }
            else
            {
                string extn = Path.GetExtension(FileUploadbooth.PostedFile.FileName).ToLower();
                if (extn != ".xls" && extn != ".xlsx")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please select only .xlsx file','error');$('#loaderdiv').hide();", true);
                    return;
                } 
                FileUploadbooth.SaveAs(Server.MapPath(filename));
                MainFunc(filename, ddlType.SelectedValue); 
            }
        }
        public DataSet MainFunc(string filepath, string district)
        {
            DataSet ds = new DataSet();
            DataSet dsDID = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                List<string> columnsnames = new List<string>();
                DataSet ds_Excel = GetExcelData(filepath, out columnsnames, district); 
                DataTable firstTable = ds_Excel.Tables[0];
                string query = "UploadAssemblyWiseoperator";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@cameraId", district); 
                SQLcommand.Parameters.AddWithValue("@tbl", firstTable);
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                SQLconn.Open();
                adp.Fill(ds); 
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','Operator Uploaded successfully','success');$('#loaderdiv').hide();", true);
                }
             
                LoadBooth();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SQLconn.Close();
            }
            return ds;
        }
         
        public DataSet GetExcelData(string filepath, out List<string> columnsnames,string typeid)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();
                using (XLWorkbook workBook = new XLWorkbook(Server.MapPath(filepath)))
                {
                    columnsnames = new List<string>();
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                                columnsnames.Add(cell.Value.ToString());
                            }
                            if (typeid == "2")
                            {
                                dt.Columns.RemoveAt(0);
                            }
                            firstRow = false;
                        }
                        else
                        {
                            if (row.FirstCellUsed() != null && row.LastCellUsed() != null)
                            {

                                dt.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.IsEmpty() ? DBNull.Value : cell.Value;
                                    i++; 
                                }
                            }

                        }

                    }
                    if (typeid == "2")
                    {
                        dt.Columns.Add("CameraDID", typeof(string));
                       dt.Columns["CameraDID"].SetOrdinal(0);
                       }
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ds;
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        { 
            LoadBooth();
        }

       

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooth();
        }
    }
     
}