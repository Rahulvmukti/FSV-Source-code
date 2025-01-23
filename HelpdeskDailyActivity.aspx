<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HelpdeskDailyActivity.aspx.cs" Inherits="exam.HelpdeskDailyActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" /> 
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
               
    <style>
        .content-wrapper > .content {
            padding: 5px;
        }

        .fixheader {
            background-color: #000;
            border-bottom: 0;
            box-shadow: inset 0 1px 0 #dee2e6, inset 0 -1px 0 #dee2e6;
            position: -webkit-sticky;
            position: sticky;
            top: 0;
            z-index: 10;
            color: #fff;
        }

        .p-td {
            padding: 5px;
        }

        .form-group {
            margin-bottom: 0px;
            padding: 2px 5px;
        }

        .card-body.p-0 .table tbody > tr > td:last-of-type, .card-body.p-0 .table tbody > tr > th:last-of-type, .card-body.p-0 .table tfoot > tr > td:last-of-type, .card-body.p-0 .table tfoot > tr > th:last-of-type, .card-body.p-0 .table thead > tr > td:last-of-type, .card-body.p-0 .table thead > tr > th:last-of-type {
            padding-right: 0;
        }

        .cardarchive {
            overflow: auto;
        }

        .paging {
        }

            .paging a {
                background-color: #add8e6;
                padding: 5px 7px;
                text-decoration: none;
                border: 1px solid #00C157;
            }

                .paging a:hover {
                    background-color: #add8e6;
                    color: #00C157;
                    border: 1px solid #00C157;
                }

            .paging span {
                background-color: #add8e6;
                padding: 5px 7px;
                color: #00C157;
                border: 1px solid #00C157;
            }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }
            .swal-btn-margin {
    margin-right: 10px; /* Adjust the margin as per your preference */
}
          
 
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header row">
                                          
                                        <input type="hidden" value="" id="hiddenField" runat="server" />
                                    
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"
                                                Width="170px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="form-control"
                                                Width="170px" OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" />
                                        </div> 
                                        <div class="form-group">
                                            <asp:TextBox ID="strm_txtBox" runat="server" AutoPostBack="true" CssClass="form-control"
                                                Width="150px">
                                            </asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="Add_Booth" runat="server" Text="Add Activity" CssClass="btn btn-block btn-primary"
                                                data-backdrop="static" OnClientClick="OpenPopupAdd();" OnClick="btnaddbooth_Click" />
                                        </div>  

                                    </div>
                                    <div class="card-body table-responsive p-0" style="max-height: 500px; overflow: auto">
                                        <div class="panel-body"> 
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-head-fixed text-nowrap"
                                                    OnRowCommand="GridView1_RowCommand" AlternatingRowStyle-CssClass="bg-light" AllowPaging="true"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PagerStyle-CssClass="paging">
                                                    <EmptyDataTemplate>
                                                        <div align="center">
                                                            <h4>
                                                                <label class="text-center text-danger">
                                                                    No Data Found</label>
                                                            </h4>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BoothID" HeaderStyle-CssClass="fixheader" Visible="false">
                                                            <ItemTemplate>
                                                                  <asp:Label Text='<%# Eval("ID")%>' ID="lblid" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="fixheader" ItemStyle-Width="5%" >
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District" ItemStyle-Width="7%" Visible="true" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("District")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Assembly" Visible="True" HeaderStyle-CssClass="fixheader" ItemStyle-Width="5%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblschoolname" runat="server" Text='<%# Eval("acname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-CssClass="fixheader" ItemStyle-Width="5%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("VehicleNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                        
                                                        <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcamloctype" runat="server" Text='<%# Eval("activity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="fixheader" ItemStyle-Width="5%" >
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" CssClass="btn btn-block btn-primary"
                                                                    Style="padding: 6px 12px; font-size: 15px" Text="Edit" ID="btnMapping" CommandName="Submit" OnClientClick="OpenPopupAdd()" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                    </Columns>
                                                </asp:GridView>
                                          
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
              <Triggers>
                                
                            <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" /> 
                            </Triggers>
                </asp:UpdatePanel>
            </div>
             
            <div class="modal wow fadeInUp animated" id="PopupAddCamera" style="z-index:1035">
                <div class="modal-dialog">
                    <div class="modal-content bg-secondary">
                     
                        <div class="modal-header" style="padding: 0px 10px">
                    
                            <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static" Style="color: #dc7c3c;">
                                </asp:Label>
                               <asp:Label ID="lblTitle" runat="server" Text="Add Activity" class="modal-title" ClientIDMode="AutoID"></asp:Label>
                             
                             <asp:HiddenField ID="lblboothid11" runat="server" Value="0"  EnableViewState="true"/>
                            <button type="button" onclick="ClosePopupAdd();" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                        </div>
                        <div class="modal-body cardarchive" id="videoContainer">
                  
                            <div>
                                <asp:HiddenField ID="lblboothid1" runat="server" Value="0" />
                                <asp:Label ID="lblStreamId" runat="server" Visible="false">
                                </asp:Label>
                                <asp:Label ID="lblaccode" runat="server" Visible="false">
                                </asp:Label>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
    <ContentTemplate>
          

        <asp:HiddenField ID="lblboothid" runat="server" Value="0" />
    
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required" id="lbldistrict">District</label>
                                        <asp:DropDownList ID="drpAddDistrict" runat="server" CssClass="form-control ddlDistrict" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpAddDistrict_SelectedIndexChanged" Style="font-size: 15px;"
                                            ClientIDMode="Static" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="District" runat="server" ControlToValidate="drpAddDistrict"
                                            ErrorMessage="Select District!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">

                                        <label style="padding: 4px; font-size: 15px;" class="required" id="lblAss">Assembly</label>

                                        <asp:DropDownList ID="drpAddAcname" runat="server" AutoPostBack="true" CssClass="ddlPC form-control"
                                            Style="padding: 4px; font-size: 15px; width: 100%" ClientIDMode="Static"
                                            OnSelectedIndexChanged="drpAddAcname_SelectedIndexChanged" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpAddAcname"
                                            ErrorMessage="Select Assembly!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
    <div class="form-group">
        <label style="padding: 4px; font-size: 15px;" class="required">Select Vehicle</label>
       <div class="form-group">
             <asp:DropDownList ID="VehicleDropDownList" runat="server" AutoPostBack="true" CssClass="form-control"
             ></asp:DropDownList>
       </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="VehicleDropDownList"
                                            ErrorMessage="Select Vehicle!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
