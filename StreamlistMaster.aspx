<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StreamlistMaster.aspx.cs" Inherits="exam.StreamlistMaster" MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select:after {
            width: 0.7rem !important;
            height: 0.7rem !important;
        }

        .py-xs {
            padding-top: 6px !important;
            padding-bottom: 6px !important;
        }

        .hide {
            display: none;
        }

        .modal-dialog {
            width: 59% !important;
        }

        .ui-menu-item-wrapper {
            font-size: 14px !important;
            font-weight: 100 !important;
            padding: 3px 1em 3px 0.4em !important;
        }

        .ui-state-active {
            font-size: 14px;
            font-weight: 100;
            background: #bcbec2 !important;
            border: none !important;
            margin: 0 !important;
            padding: 3px 1em 3px 0.4em !important;
            width: 100%;
        }

        label.required::after {
            content: '*';
            margin-right: 4px;
            color: red;
        }
        /*  .swall-overlay {
    z-index: 100005;
}
.swal-modal {
    z-index: 99999;
}*/
    </style>

</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />

<script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="flex-auto flex-col">
        <div>
            <div class="col-12 lg:col-12 pb-sm flex-col">
                <div class="bg-white shadow flex-stretch">
                    <div class="bg-white shadow flex-stretch border-b border-default p-xs">
                        <input type="hidden" value="" id="hiddenField" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%-- p-sm--%>
                                 <asp:Label ID="Label1" runat="server" ClientIDMode="Static" Style="color: #dc7c3c;">
                                        </asp:Label>
                                        <asp:TextBox runat="server" ID="lblid" Text="0" ClientIDMode="Static" Visible="false">
                                        </asp:TextBox>
                                 <asp:Label ID="lblprourl" runat="server" Visible="false">
                                        </asp:Label>
                                        <asp:Label ID="lblservername" runat="server" Visible="false">
                                        </asp:Label>
                                <table class="f-size-xxs">
                                    <tr>
                                        <td class="pr-xs">
                                            <asp:TextBox ID="strm_txtBox" runat="server" CssClass="datalist border pr-md pl-xs py-xs" Width="250px" />
                                        </td>
                                        <td class="pr-xs">
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="cursor-pointer bg-primary f-color-white  f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                OnClick="btnsearch_Click" />
                                        </td>
                                         <td class="pr-xs">
                                            <asp:Button ID="btnAddCamera" runat="server" Text="Add Camera" CssClass="cursor-pointer bg-primary f-color-white  f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                 OnClientClick="OpenPopupAdd(1)" OnClick="btnAddCamera_Click"/>
                                        </td>
                                         <td class="pr-xs">
                                                        <a href="CameraExcelFomat.xlsx" class="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin no-underline" target="_blank">
                                                           Excel format</a>
                                                    </td>
                                          <td class="pr-xs">
                                         <asp:FileUpload ID="FileUploadbooth" runat="server" CssClass="border p-thin" Style="width: 200px; padding: 0; font-size: 15px;" />
                                         <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="cursor-pointer bg-primary f-color-white  f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                         OnClientClick="javascript: return checkValidate();" OnClick="btnupload_Click"/>
                                            </td>
                                    </tr>
                                     
                                   <%-- <tr>
                                        <td class="pr-xs pt-2xs" colspan="7">
                                            <table>
                                                <tr>
                                                    <td class="pr-xs">
                                                        <a href="#" id="btnaddbooth" class="cursor-pointer bg-primary f-color-white  f-weight-semibold hover:shadow-md transition-all px-2xs py-thin no-underline" data-toggle="modal" data-backdrop="static" onclick="OpenPopupAdd(1)">Add Booth</a>
                                                    </td>
                                                    <td class="pr-xs">
                                                        <a href="BoothExcelFomat.xlsx" class="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin no-underline" target="_blank">
                                                            <%--<span class="icon mr-xs flex-none f-color-primary">
                                                <span class=""><i data-feather="download"></i></span>
                                                </span>--%><%--Excel format</a>--%>
                                                 <%--   </td>
                                                    <td class="pr-xs">
                                                        <asp:FileUpload ID="FileUploadbooth" runat="server" CssClass="border p-thin" Style="width: 200px;" />
                                                        <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="cursor-pointer bg-primary f-color-white  f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                            OnClientClick="javascript: return checkValidate();" />
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>

                                    </tr>--%>
                                </table>
                            </ContentTemplate>
                             <Triggers>
                                <asp:PostBackTrigger ControlID="btnupload" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
        <hr />
        <div class="table-responsive">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="stacked table-zebra w-full text-center capitalize border-t border-default"
                        AlternatingRowStyle-CssClass="bg-light" PagerStyle-CssClass="GridPager page" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        AllowPaging="true">
                        <EmptyDataTemplate>
                            <div align="center" class="py-xs">
                                <h4>
                                    <label class="text-center text-danger">
                                        No Data Found</label>
                                </h4>
                            </div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-CssClass="f-size-xxs p-thin  text-left" ItemStyle-Width="5%" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BoothID"  Visible="false" ItemStyle-Width="8%" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Camera-Id"  ItemStyle-Width="10%" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldeviceid" runat="server" Text='<%# Eval("deviceid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProUrl"  ItemStyle-Width="10%" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblprourl" runat="server" Text='<%# Eval("prourl")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ServerName"  ItemStyle-Width="10%" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblservername" runat="server" Text='<%# Eval("servername")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                         <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-CssClass="f-size-xxs p-thin  text-right" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-right">
                                <ItemTemplate>
                                    <asp:Button runat="server" CssClass="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                        Text="Edit" ID="btnMapping" CommandName="Submit" OnClientClick="OpenPopupAdd(0)" OnClick="btnMapping_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="10%" ItemStyle-CssClass="f-size-xxs p-thin text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left ">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdID" Value='<%# Eval("id") %>' runat="server" />
                                    <asp:Button ID="btnSweetAlert" runat="server" Text="Delete" OnClientClick='<%# "deletebyID(" + Eval("id").ToString() + ")" %>' CssClass="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                   <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlAssembly" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCameraType" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
            <div class="modal wow fadeInUp animated" id="PopupAddCamera" tabindex="-1" data-wow-animation-name="fadeInUp"
                style="visibility: visible; animation-name: fadeInUp; -webkit-animation-name: fadeInUp;">
                <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header">
                            <div class="d-inline-block">
                                <asp:Label ID="lblTitle" runat="server" Text="Add Camera" class="modal-title" ClientIDMode="Static" ></asp:Label>
                                <%--<asp:TextBox runat="server" ID="TextBox1" Text="0" ClientIDMode="Static" Style="color: #dc7c3c;">
                                </asp:TextBox>--%>
                            </div>
                            <div class="d-inline-block text-right f-right pr-sm" style="width: 10%;">
                                <button type="button" onclick="ClosePopupAdd()" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div style="text-align: center">
                                        <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static" Style="color: #dc7c3c;">
                                        </asp:Label>
                                        <asp:TextBox runat="server" ID="lblboothid" Text="0" ClientIDMode="Static" Visible="false">
                                        </asp:TextBox>
                                        <asp:Label ID="lblStreamId" runat="server" Visible="false">
                                        </asp:Label>
                                        <asp:Label ID="lblaccode" runat="server" Visible="false">
                                        </asp:Label>
                                    </div>
                                  
                                    <table cellspacing="5" cellpadding="5">
                                     
                                        <tr>
                                            <td>
                                                <label style="padding: 4px; font-size: 15px;" class="required">Camera ID</label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtcameraId" ClientIDMode="Static"
                                                    Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcameraId"
                                                    ErrorMessage="Enter Camera Id!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddPSNum"   
                                                ErrorMessage="Enter PSNum" ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator> --%>
                                                <asp:Label runat="server" ID="Label5" Visible="false"></asp:Label>
                                            </td>
                                            <td runat="server" id="tdProUrl">
                                                <label style="padding: 4px; font-size: 15px;">ProUrl</label>
                                            </td>
                                             <td  runat="server"  id="tdtxtProUrl">
                                                <asp:TextBox runat="server" ID="txtprourl" ClientIDMode="Static"
                                                    Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%">
                                                </asp:TextBox>
                                              
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddLocation"   
                                                ErrorMessage="Enter Location" ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator>--%>
                                                <asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr  runat="server" id="trserverName">
                                            <td>
                                                <label style="padding: 4px; font-size: 15px;">Server Name</label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtservername" ClientIDMode="Static" Width="100%"
                                                    Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" onkeyup="autocompletecameradid()" ValidationGroup="GrpSave">
                                                </asp:TextBox>
                                               
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddCameraId"   
                                                ErrorMessage="Enter CameraId" ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator> --%>
                                                <%--<asp:Label runat="server" ID="Label3" Visible="false"></asp:Label>--%>
                                            </td>
                                           
                                           <td>
                                                <label style="padding: 4px; font-size: 15px;">IS AI</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chlIsActive" runat="server" Checked="false" />
                                            </td>
                                                <td colspan="2"></td>
                                        </tr>
                                       
                                       
                                      
                                    </table>
                                </div>

                                <div class="modal-footer" style="text-align: center">
                                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"  ValidationGroup="GrpSave" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnCloseAdd" Text="Cancel" runat="server" OnClientClick="ClosePopupAdd()" class="btnclose cursor-pointer bg-gray f-color-default f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" data-dismiss="modal" aria-hidden="true" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

           
      
            </div>
        </section>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <script>
        var $j = jQuery.noConflict();

        function OpenconfirmPopup() {
            $j("#dialog").show();
        }
        function OpenPopupAdd(t) {
            if (t == 1) {
                $j("#txtcameraId").val('');
                $j("#txtprourl").val('');
                $j("#txtAddLocation").val('');
                $j("#txtservername").val('');
                $j("#lblmsg").val("");
                $j("#PopupAddCamera").hide();
            }
            else {
                $j("#lblTitle").text('EDIT BOOTH');
            }
            $j("#PopupAddCamera").show();

        }
        function ClosePopupAdd() {
            $j("#txtcameraId").val('');
            $j("#txtprourl").val('');
            $j("#txtAddLocation").val('');
            $j("#txtservername").val('');
           
            $j("#lblmsg").val("");
            $j("#PopupAddCamera").hide();

        }
        function autocompletecameradid() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "eledata.asmx/GETStreamName",
                data: '{"did":"' + $j("#txtAddCameraId").val() + '"}',
                dataType: "json",
                success: function (data) {
                    $j("#txtAddCameraId").autocomplete({
                        source: data.d,
                        select: function (event, ui) {
                            $j("#txtAddCameraId").val(ui.item.value);
                        }
                    });
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });

        }
       
    </script>
    <script type="text/javascript">
        
        function deletebyID(id) {
            swal({
                title: 'Confirm!',
                text: "Are you sure you want to delete this record?",
                type: 'warning',
                showCancelButton: true,
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger',
                buttonsStyling: false
            }).then(function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "StreamlistMaster.aspx/DeleteClick",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d == "Deleted") {
                                swal("Success!", "Record deleted successfully.", "success").then(function () {
                                    window.location.href = "StreamListMaster.aspx";
                                    //var obj = $("#GridView1").ejGrid("instance");
                                    //obj.model.dataSource.push(data);
                                    //obj.refreshContent();
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
        function checkValidate() {
            $("#loaderdiv .loader").css({ "left": ($(window).width() / 2) });
            $("#loaderdiv .loader").css({ "top": ($(window).height() / 2) - 70 });
            $("#loaderdiv").show();
        };
    </script>
   
</asp:Content>
