<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HelpDeskEntryReport.aspx.cs" Inherits="exam.HelpDeskEntryReport" %>
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
    .table-container {
        max-height: 500px;
        overflow: auto;
    }

    .no-records-message {
        text-align: center;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12">
            <div class="card">
                  <div class="card-header row"> 
                        <div class="form-group"> 
                      <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                             <asp:TextBox ID="FromDt" runat="server" CssClass="fromdate form-control" size="16" autocomplete="off" Width="80%"
                                                    value="" />
                          </div>
                            </div>
                     <div class="form-group" style="display:none">
                          <div id='datepicker2' class="pull-right col-md-3 input-group date datepicker border p-thin" style="width: 130px" data-date="" data-link-field="dtp_input2">
                               <asp:TextBox ID="ToDt" runat="server" CssClass="todate form-control" size="16" autocomplete="off" Width="80%"
                                                    value=""/>
                              </div>
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
                               <asp:DropDownList ID="ddlVehicle" runat="server" Width="150px"
                                                    OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged"
                                                    AutoPostBack="True" CssClass="form-control">
                                                </asp:DropDownList>
                           </div>
                       <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                      </div>
                      <div class="form-group">
                           <asp:Button ID="btnExportPDF" runat="server" OnClick="btnExportPDF_Click" Text="PDF Report" CssClass="btn btn-block btn-primary" />
                          </div> 
                      </div>
                
                                    
                   <div id="divReport" runat="server" class="card-body table-container table-responsive p-0" style="max-height: 500px; overflow:auto">
                        
                       <% if (dsReport.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                            { %>  
                 <table  id="datatbl" class="table table-head-fixed" >
                      <tr>
                                                        <td class="p-thin" style="width:7%">District:</td>
                                                        <td class="p-thin" style="width:20%"><%=dsReport.Tables[0].Rows[i]["District"].ToString() %></td>
                                                       <%-- <td class="p-thin" style="width:11%">Shift Type:</td>
                                                        <td class="p-thin" style="width:13%"><%=dsReport.Tables[0].Rows[i]["ShiftName"].ToString() %></td>--%>
                                                        <td class="p-thin" style="width:15%">Driver Name / Contact No:</td>
                                                        <td class="p-thin" style="width:20%"><%=dsReport.Tables[0].Rows[i]["DriverName"].ToString() + " / " + dsReport.Tables[0].Rows[i]["DriverContactNo"].ToString() %></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-thin" style="width:7%">Assembly:</td>
                                                        <td class="p-thin" style="width:20%"><%=dsReport.Tables[0].Rows[i]["Acname"].ToString() %></td>
                                                          <td class="p-thin" style="width:7%">Vehicle No:</td>
                                                        <td class="p-thin" style="width:20%"><%=dsReport.Tables[0].Rows[i]["VehicleNo"].ToString() %></td>
                                                        <%--<td class="p-thin" style="width:11%">Shift Start Time:</td>--%>
                                                        <%--<td class="p-thin" style="width:13%"><%=Convert.ToDateTime(dsReport.Tables[0].Rows[i]["ShiftStartTime"]) %></td>--%>
                                                    </tr>
                                  </table>                  
                 <table border="1" class="table table-head-fixed" >
                            <tr>
                                                <td class="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left" style="width:15%;">Deviceid
                                                </td>
                                                <td class="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left" style="width:15%;">Start Date Time
                                                </td>
                                                <td class="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left" style="width:15%;">Stop Date Time
                                                </td>
                                                <td class="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left" style="width:50%;">Reason
                                                </td>
                                                <td class="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">Remarks
                                                </td>
                                            </tr>
                                            <% System.Data.DataRow[] dataRows = dsReport.Tables[1].Select("District = '" + dsReport.Tables[0].Rows[i]["District"].ToString() + "' AND acname = '" + dsReport.Tables[0].Rows[i]["acname"].ToString() + "' AND VehicleNo = '" + dsReport.Tables[0].Rows[i]["VehicleNo"].ToString() + "' AND streamname = '" + dsReport.Tables[0].Rows[i]["streamname"].ToString() + "' AND ShiftName = '" + dsReport.Tables[0].Rows[i]["ShiftName"].ToString() + "' AND Dt = '" + dsReport.Tables[0].Rows[i]["Dt"].ToString() + "'");
                                                for (int j = 0; j < dataRows.Length; j++)

                                                {
                                            %>

                                            <tr class="<%= j%2!=0?"bg-light p-thin":"p-thin" %>">
                                                <td class="p-thin text-left" style="width:15%;"><%= dataRows[j]["streamname"].ToString() %></td>
                                                <td class="p-thin text-left" style="width:15%;"><%= dataRows[j]["startTime"].ToString() %></td>
                                                <td class="p-thin text-left" style="width:15%;"><%= dataRows[j]["stopTime"].ToString() %></td>
                                                <td class="p-thin text-left" style="width:50;"><%= dataRows[j]["reason"].ToString()%></td>
                                               
                                            </tr>
                                            <%}  %>  
                            <%}%> 
                          </table>         
            <%}
            else {%> 
             <div style="height:500px">
                <p class="p-thin text-center text-danger">No Record Found</p>
            </div>
             <%}%>
             
                </div>
                                     
               
           
                </div>
          </div>
        </div> 
         
   </section>    
        </div>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
     <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
     <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />
     <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <script type="text/javascript" >
        var $j = jQuery.noConflict();
        $j(".fromdate, .todate").datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date",
            //minDate: "06/06/2022",
            //maxDate: "26/06/2022"
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
                //minDate: "06/06/2022",
                //maxDate: "26/06/2022"
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - HELPDESK ENTRY REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - HELPDESK ENTRY REPORT' + '-' + newdate,
                      messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - HELPDESK ENTRY REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - HELPDESK ENTRY REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
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
        $('#HelpDeskEntryReport').addClass('active');
    </script>
 </asp:Content>
 