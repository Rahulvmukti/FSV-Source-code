<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RouteView.aspx.cs" Inherits="exam.RouteView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
    <style type="text/css">
         .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
          button.dt-button
        {
            padding: 3px 15px;
        }
            .ui-datepicker-trigger{
            display:none!important;
        }
        #ui-datepicker-div
        {
            z-index:999!important;
        }
    </style>
     <script src="<%=ResolveUrl("~/js/flv.min.js") %>" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="content-wrapper" style="background-color:#454d55">
        <section class="content">
      <div class="container-fluid">
        
        <div class="row" style="margin-right:-22px">
          <div class="col-12" style="padding:2px">
            <div class="card">
                  <div class="card-header row"> 
                       <div class="form-group">
                       <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"
                         OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                      </div>
                      <div class="form-group">
                             <asp:DropDownList ID="ddlAssembly" runat="server" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                          </div>
                        <div class="form-group">
                   <asp:DropDownList ID="ddlLocation" runat="server"  CssClass="form-control"
                                                    Width="150px" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" /> 
                          </div>
                      <div class="form-group"> 
                      <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="FromDt" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                                            </div>
                     </div>
                         <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                      </div>
                       <div class="form-group" style="float:right">
                       <table class="f-size-xxs">
                                    <tr>
                                        <td class="pr-xs">
                                              Total Distance(KM): <span id="totalKM" style="color: green;"></span>&nbsp; 
                                        </td>
                                    </tr>
                                </table>
                      </div>
                   <div class="form-group" style="float:right">
                    <table class="f-size-xxs">

                                   <tr>
                                  <td class="pr-xs">
                                            <%--Total Configured: <span id="lblConfigured" style="color: darkblue;"></span>&nbsp;--%>
                                 <span id="lblnodata" style="color: red;font-size:25px;"></span>
                                           <%-- Offline: <span id="lblOffline" style="color: red;"></span>&nbsp;--%>
                       <%-- </td>--%>
                                  <%--  </tr>--%>
                             </table>
                     </div>
                      </div>
                  <div class="col-12" style="padding:10px">
                 <div class="embed-responsive embed-responsive-16by9">
                                            <div class="embed-responsive-item" id="MapArea" data-role="page">
                                            </div>
                                        </div>
                                        </div>
                </div>
              </div>
            </div>
          </div>
            </section>
        <div class="modal  wow fadeInUp animated" id="myModal">
        <div class="modal-dialog">
          <div class="modal-content bg-secondary">
            <div class="modal-header" style="padding:0px 10px"> 
              <button type="button" onclick="ClosePopupAdd()" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
            </div>
            <div class="modal-body cardarchive" style="overflow: auto; display: flex; justify-content: center; align-items: center;">
                   <video id="videoElement_6" muted autoplay preload="auto" controls style="width:100%; padding:0px 0px 0px"> </video> 
                </div>
              <div>
                    <button style="font-size:13px" id="zoomInButton"><i class="material-icons">zoom_in</i></button> 
                         <button style="font-size:13px" id="ResetzoomButton"><i class="material-icons">center_focus_strong</i></button> 
                         <button style="font-size:13px" id="zoomOutButton"><i class="material-icons">zoom_out</i></button> 
              </div>
                  <div class="modal-footer justify-content-between">
                       <p class="data" id="modalacname"></p>
                       <p class="data" id="modalpsnum"></p>
                       <p class="data" id="modalvideoname"></p>
                      </div>
            </div>
        </div> 
    </div>
            </div>
   <script src="js/map_gps.js" type="text/javascript"></script> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"> 
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD2CF3PlGBd0tQhusHwX3ngfPaad0pmJ_Q&callback=MapInit"></script>
     <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script> 
        
         <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
                <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
                <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />
                <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
              
     <script type="text/javascript" >   
         var $j = jQuery.noConflict();
         function ClosePopupAdd() {
             $j("#myModal").hide();
         }
  </script>
    <script>

        var $j = jQuery.noConflict();
        $j(".fromdate, .todate").datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date"
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var date = $(".fromdate").val();
            var newdate = date.split("/").join("-");
            $j(".fromdate, .todate").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "both",
                buttonImage: "images/calender.png",
                buttonImageOnly: true,
                buttonText: "Select date"
            });
            $j('#datatbl').DataTable().destroy();
          
        });
    </script>
      <script type="text/javascript"> 
          $('#viewmenu').addClass('active');
          $('#HistoricView').addClass('active');
      </script>

  
</asp:Content>

