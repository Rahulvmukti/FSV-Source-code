<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="InstallationUpdation.aspx.cs" Inherits="exam.InstallationUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .dashboardcard{
            pointer-events:none;
            color:#fff;
            font-weight:600
        }
        .dashboardcardper{
            pointer-events:none;
            color:#fff;
            float:right
        }
        .content-header {
    padding: 4px 0px;
}
        .row{
            margin-left:-15px;
            margin-right:-15px;
        }
        .small-box
        {
            margin-bottom:10px;
        }
        .p-r0{
            padding-right:0px;
        }
        .card{
            margin-bottom:5px;
        }
        .card-block
        {
            text-align:-webkit-center;
        }

        #news_slide{
height:190px; 
overflow:hidden;
}
.voxNews li{
margin-top:0px;
float:left;
margin-left:5px;
padding:0px;
} 
* {
  margin: 0;
  padding: 0;
} 
.products-outer {
    width: 100%;
    margin: 0px auto 0;
    border: 0px solid #444;
    padding: 0px 20px;
    overflow: hidden;
} 
.products-inner {
/*   width: 1156px; */
  position: relative;
/*   left: 0px; */
/*   transition: all; */
}
.product {
  display: inline;
}
.products-list .product-img img {
    height: 50px;
    width: 90px;
}
.products-list .product-info { 
    text-align: center;
}
.products-list>.item {
    padding: 5px 0;
}
    </style>
    <style type="text/css">
.table {
  border-collapse: collapse;
  
}

.table td, .table th {
  border: 1px solid #ddd;
  padding: 5px;
  text-align: center;
}

.table th {
  background-color: #f2f2f2;
  color: black;
}
.table td {  
    font-size:18px;
    font-weight:600;
}
        @media (min-width: 992px) {
            .dashboarddiv {
                flex: 4 0 16.50%;
                /*max-width: 16.50%;*/
                max-width:24.50%;    
            }
        }
    </style>  
      <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js" ></script> 
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"> 
      <link rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.8/js/jquery.dataTables.min.js" defer></script>
        <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

    <style>
        .select:after {
            width: 0.7rem !important;
            height: 0.7rem !important;
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
 #divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
    </style>
    <style>
        #datepickerFrom {
    padding: 8px 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 16px;
    width: 200px; /* Adjust the width as needed */
}

/* Style for buttons */
.btn-default {
    color: #fff;
    background-color: #007bff; /* Bootstrap primary color */
    border-color: #007bff;
    padding: 8px 12px;
    font-size: 16px;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.2s ease-in-out;
}

.btn-default:hover {
    background-color: #0056b3; /* Darker shade for hover state */
    border-color: #0056b3;
}

/* Additional style to space out the buttons from the input */
#datepickerFrom + .btn-default {
    margin-left: 10px; /* Space between input and first button */
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
     
    
    <div class="content-wrapper">
    <!-- Content Header (Page header) -->
  
    <section class="content">
      <div class="container-fluid">
          
        <div class="row">
            
           <!-- Left col -->
          <section class="col-lg-6 connectedSortable p-r0">
            <!-- Custom tabs (Charts with tabs)-->
            <div class="card">
              <div class="card-header">
                <h3 class="card-title">
                  <i class="fas fa-chart-line mr-1"></i>
                 Online Offline Camera Line Chart
                </h3>
                <div class="card-tools">
          <input type="text" id="datepickerFrom" name="datepickerFrom">
