<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OfflineCameraNotification.aspx.cs" Inherits="exam.OfflineCameraNotification" %>

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
      .small-box {
        width: 30%; /* Adjust the width for medium-sized cards */
        display: inline-block;
        margin-right: 20px; /* Add margin between cards */
        margin-bottom: 20px; /* Add margin bottom for spacing */
        vertical-align: top; /* Align cards vertically */
        box-shadow: 0 4px 8px 0 rgba(0,0,0,1.0);
          transition: 0.3s;
    }
        .small-box:hover {
          background-color: cornsilk;
          box-shadow: 0 8px 16px 0 rgba(0,0,0,1.0);
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
    font-size:12px;
    font-weight:600;
}
        @media (min-width: 992px) {
            .dashboarddiv {
                flex: 4 0 16.50%;
                /*max-width: 16.50%;*/
                max-width:24.50%;    
            }
        }
        .main-footer
        {display:none;}
        .checkbox {
    margin-right: 5px;
    /* Add more styles as needed */
}
           .multiselect-wrapper .multiselect-list .multiselect-checkbox{
            -webkit-appearance: checkbox;
height: 12px;
width: 12px;
        }

    </style>  
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
     
    
    <div class="content-wrapper">
 
    <section class="content">
      <div class="panel" style="margin: 10px 30px 40px 10px">
         <div class="flex-row flex-wrap -mr-sm" id="divData">
             
                                        
                                </div>
                            </div>
        </section>
       <div class="row">
            <div id="divDataSample" class="divDataSample" style="display:none">
          

            <div class="small-box" id="#divid">
        
            <h1 class="heading" style="font-size:1.5rem;text-align:center;padding:5px;" >#deviceId</h1>
                     <table class="w-full">
    <tbody>
        <tr>
            <td colspan="2" style="padding:5px;text-align:center;">#distAc</td>
        </tr>
        <tr>
            <td class="data-bold" width="100" style="padding:5px;">#shift</td>
            <td  style="padding:5px;">#timeshift</td>
        </tr>
        <tr>
            <td class="data-bold" style="padding:5px;">Driver</td>
            <td style="padding:5px;">#driver</td>
        </tr>
        <tr>
            <td class="data-bold" style="padding:5px;">FSV Staff</td>
            <td style="padding:5px;">#fsv</td>
        </tr>
        <tr>
            <td class="data-bold" style="padding:5px;">Stop Duration</td>
            <td style="padding:5px;">#duration</td>
        </tr>
        <tr>
         <td style="border-top:2px gray solid" colspan="2">
                              <select id='#resOptid' multiple class="multi">
                                  #options
                            </select>
             </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;padding:5px;">
                <a href="#" class="btn btn-primary btnSave" data-duration="#duration" data-vehicle="#deviceId" data-id="#id" data-digname="#divid">Save</a>
            </td>
        </tr>
    </tbody>
