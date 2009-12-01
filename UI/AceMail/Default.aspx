<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AceMail.Default" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AceMail Login</title>
    <link href="~/CSS/Style.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src='<%= ResolveUrl("~/scripts/jquery-1.3.2.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/scripts/ui.core.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/scripts/jquery-ui-1.7.1.custom.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/scripts/ui.dialog.js") %>'></script>

</head>
<body>
    <form id="frmLogin" runat="server">
    <asp:ScriptManager ID="smLogin" runat="server" />
    <div id="wrap" style="width:500px; height:200px;">
        <div id="content">
            <div id="login">
                <div id="header" style="background-color:#DBEAF0; margin:0 0 0 70px;">
                        <h1>
                            AceMail</h1>
                        <h2>
                            ASP.NET Mail Portal</h2>
                </div>
                <label>
                    Login</label>
                <asp:TextBox ID="txtLogin" Columns="30" CssClass="aspinputwidth" runat="server" />
                <br />
                <label>
                    Password</label>
                <asp:TextBox ID="txtPass" Columns="30" TextMode="Password" CssClass="aspinputwidth"
                    runat="server" />
                <br />
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" Text="Login" CssClass="submitbutton"
                    runat="server" />
            </div>
        </div>
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
    </form>
</body>
</html>
