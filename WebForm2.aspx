<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="KoyaLawWeb.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default1page</title>
   <script src="../thickbox/jquery.js" type="text/javascript"></script>
    <link href="../thickbox/thickbox.css" rel="stylesheet" />
    <script src="../thickbox/thickbox.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

            <div class="row">
            <div class="col-lg-12">
                Select Agreement Types:
                 </div>
                </div>
       <div> &nbsp</div>
         <div class="row">
            <div class="col-lg-12">
                <asp:DropDownList ID="ddlagreement" runat="server" AutoPostBack="true" Width="300px" Height="30px" Style="text-align: left" OnSelectedIndexChanged="ddlagreement_SelectedIndexChanged" >
                    <asp:ListItem Text="Select Report"></asp:ListItem>
                    <asp:ListItem Text="Executed Report"></asp:ListItem>
                    <asp:ListItem Text="Draft Report"></asp:ListItem>
                </asp:DropDownList>
                         </div>
           <div class="row">
            <div class="col-lg-12">
              Select Client:
            </div>
             <div>  &nbsp</div>
        </div>

        <div class="row">
                <div class="col-lg-12">
                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true"  Width="300px" Height="30px" Style="text-align: left" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" >
                                        <asp:ListItem Text="<-- Select Client --->"></asp:ListItem>
                                    </asp:DropDownList>
                </div>

            </div>
        <div>&nbsp</div>

             <table>

                 <tr>
                     <td><asp:Label ID="txt1" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td>
                         <asp:Label ID="txt2" runat="server"></asp:Label>
                     </td>
                 </tr>
             </table>
             </div>
    </form>
</body>
</html>

