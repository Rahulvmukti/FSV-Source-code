using exam.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
namespace exam
{
    public partial class OfflineCameraNotification : System.Web.UI.Page
    {
        public DataSet dsReport
        {
            get
            {
                if (ViewState["dsCameraPopup"] != null)
                    return (DataSet)ViewState["dsCameraPopup"];
                else
                    return null;
            }
            set
            {
                ViewState["dsCameraPopup"] = value;
            }
        }
        public int UserID
        {
            get
            {
                if (ViewState["UserID"] != null)
                    return (int)ViewState["UserID"];
                else
                    return 0;
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            Session["MenuName"] = "OfflineCameraNotification";
            if (Session["userid"] != null)
                UserID = Convert.ToInt32(Session["userid"]);
            if (!IsPostBack)
            {
               
            }
        }
        [WebMethod(EnableSession = true)]
        public static string GetStopCameraList(int UserID = 0)
        {
            db_data _data = new db_data();
            DataSet ds = new DataSet();
            ds = _data.GetCameraOfflineListForPopup("", "", "", "","", UserID);


            //DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
            //var districtlist = string.Join(",",dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).ToArray());
            //var assemblylist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).ToArray());

            //DataTable dt = ds.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")) && assemblylist.Contains(x.Field<string>("acname"))).CopyToDataTable();

            //ds = _data.FilterDataByAccess(ds, true, true);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return DataTableToJSONWithJSONNet(ds.Tables[0]);
            else
                return "";
            //  dsReport = ds;
        }
        [WebMethod]
        public static string SaveReasons(string selectedReasons, int id)
        {
            db_data _data1 = new db_data();
            _data1.SaveCameraStopReasonsByID(id, selectedReasons);
            return "success==" + id;
        }
        [WebMethod]
        public static string GetReasonsList()
        {
            db_data _data = new db_data();
            DataSet ds = new DataSet();
            ds = _data.GetReasonListForCameraPopup();
            return DataTableToJSONWithJSONNet(ds.Tables[0]);
        }
        public static string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}