<button class="btn btn-default" style="display:none" id="goButton">Go</button>
<button class="btn btn-default" id="clearButton">Clear</button>
        </div>
              </div><!-- /.card-header -->
              <div class="card-body" style="padding:0px">
                <div class="tab-content p-0">
                  <!-- Morris chart - Sales -->
                  <div class="chart tab-pane active" id="revenue-chart"
                       style="position: relative; height: 315px;">
                     <%-- <canvas id="revenue-chart-canvas" height="300" style="height: 300px;"></canvas>--%>
                      <canvas id="Chart1" ></canvas>
                   </div>
                 
                </div>
              </div><!-- /.card-body -->
            </div>
            <!-- /.card -->
          </section>
          <!-- /.Left col -->
          <!-- right col (We are only adding the ID to make the widgets sortable)-->
          <section class="col-lg-6 connectedSortable p-r0">

            <div class="card">
              <div class="card-header">
                <h3 class="card-title">
                  <i class="fas fa-chart-bar mr-1"></i>
               Installation Camera Line Chart
                </h3>
               
              </div><!-- /.card-header -->
              <div class="card-body" style="padding:-4px">
                <div class="tab-content p-0">
                  <!-- Morris chart - Sales -->
                  <div class="chart tab-pane active" id="revenue-chart1"
                       style="position: relative; height: 301px;"> 
                      <canvas id="BarChart" width="590" height="270"></canvas>
                   </div> 
                </div>
              </div><!-- /.card-body -->
            </div>

             
          </section>

          <!-- right col -->
        </div> 
      </div> 
          <div id="divBackground" class="modal">
</div>
        </section>
<div id="divImage">
<table style="height: 100%; width: 100%">
    <%--<tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
            <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
        </td>
    </tr>--%>
    <tr>
        <td align="center" valign="bottom">
            <input id="btnClose" class="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" type="button" value="close" onclick="HideDiv()" />
        </td>
    </tr>
</table>
</div>
            </div>

    
                           

    
    <script type="text/javascript"> 
        var $j = jQuery.noConflict();
        var vox_news = 0;

        $j('.voxNews li').each(function () {
            vox_news += $j(this).outerWidth(true);
        });

        $j('.voxNews').parent().append($j('.voxNews').clone());
        function setupNews(w) {
            debugger;
            function phase1() {
                debugger;
                var voxNews = $j('.voxNews').first(),
                    curMargin = voxNews.css('margin-left').replace("px", ""),
                    animSpeed = (w * 100) - (Math.abs(curMargin) * 100);

                voxNews.animate({ 'margin-left': '-' + w + 'px' }, animSpeed, 'linear', phase2);
            }
            function phase2() {
                $j('.voxNews').first().css({ 'margin-left': '0px' });
                phase1();
            }
            $j('.voxNews img').hover(function () {
                $j('.voxNews').stop();
            }, function () {
                phase1();
            });
            phase1();
        }
        setupNews(vox_news);

        /* });*/
    </script>
   <%
    var distList = string.Empty;
    var installedData = string.Empty; // This will hold the installed counts
    var uninstalledData = string.Empty; // This will hold the uninstalled counts
    
    for(int i = 0; i < BarGraph.Tables[0].Rows.Count; i++)
    {
        var row = BarGraph.Tables[0].Rows[i];
        
        int onlineCount = int.Parse(row["InstalledCount"].ToString());
        int offlineCount = int.Parse(row["UninstalledCount"].ToString());
        
        installedData += onlineCount.ToString() + ","; // Accumulate installed counts
        uninstalledData += offlineCount.ToString() + ","; // Accumulate uninstalled counts
        distList += "'" + row[0].ToString() + "',"; // Assuming row[0] holds the date or category
    }
