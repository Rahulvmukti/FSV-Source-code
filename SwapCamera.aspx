<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="c.aspx.cs" Inherits="exam.SwapCamera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select:after
        {
            right: 0;
            left: 150px;
        }
        .datalist
        {
            width:170px;
            padding:5px;
            font-size:15px;
        }
        .trpadding{
            padding:4px;
        }
        .showdo{
            box-shadow:
            0 2px 4px 0 rgb(0 0 0 / 5%), 0 0 29px 0 rgb(0 0 0 / 5%);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="flex-auto flex-col">
        <div>
            <%-- class="container"--%>
              
            <div class="col-10 lg:col-10 pb-sm flex-col" style="padding-left:15%">
                <div class="bg-white shadow flex-stretch">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="col-12 flex-row flex-wrap mx-auto px-xs mt-sm" style="margin-bottom: 10px;">
                                <div class="row-fluid col-12 col-12 lg:col-12 pl-sm f-size-2xs f-color-secondary border sr-header p-xs text-center">
                                    Swap Camera into Location
                                    <br />
                                 <asp:Label class="error" id="lblmsg" runat="server"  style="color: #dc7c3c;"></asp:Label>
                                </div>
                                <div class="col-6 lg:col-6 pb-sm flex-col" style="padding:10px">
                                    <table cellspacing="5" cellpadding="5" class="showdo">
                                        <tr>
                                            <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">District</label>
                                                <asp:HiddenField ID="Id" Value="Id" runat="server"/>
                                            </td>
                                          <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="datalist border  ddlDistrict" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Assembly</label>
                                            </td>
                                          <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="ddlPC datalist border "
                                                        OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Location</label>
                                            </td>
                                          <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" CssClass="datalist border "
                                                        Style="padding: 4px; font-size: 15px;"  OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                  <asp:Label runat="server" ID="lblStremid" Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lblboothID" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Camera ID</label>
                                            </td>
                                           <td class="trpadding">
                                                    <asp:TextBox runat="server" ID="txtCameraID" AutoPostBack="true" Enabled="false"
                                                style="font-size:15px; border:1px solid #ccc; padding:3px">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                               <div class="col-6 lg:col-6 pb-sm flex-col" style="padding:10px">
                                    <table cellspacing="5" cellpadding="5" class="showdo">
                                        <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">District</label>
                                            </td>
                                        <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlDistrict1" runat="server" CssClass="datalist border  ddlDistrict" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDistrict1_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Assembly</label>
                                            </td>
                                           <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlAssembly1" runat="server" AutoPostBack="true" CssClass="ddlPC datalist border "
                                                        OnSelectedIndexChanged="ddlAssembly1_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Location</label>
                                            </td>
                                          <td class="trpadding">
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlLocation1" runat="server" AutoPostBack="true" CssClass="datalist border "
                                                        Style="padding: 4px; font-size: 15px;"  OnSelectedIndexChanged="ddlLocation1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                     <asp:Label runat="server" ID="lblStremid1" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lblBoothID1" Visible="false"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                          <tr>
                                          <td class="trpadding">
                                                <label style="padding: 4px; font-size: 15px;">Camera ID</label>
                                            </td>
                                          <td class="trpadding">
                                                    <asp:TextBox runat="server" ID="txtCameraID1" AutoPostBack="true" Enabled="false"
                                                style="font-size:15px; border:1px solid #ccc; padding:3px">
                                                </asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="row-fluid col-12 col-12 lg:col-12 pl-sm f-size-2xs f-color-secondary border sr-header p-xs text-center">
                                 <asp:Button ID="btnSwapCamera" runat="server" Text="Swap Camera" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                    OnClick="btnSwapCamera_Click" />
                                </div>
                            </div>
                            <div class="col-12 flex-row flex-wrap mx-auto px-xs mt-sm">
                            <asp:GridView ID="GridView1" CssClass="stacked table-zebra w-full text-center capitalize border-t border-default f-size-1xs" runat="server" AutoGenerateColumns="false"
                                            AlternatingRowStyle-CssClass="bg-light" PageSize="50" AllowPaging="true" PagerStyle-CssClass="page"
                                            >
                                            <EmptyDataTemplate>
                                                <div align="center">
                                                    <h4>
                                                        <label class="text-center text-danger">
                                                            No Data Found</label>
                                                    </h4>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                              <%--  s.deviceid,acname,PSNum,location--%>
                                             <asp:TemplateField HeaderText="No" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="deviceid" HeaderText="Camera ID" />
                                                <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="district" HeaderText="District Name" />
                                                 <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="acname" HeaderText="Assembly Name" />
                                                 <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="PSNum" HeaderText="PSNum" />
                                                <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="location" HeaderText="Location" />
                                            </Columns>
                                        </asp:GridView>
                                </div>
                                </ContentTemplate>

                    </asp:UpdatePanel>
                    </div>
                </div>
             
           </div>
        </section>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>--%>
