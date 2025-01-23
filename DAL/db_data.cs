using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace exam.DAL
{
    public class db_data
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
        public string stcode = ConfigurationManager.AppSettings["stcode"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        public string currentphase = ConfigurationManager.AppSettings["stateid"].ToString();
        public DateTime start_hour = Convert.ToDateTime(ConfigurationManager.AppSettings["starthour"].ToString());
        public DateTime end_hour = Convert.ToDateTime(ConfigurationManager.AppSettings["endhour"].ToString());
        string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
        string districtname = ConfigurationManager.AppSettings["district"].ToString();



        public DataSet GetUser(string username, string password)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUser", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetUser()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }



        public DataSet GetUserData(string username)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUserData", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        public DataSet GetMapBoothList(string usertype, bool isgrid, int st_id)
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string[] utype = { };
                dist = usertype.Split('_')[1];
                dist = dist.ToUpper() == "ALL DISTRICT" ? "" : dist;
                if (usertype.StartsWith("sch"))
                {
                    dist_sch = usertype.Split('_')[2];
                }
                query = "GetMapDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dst", dist);
                command.Parameters.AddWithValue("@acname", dist_sch);
                command.Parameters.AddWithValue("@userid", st_id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds1 = FilterDataByAccess(ds, true, true);
                ds1.Tables[0].TableName = "Table";
                ds1.Tables.Add(ds.Tables[1].Copy());
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds1;
        }



        public DataSet GetMapBoothListNew(string district, string assembly, string LocationType, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListNew", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);

                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@psnum", streamname);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetMapBoothListNew_Grid(int userid, string district, string assembly, int statusflag, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "", int PageNumber = 1, int pageitemcount = 6)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListNew1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@psnum", streamname);
                command.Parameters.AddWithValue("@location", streamname);
                command.Parameters.AddWithValue("@statusFlag", statusflag);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@pageIndex", PageNumber);
                command.Parameters.AddWithValue("@pageSize", pageitemcount);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetOnlineMapBoothListNew(string district, string assembly, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetOnlineBoothListNew", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@psnum", streamname);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetBoothList(string usertype, bool isgrid, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string dist_loc = string.Empty;
                string strzone = string.Empty;
                string[] utype = { };
                string tblname = string.Empty;
                if (!isgrid)
                {
                    if (usertype == "Master_Admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By b.acname,b.location asc,len(s.streamname) asc; select * from " + table_prefix + "static_count;";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By b.acname,b.location asc,len(s.streamname) asc";
                    }
                    else if (usertype.StartsWith("zn"))
                    {
                        strzone = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName =@zn) and s.IsEnable=1 and b.boothstateid='" + st_id + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                    }
                    else if (usertype.StartsWith("dst"))
                    {
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != st_id.ToString())
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 and b.boothstateid='" + utype[1] + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        //dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        //query = "select * from booth b inner join streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and b.boothstateid='" + st_id + "' Order By s.status,LEN(s.streamname),s.id";
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != st_id.ToString())
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                            dist_sch = utype[0].Split('_')[2];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist" + tblname + " s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.boothstateid='" + utype[1] + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            dist_sch = usertype.Split('_')[2];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                    }

                    else if (usertype.StartsWith("loc"))
                    {

                        dist = usertype.Split('_')[1];
                        dist_sch = usertype.Split('_')[2];
                        dist_loc = usertype.Split('_')[3];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.psnum=@dist_loc Order By b.acname,b.location asc,len(s.streamname) asc";

                    }

                }
                else
                {
                    if (usertype == "Master_Admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                    else if (usertype.StartsWith("zn"))
                    {
                        strzone = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and b.district in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName =@zn) and s.IsEnable=1 and b.boothstateid='" + st_id + "' Order By s.hasalarm desc";
                    }
                    else if (usertype.StartsWith("dst"))
                    {
                        dist = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and b.boothstateid=" + st_id + " and b.district=@dst and s.IsEnable=1 Order By s.hasalarm desc";
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        dist = usertype.Split('_')[1];
                        // dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        dist_sch = usertype.Split('_')[2];
                        //query = "select * from booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and b.boothstateid='" + st_id + "' Order By s.status,LEN(s.streamname),s.id";
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                }
                SqlCommand command = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(dist))
                {
                    command.Parameters.AddWithValue("@dst", dist);
                }
                if (!string.IsNullOrEmpty(strzone))
                {
                    command.Parameters.AddWithValue("@zn", strzone);
                }
                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(dist_sch))
                {
                    command.Parameters.AddWithValue("@dist_sch", dist_sch);
                }
                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(dist_sch) && !string.IsNullOrEmpty(dist_loc))
                {
                    command.Parameters.AddWithValue("@dist_loc", dist_loc);
                }

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public List<string> GetLocation(string location, string usertype)
        {
            try
            {
                List<string> locationres = new List<string>();
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    return locationres;
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public List<string> Getbooth(string searchtxt, string usertype)
        {
            try
            {
                List<string> locationres = new List<string>();

                return locationres;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }



        public DataSet GetDistrictList(string usertype, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid=" + st_id + " ORDER BY b.District ASC;";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }


        public DataSet Getdistrictid(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                query = @"
                SELECT *
                FROM district
                WHERE id IN (
                    SELECT CAST(value AS INT)
                    FROM STRING_SPLIT((SELECT AssemblyAccesIds FROM users WHERE usercode = 'District_Level' AND id = @UserId), ',')
                )";


                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@UserId", id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //  ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public DataSet GetDistrictListECI(string usertype, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                if (usertype == "Master_Admin" || usertype == "live")
                {
                    query = "SELECT distinct(b.district)district,b.district as SelValue  FROM " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid='" + st_id + "' and s.selected=1 ORDER BY b.District ASC;";
                }
                else if (usertype.Contains("?"))
                {
                    utype = usertype.Split('?');
                    if (utype[1] != st_id.ToString())
                    {
                        tblname = "p" + utype[1];
                    }
                    if (utype[0] == "admin" || utype[0] == "live")
                    {
                        query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth" + tblname + " b inner join " + table_prefix + "streamlist" + tblname + " s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid='" + utype[1] + "' ORDER BY b.District ASC;";
                    }

                    else
                    {
                        dist = utype[0].Split('_')[1];
                        query = "SELECT distinct(b.district)district,b.district as SelValue FROM Booth" + tblname + " b inner join streamlist" + tblname + " s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district =N'" + dist + "' and b.boothstateid='" + utype[1] + "' ORDER BY b.District ASC;";
                        //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [schoolname] ASC;";
                    }

                }
                else if (usertype.StartsWith("zn"))
                {
                    dist = usertype.Split('_')[1];
                    query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join streamlist s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district  in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName ='" + dist + "')  and b.boothstateid='" + st_id + "' ORDER BY b.District ASC;";

                }
                else
                {
                    dist = usertype.Split('_')[1];
                    query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join streamlist s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district = N'" + dist + "' and b.boothstateid='" + st_id + "' and s.selected=1 ORDER BY b.District ASC;";
                    //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [schoolname] ASC;";
                }


                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public void UpdateStatus(string streamname)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "streamlist SET hasalarm='False' WHERE streamname = @streamname;";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.Parameters.Add(new SqlParameter("@streamname", streamname));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                SQLconn.Close();
            }
        }

        public void updateloginstatus(bool status, string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "users set islogin = @islogin, logincount=logincount-1 where username = @username";
                //  string query = query = "UPDATE " + table_prefix + "users set islogin = @islogin where username = @username";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.Parameters.Add(new SqlParameter("@islogin", status));
                SQLcommand.Parameters.Add(new SqlParameter("@username", username));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                SQLconn.Close();
            }
        }

        public bool updatelogincount(string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "users set logincount = logincount+1 where username = @username";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);

                SQLcommand.Parameters.Add(new SqlParameter("@username", username));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }



        public DataSet getUnMappedCamera()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUnMappedCamera", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("getCameraMatchData()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        public DataSet GetAllDistrictByStateId(int stateid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty; 
                query = "getdistrictName"; 
                SqlCommand command = new SqlCommand(query, conn);
                 command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetAllAssemblyByDistrict(int stateid, string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT accode,acname  from " + table_prefix + "district WHERE stateid=" + stateid + " AND district='" + district + "' ORDER BY District ASC;";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, false, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet FilterDataByAccess(DataSet ds, bool filterbyDistrict, bool filterbyAssembly)
        {
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                    var districtlist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).ToArray());
                    var assemblylist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).ToArray());
                    DataTable dt = null;
                    if (filterbyDistrict)
                    {
                        var a = ds.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")));
                        if (a != null && a.Count() > 0)
                            dt = ds.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district"))).CopyToDataTable();
                    }
                    if (filterbyAssembly)
                    {
                        dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var b = dt.AsEnumerable().Where(x => assemblylist.Contains(x.Field<string>("acname")));
                            if (b != null && b.Count() > 0)
                                dt = dt.AsEnumerable().Where(x => assemblylist.Contains(x.Field<string>("acname"))).CopyToDataTable();
                        }
                    }
                    DataSet returnds = new DataSet();
                    if (dt != null)
                        returnds.Tables.Add(dt.Copy());
                    return returnds;
                }
            }
            return ds;
        }

          
        public DataSet GetDistrictAssemblyList()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetDistrictAssemblyList";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictAssemblyList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet UpdateUserDetails(string username, string password, int identifier, int isenable, int stateid, int userid, string AssemblyAccesIds, string usercode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "UpdateUserDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@identifier", identifier);
                command.Parameters.AddWithValue("@isenable", isenable);
                command.Parameters.AddWithValue("@stateid", stateid);
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@AssemblyAccesIds", AssemblyAccesIds);
                command.Parameters.AddWithValue("@usercode", usercode);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("UpdateUserDetails()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetDashboardList(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "GetDashboardList";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetIndoorOutDoorForGraph(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "GetIndoorOutDoorForGraph";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetDashboardDetailListByAssembly(string acname, string status, string CameraType = "", int IsPink = -1)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetDashboardDetailListByAcName";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@CameraType", CameraType);
                command.Parameters.AddWithValue("@IsPink", IsPink);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetPTZViewData(string vehicleNo)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetPTZViewdtels";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vehicalno", vehicleNo);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetPTZViewDatawithcamera(string streamid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetPTZViewdtelscamera";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@streamid", streamid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetShiftWizeVehicleByAcCode(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetLocationbyac";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@location", accode);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetCameraOfflineList(string FromDt, string ToDt, string district, string accode, string vehicleNo, string shift)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOfflineList5";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetCameraStatusReport(string FromDt, string ToDt, string district, string accode, string vehicleNo)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraStatusReport3";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
              //  ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetCameraOnlineStatusReport(string district, string acname, string dt)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOnlineReportDayWise";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@dt", dt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //  ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetUserLoginHistoryReport(string dt)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetUserLoginHistoryReport";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dt", dt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserLoginHistoryReport()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetExcelReport(string FromDt, string ToDt, string district, string accode, string vehicleNo, int islive, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetExeclBoothReport]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.Parameters.AddWithValue("@Status", "");
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetUnmappedCameraList(string CameraID, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                SqlCommand command = new SqlCommand("GetUnmappedCameraList", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CameraID", CameraID);
                command.Parameters.AddWithValue("@Status", status);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet SaveLoginUserHistory(int userid, string ipaddress)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("SaveLoginUserHistory", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@IPAddress", ipaddress));
                SQLconn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQLcommand);
                da.Fill(ds);
                //SQLcommand.ExecuteNonQuery();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public DataSet UpdateUserSessionActivityByUserId(int userid, string sessionid)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("UpdateUserSessionActivityByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet getuserbysessionid(int userid, string sessionid)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("GetActiveUserSessionByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQLcommand);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public bool deleteuserbysessionid(int userid, string sessionid)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("DeleteUserSessionActivityByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet SwapCameraIDs(int id1, int id2)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("swapCameraIDs", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id1", id1);
                command.Parameters.AddWithValue("@id2", id2);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Common.Log("SwapCameraIDs()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet AddUnmapToMapCamera(int id, int stremid, string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = "UnMapToMapCamera";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@StremlistID", stremid);
                SQLcommand.Parameters.AddWithValue("@UserName", username);
                SQLcommand.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("AddUnmapToMapCamera()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet SwapCamera(int id, int id1, int stremid, int stremid1, string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = "SwapCamemainLocation";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@id1", id1);
                SQLcommand.Parameters.AddWithValue("@StremlistID", stremid);
                SQLcommand.Parameters.AddWithValue("@StremlistID1", stremid1);
                SQLcommand.Parameters.AddWithValue("@UserName", username);
                SQLcommand.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("SwapCamera()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet GETMultimappedCamera()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                SqlCommand command = new SqlCommand("GETMultimappedCamera", conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetassemblywiseLocation(string district, string assembly)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetAssemblyWiseLocation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@flag", "Location");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetassemblywiseLocation()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet Getstremid(string district, string assembly, int boothid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetAssemblyWiseLocation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@boothID", boothid);
                command.Parameters.AddWithValue("@flag", "stremid");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("Getstremid()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetOperatorName(int stateid, string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                query = "select * from operator_info WHERE stateid=" + stateid + " AND district='" + district + "'";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetOperatorName()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetTrailRunRpt(string district, string assembly, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListTrailRunRpt", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetTrailRunRpt()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetNetworkFeasiblityRpt()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetNetworkFeasiblityRpt", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetNetworkFeasiblityRpt()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }


        public DataSet GetMapBoothListNew_Download(string district, string assembly, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "", string date = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListNew_download", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@OrderBY", orderBy);
                command.Parameters.AddWithValue("@date", orderBy);
                command.CommandTimeout = 420;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }


        public int SaveStreamList(string deviceid, string servername, string prourl, string userName, string PageName)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("SaveStreamList", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@deviceid", deviceid));
                SQLcommand.Parameters.Add(new SqlParameter("@servername", servername));
                SQLcommand.Parameters.Add(new SqlParameter("@prourl", prourl));
                SQLcommand.Parameters.Add(new SqlParameter("@UserName", userName));
                SQLcommand.Parameters.Add(new SqlParameter("@PageName", PageName));
                SQLconn.Open();
                SqlDataAdapter sqa = new SqlDataAdapter(SQLcommand);
                sqa.Fill(ds);
                int.TryParse(ds.Tables[0].Rows[0]["ID"].ToString(), out int ID);
                return ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        //Booth Master Function
        public DataSet GetMapBoothListNew_Master(string district, string assembly, string Search, string cameratype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothList", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", assembly);
                command.Parameters.AddWithValue("@search", Search);
                command.Parameters.AddWithValue("@cameratype", cameratype);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetDeviceID(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetStreamNameListAutoComplete", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDeviceID()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GETInvStreamName(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetInvStreamListAutoComplete", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GETInvStreamName()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetBoothListMaster(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListByID", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet SaveBooth(int id, int streamid, string OpName, string OpMobNo, string OpDesignation, string district, string accode, string acname, string PSNum, string location, string cameralocationtype, string userName)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = "SaveBoothByID";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@streamid", streamid);
                SQLcommand.Parameters.AddWithValue("@OperatorName", OpName);
                SQLcommand.Parameters.AddWithValue("@OperatorNumber", OpMobNo);
                SQLcommand.Parameters.AddWithValue("@OperatorDesignation", OpDesignation);
                SQLcommand.Parameters.AddWithValue("@district", district);
                SQLcommand.Parameters.AddWithValue("@accode", accode);
                SQLcommand.Parameters.AddWithValue("@acname", acname);
                SQLcommand.Parameters.AddWithValue("@PSNum", PSNum);
                SQLcommand.Parameters.AddWithValue("@location", location);
                SQLcommand.Parameters.AddWithValue("@cameralocationtype", cameralocationtype);
                SQLcommand.Parameters.AddWithValue("@UserName", userName);
                SQLcommand.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("SaveBooth()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public bool DeleteBoothListMaster(int id, string UserName)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                SqlCommand comm = new SqlCommand("DeleteBoothByID", SQLconn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@UserName", UserName);
                comm.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SQLconn.Open();
                comm.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("deletebooth()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public static string DeleteBoothByID(int id, string UserName, string IPAddress)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString);
            try
            {
                string query = "DeleteBoothByID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i > 0 ? "Deleted" : "Failed";
            }
            catch (Exception ex)
            {
                Common.Log("deletebooth()--> " + ex.Message);
                return "Failed";
            }
            finally
            {
                con.Close();
            }
        }
        //streamlist master
        public DataSet GetStreamListMaster(string Search)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetStreamList", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@search", Search);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetStreamListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetStreamListMaster(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetStreamlistByID", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet SaveBulkStreamList(DataTable dt, string username, string updatedfrom)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("BulkInsertCamera", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@tblData", dt);
                SQLcommand.Parameters.AddWithValue("@UserName", username);
                SQLcommand.Parameters.AddWithValue("@UpdatedFrom", updatedfrom);
                SQLconn.Open();
                SqlDataAdapter sqa = new SqlDataAdapter(SQLcommand);
                sqa.Fill(ds);
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
        public DataSet getDataSet(string Action, int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connstr);
            string CommandText2 = "GetLatestBoothHistoryByID";
            SqlCommand sqlCommand = new SqlCommand(CommandText2, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BoothId", id);
            sqlCommand.Parameters.AddWithValue("@Action", Action);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                sqlDataAdapter.Fill(dataSet, "header");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                return null;
            }
            return dataSet;
        }

        public DataSet GetMapBoothListView(int userid, string district, string assembly, string LoactionType, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", int PageNumber = 1, int pageitemcount = 100)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetMapBoothListViewNew1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@LocationType", LoactionType);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@pageIndex", PageNumber);
                command.Parameters.AddWithValue("@pageSize", pageitemcount);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }



        public DataSet GetAlllocationbyAcCode(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT location FROM  booth WHERE Accode ='" + accode + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllVehicleDetail()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetAlllocationbyAcname(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT location FROM  booth WHERE acname ='" + accode + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllVehicleDetail()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet getLocationWise(string district, string acname, string Location)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "getlocationwisedeviceid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@location", Location);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet getgaugechartdtls()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "gaugechartdistrictwise";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("gaugechartdistrictwise()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet getImgNotification(string flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetImagenotification";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", flag);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("getImgNotification()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet Getnotification(string district, string assembly, string sel_date, string todt, string streamname, string imagurl = "", int type = 0)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                SqlCommand command = new SqlCommand("GetAnalyticsNotification", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@dt", sel_date);
                command.Parameters.AddWithValue("@todt", todt);
                command.Parameters.AddWithValue("@DID", streamname);
                command.Parameters.AddWithValue("@ImageURL", imagurl);
                command.Parameters.AddWithValue("@Type", type);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetBoothWiseCamOperator(string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetBoothWiseCamOperator";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Search", district);

                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothWiseCamOperator()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetRecordingSize(string sel_date, string streamname)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                SqlCommand command = new SqlCommand("Sp_RecordingSize", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CameraID", streamname);
                command.Parameters.AddWithValue("@RecordingDate", sel_date);
                command.Parameters.AddWithValue("@flag", "GETRECORDINGDATA");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetRecordingSize()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetChannelviewdata(string district, string assembly)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetChannelviewdata";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@district", district));
                command.Parameters.Add(new SqlParameter("@accode", assembly));
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, false, true);
            }
            catch (Exception ex)
            {
                Common.Log("getLastMinuteBusData()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetAssemblyWizeTotlechannelData()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetAssemblyWizeTotlechannelData";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("getLastMinuteBusData()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet getvideobitratedata(string videodate, string district, string acname)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                SqlCommand command = new SqlCommand("Savevideobitrate", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", acname);
                command.Parameters.AddWithValue("@VideoDate", videodate);
                command.Parameters.AddWithValue("@flag", "getBitrate");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetRecordingSize()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet getservername(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                SqlCommand command = new SqlCommand("getservarname1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Camearaid", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetRecordingSize()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet AddIncidenceDetail(int incidenceid, string districtNme, string assemblyNme, string vehicleId, string incidenceDetailText, string incidencedate)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "AddIncidenceDetail";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@districtName", districtNme);
                command.Parameters.AddWithValue("@acName", assemblyNme);
                command.Parameters.AddWithValue("@vehicleId", vehicleId);
                command.Parameters.AddWithValue("@incidenceDetailText", incidenceDetailText);
                command.Parameters.AddWithValue("@incidenceid", incidenceid);
                DateTime datetime = DateTime.ParseExact(incidencedate, "dd/MM/yyyy", null);
                // datetime = Convert.ToDateTime(datetime, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                command.Parameters.AddWithValue("@incidenceDateTime", datetime);
                command.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("AddIncidenceDetail()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetIncidenceDetails(string district, string assembly, string serchtxt)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetIncidenceDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@districtName", district);
                command.Parameters.AddWithValue("@accode", assembly);
                command.Parameters.AddWithValue("@serchtext", serchtxt);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                // ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllIncidenceDetails()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetGeoFenceDetails(string district, string assembly, string serchtxt)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetGeofenceDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@districtName", district);
                command.Parameters.AddWithValue("@acname", assembly);
                command.Parameters.AddWithValue("@serchtext", serchtxt);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("GetGeofenceDetails()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet AddGeoFenceDetails(int geofencingid, string incidenceDetailText, string flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "AddGeofenceDetail";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@remarks", incidenceDetailText);
                command.Parameters.AddWithValue("@geofencingid", geofencingid);
                command.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
 
                command.Parameters.AddWithValue("@flag", flag);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("AddIncidenceDetail()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet Getincidencebyid(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetincidencbyID", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetincidencbyID()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }


        public bool DeleteIncidenceDetail(int incidenceId, string UserName)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                SqlCommand comm = new SqlCommand("DeleteIncidenceDetail", SQLconn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@id", incidenceId);
                comm.Parameters.AddWithValue("@UserName", UserName);
                SQLconn.Open();
                comm.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("DeleteIncidenceDetail()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public DataSet GetIncidenceDetails(int incidenceid = 0, string fromDt = "", string toDt = "", string district = "", string assembly = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetIncidenceDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@incidenceid", incidenceid);
                command.Parameters.AddWithValue("@districtName", district);
                command.Parameters.AddWithValue("@accode", assembly);
                command.Parameters.AddWithValue("@FromDt", fromDt);
                command.Parameters.AddWithValue("@ToDt", toDt);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //   ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllIncidenceDetails()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetDistrictList1()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                query = "getdistrictname1";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public DataSet GetRouteList(string usertype, bool isgrid, int st_id, string fromdate, string location)
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string[] utype = { };
                dist = usertype.Split('_')[1];
                dist = dist.ToUpper() == "ALL DISTRICT" ? "" : dist;
                //if (usertype.StartsWith("sch"))
                //{
                //    dist_sch = usertype.Split('_')[2];
                //}

                query = "GetMapDetails_routeToday";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dst", dist);
                command.Parameters.AddWithValue("@acname", dist_sch);
                command.Parameters.AddWithValue("@userid", st_id);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@Fromdate", fromdate);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetBoothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetAllVehicleByAcCode(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("dbo.GetAllVehicleByAcCode", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@accode", accode);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetAllVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }



        public DataSet GetStopVehicleList(string FromDt, string ToDt, int districtid, string accode = "", int vehicleId = 0, int ShiftID = 0)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetStopVehicleList";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.Parameters.AddWithValue("@districtid", districtid);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicleId", vehicleId);


                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetStopVehicleList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetAllInventoryUpdationReport(string FromDt, string ToDt, string districtid, string accode, string vehicleId, int ShiftID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                string query = string.Empty;
                query = "GetInventoryUpdationReport";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.Parameters.AddWithValue("@districtid", districtid);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicleId", vehicleId);
                command.Parameters.AddWithValue("@ShiftID", ShiftID);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllInventoryUpdationReport()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetallInventoryData(string districtid, string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetInventoryUpdation_12";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@districtid", districtid);
                command.Parameters.AddWithValue("@accode", accode);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllInventoryUpdationReport()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetCameraOfflineListForPopup(string FromDt, string ToDt, string district, string accode, string vehicleNo, int UserID = 0)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOfflineListForPopup";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicleno", vehicleNo);
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public void SaveCameraStopReasonsByID(int ID, string Reason)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "SaveCameraStopReasonsByID";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@id", ID));
                SQLcommand.Parameters.Add(new SqlParameter("@Reason", Reason));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
            }
            catch (Exception ex)
            {
                Common.Log("SaveGPSStopReasonsByID()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet GetReasonListForCameraPopup()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetReasonListForCameraPopup";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetReasonListForCameraPopup()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetAllMaterial()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT [id], [name], [desc], [active] FROM [dbo].[material]";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllMaterial()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetInventoryUpdationById(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("dbo.GetInventoryUpdationById", conn))
                {
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetInventoryUpdationById()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet AddInventoryUpdation(int repairid, string districtname, string acname, string vehicleNo, string material, string oldsnno, string newsnno, string accode, int status, string startdate, string enddate, string addedby, string updatedby,string remarks)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "AddInventoryUpdation";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@districtName", districtname);
                command.Parameters.AddWithValue("@acName", acname);
                command.Parameters.AddWithValue("@vehicleId", vehicleNo);
                command.Parameters.AddWithValue("@material", material);
                command.Parameters.AddWithValue("@oldsrno", oldsnno);
                command.Parameters.AddWithValue("@newserno", newsnno);
                command.Parameters.AddWithValue("@acCode", accode);
                command.Parameters.AddWithValue("@RepairReplaceId", repairid);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@startDate", startdate);
                command.Parameters.AddWithValue("@EndDate", enddate);
                command.Parameters.AddWithValue("@addedby", addedby);
                command.Parameters.AddWithValue("@updatedby", updatedby);
                command.Parameters.AddWithValue("@remarks", remarks);
                command.CommandType = CommandType.StoredProcedure;
                //  query = "INSERT INTO [dbo].[inventoryupdatation] VALUES ('" + districtname + "','" + acname + "','" + vehicleNo + "','" + material + "','" + oldsnno + "','" + newsnno + "', '" + accode + "', '" + status + "', '" + startdate + "', '" + enddate + "','" + addedby + "','" + updatedby + "');";

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("AddMaterial()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet getAcWise(string district, string acname)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "getacwisedeviceid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", acname);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetKITHANDOVERRpt(string serchtext, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("kithandover", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@txtserch", serchtext);
                command.Parameters.AddWithValue("@DIDStatus", status);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetNetworkFeasiblityRpt()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        //GetZone
        public DataSet GetZone()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;

                query = "SP_VehicleInstallation";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETZONE");
                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //Getdistrict by zone
        public DataSet GetAllistictbyzone(string zone)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SP_VehicleInstallation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETDistrict");
                command.Parameters.AddWithValue("@Zone", zone);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //Getacname by zone,distict
        public DataSet GetAllacnamebyzoneandistrict(string zone, string distict)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SP_VehicleInstallation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETacname");
                command.Parameters.AddWithValue("@Zone", zone);
                command.Parameters.AddWithValue("@District", distict);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //Vehicle_installation main method
        public DataSet GetInstallVehicle(string zone, string district, string assembly)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("SP_VehicleInstallation", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Zone", zone);
                command.Parameters.AddWithValue("@District", district);
                command.Parameters.AddWithValue("@acname", assembly);
                command.Parameters.AddWithValue("@flag", "GETDATA");

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        //save vehicle installation
        public DataSet SaveVehicleinstallation(int id, string zone, string district, string acname, string expecteddate, string vehiclenumber, string vehicletype, string drivename, string drivenumber, string installationdate, string status, string userName, string deviceid)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                // Assuming installationdate comes in "yyyy-MM-dd" format and you want to add the current time
                string installationDateTime = installationdate + " " + DateTime.Now.ToString("HH:mm:ss");
                // Now installationDateTime includes the current time

                string query = "SP_VehicleInstallation";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@Zone", zone);
                SQLcommand.Parameters.AddWithValue("@district", district);
                SQLcommand.Parameters.AddWithValue("@acname", acname);
                SQLcommand.Parameters.AddWithValue("@expecteddate", expecteddate);
                SQLcommand.Parameters.AddWithValue("@vehicleno", vehiclenumber);
                SQLcommand.Parameters.AddWithValue("@VehicleType", vehicletype);
                SQLcommand.Parameters.AddWithValue("@driverName", drivename);
                SQLcommand.Parameters.AddWithValue("@driverno", drivenumber);
                SQLcommand.Parameters.AddWithValue("@Installationdate", installationDateTime); // Modified to include time
                SQLcommand.Parameters.AddWithValue("@status", status);
                SQLcommand.Parameters.AddWithValue("@username", userName);
                SQLcommand.Parameters.AddWithValue("@deviceid", deviceid);
                SQLcommand.Parameters.AddWithValue("@flag", "UPDATE");
                SQLcommand.Parameters.AddWithValue("@ipaddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());

                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("SaveVehicleinstallation()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }

        //vehiclebyid
        public DataSet Getvehiclebyid(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("SP_VehicleInstallation", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@flag", "GETDATAbyid");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        //vehicleIndooroutdoorchart
        public DataSet Vehicleindooroutdoorgraph(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "GetIndoorOutDoorForGraph_installation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                // ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        //vehicletypeReport
        public DataSet GetVehicletypeReport(string zone, string district, string accode, string vehicletype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetVehicleReport]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@zone", zone);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicleType", vehicletype);

                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //VehicleType
        public DataSet GetAllVehicleType()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SP_VehicleInstallation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "VehicleType");

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetExcelReport1(string zone, string FromDt, string ToDt, string district, string accode, string vehicleNo, int islive, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetExeclBoothReport_install]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@zone", zone);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);

                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                //command.Parameters.AddWithValue("@islive", islive);
                command.Parameters.AddWithValue("@Status", status);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetgpsdailyReport(string FromDt, string district, string acname,int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                string query = string.Empty;
                query = "GetMapDetails_DailyReport";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dst", district);
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@userid", 0);
                command.Parameters.AddWithValue("@Fromdate", FromDt);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllInventoryUpdationReport()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetgeofenceReport(string district, string acname, int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                string query = string.Empty;
                query = "GetGeofenceDetailsRpt";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@districtName", district);
                command.Parameters.AddWithValue("@acname", acname);
                //command.Parameters.AddWithValue("@userid", 0);
                //command.Parameters.AddWithValue("@Fromdate", FromDt);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllInventoryUpdationReport()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetHelpdeskactivity(string district, string assembly,string username,string flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SPHelpdeskuserActivity";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@District", district);
                command.Parameters.AddWithValue("@acname", assembly); 
                command.Parameters.AddWithValue("@username", username); 
                command.Parameters.AddWithValue("@flag", flag); 
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("GetHelpdeskactivity()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet AddHelpdeskactivity(int id,string districtNme, string assemblyNme, string vehicleId, string activity, string username,string flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SPHelpdeskuserActivity";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@District", districtNme);
                command.Parameters.AddWithValue("@acname", assemblyNme);
                command.Parameters.AddWithValue("@VehicleNo", vehicleId);
                command.Parameters.AddWithValue("@activity", activity);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                command.Parameters.AddWithValue("@flag",flag);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("AddHelpdeskactivity()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetHelpdeskactivitybyid(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("SPHelpdeskuserActivity", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@flag", "GetOneData");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetHelpdeskactivitybyid()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetAllDistrictByuserid(int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                query = "getdistrictNamebyuserid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", userid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("GetAllDistrictByuserid()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetAllacnameByuserid(string district,int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                query = "getacnamebyuserid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@userid", userid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllDistrictByuserid()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetVehicleByAcCode(string acname)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "Getvehiclebyacname";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acname", acname);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetHelpdeskactivitydtls(string fromDt = "", string toDt = "", string district = "", string assembly = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SPHelpdeskActivityAll";
                SqlCommand command = new SqlCommand(query, conn); 
                command.Parameters.AddWithValue("@District", district);
                command.Parameters.AddWithValue("@acname", assembly);
                command.Parameters.AddWithValue("@FromDt", fromDt);
                command.Parameters.AddWithValue("@ToDt", toDt); 
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("GetHelpdeskactivitydtls()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetCameraOfflineList11(string FromDt, string ToDt, string district, string accode, string vehicleNo, string shift)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOfflineList11";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicle", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetStaticacname(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "Getacnamestatic";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", 0);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

    }
}
                    