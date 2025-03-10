﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="InstallationStatus.aspx.cs" Inherits="exam.InstallationStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css"/> 
    <style type="text/css">
        @media (min-width: 768px) {
            .box-pd {
                padding: 5px 2px;
                flex: 0 0 20%;
                max-width: 20%;
            }
        }
        .btnexport {
  background-image: url('images/download-24.png');
  background-size: contain;
  background-repeat: no-repeat;
  width: 100%; 
  padding: 0px 12px; /* remove padding */
  border: none; /* remove border */
  cursor: pointer; 
}
        .collapsbtnpd{sssss
               
            background-color: #aeb0b1;
        }
        .btn-tool {
            background-color: transparent;
            color: #fff;
        }
        /*.info-box
        {
            border-radius: 15px;
            text-align: center;
            display: block;
        }
        .info-box .info-box-icon
        {
            padding: 3px 0px;
            text-align: center;
    width: 100%;
        }*/
        .bg-primary {
    background-color: #094D92!important;
}
         .btn-primary { 
    background-color: #094D92!important;
    border-color: #094D92!important;
}
        .btn-primary:hover {
    color: #fff;
    background-color: #094D92!important;
    border-color: #094D92!important;
}
        .bg-warning {
    background-color: #236a8d!important;
}
        .bg-success {
    background-color: #68b187!important;
}
        .btn-success {
    color: #fff;
    background-color: #68b187!important;
    border-color: #68b187!important;
    box-shadow: none;
}
        .lastonehour {
    background: #17a2b8!important;
} .offlinediv {
    background: #d54d4d!important;
}
       .whiteText {
    color: #fff;
}
       .cnectonediv {
    background-color: #91C8E4!important;
}
       .dark-mode .btn-primary {
     color: #fff; 
     background-color: #007bff; 
     border-color: #007bff; 
     box-shadow: none; 
}
        .dark-mode .btn-success {
            color: #fff;
            background-color: #28a745 !important;
            border-color: #28a745 !important;
            box-shadow: none;
        }
        .table td, .table th {
    padding: 3px 10px;
    vertical-align: top;
    border-top: none;
}
        .info-box .info-box-number {
    font-size: 24px;

}
        .btnwidth2{
            font-size:20px;
            padding:2px;
        }
        .btnplus{
            padding:15px 10px;
        }
        .shadow {
    padding: 10px 0px;
}
                label:not(.form-check-label):not(.custom-file-label) {
    font-weight: 400;
}
        input[type=checkbox], input[type=radio] { 
    visibility: hidden;
}
        .checked-radio-button {
    background-color: #e7e7e7;
}
        .selectedRadioButton
        {
            background-color: #e7e7e7;
        }
       .unselectedRadioButton {
    background-color: #f3f3f3;
} 
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $('.camerastatusmenu').addclass('active')
    </script>
    <div class="content-wrapper">
         
        <section class="content">
      <div class="container-fluid">
        <!-- Info boxes -->
           
           <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                         
        <div class="row">
              <div class="col-12 col-sm-8 col-md-4">
            <div class="info-box">
              <span class="info-box-icon elevation-1" style="background-color:#236a8d">
                  <i class='fas fa-person-booth' style='font-size:35px;color: white;'></i></span>

              <div class="info-box-content">
                <span class="info-box-text">Total Installed</span>
                <span class="info-box-number" runat="server" id="totalbooth">
                  <%--<small>%</small>--%>
                </span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
          <!-- /.col -->
         
          <!-- /.col -->
             

           <div class="col-12 col-sm-8 col-md-4">
            <div class="info-box">
              <span class="info-box-icon bg-success elevation-1"><%--<i class="fas fa-shopping-cart"></i>--%>
                <i class="fa fa-video-camera" style="font-size:35px;"></i></span>
              <div class="info-box-content">
                <span class="info-box-text">Installed</span>
                 <span class="info-box-number" id="runningbooth" runat="server"></span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
          <!-- /.col -->

              <div class="col-12 col-sm-8 col-md-4">
            <div class="info-box">
              <span class="info-box-icon elevation-1" style="background-color:#d54d4d">
                 <i class='fas fa-video-slash' style="color:#fff"></i></span>

              <div class="info-box-content">
                <span class="info-box-text">UnInstalled</span>
                 <span class="info-box-number" id="stopbooth" runat="server"></span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>  
        <div style="width:4%; margin:6px">
                
           </div>
            <div style="width:4%; margin:6px">
                
           </div>
            <div style="width:4%; margin:6px">
                
           </div>
        </div> 
                             
           <div class="row">
            <div class="card-body shadow" style="max-height:450px;overflow:auto; padding:0px">       
                <table id="tblboothlist" class="table">
                        <thead class="f-size-sm f-color-secondary border-default border-b text-left">
        <tr>
            <th class="p-xs" colspan="7"><asp:Literal runat="server" Text="Zone List" /></th>
        </tr>
    </thead>
                           
                                    <%if (BoothList != null)
                                        {
                                            if (BoothList.Tables.Count > 0)
                                            {%>

  <tr>
                        
    <th>
   </th>
 <% if ((Session["userType"].ToString().ToLower() == "master_admin" || 
         Session["userType"].ToString().ToLower() == "vmukti_internal" || 
         Session["userType"].ToString().ToLower() == "ceo" ||
         Session["AssemblyIdsString"].ToString().Length >= 1) && 
       (Session["userType"].ToString().ToLower() != "assembly_level" && 
        Session["userType"].ToString().ToLower() != "eci" 
        )) 
{ %>
   
        <th style="font-size: 16px; text-align: center; font-weight: 400;">
    <label class="sort-label">
        <asp:CheckBox ID="testdist" runat="server" OnCheckedChanged="RadioButton_CheckedChanged" CssClass="unselectedRadioButton" GroupName="headerRadioButton"   Style="display: none;" Text="DISTRICT" />
      <%--   <span class="sort-toggle" style="font-size: 12px;">&#9650;</span>--%>
           <span class="sort-text">Zone</span>
     <%--<span class="sort-toggle" style="font-size: 12px;">&#9660;</span>--%>
     
    </label>
</th>


      <th style=" font-size: 16px; text-align: center;font-weight: 400;">
           <label class="sort-label">
<%--<asp:RadioButton runat="server" Text="TOTAL PS" GroupName="headerRadioButton" CssClass="unselectedRadioButton" ID="test" OnCheckedChanged="RadioButton_CheckedChanged" />--%>
           <asp:CheckBox ID="testtotalps" runat="server" OnCheckedChanged="RadioButton_CheckedChanged"  GroupName="headerRadioButton" CssClass="unselectedRadioButton" Style="display: none;" Text="TOTAL PS" />
             
         <span class="sort-text">TOTAL</span>
    
<th style=" font-size: 16px; text-align: center;font-weight: 400;">
       <label class="sort-label">
<%--<asp:RadioButton runat="server" Text="TOTAL PS" GroupName="headerRadioButton" CssClass="unselectedRadioButton" ID="test" OnCheckedChanged="RadioButton_CheckedChanged" />--%>
           <asp:CheckBox ID="testonline" runat="server" OnCheckedChanged="RadioButton_CheckedChanged"  GroupName="headerRadioButton" CssClass="unselectedRadioButton" Style="display: none;"  Text="ONLINE" />
      <%--     <span class="sort-toggle" style="font-size: 12px;">&#9650;</span>--%>
               <span class="sort-text">INSTALLED</span>
     <%--<span class="sort-toggle" style="font-size: 12px;">&#9660;</span></th>   --%>

       <label class="sort-label">
   <th style=" font-size: 16px; text-align: center;font-weight: 400;">
       <label class="sort-label">
<%--<asp:RadioButton runat="server" Text="TOTAL PS" GroupName="headerRadioButton" CssClass="unselectedRadioButton" ID="test" OnCheckedChanged="RadioButton_CheckedChanged" />--%>
           <asp:CheckBox ID="testoffline" runat="server" OnCheckedChanged="RadioButton_CheckedChanged"  GroupName="headerRadioButton" CssClass="unselectedRadioButton"  Style="display: none;" Text="OFFLINE"/>
           <%--<span class="sort-toggle" style="font-size: 12px;">&#9650;</span>--%>
             <span class="sort-text">UNINSTALLED</span>
<%--     <span class="sort-toggle" style="font-size: 12px;">&#9660;</span></th>     --%>      

  </tr>

       <%} %>    
                            <% for (int i = 0; i < BoothList.Tables[0].Rows.Count; i++)
                                                {
                                    %>
                                    <tbody>
                                    <tr class='<%=i%2!=0?"bg-light":"" %>'>
                                        <td class="py-xs px-sm" style="width: 2%;">
                                           <div>
                                                <a id="a_<%=i %>" class='btnplus btn btn-primary btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" 
                                                    onclick="loadac('<%=exam.Common.Encode("010$"+ BoothList.Tables[0].Rows[i]["Zone"].ToString())%>',<%=i %>)">
                                                     <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i>
                  </button>
                                                </a>
                                            </div>
                                        </td>
                                
                         <td class="py-xs px-sm" style="width: 18%;">
                                            <div class="flex-stretch  bg-primary f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-primary capitalize">
                                                <h2 class="f-size-sm md:f-size-sm"><a class='btn btn-primary btnwidth2' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["Zone"].ToString() %>'>
                                                    <%=BoothList.Tables[0].Rows[i]["Zone"].ToString().Length > 25 ? BoothList.Tables[0].Rows[i]["Zone"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["Zone"].ToString()%>
                                                </a></h2>
                                            </div>
                                        </td>

                                        <td class="py-xs px-sm" style="width: 10%;">
                                            <div class="flex-stretch  bg-warning f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-primary capitalize">
                                                <h2 class="f-size-sm md:f-size-sm"><a class='btn whiteText btnwidth2' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["TotalCount"].ToString() %>'>
                                                    <%=BoothList.Tables[0].Rows[i]["TotalCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["TotalCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["TotalCount"].ToString()%>
                                                </a></h2>
                                            </div>
                                        </td>
                                         
                                        <td class="py-xs px-sm" style="width: 10%;">
                                            <div class="flex-stretch  bg-success f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-success capitalize">
                                                <h2 class="f-size-sm md:f-size-sm"><a class='btn btn-success btnwidth2' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["InstalledCount"].ToString() %>'>
                                                    <%=BoothList.Tables[0].Rows[i]["InstalledCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["InstalledCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["InstalledCount"].ToString()%>
                                                </a></h2>
                                            </div>
                                        </td>
                                         
                                        <td class="py-xs px-sm" style="width: 10%;">
                                            <div class=" offlinediv flex-stretch f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-warning capitalize">
                                                <h2 class="f-size-sm md:f-size-sm"><a class='whiteText btn btnwidth2' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString() %>'>
                                                    <%=BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["UninstalledCount"].ToString()%>
                                                </a></h2>
                                            </div>
                                        </td>
                            
                                        <%--<%if (useridentifer == "5")
                                  {%>--%>
                                       
                                      
                                    </tr>
                                        
                                    <%--<%} %>--%>
                                    <tr id="trac_<%=i%>" class="sr-row-0 sr-hide">
                                        <td colspan="7"></td>
                                    </tr>
                                    <%}
                                            }
                                        } %>
                              
                                </tbody> 
                            </table>
             </div> 
          </div>
            </ContentTemplate> 
                    </asp:UpdatePanel>
           </div>
            </section>
                               
        </div>
     
    <script type="text/javascript">
        function loadac(pcname, i) { 
            var requestUrl = 'assemblydistrict.aspx?pcname=' + encodeURIComponent(pcname); 
            $.get(requestUrl, function (data) { 
               // $('[id^=trac_]').hide(); 
                $('#tblboothlist').find('polyline').attr('points', '6 9 12 15 18 9'); 
                $("#trac_" + i).show('slider'); 
                var currentTracTd = $("#trac_" + i + " td");
                currentTracTd.empty(); 
                currentTracTd.html(data); 
                $('#a_' + i).find('polyline').attr('points', '18 15 12 9 6 15');
            });
        } 
    </script>  
    <script type="text/javascript">
        function triggerPostBack() {
            var checkbox = document.getElementById('<%= testdist.ClientID %>');
            var checkbox1 = document.getElementById('<%= testtotalps.ClientID %>'); 
            var checkbox3 = document.getElementById('<%= testonline.ClientID %>'); 
            var checkbox5 = document.getElementById('<%= testoffline.ClientID %>');
            __doPostBack(checkbox.name, checkbox1.name, checkbox2.name, checkbox3.name, checkbox4.name, checkbox5.name,'');
        }
    </script> 
     <script type="text/javascript"> 
         $('#camerastatusmenu').addClass('active');
     </script>
</asp:Content>
