<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="exam.MapView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
           .full-screen{
    width: 30px;
    height: 30px;
    background-color: transparent;
    position: absolute;
    bottom: 0px;
    right: 0px;
    cursor: pointer;
 }
 .full-screen i{
   color: #fff;
   font-weight:bolder;
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
                       <div class="form-group" style="float:right">
                       <table class="f-size-xxs">
                                    <tr>
                                        <td class="pr-xs">
                                            <%--Total Configured: <span id="lblConfigured" style="color: darkblue;"></span>&nbsp;--%>
                                            Online: <span id="lblOnline" style="color: green;"></span>&nbsp;
                                            Offline: <span id="lblOffline" style="color: red;"></span>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                      </div>
     <div class="form-group text-right" style="position: absolute; top: 0; right: 10px;" id="indicators" visible="true" runat="server">
    <label class="btn btn-default text-center" >
         ONLINE
        <br>
        <span class="fas fa-circle" style="color:#2e640bad"></span>
    </label>
    <label class="btn btn-default text-center" >
        OFFLINE
        <br>
         <span class="fas fa-circle" style="color:#eb0808ad"></span>
    </label> 
         <label class="btn btn-default text-center" >
                 Vehicle Stop
                  <br>
                  <span class="fas fa-circle" style="color:#f9df2afa"></span>
                </label>
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
            </div>
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
                  <span onclick="fullscreen(0)" class="full-screen"><i class="fas fa-expand"></i></span>
                </div>
               
                  <div class="modal-footer justify-content-between">
                       <p class="data">District/AcName:-<span id="modalDistrict"></span></p><br />
                      <p class="data">Vehicle No:-<span id="modalVehicle"></span></p><br />
                      <p class="data">Driver Info:-<span id="modalDetails"></span></p> 
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
  <script type="text/javascript" >   
      var $j = jQuery.noConflict(); 
      function ClosePopupAdd() {
          $j("#myModal").hide();
      }
  </script>
    <script>
        function LoadPlayer(data) {
            //  var modal = document.getElementById("myModal");
            $("#myModal").show();
            //var span = document.getElementsByClassName("close")[0];
           
                var videoElement = document.getElementById('videoElement_6');
                var flvurl = 'wss://' + data.cdnsvc + '/live-record/' + data.videoname + '.flv';
                var flvPlayer = flvjs.createPlayer({
                    type: 'flv',
                    url: flvurl
                });
                flvPlayer.attachMediaElement(videoElement);
                flvPlayer.load();
            flvPlayer.play();
            document.getElementById("modalDistrict").innerHTML = data.district + " / " + data.acname;
            document.getElementById("modalVehicle").innerHTML = data.location;
            document.getElementById("modalDetails").innerHTML = data.operatorName + " / " + data.operatorNumber;
            modal.style.display = "block";
                //flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
                //    setTimeout(() => {
                //        flvPlayer.currentTime = delayInSeconds;
                //    }, delayInSeconds * 1000);
                //});
                //flvPlayer.on(flvjs.Events.BUFFER_FULL, () => {
                //    setTimeout(() => {
                //        flvPlayer.currentTime = delayInSeconds;
                //    }, delayInSeconds * 1000);
                //});
                 
                
            }
            
            span.onclick = function () {
                modal.style.display = "none"; 
            }

            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none"; 
                }
            }
        function fullscreen(index) {
            document.getElementsByTagName('video')[index].webkitEnterFullscreen();
        }
    </script>
      <script type="text/javascript"> 
          $('#viewmenu').addClass('active');
          $('#Mapviewmenu').addClass('active');
      </script>
</asp:Content>
