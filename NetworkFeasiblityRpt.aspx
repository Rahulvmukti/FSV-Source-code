<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="NetworkFeasiblityRpt.aspx.cs" Inherits="exam.NetworkFeasiblityRpt" %>
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
        
        table.dataTable thead th, table.dataTable thead td {
    padding: 5px 25px!important; 
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


                <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto">
                 <table  id="datatbl" class="table table-head-fixed">
                       <thead>
                                                <tr>
                                                    <th class="p-thin text-left">AC NO</th>
                                                    <th class="p-thin text-left">AC Name</th>
                                                    <th class="p-thin text-left">Part_No</th>
                                                    <th class="p-thin text-left">PS NO.</th>
                                                    <th class="p-thin text-left">Location</th>
                                                    <th class="p-thin text-left">Longitude</th> 
                                                    <th class="p-thin text-left">Latitude</th> 
                                                    <th class="p-thin text-left">Coverage Status</th> 
                                                </tr>
                                                    </thead>
                     <tbody>
                                              <% if (dsReport.Tables[0].Rows.Count > 0)%>
                                                    <% {%>
                                                         <%for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                    { 
                                                          %>
                                                                
                                                   <tr>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["acno"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=(dsReport.Tables[0].Rows[i]["acname"]).ToString()%></td>
                                                 <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["partno"].ToString() %></td>
                                                <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["psno"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["location"].ToString() %></td>
                                                     <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["longitude"].ToString() %></td>
                                                     <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["latitude"].ToString() %></td>
                                                     <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["Coveragestatus"].ToString() %></td>
                                                </tr>
                                                <%} %>
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
                           <%--  </ContentTemplate> 
                    </asp:UpdatePanel> --%>
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
    <script >
        var $j = jQuery.noConflict();
        //$j(".fromdate, .todate").datepicker({
        //    dateFormat: 'dd/mm/yy',
        //    showOn: "both",
        //    buttonImage: "images/calender.png",
        //    //buttonImageOnly: true,
        //    buttonText: "Select date",
        //    //minDate: "06/06/2022",
        //    //maxDate: "26/06/2022"
        //});
        //Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
        //    $j("#loaderdiv").show();
        //});

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            //var date = $(".fromdate").val();
            //var newdate = date.split("/").join("-");
            //$j(".fromdate, .todate").datepicker({
            //    dateFormat: 'dd/mm/yy',
            //    showOn: "both",
            //    buttonImage: "images/calender.png",
            //    //buttonImageOnly: true,
            //    buttonText: "Select date",
            //    //minDate: "06/06/2022",
            //    //maxDate: "26/06/2022"
            //});
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - Network Feasiblity Report',
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - Network Feasiblity Report',
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
            //var date = $(".fromdate").val();
            //var newdate = date.split("/").join("-");
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - Network Feasiblity Report',
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - Network Feasiblity Report',
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
        $('#NetworkFeasiblityReport').addClass('active');
    </script>
</asp:Content>
