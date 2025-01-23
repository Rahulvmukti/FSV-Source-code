using exam.DAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace exam
{
    public partial class SwapCamera : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();
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
                if (!IsPostBack)
                {
                    DataSet ds1 = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds1.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds1.Tables[0].Rows[0]["stateid"]);
                    Session["UserName"] = ds1.Tables[0].Rows[0]["username"].ToString();
                    LoadDistrict(usertype);
                    LoadPC(ddlDistrict.SelectedItem.Text, usertype);
                    LoadDistrict1(usertype);
                    LoadPC1(ddlDistrict1.SelectedItem.Text, usertype);
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

        private void LoadDistrict(string user_type)
        {
            try
            {
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
        private void LoadPC(string District, string usertype)
        {
            try
            {
                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);
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

        private void LoadDistrict1(string user_type)
        {
            try
            {
                var Alldist1 = _boothlist.GetAllDistrictByStateId(stateid);

                ddlDistrict1.DataSource = Alldist1;
                ddlDistrict1.DataTextField = "district";
                ddlDistrict1.DataValueField = "district";
                ddlDistrict1.DataBind();

                ddlDistrict1.Items.Insert(0, new ListItem("ALL Area", ""));
                ddlAssembly1.Items.Insert(0, new ListItem("ALL SubArea", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict1() -- >  " + ex.Message);
            }
        }
        private void LoadPC1(string District, string usertype)
        {
            try
            {
                var Assembly1 = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict1.SelectedItem.Text);
                ddlAssembly1.Items.Clear();
                ddlAssembly1.DataSource = Assembly1;
                ddlAssembly1.DataTextField = "acname";
                ddlAssembly1.DataValueField = "accode";
                ddlAssembly1.DataBind();
                ddlAssembly1.Items.Insert(0, new ListItem("ALL SubArea", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }

        private void LoadLocation(string District, string usertype)
        {
            try
            {
                var Location = _boothlist.GetassemblywiseLocation(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text);
                ddlLocation.Items.Clear();
                ddlLocation.DataSource = Location;
                ddlLocation.DataTextField = "location";
                ddlLocation.DataValueField = "id";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
            }
            catch (Exception ex)
            {
                Common.Log("Loadoperator() -- >  " + ex.Message);
            }
        }
        private void LoadLocation1(string District, string usertype)
        {
            try
            {
                var Location1 = _boothlist.GetassemblywiseLocation(ddlDistrict1.SelectedItem.Text, ddlAssembly1.SelectedItem.Text);
                ddlLocation1.Items.Clear();
                ddlLocation1.DataSource = Location1;
                ddlLocation1.DataTextField = "location";
                ddlLocation1.DataValueField = "id";
                ddlLocation1.DataBind();
                ddlLocation1.Items.Insert(0, new ListItem("ALL Location", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadLocation1() -- >  " + ex.Message);
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrict.SelectedValue == "0")
                    LoadPC("", usertype);
                else
                    LoadPC(ddlDistrict.SelectedItem.Text, usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
            lblmsg.Text = "";
        }
        protected void ddlDistrict1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrict1.SelectedValue == "0")
                    LoadPC1("", usertype);
                else
                    LoadPC1(ddlDistrict1.SelectedItem.Text, usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict1_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
            lblmsg.Text = "";
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            LoadLocation(ddlDistrict.SelectedItem.Text, usertype);
        }
        protected void ddlAssembly1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            LoadLocation1(ddlDistrict1.SelectedItem.Text, usertype);
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            lblboothID.Text = ddlLocation.SelectedValue.ToString();
            DataSet ds = _boothlist.Getstremid(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text,Convert.ToInt32(lblboothID.Text));
            lblStremid.Text = ds.Tables[0].Rows[0]["streamid"].ToString();
            txtCameraID.Text = ds.Tables[0].Rows[0]["CameraName"].ToString();
        }
        protected void ddlLocation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmsg.Text = "";

            lblBoothID1.Text = ddlLocation1.SelectedValue.ToString();
            DataSet ds = _boothlist.Getstremid(ddlDistrict1.SelectedItem.Text, ddlAssembly1.SelectedItem.Text,Convert.ToInt32(lblBoothID1.Text));
            lblStremid1.Text = ds.Tables[0].Rows[0]["streamid"].ToString();
            txtCameraID1.Text = ds.Tables[0].Rows[0]["CameraName"].ToString();
        }
        protected void btnSwapCamera_Click(object sender, EventArgs e)
            {
            if(lblboothID.Text!=lblBoothID1.Text && lblboothID.Text!="" && lblBoothID1.Text!="")
            {
                DataSet ds = _boothlist.SwapCamera(Convert.ToInt32(lblboothID.Text), Convert.ToInt32(lblBoothID1.Text), Convert.ToInt32(lblStremid.Text), Convert.ToInt32(lblStremid1.Text), Session["UserName"].ToString());
                // lblmsg.Text = ds.Tables[0].Rows[0][0].ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0][0].ToString() + "','success');", true);
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                DataSet dataSet = getDataSet();
                DataSet ds1 = getDataSet();
                string htmlString = getHtml(dataSet);
                string htmlString1 = gethtmlsecond(ds1);
                SendAutomatedEmail(htmlString, htmlString1);
                clear();
                

            }
            else
            {
                //lblmsg.Text = "Please Select diffrent Location";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please Select diffrent Location','error');", true);
            }
           
        }

        public void clear()
        {
            ddlDistrict.SelectedIndex = 0;
            ddlDistrict1.SelectedIndex = 0;
            ddlAssembly.SelectedIndex = 0;
            ddlAssembly1.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlLocation1.SelectedIndex = 0;
            txtCameraID.Text = "";
            txtCameraID1.Text = "";
            lblboothID.Text = "";
            lblBoothID1.Text = "";
            lblStremid.Text = "";
            lblStremid1.Text = "";
        }
        //Email Functionality
        public  DataSet getDataSet()
        {
            string cnString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(cnString);

            // string CommandText12 = "select * from(select top 2 * from boothhistory where boothid in (" + lblboothID.Text +","+ lblBoothID1.Text + ") and isdelete = 1 order by id desc)tbl union  select* from boothhistory where boothid in(" + lblboothID.Text + "," + lblBoothID1.Text + ") and isdelete = 0";
            string CommandText2 = "SendSwapMail";
            SqlCommand sqlCommand = new SqlCommand(CommandText2, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BoothId", lblboothID.Text);
            sqlCommand.Parameters.AddWithValue("@BoothId1", lblBoothID1.Text);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            DataSet dataSet = new DataSet();

            try
            {

                sqlDataAdapter.Fill(dataSet, "header");
                sqlConnection.Close();
            }
            catch (Exception _Exception)
            {
                sqlConnection.Close();

                return null;
            }

            return dataSet;

        }


        public static string getHtml(DataSet dataSet)
        {
            try
            {
                string messageBody = "<font><b>Change 1: </b></font><br><br>";

                if (dataSet.Tables[0].Rows.Count == 0)
                    return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                //string htmlTrupdatedfromStart = "<tr style =\"color:#555555;\">";
                //string htmlTrupdatedfromEnd = "</tr>";
               string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                //string htmlupdatedfromTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                //string htmludtaedfromTdEnd = "</td>";
              
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "" + htmlTdEnd;
                messageBody += htmlTdStart + "Camera Id " + htmlTdEnd;
                messageBody += htmlTdStart + "District " + htmlTdEnd;
                messageBody += htmlTdStart + "Assemblycode " + htmlTdEnd;
                messageBody += htmlTdStart + "AssemblyName " + htmlTdEnd;
                messageBody += htmlTdStart + "PSNo" + htmlTdEnd;
                messageBody += htmlTdStart + "Location" + htmlTdEnd;
                messageBody += htmlTdStart + "Action Date" + htmlTdEnd;
                messageBody += htmlTdStart + "Action By" + htmlTdEnd;
                messageBody += htmlTdStart + "Action From" + htmlTdEnd;
                messageBody += htmlTdStart + "Camera Location Type" + htmlTdEnd;
                messageBody += htmlTdStart + "Action Intiated" + htmlTdEnd;
                messageBody += htmlTdStart + "IP Address" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody += htmlHeaderRowStart;
              //  messageBody += htmlupdatedfromTdStart + "updatedFrom " + htmludtaedfromTdEnd;
                messageBody += htmlHeaderRowEnd;
             
                foreach (DataRow Row in dataSet.Tables[0].Rows)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + Row["Status"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["deviceid"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["district"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["accode"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["acname"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["PSNum"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["location"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["updatedDate"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["updatedBy"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["updatedFrom"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["cameralocationtype"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["Action"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["IPAddress"] + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    //messageBody = messageBody + htmlTrupdatedfromStart;
                    //messageBody = messageBody + htmlupdatedfromTdStart + Row["updatedFrom"] + htmludtaedfromTdEnd;
                    //messageBody = messageBody + htmlTrupdatedfromEnd;
                }
                messageBody = messageBody + htmlTableEnd;

              
                messageBody = messageBody + htmlTableEnd;
                return messageBody;

          
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string gethtmlsecond(DataSet ds)
        {
            string messageBody1 = "<font><b>Change 2: </b></font><br><br>";

            if (ds.Tables[0].Rows.Count == 0)
                return messageBody1;
            string htmlTableStart1 = "<table style=\"border-collapse:collapse; text-align:center;\" >";
            string htmlTableEnd1 = "</table>";
            string htmlHeaderRowStart1 = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
            string htmlHeaderRowEnd1 = "</tr>";
            string htmlTrStart1 = "<tr style =\"color:#555555;\">";
            string htmlTrEnd1 = "</tr>";
            //string htmlTrupdatedfromStart = "<tr style =\"color:#555555;\">";
            //string htmlTrupdatedfromEnd = "</tr>";
            string htmlTdStart1 = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
            string htmlTdEnd1 = "</td>";
            //string htmlupdatedfromTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
            //string htmludtaedfromTdEnd = "</td>";

            messageBody1 += htmlTableStart1;
            messageBody1 += htmlHeaderRowStart1;
            messageBody1 += htmlTdStart1 + "" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Camera Id " + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "District " + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Assemblycode " + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "AssemblyName " + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "PSNo" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Location" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Action Date" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Action By" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Action From" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Camera Location Type" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "Action Intiated" + htmlTdEnd1;
            messageBody1 += htmlTdStart1 + "IP Address" + htmlTdEnd1;
            messageBody1 += htmlHeaderRowEnd1;
            messageBody1 += htmlHeaderRowStart1;
            //  messageBody += htmlupdatedfromTdStart + "updatedFrom " + htmludtaedfromTdEnd;
            // messageBody1 += htmlHeaderRowEnd1; messageBody1 += "<font>The following are the new records: </font><br><br>";
            foreach (DataRow Row in ds.Tables[1].Rows)
            {
                messageBody1 = messageBody1 + htmlTrStart1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["status"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["deviceid"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["district"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["accode"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["acname"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["PSNum"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["location"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["updatedDate"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["updatedBy"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["updatedFrom"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["cameralocationtype"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["Action"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTdStart1 + Row["IPAddress"] + htmlTdEnd1;
                messageBody1 = messageBody1 + htmlTrEnd1;
                //messageBody = messageBody + htmlTrupdatedfromStart;
                //messageBody = messageBody + htmlupdatedfromTdStart + Row["updatedFrom"] + htmludtaedfromTdEnd;
                //messageBody = messageBody + htmlTrupdatedfromEnd;
            }
            messageBody1 = messageBody1 + htmlTableEnd1;


            messageBody1 = messageBody1 + htmlTableEnd1;

            return messageBody1;
        }
        public static void SendAutomatedEmail(string htmlString,string htmlString1)

        {
            try
            {
                string userName = ConfigurationManager.AppSettings["SMTPUSERNAME"].ToString();
                string password = ConfigurationManager.AppSettings["SMTPPASSWORD"].ToString();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                string ToEmail="";
                string[] multi = ToEmail.Split(',');
                foreach(string multiemailid in multi )
                {
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["MailTo"].ToString()));
                }
                message.CC.Add(new MailAddress(ConfigurationManager.AppSettings["MailCC"].ToString()));
                message.Subject = "Swap Camera Activity In Booth";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString+htmlString1 ;
               // message.Body = htmlString1;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {

            }

        }
    }
}