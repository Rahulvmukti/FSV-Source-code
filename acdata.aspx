<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="acdata.aspx.cs" Inherits="exam.acdata" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">



       <style>
           .btn{
               border-radius:0px;
           }
        .content-wrapper>.content {
    padding: 5px;
}
        .fixheader{
            background-color: #000;
    border-bottom: 0;
    box-shadow: inset 0 1px 0 #dee2e6, inset 0 -1px 0 #dee2e6;
    position: -webkit-sticky;
    position: sticky;
    top: 0;
    z-index: 10; 
    color:#fff;
        text-align: center;
        }
        .p-td{
            padding:5px;
        }
        .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
        .card-body.p-0 .table tbody>tr>td:last-of-type, .card-body.p-0 .table tbody>tr>th:last-of-type, .card-body.p-0 .table tfoot>tr>td:last-of-type, .card-body.p-0 .table tfoot>tr>th:last-of-type, .card-body.p-0 .table thead>tr>td:last-of-type, .card-body.p-0 .table thead>tr>th:last-of-type {
     padding-right: 0; 
}
        .cardarchive{
             overflow:hidden;
         }
        .location-header {
    width: 300px; /* Adjust the width as needed */
    text-align: center;
    padding: 5px; /* Adjust the padding to reduce white space */
    margin: 0; /* Remove any margin */
}

 
content-wrapper {
    overflow: hidden;
}
.bg-gradient-success {
    background: #68b187!important;
    color: #fff;
}
.bg-gradient-danger{
     background: #d54d4d!important;
     border:#d54d4d!important;
    color: #fff;
}
.bg-gradient-primary{
     background: #236a8d!important;
    color: #fff;
    border:#236A8d!important;
   /*  background-color: #236A8d!important;color: #FFF;border:#236A8d!important;*/
}
.district 
{
   background: #20282D!important;
   border:#20282D!important;
}
.district:hover
{
    background: #32383b!important;
    border:#32383b!important;
}
.bg-gradient-primary.btn:hover
{
        background: #125B7F!important;
        border:#125B7F!important;
}
.district.btn:hover
{
        background: #32383b!important;
         border:#32383b!important;
}
.bg-gradient-warning {
    background:#ffc10769 !important; /* Bootstrap's default yellow */
    border: #ffc10769 !important;
}

.bg-gradient-warning.btn:hover {
    background: #ffc10769 !important; /* A slightly darker yellow for hover */
    border: #ffc10769 !important;
}

.bg-gradient-warning.btn:not(:disabled):not(.disabled):active,
.bg-gradient-warning.btn:not(:disabled):not(.disabled).active, /* Adding .active for consistency */
.bg-gradient-warning.show > .btn.dropdown-toggle { /* For dropdowns, if necessary */
    background: #ffc10769 !important; /* An even darker yellow for active/click */
    border: #ffc10769 !important;
}
.bg-gradient-orange {
    background:#fd7e1496 !important; /* Bootstrap's default yellow */
    border: #fd7e1496 !important;
}

.bg-gradient-orange.btn:hover {
    background: #fd7e1496 !important; /* A slightly darker yellow for hover */
    border: #fd7e1496 !important;
}

.bg-gradient-orange.btn:not(:disabled):not(.disabled):active,
.bg-gradient-orange.btn:not(:disabled):not(.disabled).active, /* Adding .active for consistency */
.bg-gradient-orange.show > .btn.dropdown-toggle { /* For dropdowns, if necessary */
    background: #fd7e1496 !important; /* An even darker yellow for active/click */
    border: #fd7e1496 !important;
}
.indicator {
    height: 15px;
    width: 15px;
    border-radius: 50%;
    display: inline-block;
    margin-left: 5px; /* Spacing between indicators */
}

