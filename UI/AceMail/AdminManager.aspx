<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="AdminManager.aspx.cs"
    Inherits="AceMail.AdminManager" Title="Admin Manager" %>
<%@ Register Src="Controls/UserGridView.ascx" TagName="UserGridView" TagPrefix="cc" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <asp:UpdatePanel ID="upnlHeader" runat="server">
        <ContentTemplate>
            <div class="divFormHeader">
                Add a New User
            </div>
            <br />
            <div>
                <label>
                    Login</label>
                <asp:TextBox ID="txtLogin" CssClass="aspshortinput" runat="server"></asp:TextBox>
                <br />
                <label>
                    Password</label>
                <asp:TextBox ID="txtPass" TextMode="Password" CssClass="aspshortinput" runat="server"></asp:TextBox>
                <br />
                <label>Admin?</label>
                <asp:CheckBox ID="chkAdmin" CssClass="aspshortinput" runat="server" />
                <br />
            </div>
            <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_OnClick" CssClass="submitbutton" runat="server" />
            <asp:Label ID="lblConfirm" Visible="false" runat="server" />
            <div class="divUsers">
                <cc:UserGridView ID="gvUsers" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <cc:ErrorPopUp ID="popError" runat="server" />
    </div>
</asp:Content>
