﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CameraOfflineReportnew.aspx.cs" Inherits="exam.CameraOfflineReportnew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
                <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
                
    <style>
        .form-group {
    margin-bottom: 0px;
    padding: 0px 4px;
}
        button.dt-button
        {
            padding: 3px 15px;
        }
        .dataTables_wrapper .dataTables_filter input { 
    border-radius: 4px!important;
    padding: 0px!important;
}
        div.dt-buttons { 
    padding: 0px 10px!important;
}
        .ui-datepicker-trigger{
            display:none!important;
        }
        #ui-datepicker-div
        {
            z-index:999!important;
        }
        table.dataTable thead th, table.dataTable thead td {
    padding: 5px 25px!important; 
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
<%--    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
    <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12">
            <div class="card">
                  <div class="card-header row"> 
                 <div class="form-group"> 
                      <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="FromDt" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                                            </div>
                     </div>
                        <div class="form-group" style="display:none">
                              <div id='datepicker2' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="ToDt" runat="server" CssClass="todate form-control" size="16" autocomplete="off" Width="80%" Text=''/> 
                                            </div>
                      </div>
                       <div class="form-group" >
                           <asp:DropDownList ID="ddlFromTime" runat="server" Width="170px"
                                                    CssClass="form-control">
                                                    <asp:ListItem Text="Select From Time" Value="" />
                                                    <asp:ListItem Text="12:00 AM" Value="00:00:00" />
                                                    <asp:ListItem Text="12:30 AM" Value="00:30:00" />
                                                    <asp:ListItem Text="1:00 AM" Value="01:00:00" />
                                                    <asp:ListItem Text="1:30 AM" Value="01:30:00" />
                                                    <asp:ListItem Text="2:00 AM" Value="02:00:00" />
                                                    <asp:ListItem Text="2:30 AM" Value="02:30:00" />
                                                    <asp:ListItem Text="3:00 AM" Value="03:00:00" />
                                                    <asp:ListItem Text="3:30 AM" Value="03:30:00" />
                                                    <asp:ListItem Text="4:00 AM" Value="04:00:00" />
                                                    <asp:ListItem Text="4:30 AM" Value="04:30:00" />
                                                    <asp:ListItem Text="5:00 AM" Value="05:00:00" />
                                                    <asp:ListItem Text="5:30 AM" Value="05:30:00" />
                                                    <asp:ListItem Text="6:00 AM" Value="06:00:00" />
                                                    <asp:ListItem Text="6:30 AM" Value="06:30:00" />
                                                    <asp:ListItem Text="7:00 AM" Value="07:00:00" />
                                                    <asp:ListItem Text="7:30 AM" Value="07:30:00" />
                                                    <asp:ListItem Text="8:00 AM" Value="08:00:00" />
                                                    <asp:ListItem Text="8:30 AM" Value="08:30:00" />
                                                    <asp:ListItem Text="9:00 AM" Value="09:00:00" />
                                                    <asp:ListItem Text="9:30 AM" Value="09:30:00" />
                                                    <asp:ListItem Text="10:00 AM" Value="10:00:00" />
                                                    <asp:ListItem Text="10:30 AM" Value="10:30:00" />
                                                    <asp:ListItem Text="11:00 AM" Value="11:00:00" />
                                                    <asp:ListItem Text="11:30 AM" Value="11:30:00" />
                                                    <asp:ListItem Text="12:00 PM" Value="12:00:00" />
                                                    <asp:ListItem Text="12:30 PM" Value="12:30:00" />
                                                    <asp:ListItem Text="1:00 PM" Value="13:00:00" />
                                                    <asp:ListItem Text="1:30 PM" Value="13:30:00" />
                                                    <asp:ListItem Text="2:00 PM" Value="14:00:00" />
                                                    <asp:ListItem Text="2:30 PM" Value="14:30:00" />
                                                    <asp:ListItem Text="3:00 PM" Value="15:00:00" />
                                                    <asp:ListItem Text="3:30 PM" Value="15:30:00" />
                                                    <asp:ListItem Text="4:00 PM" Value="16:00:00" />
                                                    <asp:ListItem Text="4:30 PM" Value="16:30:00" />
                                                    <asp:ListItem Text="5:00 PM" Value="17:00:00" />
                                                    <asp:ListItem Text="5:30 PM" Value="17:30:00" />
                                                    <asp:ListItem Text="6:00 PM" Value="18:00:00" />
                                                    <asp:ListItem Text="6:30 PM" Value="18:30:00" />
                                                    <asp:ListItem Text="7:00 PM" Value="19:00:00" />
                                                    <asp:ListItem Text="7:30 PM" Value="19:30:00" />
                                                    <asp:ListItem Text="8:00 PM" Value="20:00:00" />
                                                    <asp:ListItem Text="8:30 PM" Value="20:30:00" />
                                                    <asp:ListItem Text="9:00 PM" Value="21:00:00" />
                                                    <asp:ListItem Text="9:30 PM" Value="21:30:00" />
                                                    <asp:ListItem Text="10:00 PM" Value="22:00:00" />
                                                    <asp:ListItem Text="10:30 PM" Value="22:30:00" />
                                                    <asp:ListItem Text="11:00 PM" Value="23:00:00" />
                                                    <asp:ListItem Text="11:30 PM" Value="23:30:00" />
                                                    <asp:ListItem Text="11:59 PM" Value="23:59:59" />
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group">
                           <asp:DropDownList ID="ddlToTime" runat="server" Width="150px"
                                                    CssClass="form-control">
                                                    <asp:ListItem Text="Select To Time" Value="" />
                                                    <asp:ListItem Text="12:00 AM" Value="00:00:00" />
                                                    <asp:ListItem Text="12:30 AM" Value="00:30:00" />
                                                    <asp:ListItem Text="1:00 AM" Value="01:00:00" />
                                                    <asp:ListItem Text="1:30 AM" Value="01:30:00" />
                                                    <asp:ListItem Text="2:00 AM" Value="02:00:00" />
                                                    <asp:ListItem Text="2:30 AM" Value="02:30:00" />
                                                    <asp:ListItem Text="3:00 AM" Value="03:00:00" />
                                                    <asp:ListItem Text="3:30 AM" Value="03:30:00" />
                                                    <asp:ListItem Text="4:00 AM" Value="04:00:00" />
                                                    <asp:ListItem Text="4:30 AM" Value="04:30:00" />
                                                    <asp:ListItem Text="5:00 AM" Value="05:00:00" />
                                                    <asp:ListItem Text="5:30 AM" Value="05:30:00" />
                                                    <asp:ListItem Text="6:00 AM" Value="06:00:00" />
                                                    <asp:ListItem Text="6:30 AM" Value="06:30:00" />
                                                    <asp:ListItem Text="7:00 AM" Value="07:00:00" />
                                                    <asp:ListItem Text="7:30 AM" Value="07:30:00" />
                                                    <asp:ListItem Text="8:00 AM" Value="08:00:00" />
                                                    <asp:ListItem Text="8:30 AM" Value="08:30:00" />
                                                    <asp:ListItem Text="9:00 AM" Value="09:00:00" />
                                                    <asp:ListItem Text="9:30 AM" Value="09:30:00" />
                                                    <asp:ListItem Text="10:00 AM" Value="10:00:00" />
                                                    <asp:ListItem Text="10:30 AM" Value="10:30:00" />
                                                    <asp:ListItem Text="11:00 AM" Value="11:00:00" />
                                                    <asp:ListItem Text="11:30 AM" Value="11:30:00" />
                                                    <asp:ListItem Text="12:00 PM" Value="12:00:00" />
                                                    <asp:ListItem Text="12:30 PM" Value="12:30:00" />
                                                    <asp:ListItem Text="1:00 PM" Value="13:00:00" />
                                                    <asp:ListItem Text="1:30 PM" Value="13:30:00" />
                                                    <asp:ListItem Text="2:00 PM" Value="14:00:00" />
                                                    <asp:ListItem Text="2:30 PM" Value="14:30:00" />
                                                    <asp:ListItem Text="3:00 PM" Value="15:00:00" />
                                                    <asp:ListItem Text="3:30 PM" Value="15:30:00" />
                                                    <asp:ListItem Text="4:00 PM" Value="16:00:00" />
                                                    <asp:ListItem Text="4:30 PM" Value="16:30:00" />
                                                    <asp:ListItem Text="5:00 PM" Value="17:00:00" />
                                                    <asp:ListItem Text="5:30 PM" Value="17:30:00" />
                                                    <asp:ListItem Text="6:00 PM" Value="18:00:00" />
                                                    <asp:ListItem Text="6:30 PM" Value="18:30:00" />
                                                    <asp:ListItem Text="7:00 PM" Value="19:00:00" />
                                                    <asp:ListItem Text="7:30 PM" Value="19:30:00" />
                                                    <asp:ListItem Text="8:00 PM" Value="20:00:00" />
                                                    <asp:ListItem Text="8:30 PM" Value="20:30:00" />
                                                    <asp:ListItem Text="9:00 PM" Value="21:00:00" />
                                                    <asp:ListItem Text="9:30 PM" Value="21:30:00" />
                                                    <asp:ListItem Text="10:00 PM" Value="22:00:00" />
                                                    <asp:ListItem Text="10:30 PM" Value="22:30:00" />
                                                    <asp:ListItem Text="11:00 PM" Value="23:00:00" />
                                                    <asp:ListItem Text="11:30 PM" Value="23:30:00" />
                                                    <asp:ListItem Text="11:59 PM" Value="23:59:59" />
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group">
                           <asp:DropDownList ID="ddlDistrict" runat="server"
                                                    AutoPostBack="True" Width="150px"
                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group">
                           <asp:DropDownList ID="ddlAssembly" runat="server"
                                                    AutoPostBack="True" Width="165px"
                               OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                      </div>
                         <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                      </div>
                       <div class="form-group">
                           <asp:Label runat="server" ID="lblmsg" Text="" style="color:#4198f5; font-style:italic"></asp:Label>
                             <asp:Label runat="server" ID="lblmsgfooter" Text="" Visible="false" ></asp:Label>
                           </div>
                           <div class="navbar-nav ml-auto"> 
                           <h5>Camera Downtime Report</h5>
                           </div>
                      </div>


                  <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto">
                 <table  id="datatbl" class="table table-head-fixed">
                       <thead>
                                                <tr>
                                                    <th class="p-thin text-left tdist">District</th> 
                                                    <th class="p-thin text-left tacname ">Assembly</th>
                                         <%--                <th class="p-thin text-left tpsno">PS-No</th>--%> 
                                                    <th class="p-thin text-left tlocation">VehicleNo</th> 
                                                  <%--  <th class="p-thin text-left toperinfo">Operator Info</th>--%>
                                                    <th class="p-thin text-left tdid">Camera Id</th> 
                                                    <th class="p-thin text-left tstarttime">Start Time</th>
                                                    <th class="p-thin text-left tendtime">End Time</th>
                                                    <th class="p-thin text-center ttime">Time (HH:MM)</th>
                                                    <%--<th class="p-thin text-left tremark">Remarks</th>--%>
                                                </tr>
                                                    </thead>
                     <tbody>
                           <% if (dsReport1.Tables[0].Rows.Count > 0)%>
                                                    <% {%>
                                                          <%  for (int i = 0; i < dsReport1.Tables[0].Rows.Count; i++) {
                                                  // TimeSpan datediff = TimeSpan.FromMinutes(Convert.ToInt32(dsReport1.Tables[0].Rows[i]["DiffMin"]));%>

                         <% 
    int diffMin = Convert.ToInt32(dsReport1.Tables[0].Rows[i]["DiffMin"]);
    int hours = diffMin / 60;
    int minutes = diffMin % 60;
    string formattedTime = hours.ToString() + ":" + minutes.ToString("00");
                           
%>
                                                    <tr >
                                                          <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>
                                                         <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["district"].ToString() %></td> 
                                                         <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["acname"].ToString() %></td> 
                                                        <%-- <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["psnum"].ToString() %></td> --%>
                                                     
                                                         <%} %>
                                                          <%  else{%>
                                                        <td class="p-thin text-left tdist"></td> 
                                                        <td class="p-thin text-left tdist"></td> 
                                                         <%} %>
                                                     <td class="p-thin text-left tlocation"><%=dsReport1.Tables[0].Rows[i]["VehicleNo"].ToString() %></td>
                                                         <%-- <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>--%>
                                                         <td class="p-thin text-left tdid"><%=dsReport1.Tables[0].Rows[i]["streamname"].ToString() %></td>
                                                     
                                                       <%-- <%} else{%>
                                                     <td class="p-thin text-left tdid"></td>
                                                         <%} %>--%>
                                                       <td class="p-thin text-left tstarttime"><%=dsReport1.Tables[0].Rows[i]["StartTime"].ToString() %></td>

                                                        <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>
                                                          <td class="p-thin text-left tstarttime">TOTAL</td> 
                                                         <%} else{%>

                                                       <td class="p-thin text-left tstarttime"><%=dsReport1.Tables[0].Rows[i]["StopTime"].ToString() %></td>
                                                         <%} %> 
                                             <td class="p-thin text-center tendtime"><%= formattedTime %></td>
                                                     <%--<td  class="p-thin text-center ttime"><%= datediff.TotalHours == 24 && datediff.Minutes == 0 ? "24:00" : datediff.TotalHours.ToString("00") == "24" && datediff.Minutes > 0 ? ("23:" + datediff.Minutes.ToString("00")) : (datediff.Hours.ToString("00") + ":" + datediff.Minutes.ToString("00")) %></td>--%>
                                                   <%-- <td class="p-thin text-left tremark"><%=dsReport.Tables[0].Rows[i]["Reason"].ToString() %></td>--%>
                                                        </tr>
                         <%}%>
                         <tr>
                            
                             <td></td>
                                  <td></td>
                           <%--<td class="p-thin text-right" style="font-weight:bold">Total Downtime:- </td> 
                                 <td id="Td1" class="p-thin text-left" runat="server" style="font-weight:bold">From <%= FromDt.Text %> 09:00:00  </td> 
                       <td id="myTd" runat="server" style="font-weight:bold">To <%= ToDt.Text +" "+ lblmsgfooter.Text %></td>--%> 
                             <td class="p-thin text-right" style="font-weight:bold"> </td> 
                                 <td id="Td1" class="p-thin text-left" runat="server" style="font-weight:bold"> </td> 
                       <td id="myTd" runat="server" style="font-weight:bold"></td>

                                          <% 
    int TotalSum = Convert.ToInt32(dsReport1.Tables[1].Rows[0]["TotalSum"]);
    int hours1 = TotalSum / 60;
    int minutes1 = TotalSum % 60;
    string TotalSum1 = hours1.ToString() + ":" + minutes1.ToString("00"); 
