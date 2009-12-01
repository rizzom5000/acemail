<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="ContactManager.aspx.cs"
    Inherits="AceMail.ContactManager" Title="ContactManger" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<%@ Register Src="Controls/ContactDataList.ascx" TagName="ContactDataList" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div id="content">
        <div id="menu">
            <ul>
                <li>
                    <asp:LinkButton ID="btnNew" Text="New" OnClick="btnNew_Click" runat="server" /></li>
            </ul>
        </div>
        <cc:ContactDataList ID="ddlContacts" runat="server" />
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