.online { background-color: #68b187; } /* Green */
.offline { background-color: #ffc10785; } /* Red */
.state2 { background-color: #fd7e1480; } /* Yellow */
.state3 { background-color: #d54d4d; } /* Orange */



    </style>
     <script type="text/javascript" >
         $(document).ready(function () {
             $('#sidebarmini1').prop('checked', false);
             if ($('body').hasClass('sidebar-mini')) {
                 $('body').removeClass('sidebar-mini')
             }
             else {
                 $('body').addClass('sidebar-mini')
             }
         });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                             <asp:Timer ID="Timer1" runat="server" Interval="60000" Enabled="true" OnTick="Timer1_Tick1">
                        </asp:Timer>
                              <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
        
        <div class="row">
          <div class="col-12">
         
                  <div class="card">
                        <div class="row" style="margin-bottom:3px">
          <div class="col-md-3"> 
               <div class="col-md-12 btn btn-default"  style="padding:0px;color:#fff; background-color:#68b187; border:1px solid #333" >3 Online</div>
            </div>
                         <div class="col-md-3">
                             <div class="col-md-12 btn btn-default"  style="padding:0px;color:#fff; background-color:#ffc10785; border:1px solid #333" >2 Online</div>

</div>
                         <div class="col-md-3">
          <div class="col-md-12 btn btn-default"  style="padding:0px;color:#fff; background-color:#fd7e1480; border:1px solid #333" >1 Online</div>
</div>
                         <div class="col-md-3">
      <div class="col-md-12 btn btn-default"  style="padding:0px;color:#fff; background-color:#d54d4d; border:1px solid #333" >0 Online</div>
</div>
</div>
                         <div class="col-12 row" >
        <asp:ListView ID="listview1" runat="server" OnItemDataBound="listview1_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-1 btn btn-default"  style='<%# Convert.ToInt32(Eval("online")) == 3 ? "background-color:#68b187!important;" : 
                               Convert.ToInt32(Eval("online")) == 2 ? "background-color:#ffc10785;" : 
                               Convert.ToInt32(Eval("online")) == 1 ? "background-color:#fd7e1480;" : 
                               "background-color:#d54d4d!important;" %>; padding:0px' >
            <div class="row">
                <div class="col-md-12">
                    <a class='btn btn-block btn-flat'  
                      style="padding:0px 2px; color:#fff;""
                       data-toggle="modal" 
                       data-target="#myModal" 
                       title='<%#Eval("acname") %>'>
                        <%#Eval("acname").ToString().Length > 20 ? Eval("acname").ToString().PadRight(140).Substring(0, 20).TrimEnd() + ".." : Eval("acname") %>
                    </a>
                </div>
            </div>
        </div> 
    </ItemTemplate>
</asp:ListView>


                             </div>
                   
                </div>
              </div>
            </div>

          </div>
        </section>

                              </div>
                               </ContentTemplate> 
                    </asp:UpdatePanel>   
   
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.js" ></script>
<script type="text/javascript">

    function loadac(pcname) {

        document.getElementById("iframesetting").src = 'assembly.aspx?pcname=' + pcname;


    }
    function LoadData(loc) {
        // Check the condition for the particular login
      
            window.top.location.href = "Listview.aspx?d=" + loc;
        }
        function LoadData2(loc) {
        // Perform an AJAX request to get the user's login type
        $.ajax({
            url: 'Dashboard.aspx/setusertype', // Replace this with your actual endpoint to get the user's login type
            method: 'GET',
            success: function (userLoginType) {
                // Check if loc is not equal to the current user's login type
                if (loc == userLoginType) { 
                    window.location.href = "Listview.aspx?d=" + loc;
                } else {
                     
                    alert("no such permission")
                }
            },
            error: function () { 
                alert("error");
            }
        });
    }
    
    function loadgrid(loc) {
        window.top.location.href = "Gridview.aspx?d=" + loc;
    }
    function handleButtonClick(encodedData, district, live) { 
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live == 0 && string2 != district ) {
            Swal.fire({
                icon: 'warning',
              //  title: 'Warning',
                text: 'ACCESS NOT ALLOWED.',
            
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });

        }
        else if (live == 0) {
            Swal.fire({
                icon: 'warning',
                //title: 'Warning',
                text: 'PRESENTLY THERE ARE NO STREAMING LIVE CAMERAS',
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });
        } 

        else {
            // Call your loadgrid1 function with the appropriate parameters
            loadgrid1(encodedData, district);
        }
    }
    function handleButtonClick_1(encodedData, district, live) {
        debugger;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live == 0) {
            Swal.fire({
                icon: 'warning',
                //title: 'Warning',
                text: 'PRESENTLY THERE ARE NO STREAMING LIVE CAMERAS',
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });
        } else {
            // Call your loadgrid1 function with the appropriate parameters
            loadgrid(encodedData, district);
        }
    }

    function handleButtonClick_offline(encodedData, district, live) {
        debugger;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
           if (live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //title: 'Warning',
                   text: 'PRESENTLY THERE ARE NO OFFLINE CAMERAS',
                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });
           }



           else {
               // Call your loadgrid1 function with the appropriate parameters
               LoadData(encodedData,district);
           }
    }
    function handleButtonClick_district(encodedData, district, live) { 
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live != 0 && string2 != district && live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //  title: 'Warning',
                   text: 'ACCESS NOT ALLOWED.',

                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });

           }
           else if (live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //title: 'Warning',
                   text: 'PRESENTLY THERE ARE NO OFFLINE CAMERAS',
                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });
           } 
           else {
               // Call your loadgrid1 function with the appropriate parameters
               LoadData1(encodedData, district);
           }
       }
    function loadgrid1(loc, strdist) {
        debugger;
        var string1 = strdist;
         var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (string1.localeCompare(string2) === 0) {
            window.top.location.href = "Gridview.aspx?d=" + loc;
        }
     
    }
    function LoadData1(loc, strdist) {
        var string1 = strdist;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (string1.localeCompare(string2) === 0) {
            window.top.location.href = "ListView.aspx?d=" + loc;
        }
    } 
</script>
     <script type="text/javascript"> 
         $('#dashboardmenu').addClass('active');
     </script>
    </asp:Content>