</div>
                                
                                </div>
         <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Activity Details</label> 
                                        <asp:TextBox runat="server" ID="txtIncidencedtls" ClientIDMode="Static" TextMode="MultiLine"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIncidencedtls"
            ErrorMessage="Enter Incidence details!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                         <asp:Label runat="server" ID="Label5" Visible="false"></asp:Label>
                                    </div>
                                </div>
                        
                            </div> 
    <div class="loading-overlay" id="loadingOverlay">
    <div class="loader"></div>
</div>
                           
        </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                         <div class="modal-footer" style="text-align: center"> 
                                 <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" OnClick="btnAdd_Click" ValidationGroup="GrpSave"/>
                                <asp:Button ID="btnCloseAdd" Text="Cancel" runat="server" OnClientClick="ClosePopupAdd()" class="btnclose cursor-pointer bg-gray f-color-default f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" data-dismiss="modal" aria-hidden="true" />
                            </div>
                    </div>
                </div>
                </div>
        </section>
    </div>
     <div id="loaderdiv" style="position: fixed; width: 100%; height: 100%; top: 0; left: 0; text-align: center; opacity: 0.7; background-color: #ebe8e8; z-index: 10000; display: none;">
            <div class="table-responsive loader" align="center" style="position: absolute; z-index: 1000; top: 50%; left: 50%; width: 18em; height: 18em;">
                <asp:Image ID="ImgProgress1" runat="server" ImageUrl="images/loading.gif" ToolTip="loading..." style=" background-repeat: no-repeat;"/><br />
                Please wait...
            </div>
        </div>
     
        <script>
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
                $("#loaderdiv .loader").css({ "left": ($(window).width() / 2) });
                $("#loaderdiv .loader").css({ "top": ($(window).height() / 2) - 70 });
                $("#loaderdiv").show();
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                $("#loaderdiv").hide();
            });
        </script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>

    <script type="text/javascript">
        var $j = jQuery.noConflict();

        
        function OpenPopupAdd() {
          
            $j("#lblTitle").val("");
            $j("#PopupAddCamera").show();
        }
        function ClosePopupAdd() {     
            $j("#lblboothid").val('0'); 
            $j("#drpAddDistrict").val("");
            $j("#drpAddAcname").val("");
            $j("#VehicleDropDownList").val("");
            $j("#lblmsg").val("");
            $j("#PopupAddCamera").hide();

            return false;
        }

        
    </script>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btncancel').on('click', function () {
                $('#btnCloseAdd').text(''); // Clear the text of the label
            });
        });
    </script> 
       <script type="text/javascript">
         
          var $j = jQuery.noConflict();
          $j(".fromdate").datepicker({
              dateFormat: 'dd/mm/yy',
              showOn: "both",
              buttonImage: "images/calender.png", 
              buttonText: "Select date",
              minDate: "01/03/2024",
              maxDate: new Date(),
              beforeShowDay: function (date) {
                  var currentDate = new Date();
                  currentDate.setHours(0, 0, 0, 0);  
                  return [date.getTime() === currentDate.getTime()];
              },
              
          });

          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
              $j(".fromdate").datepicker({
                  dateFormat: 'dd/mm/yy',
                  showOn: "both",
                  buttonImage: "images/calender.png", 
                  buttonText: "Select date",
                  minDate: "01/03/2024",
                  maxDate: new Date()
              });
          }); 
       </script> 
           <script type="text/javascript"> 
               $('#Mastermenu').addClass('active');
               $('#helpdeskactivity').addClass('active');
           </script>
</asp:Content>
