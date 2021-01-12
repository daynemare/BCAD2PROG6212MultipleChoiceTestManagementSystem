<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="PortalHome.aspx.cs" Inherits="WebAppExtension.PortalHome" MasterPageFile="~/Site.Master" %>


       <asp:Content ID="ContentOne" ContentPlaceHolderID="MainContent" runat="server">
           
           <br />
           <strong>
           <asp:Label ID="lblUser" runat="server" ForeColor="White" Text="USER:"></asp:Label>
           <asp:Image ID="Image2" ImageUrl="~/Content/Images/Seanau-Flat-App-Education.ico" runat="server" Height="36px" Width="36px" />
           </strong>
           <h1 style="color:white" class="text-left">Home</h1>
           
           <p style="color:white">
               The Multiple Choice Test Management System - Online Student Portal, allows students to view their multiple choice test results online. 
               <br />
               Further functionality will be added to the Online Student Portal in future updates.
           </p>
           <asp:Image ID="homeImage" ImageUrl="~/Content/Images/home.jpg" runat="server" Height="673px" Width="1001px" />
            </asp:Content>