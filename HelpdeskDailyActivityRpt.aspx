<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HelpdeskDailyActivityRpt.aspx.cs" Inherits="exam.HelpdeskDailyActivityRpt" %>
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
  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
    <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12">
            <div class="card">
                  <div class="card-header row"> 
                       <div class="form-group">
                               <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="FromDt" runat="server" CssClass="fromdate form-control" size="16" autocomplete="off" Width="80%" Text='' />
                                            </div>
                           </div>
                      <div class="form-group" style="display:none">
                          <div id='datepicker2' class="row" data-date="" data-link-field="dtp_input2">
                         
                                                <asp:TextBox ID="ToDt" runat="server" CssClass="todate form-control" size="16" autocomplete="off" Width="80%"
                                                    value="" />
                                            </div>
                          </div>
                       <div class="form-group">
                          
                           <asp:DropDownList ID="ddlDistrict" runat="server"
                                                    AutoPostBack="True" Width="150px"
                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                           </div>
                       <div class="form-group">
                               <asp:DropDownList ID="ddlAssembly" runat="server"
                                                    AutoPostBack="True" Width="150px"
                                                    OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                           </div>
                        <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                   
                      </div>
                      <div class="navbar-nav ml-auto"> 
                           <h5>Helpdesk Daily Activity Report</h5>
                           </div>
                      </div>
                   <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto">
                 <table  id="datatbl" class="table table-head-fixed">
                       <thead>
                                                <tr>
                                                    <th class="p-thin text-left">District</th>
                                                    <th class="p-thin text-left">Assembly</th>
                                                    <th class="p-thin text-left">Vehicle No</th>  
                                                    <th class="p-thin text-left">Camera ID</th>  
                                                    <th class="p-thin text-left">Driver Info</th>  
                                                    <th class="p-thin text-left">Agent Name</th>  
                                                    <th class="p-thin text-left">Activity</th>
                                                    <th class="p-thin text-left">Date Time</th>
                                                </tr>
                                                    </thead>
                                                <tbody>
                                                 <%for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                  {       
                                                         %>
                                               
                                                <tr >
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["District"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["acname"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["vehicle"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["Cameraid"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["Driverinfo"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["username"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["activity"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["ActivityDateTime"].ToString() %></td>
                                                   <%--  <td class="p-thin text-left"><%=Convert.ToDateTime(dsReport.Tables[0].Rows[i]["ActivityDateTime"]).ToString("dd/MM/yyyy HH:mm") %></td> --%>
                                                    </tr>
                                                    
                                                <%} %>
                                                    </tbody>
                     </table>
                       </div>
                </div>
              </div>
                </div>
          </div>
        </section>
        </div>
       </ContentTemplate>
      </asp:UpdatePanel>
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
            buttonText: "Select date",
            minDate: "23/10/2023",
            maxDate: new Date()
        });
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            $j("#loaderdiv").show();
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
                minDate: "23/10/2023",
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
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - Helpdesk Activity Report' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                      , exportOptions: {
                          columns: ':not(.d-hidden)',
                      }
                  },
                  {
                      extend: 'pdfHtml5',
                      orientation: 'landscape',
                      pageSize: 'LEGAL', 
                      title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - Helpdesk Activity Report' + '-' + newdate,
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
                    //'copy', 'csv', 'excel', 'pdf', 'print'
                    {
                        extend: 'excelHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - Helpdesk Activity Report ' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - Helpdesk Activity Report' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    }
                ]
            });
});
        
    </script>
    <script type="text/javascript"> 
        $('#Reportmenu').addClass('active');
        $('#Helsdeskactivityrpt').addClass('active');
    </script>
</asp:Content>                                 
                                      
                                       
                    
                  

    
         