%>
  
  
   <script type="text/javascript">
       debugger;
       const barCtx = document.getElementById('BarChart').getContext('2d');
       const barLabels = [<%= distList.Length > 0 ? distList.Remove(distList.Length - 1) : "" %>];
       const installedData = [<%= installedData.Length > 0 ? installedData.Remove(installedData.Length - 1) : "" %>];
    const uninstalledData = [<%= uninstalledData.Length > 0 ? uninstalledData.Remove(uninstalledData.Length - 1) : "" %>];

       const barData = {
           labels: barLabels,
           datasets: [
               {
                   label: 'Installed',
                   data: installedData,
                   backgroundColor: "#68b187", // A color for installed
                   barThickness: 5
               },
               {
                   label: 'Uninstalled',
                   data: uninstalledData,
                   backgroundColor: "#d54d4d", // A different color for uninstalled
                   barThickness: 5
               }
           ]
       };

       const barChart = new Chart(barCtx, {
           type: 'bar',
           data: barData,
           options: {
               plugins: {
                   title: {
                       display: true,
                       text: 'Installation Status'
                   },
               },
               responsive: true,
               scales: {
                   x: { // Updated for Chart.js 3.x
                       stacked: true,
                   },
                   y: { // Updated for Chart.js 3.x
                       stacked: true
                   }
               }
           }
       });

       setInterval(function () {
           window.location.reload();
       }, 300000);
   </script>

     <script type="text/javascript">
         $(function () {
             // Initialize the date picker with the correct format and onSelect event handler
             $("#datepickerFrom").datepicker({
                 dateFormat: "yy-mm-dd",
                 onSelect: function (dateText) {
                     getData(dateText); // Fetch data using the selected date
                 }
             });

             // Set to current date if no date is selected
             if (!$("#datepickerFrom").val()) {
                 var currentDate = new Date();
                 var currentDateString = currentDate.getFullYear() + '-' +
                     ('0' + (currentDate.getMonth() + 1)).slice(-2) + '-' +
                     ('0' + currentDate.getDate()).slice(-2);
                 $("#datepickerFrom").datepicker("setDate", currentDateString);
                 // Automatically fetch data for the current date as well
                 getData(currentDateString);
             }
         });




         function getData(fromDate) {
             // Example of sending the date to the server
             var dataToSend = { startDate: fromDate }; // Adjust according to your backend requirements
             debugger;
             $.ajax({
                 type: "POST",
                 url: "InstallationUpdation.aspx/GetChartData", // Adjust this URL
                 data: JSON.stringify(dataToSend),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     populateChart(response.d); // Ensure this function correctly maps your data to the chart
                 },
                 error: function (xhr, ajaxOptions, thrownError) {
                     alert("Error: " + thrownError);
                 }
             });
         }
         var myChart = null; // A variable to hold the chart instance

         // Ensure you import Luxon and the Chart.js adapter for Luxon at the top of your script


         // Assuming `var myChart = null;` is declared outside your function to track the chart instance
        
         var chart = null;
         function populateChart(data) {
             var ctx = document.getElementById('Chart1').getContext('2d');
             if (window.chart) { // Assuming 'chart' is globally defined
                 window.chart.destroy(); // Destroy the existing chart
             }
             window.chart = new Chart(ctx, {
                 type: 'line',
                 data: {
                     labels: data.map(d => d.label), // Extracting the labels from the data
                     datasets: [
                         {
                             label: 'Installed',
                             // Check each data item and make it null if Installed is 0
                             data: data.map(d => d.Installed === 0 ? null : d.Installed),
                             backgroundColor: '#68b187', // Semi-transparent fill
                             borderColor: '#68b187',
                             borderWidth: 1,
                             fill: false, // Ensuring the area under the line isn't filled
                             spanGaps: false, // Do not connect the line over null points
                         },
                         {
                             label: 'Uninstalled',
                             // Check each data item and make it null if Uninstalled is 0
                             data: data.map(d => d.Uninstalled),
                             backgroundColor: '#d54d4d', // Semi-transparent fill
                             borderColor: '#d54d4d',
                             borderWidth: 1,
                             fill: false, // Ensuring the area under the line isn't filled
                             spanGaps: false, // Do not connect the line over null points
                         }
                     ]
                 },
                 options: {
                     scales: {
                         y: {
                             beginAtZero: true
                         }
                     },
                     elements: {
                         line: {
                             tension: 0 // This makes the line straight
                         }
                     }
                 }
             });
         }






     </script>
  <script type="text/javascript"> 
      $('#dashboardmenu').addClass('active');
  </script>
                          
    </asp:Content>


     
                          
