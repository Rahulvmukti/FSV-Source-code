using exam.BAL;
using exam.DAL;
using exam.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace exam
{
    public partial class RouteView : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();
        LoadTopSelections objloadtop = new LoadTopSelections(); 
        db_list_lq c1 = new db_list_lq();
        public string district = ConfigurationManager.AppSettings["district"].ToString();
        public string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
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
        public string usernametotal = ConfigurationManager.AppSettings["usertotal"].ToString();
        public int usernametotalen = Convert.ToInt32(ConfigurationManager.AppSettings["usertotalen"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                Session["MenuName"] = "LiveTracking";
                //var oresp = GetVehicleDetails();
                if (!IsPostBack)
                {
                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    utypeall = usertype;
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    _usertype = usertype;
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                        if (utypeall.Contains("_all"))
                        {

                            if (ddlDistrict.SelectedIndex > 0)
                            {
                                usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                                LoadBooth(usertype);
                            }
                            else
                            {

                                LoadBooth(usertype);
                            }
                        }
                        else if (utypeall.StartsWith("sch"))
                        {
                            usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                            LoadBooth(usertype);
                        }
                        else
                        {
                            // LoadBooth("dst_" + ddlDistrict.SelectedItem.Text);

                            LoadBooth("dst_" + ddlDistrict.SelectedItem.Text);
                        }
                    }
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

        //private GPSAPIResponse GetVehicleDetails()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://pullapi.tracknowgps.com/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue BasicAuthenticationHeaderValue("Vmspl", "Vmukti2021");
        //        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("Vmspl" + ":" + "Vmukti2021"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
        //        //GET Method
        //        HttpResponseMessage response = client.GetAsync("api/v1/index.php/getDevPos").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string res = response.Content.ReadAsStringAsync().Result;
        //            GPSAPIResponse oresponse = JsonConvert.DeserializeObject<GPSAPIResponse>(res);
        //            return oresponse;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
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
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
        private void LoadSchool(string District, string usertype)
        {
            try
            {
                
                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();
                ddlAssembly.Items.Insert(0, new ListItem("ALL SubArea", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadSchool_map() -- >  " + ex.Message);
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (utypeall.Contains("_all"))
                {
                    if (ddlDistrict.SelectedValue == "0")
                    {
                        //DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                        usertype = utypeall.Replace("_all", "");
                    }
                    else
                    {
                        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                    }
                }
                else
                {
                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                }
                LoadSchool(ddlDistrict.SelectedItem.Text, usertype);
                ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(ddlDistrict.SelectedItem.Text));
                Session["ddldistgrid"] = ddlDistrict.SelectedIndex;
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
                // string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
                //LoadBooth_filter(usertype, fromdate);
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_map() -- >  " + ex.Message);
            }
        }
        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {  if (ddlAssembly.SelectedValue == "0")
                {
                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                }
                else
                {
                    usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                }
                LoadLocation();
               
            }
            catch (Exception ex)
            {
                Common.Log("ddlAssembly_SelectedIndexChanged_map() -- >  " + ex.Message);
            }
        }

        private void LoadLocation()
        {
            try
            {
                var Location = _boothlist.GetAlllocationbyAcname(ddlAssembly.SelectedValue);
                ddlLocation.Items.Clear();
                ddlLocation.DataSource = Location;
                ddlLocation.DataTextField = "Location";
                ddlLocation.DataValueField = "Location";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
                
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        private void LoadBooth(string usertype)
        {
            try
            {
                

                string json = Common.Encode(usertype);
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", " jQuery(function ($) {LoadLocation_route('" + json + "',0);});", true);


            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_map() -- >  " + ex.Message);
            }
        }
        private void LoadBooth_filter(string usertype, string selectedDate,string location)
        {
            try
            {
                string json = Common.Encode(usertype);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "jQuery(function ($) {LoadLocation_route('" + json + "', '" + selectedDate + "', '" + location + "', 0);});", true);
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_map() -- >  " + ex.Message);
            }
        }

        [WebMethod]

        public static string LoadMap(string d, string selectedDate,string location)
        {
            try
            {
                db_data _boothlist = new db_data();
                DataSet ds = new DataSet();

                ds = _boothlist.GetRouteList(Common.Decode(d), false, 1, selectedDate,location);

                string json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return json;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlLocation.SelectedIndex != 0)
                {
                    string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
                    LoadBooth_filter(usertype, fromdate, ddlLocation.SelectedItem.Text);
                }

            }

            catch (Exception ex)
            {

            }
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            LoadBooth_filter(usertype,fromdate, ddlLocation.SelectedItem.Text);
        }
    }
    public class RouteData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // Add other properties here if necessary
    }



}