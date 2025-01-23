<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="InventoryUpdation.aspx.cs" Inherits="exam.InventoryUpdation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
               
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
          
/* Autocomplete Dropdown Styles for AdminLTE 3 Theme */
/* Autocomplete Dropdown Styles */
.ui-autocomplete {
    max-height: 200px;
    overflow-y: auto;
    z-index: 99999;
    background-color: #ffffff;
    border: 1px solid #ced4da;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    position: absolute;
    width: auto; 
    top: 100%; /* Position the dropdown above the textbox */
    left: 0;
}
/* For positioning above modal popup */


.ui-menu-item {
    padding: 5px 10px;
    cursor: pointer;
    transition: background-color 0.3s;
    font-family: inherit;
}

.ui-menu-item:hover {
    background-color: #f8f9fa;
}

.ui-menu-item a {
    color: #007bff;
    text-decoration: none;
}

.ui-menu-item a:hover {
    text-decoration: underline;
}

.ui-state-active {
            font-size: 14px;
            font-weight: 100;
            background: #bcbec2 !important;
            border: none !important;
            margin: 0 !important;
            padding: 2px 1em 2px 0.4em !important;
            width: 100%;
        }
 
  .loading-overlay {
        display: flex;
        justify-content: center;
        align-items: center;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(255, 255, 255, 0.7); /* Adjust opacity and color as needed */
        z-index: 9999; /* Make sure it's above other elements */
    }

    /* Style for the loader image */
    .loading-overlay img {
        width: 100px; /* Adjust the width and height of the loader image */
        height: 100px;
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
                                            <asp:Button ID="Add_Booth" runat="server" Text="Add Repair/Replacement" CssClass="btn btn-block btn-primary"
                                                data-backdrop="static" OnClientClick="OpenPopupAdd();" OnClick="btnaddbooth_Click" />
                                        </div>  

                                    </div>
          <div class="card-body table-responsive p-0" style="max-height: 500px; overflow: auto">
                                        <div class="panel-body"> 
                                            <asp:GridView ID="InventoryUpdationGrid"
           CssClass="table table-head-fixed text-nowrap"
            runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            OnPageIndexChanging="OnPageIndexChanging"
            OnRowCommand="InventoryUpdationGrid_RowCommand"
            AlternatingRowStyle-CssClass="bg-light" OnRowDataBound="InventoryUpdationGrid_RowDataBound">
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
                                                                <%--<asp:HiddenField ID="lblid" runat="server" Value='<%# Eval("id")%>' />--%>
                                                                <asp:Label Text='<%# Eval("id")%>' ID="lblid" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                <asp:TemplateField HeaderText="No" ItemStyle-CssClass="p-thin" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="District Name" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("[districtname]") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assembly Name" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("acname")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vehicle No" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("vehicleNo")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Material" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("material")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OLD SR NO." ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("oldsnno")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NEW SR No" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("newsnno")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Repaired / Replaced" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("repaired/replaced").ToString().Equals("") ? "" : Convert.ToBoolean(Eval("repaired/replaced")) ? "Replaced" : "Repaired"%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date" ItemStyle-CssClass="p-thin text-left" ItemStyle-Width="100px" HeaderStyle-Width="20px" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("startdate").ToString().Equals("") ? "" : Convert.ToDateTime(Eval("startdate")).ToString("dd-MM-yyyy") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" ItemStyle-CssClass="p-thin text-left" ItemStyle-Width="100px" HeaderStyle-Width="20px" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate><%# Eval("enddate").ToString().Equals("") ? "" : Convert.ToDateTime(Eval("enddate")).ToString("dd-MM-yyyy") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="p-thin text-left" ItemStyle-Width="100px" HeaderStyle-Width="20px" HeaderStyle-CssClass="fixheader">
                  <ItemTemplate><%# Eval("remarks")%></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" CssClass="btn btn-block btn-primary"
                                                                    Style="padding: 6px 12px; font-size: 15px" Text="Edit" ID="btnMapping" CommandName="EditInventoryUpdation" OnClientClick="OpenPopupAdd();" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="fixheader"> 
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdID" Value='<%# Eval("id") %>' runat="server" />
                                                                <asp:Button ID="btnSweetAlert" runat="server"  OnClientClick='<%# "deletebyID(" + Eval("id").ToString() + ")" %>'  Text="Delete" CssClass="btn btn-block btn-danger" Style="padding: 6px 12px; font-size: 15px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="p-thin text-left" ItemStyle-Width="20px" HeaderStyle-Width="20px" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate>
                        <asp:LinkButton ID="DeleteInventoryUpdation" runat="server"
                            ToolTip="Delete Inventory Updation"
                            CssClass="button f-color-primary opacity-50 hover:opacity-100 hover:f-color-secondary"
                            CommandName="DeleteInventoryUpdation"
                            CommandArgument='<%# Eval("id")%>'>                      
                                    <img src="images/delete-24.png" alt="Edit" width="20" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="p-thin text-left" ItemStyle-Width="20px" HeaderStyle-Width="20px" HeaderStyle-CssClass="fixheader">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditInventoryUpdation" runat="server"
                            ItemStyle-CssClass="p-thin" HeaderStyle-CssClass="fixheader"
                            ToolTip="Edit Inventory Updation"
                            CssClass="button f-color-primary opacity-50 hover:opacity-100 hover:f-color-secondary"
                            CommandName="EditInventoryUpdation"
                            CommandArgument='<%# Eval("id")%>'>
                                    <img src="images/edit2.png" alt="Edit" width="20" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                               <asp:Label ID="lblTitle" runat="server" Text="Add Repair/Replacement" class="modal-title" ClientIDMode="AutoID"></asp:Label>
                             
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
                                        <asp:DropDownList ID="DistrictDropDown" runat="server" CssClass="form-control ddlDistrict" AutoPostBack="true"
                                            OnSelectedIndexChanged="DistrictDropDownOnSelectedIndexChanged" Style="font-size: 15px;"
                                            ClientIDMode="Static" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="District" runat="server" ControlToValidate="DistrictDropDown"
                                            ErrorMessage="Select District!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">

                                        <label style="padding: 4px; font-size: 15px;" class="required" id="lblAss">Assembly</label>

                                        <asp:DropDownList ID="AssemblyDropDownList" runat="server" AutoPostBack="true" CssClass="ddlPC form-control"
                                            Style="padding: 4px; font-size: 15px; width: 100%" ClientIDMode="Static"
                                            OnSelectedIndexChanged="AssemblyDropDownOnSelectedIndexChanged" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AssemblyDropDownList"
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
                                    <div class="col-sm-6">
    <div class="form-group">
        <label style="padding: 4px; font-size: 15px;" class="required">Select Material</label>
       <div class="form-group">
             <asp:DropDownList ID="ddlMaterial" runat="server"  CssClass="form-control">
            </asp:DropDownList>
       </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMaterial"
                                            ErrorMessage="Select Material!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
</div>  
                                      <div class="col-sm-6">
    <div class="form-group">
        <label style="padding: 4px; font-size: 15px;" class="required">Select Status</label>
       <div class="form-group">
         <asp:DropDownList ID="Repair_ReplaceDD"
                                                runat="server"
                                               CssClass="form-control"
                                                Width="100%">
                                                <asp:ListItem Text="Repaired" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Replaced" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
       </div>

        
    </div>
</div> 
     <div class="col-sm-6">
          <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Remarks</label>
                                        <asp:TextBox runat="server" ID="txtremarks" ClientIDMode="Static" TextMode="MultiLine"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                          </div>
</div>
        

                                
                               
                                 <div class="col-sm-6">
    <div class="form-group">
        <label style="padding: 4px; font-size: 15px;" class="required">Start Date</label>
 
           <div id='StartDatedatepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="StartDate" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                                            </div> 
    </div>
</div>
                                   <div class="col-sm-6">
    <div class="form-group">
        <label style="padding: 4px; font-size: 15px;" class="required">End Date</label>
 
           <div id='EndDatedatepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="EndDate" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                                            </div> 
    </div>
</div>
                               </div> 
        <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Old Sr. No</label> 
                                        <asp:TextBox runat="server" ID="oldsrno" ClientIDMode="Static" Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="oldsrno"
            ErrorMessage="Enter Old Sr.No!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                         <asp:Label runat="server" ID="Label5" Visible="false"></asp:Label>
                                    </div>
                                </div>
                          <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">New Sr. No</label> 
                                        <asp:TextBox runat="server" ID="newserno" ClientIDMode="Static" Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="newserno"
            ErrorMessage="Enter New Sr.No!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                         <asp:Label runat="server" ID="desc" Visible="false"></asp:Label>
                                    </div>
                                </div>

                            </div> 
        
   <div class="loading-overlay" id="loadingOverlay" style="display: none;">
   <img src="images/loading.gif" alt="Loading..."/>
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
        <!-- Modal Start -->
        </div>
  

    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
     <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>
    
    <script>
        function OpenconfirmPopup() {
            $j("#dialog").show();
        }
        function OpenPopupAdd() {
            $("#lblTitle").val("");
            $("#PopupAddCamera").show();

        }
        function deletebyID(id) {
            swal({
                title: 'Confirm!',
                text: "Are you sure you want to delete this record?",
                type: 'warning',
                showCancelButton: true,
                confirmButtonClass: 'btn btn-success swal-btn-margin',
                cancelButtonClass: 'btn btn-danger swal-btn-margin',
                buttonsStyling: false
            }).then(function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "InventoryUpdation.aspx/DeleteClick",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d == "Deleted") {
                                swal("Success!", "Record deleted successfully.", "success").then(function () {
                                    window.location.href = "InventoryUpdation.aspx";
                                });
                            }
                            else {
                                swal("Error!", "Error occured while deleting record.", "success");
                            }
                        }
                    });
                }
            },
                function (dismiss) {
                    if (dismiss == 'cancel') {
                        swal('Cancelled', 'No record Deleted', 'error');
                    }
                });
            return false;
        }
        function ClosePopupAdd() {
          
            $("#lblboothid").val('0');
            $("#DistrictDropDown").val("");
            $("#AssemblyDropDownList").val("");
            
            $("#VehicleDropDownList").val("");
            $("#ddlMaterial").val("");
            $("#ddlMaterial").val("");
            $("#Repair_ReplaceDD").val("");
            
            $("#PopupAddCamera").hide();
            return false;
        }
    </script>
      <%--<script type="text/javascript">
          $(document).ready(function () {
              $('#btncancel').on('click', function () {
                  $('#btnCloseAdd').text(''); // Clear the text of the label
              });
          });
      </script> --%>
      <script type="text/javascript">

          var $j = jQuery.noConflict();
          $j(".fromdate").datepicker({
              dateFormat: 'dd/mm/yy',
              showOn: "both",
              buttonImage: "images/calender.png",
              buttonText: "Select date",
              minDate: "23/10/2023",
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
                  minDate: "23/10/2023",
                  maxDate: new Date()
              });
          });
      </script> 
 <script type="text/javascript">
  
     function showSuccessAlert(message) {
         debugger;
         swal('Success!', message, 'success').then(() => {
             $j("#PopupAddCamera").hide();
       
           
         });
     }

     function showLoader() {
         $('#loadingOverlay').show(); // Show loader when save is clicked
     }
</script>
        <script type="text/javascript"> 
            $('#Mastermenu').addClass('active');
            $('#InventoryUpdation').addClass('active');
        </script>
    </asp:Content>
