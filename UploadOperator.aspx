<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UploadOperator.aspx.cs" Inherits="exam.UploadOperator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
           .form-group {
    margin-bottom: 0px;
    padding: 4px 4px;
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
        .p-td{
            padding:5px;
        }
        
        .card-body.p-0 .table tbody>tr>td:last-of-type, .card-body.p-0 .table tbody>tr>th:last-of-type, .card-body.p-0 .table tfoot>tr>td:last-of-type, .card-body.p-0 .table tfoot>tr>th:last-of-type, .card-body.p-0 .table thead>tr>td:last-of-type, .card-body.p-0 .table thead>tr>th:last-of-type {
     padding-right: 0; 
}
        .cardarchive{
             overflow:auto;
         }
          .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding: 0px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
        }

        /* Modal Content */
        .modal-content {
            margin: auto;
            padding: 20px;
            /*border: 1px solid #888;*/
            width: 50%;
        }
        /* The Close Button */
        .close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }
 .modal
{
    display: none;
    position: absolute;
    top: 50px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 70;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
.modal-dialog {
            width: 59% !important;
        }

.paging
        {
        }
         
        .paging a
        {
            background-color: #add8e6;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #00C157;
        }
         
        .paging a:hover
        {
            background-color: #add8e6;
            color: #00C157;
            border: 1px solid #00C157;
        }
         
        .paging span
        {
            background-color: #add8e6;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #00C157;
        }
         
        tr.paging
        {
            background: none !important;
        }
         
        tr.paging tr
        {
            background: none !important;
        }
        tr.paging td
        {
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    

    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        
        <div class="row">
          <div class="col-12">
            <div class="card">
                 
              <div class="card-header row"> 
                 
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
                   <a href="OperatorUploadFomat.xlsx" class="btn btn-block btn-primary" target="_blank">
                                                            Excel format</a>
                         </div> 
                   <div class="form-group">
                           <asp:DropDownList ID="ddlType" runat="server" Width="150px"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                               <asp:ListItem Text="Select Type" Value="" />
                                                    <asp:ListItem Text="CameraWise" Value="1" />
                                                    <asp:ListItem Text="LocationWise" Value="2" />
                               </asp:DropDownList>
                      </div>
                  <div class="form-group">
                 
                    <div class="input-group">
                       <asp:FileUpload ID="FileUploadbooth" runat="server" CssClass="form-control" Style="width: 200px; padding: 0; font-size: 15px;" />
                                                        <asp:Button ID="btnupload" runat="server" Text="Upload Operator" CssClass="input-group-append"
                                                            OnClientClick="javascript: return checkValidate();" OnClick="btnupload_Click" />
                    </div>
                  </div>
                 </div>
                <div class="panel-body">  
                                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-head-fixed text-nowrap"
                        AlternatingRowStyle-CssClass="bg-light" PagerStyle-CssClass="paging" OnPageIndexChanging="GridView1_PageIndexChanging"
                        AllowPaging="true" >
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
                            <asp:TemplateField HeaderText="District" ItemStyle-Width="7%" Visible="true" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("district")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Assembly"  Visible="True" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblschoolname" runat="server" Text='<%# Eval("acname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PS No" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblps" runat="server" Text='<%# Eval("psnum")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Operator name" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldisp" runat="server" Text='<%# Eval("operatorname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operator no." ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldispmob" runat="server" Text='<%# Eval("operatornumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" Visible="false" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldispDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Camera No" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblstreamnames" runat="server" Text='<%# Eval("streamname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>   
                        </Columns>
                    </asp:GridView>   
                                </div>
                </div>
              </div>
            </div>
          </div>
        </section>
        </div>
   <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>
    <script>
        var $j = jQuery.noConflict();

        function OpenconfirmPopup() {
            $j("#dialog").show();
        }
        function checkValidate() { 
             var Ac_name = $j("#<%=ddlType.ClientID %> :selected").text();
            if (Ac_name == "Select Type" || $j("#<%=ddlType.ClientID %>").val() == "") {
                swal("Error!", "Please select Type!!", "error");
                return false;
            } 
        };
    </script>
</asp:Content>

