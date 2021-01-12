<%@ Page Title="View Test Results" Language="C#" AutoEventWireup="true" CodeBehind="ViewResults.aspx.cs" Inherits="WebAppExtension.ViewResults" MasterPageFile="~/Site.Master"%>

       <asp:Content ID="ContentTwo" ContentPlaceHolderID="MainContent" runat="server">
           
             <title>Student Login</title>
 
           <br />
           <strong>
           <asp:Label ID="lblUser" runat="server" ForeColor="White" Text="USER:"></asp:Label>
           <asp:Image ID="Image2" ImageUrl="~/Content/Images/Seanau-Flat-App-Education.ico" runat="server" Height="36px" Width="36px" />
           </strong>
           <h1 style="color:white" class="text-left">View Results</h1>
           <div align="center"> 
           <asp:GridView ID="dgClassList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="397px" Width="1151px">
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
               <EditRowStyle BackColor="#999999" />
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#E9E7E2" />
               <SortedAscendingHeaderStyle BackColor="#506C8C" />
               <SortedDescendingCellStyle BackColor="#FFFDF8" />
               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
               </asp:GridView>

            </div>

            </asp:Content>