%>
                                   <td class="p-thin text-left tendtime" style="font-weight:bold">Total Downtime:- </td> 
                                   <td class="p-thin text-left tendtime" style="font-weight:bold"><%= TotalSum1 %></td> 
                         </tr>
                         </tbody>
                      <%} %>
                     </table>
                      </div>
                </div>
              </div>

            </div>
          </section>
          </div> 
                       <%--     </ContentTemplate> 
                    </asp:UpdatePanel>--%>
       <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
                <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
                <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />
                <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
                 <script src="https://cdn.datatables.net/buttons/2.1.0/js/dataTables.buttons.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
                <script src="https://cdn.datatables.net/buttons/2.1.0/js/buttons.html5.min.js"></script>
                <script src="https://cdn.datatables.net/buttons/2.1.0/js/buttons.print.min.js"></script>      
    <script>
        var $j = jQuery.noConflict();
        $j(".fromdate, .todate").datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date"
            , minDate: "01/03/2024",
            maxDate: new Date()
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var date = $(".fromdate").val();
            var newdate = date.split("/").join("-");
            $j(".fromdate, .todate").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "both",
                buttonImage: "images/calender.png",
                //buttonImageOnly: true,
                buttonText: "Select date",
                minDate: "01/03/2024",
                maxDate: new Date()
            });
            $j('#datatbl').DataTable().destroy();
            $j('#datatbl').DataTable({
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                "pageLength": 10,
                "bSort": false,
                dom: 'Bfrtip',
                buttons: [
                    //'copy', 'csv', 'excel', 'pdf', 'print'
                    {
                        extend: 'excelHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    }
                ]
            });
        });
    </script>
        <script>
            $j(document).ready(function () {
                var date = $(".fromdate").val();
                var newdate = date.split("/").join("-");
                $j('#datatbl').DataTable({
                    "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    "pageLength": 10,
                    "bSort": false,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            orientation: 'landscape',
                            pageSize: 'LEGAL',
                            title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                           messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                , exportOptions: {
                    columns: ':not(.d-hidden)',
                           },

            },
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate
                   
                , exportOptions: {
                    columns: ':not(.d-hidden)',
                },
                customize: function (doc) {
                    var footerText = '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>';
                               var img = 'data:image/png;base64,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';

                               // Add a footer to every page
                               doc['footer'] = (currentPage, pageCount) => {
                                   // Initialize an empty stack for the footer content
                                   let footerStack = {
                                       stack: [],
                                       alignment: 'center' // Aligns the entire stack to the right
                                   };

                                   // Condition to check if it's the last page
                                   if (currentPage === pageCount) {
                                       // Add the image to the stack
                                       footerStack.stack.push({ image: img, width: 50, margin: [0, 0, 10, 0] }); // Adjust the margin as needed

                                       // Add the footer text next to the image
                                       footerStack.stack.push({ text: footerText, margin: [0, 0, 10, 0] }); // Adjust the margin to ensure proper spacing
                                   }

                                   // Create the footer content with the adjusted structure
                                   let footerContent = {
                                       columns: [
                                           footerStack
                                       ],
                                       margin: [40, -20] // Adjust the overall footer positioning as needed
                                   };

                                   return footerContent;
                               };
                           }
                       }
                   ]
               });
           });




        </script>
    <script type="text/javascript"> 
        $('#Reportmenu').addClass('active');
        $('#offlineReport').addClass('active');
    </script>
</asp:Content>