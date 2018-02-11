<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="KoyaLawWeb.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
    <link href="assets/css/ace.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server">
<%--          <asp:GridView runat="server" AutoGenerateColumns ="false" ID ="questiongrid" ></asp:GridView>

          <asp:Button runat="server" Text="Submit" ID="submit" OnClick="submit_Click"/>--%>

           <asp:ScriptManager ID="ScriptManager2" runat="server" />
        <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
            <ProgressTemplate>
                Please wait til the operation completes
                <%--You can add your loading image here--%>
                <img src="CSS/images.png" />
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="container">
            <div class="row">
                <div class=" col-lg-12">
             
        <asp:UpdatePanel runat="server" ID="Panel">
            <ContentTemplate>
                <asp:Button  runat="server" ID="UpdateButton" OnClick="UpdateButton_Click" Text="Generate" />
            </ContentTemplate>
        </asp:UpdatePanel>
                   
                </div>
            </div>
        </div>

    </form>
</body>
</html>
