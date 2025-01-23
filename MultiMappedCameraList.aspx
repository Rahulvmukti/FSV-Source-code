<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MultiMappedCameraList.aspx.cs" Inherits="exam.MultiMappedCameraList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page td table {
            margin: auto;
            margin-top: 1rem;
        }

        .page span {
            background: #1c3480;
            font-size: 1.1rem;
            padding: 5px 8px;
            color: #fff;
            border: 1px solid transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="flex-auto flex-col">
        <div>
            <%-- class="container"--%>
            <div class="col-12 lg:col-12 pb-sm flex-col">
                <div class="bg-white shadow flex-stretch">
                   
                    <div class="panel panel-primary">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div id="scrollDiv" class="pd0" align="center" style="overflow-y: auto; height: calc(108vh - 250px);">
                                        <asp:GridView ID="GridView1" CssClass="stacked table-zebra w-full text-center capitalize border-t border-default f-size-1xs" runat="server" AutoGenerateColumns="false"
                                            AlternatingRowStyle-CssClass="bg-light" PageSize="10" AllowPaging="true" PagerStyle-CssClass="page" OnPageIndexChanging="GridView1_PageIndexChanging"
                                             OnRowCommand="GridView1_RowCommand" >
                                            <EmptyDataTemplate>
                                                <div align="center">
                                                    <h4>
                                                        <label class="text-center text-danger">
                                                            No Data Found</label>
                                                    </h4>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                             
                                               
                                                <asp:TemplateField HeaderText="District" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("district")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AccemblyName" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lblacname" runat="server" Text='<%# Eval("acname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accode" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lblaccode" runat="server" Text='<%# Eval("accode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PSNum" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lblPSNum" runat="server" Text='<%# Eval("PSNum")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="CameraId" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lblCameraId" runat="server" Text='<%# Eval("deviceid")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Location" Visible="true" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                             <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("location")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--<nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
                                    <div class="mrfive">
                                        <div class="page text-center">
                                            <div>
                                                <asp:LinkButton ID="prev" runat="server" Text="«" OnClick="prev_Click" CssClass="btn btn-default btncustom"></asp:LinkButton>
                                                <asp:Repeater ID="rptPages" runat="server" OnItemDataBound="rptPages_ItemDataBound">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btn_page_camera" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>'
                                                            Enabled='<%#Eval("Enabled") %>' OnClick="Page_Changed" CssClass="btn btn-default btncustom" />

                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:LinkButton ID="next" runat="server" Text="»" OnClick="next_Click" CssClass="btn btn-default btncustom" ></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </nav>--%>

                                  
                            </ContentTemplate>
                            <Triggers>
                               
                            </Triggers>
                        </asp:UpdatePanel>
                       
                    </div>
                </div>

            </div>

        </div>
        <script type="text/javascript">

            jQuery(function ($) {

                var xPos, yPos;
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(BeginRequestHandler);
                prm.add_endRequest(EndRequestHandler);
                var nodeId;
                function BeginRequestHandler(sender, args) {
                    $("#scrollDiv").hide();
                    xPos = $get('scrollDiv').scrollLeft;
                    yPos = $get('scrollDiv').scrollTop;
                }
                function EndRequestHandler(sender, args) {
                    $get('scrollDiv').scrollLeft = xPos;
                    $get('scrollDiv').scrollTop = yPos;
                    $("#scrollDiv").show();
                }

            });
            function OpenPopup() {
                $("#popupMapcamera").show();
            }
            function ClosePopup() {
                $("#popupMapcamera").hide();
            }
        </script>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
