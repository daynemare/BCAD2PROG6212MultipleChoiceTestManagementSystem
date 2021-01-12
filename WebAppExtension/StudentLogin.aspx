<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="WebAppExtension.StudentLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Login</title>
     <link rel="icon" 
            type="image/png" 
            href="Content/Images/Seanau-Flat-App-Education.ico"/>
      <link rel="shortcut icon" 
            type="image/png"
           href="Content/Images/Seanau-Flat-App-Education.ico"/>
    <link rel="stylesheet" href="Content/style.css" />
    <style type="text/css">
        .auto-style1 {
            width: 70%;
            border: 1px solid #666666;
            background-color: #666666;
            color:white;
            font-family:'Yu Gothic UI';
           
           
        }
        .auto-style2 {
            width: 300px;
        }

         .auto-style4 {
            background-color: black;
            color: white;
            border-radius: 4px;
            border: 1px solid #3287a8;
            margin-left: 103px;
        }
        .auto-style5 {
            background-color: black;
            color: white;
            border-radius: 4px;
            border: 1px solid #3287a8;
            margin-left: 0px;
        }
        .auto-style6 {
            text-align: center;
        }
        .auto-style7 {
            width: 300px;
            font-size: x-large;
            text-align: right;
        }
        .auto-style8 {
            font-size: larger;
        }
        .auto-style10 {
            font-size: xx-large;
        }
    </style>
</head>
<body>

      <form id="form1" runat="server">

      <h1 class="auto-style6">Multiple Choice Test Management System - Online Student Portal</h1>
         <br />
             <br />
             <br />
        
      <table class="auto-style1" align="center">
          <tr>
              <td class="auto-style7">&nbsp;</td>
              <td class="auto-style8">
                  <strong><span class="auto-style10">Student Login</span><asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/Seanau-Flat-App-Education.ico" Height="36px" Width="36px"/>
                  </strong>
              </td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  <asp:Label ID="lbNotify" runat="server" ForeColor="Red"></asp:Label>
              </td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>STUDENT NUMBER</td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  <asp:TextBox ID="tbUsername" runat="server" Width="500px" MaxLength="8"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>PASSWORD</td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  <asp:TextBox ID="tbPassword" runat="server" Width="500px" TextMode="Password" MaxLength="50"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  <asp:Button ID="btLogin" runat="server" Text="LOGIN" CssClass="auto-style5" Width="200px" Height="50px" OnClick="btLogin_Click" />
                  <asp:Button ID="btClearAll" runat="server" Text="CLEAR ALL" CssClass="auto-style4" Height="50px" Width="200px" OnClick="btClearAll_Click" />
              </td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
          <tr>
              <td class="auto-style2">&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
      </table>
          <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
    <hr />
            <footer>
                <p style="color:white">&copy; <%: DateTime.Now.Year %> - Multiple Choice Test Management System - Online Student Portal</p>
            </footer>
      </form>
        
</body>
    
</html>
