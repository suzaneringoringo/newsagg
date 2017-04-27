
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DEWITA SONYA</title>
</head>
<body style="background-color:#a0a0a0;">
	<center>
	<h2>My Search App</h2>
    <form id="form2" runat="server">
        <asp:Label ID="LabelKeyword" runat="server" style="z-index: 1; height: 19px" Text="Keyword : "></asp:Label>
		<asp:TextBox ID="SearchBox" runat="server" style="z-index: 1;"></asp:TextBox>
	    <br>
		<br>
		<asp:RadioButtonList ID="RadioButtonList1" runat="server" style="z-index: 1; height: 27px; width: 151px; text-align: left">
            <asp:ListItem Selected="True">Boyer-Moore</asp:ListItem>
            <asp:ListItem>KMP</asp:ListItem>
            <asp:ListItem>Regex</asp:ListItem>
        </asp:RadioButtonList>
        <br>
	    <asp:Button ID="Cari" runat="server" style="z-index: 1;" Text="Cari!" OnClick="Cari_Click" />
        <ol id="list" runat="server" style="z-index: 1; height: 19px; width: 576px; text-align: left ">
            
        </ol>
	</form>
	</center>
</body>
</html>

    </form>
</body>
</html>
