<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="LoadTestReport.aspx.cs" Inherits="exam.LoadTestReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <iframe src="SimulationReport_PunjabElection.pdf" frameborder="0" style="width: 100%; height: 100vh;"></iframe>
</asp:Content>
