<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="MailManager.aspx.cs"
    Inherits="AceMail.MailManager" AspCompat="true" Title="Mail Manager" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<%@ Register Src="Controls/FolderPopUp.ascx" TagName="FolderPopUp" TagPrefix="cc" %>
<%@ Register Src="Controls/MailGridView.ascx" TagName="MailGridView" TagPrefix="cc" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div class="right" dir="ltr">
        <div id="menu">
            <ul>
                <li>
                    <asp:LinkButton ID="btnJunk" Text="Junk" OnClick="btnJunk_Click" runat="server" /></li>
                <li>
                    <asp:LinkButton ID="btnMove" Text="Move" OnClick="btnMove_Click"
                        runat="server" /></li>
                <li>
                    <asp:LinkButton ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" runat="server" /></li>
                <li>
                    <asp:LinkButton ID="newBtn" Text="New" OnClick="btnNew_Click" runat="server" /></li>
            </ul>
        </div>
        <cc:MailGridView ID="gvMessages" runat="server" />
    </div>
    <div class="left">
        <br />
        <ul>
            <li>
                <asp:LinkButton ID="btnInbox" Text="Inbox" OnClick="btnInbox_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnDrafts" Text="Drafts" OnClick="btnDrafts_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnSent" Text="Sent" OnClick="btnSent_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnEvents" Text="Events" OnClick="btnEvents_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnStorage" Text="Storage" OnClick="btnStorage_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnDeleted" Text="Deleted" OnClick="btnDeleted_Click" runat="server" /></li>
        </ul>
        <br />
        <ul>
            <li>
                <asp:LinkButton ID="btnContacts" Text="Contacts" OnClick="btnContacts_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnOptions" Text="Options" OnClick="btnOptions_Click" runat="server" /></li>
        </ul>
    </div>
    <cc:FolderPopUp ID="popFolder" runat="server" />
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
