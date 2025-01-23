using ClosedXML.Excel;
using exam.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace exam
{
    public partial class StreamlistMaster : System.Web.UI.Page
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
                Session["MenuName"] = "StreamListMaster";

                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                Session["UserName"]= ds.Tables[0].Rows[0]["username"].ToString();
                stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
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
                    LoadStreamList();
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load() -- >  " + ex.Message);
            }
        }
        private void LoadStreamList()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _boothlist.GetStreamListMaster(strm_txtBox.Text.Trim());

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
                LoadStreamList();
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
                DataSet ds = _boothlist.GetStreamListMaster(strm_txtBox.Text.Trim());
                  GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        [System.Web.Services.WebMethod]
        public static string DeleteClick(int id)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            string query = "DeleteStreamByID";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
            ////cmd.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i > 0 ? "Deleted" : "Failed";
          
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
                try
                {
                    int streamid = 0;
                    DataSet ds = new DataSet();
                    DataSet ds1 = _dbadmin.GetStreamDetailsByDID(txtcameraId.Text.Trim().ToUpper());
                    if (ds1.Tables[0].Rows.Count <= 0)
                    {
                        streamid = SaveCameraDetailsByDeviceID(txtcameraId.Text.Trim().ToUpper());

                        if (streamid <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + txtcameraId.Text.Trim().ToUpper() + " does not exist in System! Please enter valid CameraID or Contact to System Administrator.','error');$j('#PopupAddCamera').show();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + txtcameraId.Text + " Saved Sucessfully" + "','success');$j('#PopupAddCamera').hide();", true);
                            LoadStreamList();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + txtcameraId.Text.Trim().ToUpper() + " Already Exists in the System.','error');$j('#PopupAddCamera').hide();", true);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        private int SaveCameraDetailsByDeviceID(string deviceid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://media5.ambicam.com:5000");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = client.GetAsync("api/camera/query/?uuid=" + deviceid.ToString().ToUpper()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    CameraAPI oresponse = JsonConvert.DeserializeObject<CameraAPI>(res);

                    if (oresponse?.rtmp != null && oresponse?.tcpurl != null)
                    {
                        string servername = "";
                        string tcpurl = "";
                        servername = oresponse.rtmp.Split('/')[2]?.Split(':').FirstOrDefault() ?? "";
                        tcpurl = oresponse.tcpurl.Split('/')[2] ?? "";
                        return _boothlist.SaveStreamList(deviceid, servername, tcpurl, Session["username"].ToString(),"StreamlistMaster.aspx");
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
            return 0;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Submit")
            {
                GridViewRow Row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                Label lblIdtest = (Label)Row.FindControl("lblid");
                lblid.Text = lblIdtest.Text;
               
                DataSet ds = _boothlist.GetStreamListMaster(Convert.ToInt32(lblid.Text));

               
                txtcameraId.Text = ds.Tables[0].Rows[0]["deviceid"].ToString();
                txtprourl.Text = ds.Tables[0].Rows[0]["prourl"].ToString();
                txtservername.Text = ds.Tables[0].Rows[0]["servername"].ToString();
             

                string chlIsActive1 = ds.Tables[0].Rows[0]["IsAICamera"].ToString();
                if (chlIsActive1 == "True")
                {
                    chlIsActive.Checked = true;
                }
                else
                {
                    chlIsActive.Checked = false;
                }

            }
            
        }
        public void clear()
        {
            txtcameraId.Text = "";
            txtprourl.Text = "";
            txtservername.Text = "";
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
                //8/3/2021 4:30:41 PM
                FileUploadbooth.SaveAs(Server.MapPath(filename));
                MainFunc(filename);
            }
        }
        public DataSet GetExcelData(string filepath, out List<string> columnsnames)
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
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }

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

        public static DataTable GetJSONToDataTableUsingMethod(string JSONData)
        {
            DataTable dtUsingMethodReturn = new DataTable();
            string[] jsonStringArray = Regex.Split(JSONData.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string strJSONarr in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(strJSONarr.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx).Replace("\"", "").Trim();
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dtUsingMethodReturn.Columns.Add(AddColumnName);
            }
            foreach (string strJSONarr in jsonStringArray)
            {
                string[] RowData = Regex.Split(strJSONarr.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dtUsingMethodReturn.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx).Replace("\"", "").Trim();
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "").Trim();
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dtUsingMethodReturn.Rows.Add(nr);
            }
            return dtUsingMethodReturn;
        }

        public DataSet MainFunc(string filepath)
        {
            DataSet ds = new DataSet();
            DataSet streamdata = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                List<string> columnsnames = new List<string>();
                DataSet ds_Excel = GetExcelData(filepath, out columnsnames);
                DataTable CameraDID = new DataTable();
                CameraDID.Columns.Add("uuid");

                foreach (DataRow row in ds_Excel.Tables[0].Rows)
                {
                    CameraDID.Rows.Add(row["CameraDID"].ToString());
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://media5.ambicam.com:5000");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string myJson = JsonConvert.SerializeObject(CameraDID);
                    HttpResponseMessage response = client.PostAsync("/api/camera/json/", new StringContent(myJson, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        streamdata = JsonConvert.DeserializeObject<DataSet>(res);
                    }
                    foreach (DataRow row in streamdata.Tables[0].Rows)
                    {
                        if (row["rtmp"].ToString() != "" || row["tcpurl"].ToString() != "")
                        {
                            row["rtmp"] = row["rtmp"].ToString().Split('/')[2]?.Split(':').FirstOrDefault() ?? "";
                            row["tcpurl"] = row["tcpurl"].ToString().Split('/')[2] ?? "";
                        }
                        else
                        {
                            row["rtmp"] = "NA";
                            row["tcpurl"] = "NA";
                        }
                    }
                    if (streamdata.Tables[0].Columns.Count == 4)
                    {
                        streamdata.Tables[0].Columns.Remove("message");
                    }
                    streamdata.Tables[0].Columns[0].ColumnName = "CameraDID";
                    streamdata.Tables[0].Columns[1].ColumnName = "SERVERName";
                    streamdata.Tables[0].Columns[2].ColumnName = "ProUrl";
                    streamdata.Tables[0].Columns.Add("ISAI");
                    streamdata.Tables[0].AcceptChanges();
                    clearFileUpload();
                    DataTable firstTable = streamdata.Tables[0];
                    ds = _boothlist.SaveBulkStreamList(firstTable, Session["UserName"].ToString(), "StreamlistMaster.aspx");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["ErrorDt"] = ds.Tables[0];
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Camera List Uploaded with error!! Please check downloaded File','error').then(function() {$('#loaderdiv').hide();setTimeout(function () {window.location.href = 'streamlistMaster.aspx';}, 2000);});", true);
                        Response.AppendHeader("Refresh", "2;url=downloadhandler.ashx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','Camera List Uploaded successfully','success');$('#loaderdiv').hide();", true);
                    }
                    LoadStreamList();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "swal('Fail!','Camera List Not Uploaded','error');", true);
            }
            finally
            {
                SQLconn.Close();
            }
            return ds;
        }

        private void clearFileUpload()
        {
            FileUploadbooth.PostedFile.InputStream.Dispose();
        }

        protected void btnAddCamera_Click(object sender, EventArgs e)
        {
            tdProUrl.Visible = false;
            tdtxtProUrl.Visible = false;
            trserverName.Visible = false;
        }

        protected void btnMapping_Click(object sender, EventArgs e)
        {
            tdProUrl.Visible = true;
            tdtxtProUrl.Visible = true;
            trserverName.Visible = true;

        }
    }

    //public class Root
    //{
    //    public List<CameraAPI> data { get; set; }
    //}
}