</table>

            </div>
          </div>
  </div>
     
          </div> 
     
                           
    <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js" type="text/javascript"></script>ipt>
 
        <script type="text/javascript">
            function loaddata(prepend) {
                $.ajax({
                    type: "POST",
                    url: "OfflineCameraNotification.aspx/GetStopCameraList",
                    data: '{ "UserID": <%= Session["userid"] == null?0:System.Convert.ToInt32(Session["userid"]) %> }',
                     //data: JSON.stringify({ "selectedReasons": selectedrsn, "id": id }),
                     contentType: "application/json",
                     dataType: "json",
                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                         alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                     },
                     success: function (result) {
                         $.ajax({
                             type: "POST",
                             url: "OfflineCameraNotification.aspx/GetReasonsList",
                             //data: JSON.stringify({ "selectedReasons": selectedrsn, "id": id }),
                             contentType: "application/json",
                             dataType: "json",
                             success: function (reasonList) {
                                 var data = result.d;
                                 var time = 0

                                 var datares = reasonList.d;
                                 datares = $.parseJSON(datares);
                                 if (data != "") {
                                     data = $.parseJSON(data);
                                     var sdata
                                     sdata = $("#divDataSample").html()
                                     time = 0
                                     for (let i = 0; i < data.length; i++) {

                                         var diagid = "div" + data[i].id
                                         if ($('#' + diagid).length <= 0) {
                                             htldog = "";
                                        
                                             sdata = $("#divDataSample").html()
                                             sdata = sdata.replaceAll("#id", data[i].id).replaceAll("#divid", "div" + data[i].id)
                                             sdata = sdata.replaceAll("#resOptid", "resOpt" + data[i].id)
                                             sdata = sdata.replaceAll("#deviceId", data[i].streamname).replaceAll("#distAc", data[i].district + " / " + data[i].acname).replaceAll("#shift", data[i].ShiftName)
                                             sdata = sdata.replaceAll("#timeshift", data[i].ShiftStartTime.split(".")[0] + " - " + data[i].ShiftEndTime.split(".")[0]).replaceAll("#driver", data[i].operatorName + " (" + data[i].operatorNumber + ")")
                                             sdata = sdata.replaceAll("#fsv", data[i].operatorName + " (" + data[i].operatorNumber + ")").replaceAll("#duration", data[i].StartTime + " - " + data[i].StopTime);

                                             for (let j = 0; j < datares.length; j++) {
                                                 htldog = htldog + "<option value='" + datares[j].name + "'>" + datares[j].name + "</option>";
                                             }
                                             sdata = sdata.replaceAll("#options", htldog)
                                             var item = $(sdata).hide().delay(time).fadeIn(100);
                                             //$('#resOpt' + data[i].Id).multiselect({
                                             //    columns: 1,
                                             //    placeholder: 'Select Languages',
                                             //    search: true,
                                             //    selectAll: true
                                             //});


                                             if (prepend == "1") {
                                                 $('#divData').prepend(item);

                                             }
                                             else {
                                                 $('#divData').append(item);

                                             } time = time + 1000
                                             debugger;
                                         //    generateCheckboxes(datares, 'resOpt' + data[i].id);
                                          //   document.multiselect("#resOpt" + data[i].id);
                                         ///  $("#resOpt" + data[i].Id + "_itemList").child("span").hide();
                                         }

                                     }

                                     //$('.multi').each(function (i, obj) {
                                     //    //test
                                     //    $(this).removeClass("multi")
                                     //});
                                 }

                             }

                         });

                         $(document)
                             .delay(30000)
                             .queue(function (next) {
                                 loaddata(1);
                                 next();
                             })
                     }
                 });

            }
            $(document).on('focus', '.multiselect-input', function () {
                debugger;
                setInterval(function () {
                    $("div[id*='_itemList']").find("span:first").hide();
                }, 100);

            })
            $(document).on('click', '.btnSave', function () {

                //save reason
                var nm = $(this).attr("data-digname");
                var vehicle = $(this).attr("data-vehicle");
                var duration = $(this).attr("data-duration");
                var id = $(this).attr("data-id");

                var selectedrsn = $('#resOpt' + id).val();
                if (selectedrsn == "")
                    alert("Please select reason.")
                else {
                    var result = confirm("Want to save '" + selectedrsn + "' for " + vehicle + " at " + duration + "?");

                    if (result) {
                        // alert("saving")    
                        $.ajax({
                            type: "POST",
                            url: "OfflineCameraNotification.aspx/SaveReasons",
                            data: JSON.stringify({ "selectedReasons": selectedrsn.join(","), "id": id }),
                            contentType: "application/json",
                            dataType: "json",
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                            },
                            success: function (result) {
                                //alert("closing==" + $(this).attr("dt-digname"))
                                // alert("We returned: " + result.d);
                                $("#" + nm).remove();
                                //$("#" + nm).dialog("close");
                            }
                        });

                    }
                }

                return false;

            });


            setTimeout(function () {
                loaddata(0)
            }, 100);



        </script>  

<script type="text/javascript">
      function showCheckboxes() {
          var checkboxesDiv = document.getElementById('checkboxes1');
          checkboxesDiv.style.display = checkboxesDiv.style.display === 'none' ? 'block' : 'none';

          if (checkboxesDiv.style.display === 'block') {
              // Call a function to populate checkboxes when shown
              populateCheckboxes();
          }
      }

      // Function to populate dynamic checkboxes
      function populateCheckboxes() {
          var checkboxesDiv = document.getElementById('checkboxes1');
          checkboxesDiv.innerHTML = ''; // Clear previous content

          // Replace this with your array of options
          var options = [
              { value: 'option1', label: 'Option 1' },
              { value: 'option2', label: 'Option 2' },
              { value: 'option3', label: 'Option 3' }
              // Add more options as needed
          ];

          options.forEach(function (option) {
              var checkboxLabel = document.createElement('label');
              var checkbox = document.createElement('input');
              checkbox.type = 'checkbox';
              checkbox.value = option.value;
              checkbox.className = 'checkbox'; // Apply a class for styling
              checkboxLabel.appendChild(checkbox);
              checkboxLabel.appendChild(document.createTextNode(option.label));
              checkboxesDiv.appendChild(checkboxLabel);
          });
      }
  </script>t>
    
 
     <script type="text/javascript"> 
         $('#Mastermenu').addClass('active');
         $('#helpdeskentry').addClass('active');
     </script>ipt>
               
    </asp:Content>