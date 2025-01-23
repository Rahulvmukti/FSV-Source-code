<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="exam.Training" MasterPageFile="~/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
<style type="text/css" > 
           .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        } 
       .card-header
       {
           padding:5px;
       }
       .btn-default {
    background-color: #3d3e3e;
    border-color: #746e6e;
    color: #f1eaea;
    padding:1px 10px;
}
       .navbar{
           padding:0px;
       }
       button.dt-button
        {
            padding: 3px 15px;
        }
         
        
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <script src="<%=ResolveUrl("~/js/flv.min.js") %>" type="text/javascript"></script>
    
     <div class="content-wrapper">  
    <section class="content">
      <div class="container-fluid">
      <div class="row">
   
           <div class="col-md-6">
    <div class="card card-widget">
        <div class="card-header">
            <div class="user-block">
                <span class="username"><a href="#">INSTALLATION--INSTALLATION SETUP</a></span>
            </div>
            <!-- /.user-block -->
            <div class="card-tools">
                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                </button>
            </div>
            <!-- /.card-tools -->
        </div>
        <!-- /.card-header -->
        <div class="card-body">
            <video id="videoElement" muted preload="auto" controls style="width:100%; height:400px; padding:10px"></video>
        </div>
        <button type="button" class="btn btn-block bg-gradient-primary" onclick="loadplayer('videoElement', 'https://ptz11.vmukti.com:443/live-record/SSAM-160547-DCBFC.flv')">
            Play
        </button>
    </div>
</div>

<div class="col-md-6">
    <div class="card card-widget">
        <div class="card-header">
            <div class="user-block">
                <span class="username"><a href="#">FINAL INSTALLATION IN VEHICLE</a></span>
            </div>
            <!-- /.user-block -->
            <div class="card-tools">
                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                </button>
            </div>
            <!-- /.card-tools -->
        </div>
        <!-- /.card-header -->
        <div class="card-body">
            <video id="videoElement1" muted preload="auto" controls style="width:100%; height:400px; padding:10px"></video>
        </div>
        <button type="button" class="btn btn-block bg-gradient-primary" onclick="loadplayer('videoElement1', 'https://ptz11.vmukti.com:443/live-record/SSAM-160547-DCBFC.flv')">
            Play
        </button>
    </div>
</div>
           </div>
          </div>       
           </section>
          </div>  
     
    <script type="text/javascript">
        function loadplayer(videoElementId, flvurl) { 
                var videoElement = document.getElementById(videoElementId); 
                var flvPlayer = flvjs.createPlayer({
                    type: 'flv',
                    url: flvurl
                }); 
                flvPlayer.attachMediaElement(videoElement);
                flvPlayer.load();
                flvPlayer.play();

                //videoElement.addEventListener('play', function () {
                //    console.log('Video is playing');
                //});

                //videoElement.addEventListener('pause', function () {
                //    console.log('Video is paused');
                //});

                //videoElement.addEventListener('timeupdate', function () {
                //    console.log('Current time: ' + videoElement.currentTime);
                //});
            
        }
    </script>


    <script type="text/javascript"> 
        $('#Training').addClass('active');
        $('#Trainingmenu').addClass('active');
    </script> 
</asp:Content>
