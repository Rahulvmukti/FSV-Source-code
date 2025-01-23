using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using exam.DAL;
namespace exam.services
{
    public class LoadTopSelections
    {
        db_data _boothlist = new db_data();
        public string pcname = ConfigurationManager.AppSettings["pcname"].ToString();
        public string assemblyname = ConfigurationManager.AppSettings["assemblyname"].ToString();
        public string district = ConfigurationManager.AppSettings["district"].ToString();
        public string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
        public void LoadDistrict(string usertype, string utypeall, int stateid, out DataSet ds_district)
        {
            try
            {
                DataSet ds_dist;
                if (utypeall.StartsWith("eci"))
                {
                    ds_dist = _boothlist.GetDistrictListECI(usertype, stateid);
                }
                else
                {
                    ds_dist = _boothlist.GetDistrictList(usertype, stateid);
                }
                //if (usertype.StartsWith("dst") == false)
                //{
                //    DataRow dr = ds_dist.Tables[0].NewRow();
                //    dr[0] = allKeyword + district;
                //    dr[1] = "0";
                //    ds_dist.Tables[0].Rows.InsertAt(dr, 0);
                //}
                ds_district = ds_dist;

            }
            catch (Exception ex)
            {
                ds_district = null;
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
       

        //public void LoadPC(string District, string usertype, string selectpc,  string utypeall, int stateid, out DataSet ds_PCsource)
        //{
        //    try
        //    {
        //        ds_PCsource = null;
        //        if (!utypeall.StartsWith("pc_"))
        //        {
        //            DataSet ds_PC;
                   
                 
        //                ds_PC = _boothlist.GetPCFromDistrict(District, usertype, stateid);
        //                //DataRow dr = ds_PC.Tables[0].NewRow();
        //                //dr[0] = allKeyword + pcname;
        //                //dr[1] = "0";
        //                //ds_PC.Tables[0].Rows.InsertAt(dr, 0);

                    
        //            ds_PCsource = ds_PC;

        //            db_data db = new db_data();
        //            ds_PCsource = db.FilterDataByAccess(ds_PC, false, true);

        //            DataRow dr2 = ds_PCsource.Tables[0].NewRow();
        //            dr2[0] = allKeyword + pcname;
        //            dr2[1] = "";
        //            ds_PCsource.Tables[0].Rows.InsertAt(dr2, 0);
        //            //ddlPC.DataSource = ds_PC;
        //            //ddlPC.DataTextField = "accode";
        //            //ddlPC.DataValueField = "accode";
        //            //ddlPC.DataBind();
        //            /* int sch_count = ds_PC.Tables[0].Rows.Count;
        //             if (sch_count > 1 || sch_count == 0)
        //             {
        //                 ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));
        //                 ddlpsbooth.Items.Insert(0, new ListItem("ALL Booth", "0"));
        //             }
        //             else
        //             {
        //                 if (utypeall.StartsWith("sch_"))
        //                 {
        //                     ddlPC.Items.Clear();
        //                     ddlPC.Items.Insert(0, new ListItem(usertype.Split('_')[2], "0"));
        //                 }
        //                 else
        //                 {
        //                     ddlPC.Items.Insert(0, new ListItem("SELECT " + pcname, "0"));
        //                 }
        //             }*/
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        ds_PCsource = null;
        //        Common.Log("LoadSchool_list() -- >  " + ex.Message);
        //    }
        //}

        //public void LoadSchool(string District, string PCname, string usertype, string utypeall, int stateid, out DataSet ds_boothSource)
        //{
        //    try
        //    {
        //        ds_boothSource = new DataSet();
        //        DataSet ds_booth;
        //        if (utypeall.StartsWith("eci"))
        //        {
        //            ds_booth = _boothlist.GetBoothFromDistrictECI(District, usertype, stateid);
        //        }
        //        else
        //        {
        //            ds_booth = _boothlist.GetBoothFromDistrictPC(District, PCname, usertype);
        //        }
        //        DataRow dr = ds_booth.Tables[0].NewRow();
        //        dr[0] = allKeyword + assemblyname;
        //        dr[1] = "0";
        //        ds_booth.Tables[0].Rows.InsertAt(dr, 0);
        //        ds_boothSource = ds_booth;

        //       /* ddlbooth.DataSource = ds_booth;
        //        ddlbooth.DataTextField = "acname";
        //        ddlbooth.DataValueField = "acname";
        //        ddlbooth.DataBind();
        //        int sch_count = ds_booth.Tables[0].Rows.Count;
        //        if (sch_count > 1 || sch_count == 0)
        //        {
        //            ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));
        //            ddlpsbooth.Items.Insert(0, new ListItem("ALL Booth", "0"));

        //        }
        //        ddlbooth.SelectedIndex = 0;

        //        loadpsbooth(ddlDistrict.SelectedItem.Text, ddlPC.SelectedItem.Text, ddlbooth.SelectedItem.Text, usertype);*/
        //    }
        //    catch (Exception ex)
        //    {
        //        ds_boothSource = null;
        //        Common.Log("LoadSchool_list() -- >  " + ex.Message);
        //    }
        //}



    }
}