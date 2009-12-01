<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="MailOptions.aspx.cs"
    Inherits="AceMail.MailOptions" Title="Mail Options" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <br />
    <div id="left">
        <fieldset>
            <legend class="bold">Forwarding </legend>
            <label class="fs">
                Enable Forwarding</label>
            <asp:CheckBox ID="chkForward" CssClass="checkbox" OnCheckedChanged="chkForward_CheckedChanged"
                AutoPostBack="True" runat="server" />
            <br />
            <label class="fs">
                Forwarding Address</label>
            <asp:TextBox ID="txtForward" CssClass="input" runat="server" />
        </fieldset>
        <br />
        <fieldset>
            <legend class="bold">Vacation Message </legend>
            <label class="fs">
                Enable Vacation Message</label>
            <asp:CheckBox ID="chkVacation" CssClass="checkbox" OnCheckedChanged="chkVacation_CheckedChanged"
                AutoPostBack="True" runat="server" />
            <br />
            <label class="fs">
                Vacation Message</label>
            <asp:TextBox ID="txtVacation" CssClass="input" TextMode="MultiLine" Rows="3" runat="server" />
        </fieldset>
        <br />
        <asp:Button ID="btnOptions" OnClick="btnOptions_Click" Text="Save" CssClass="button" runat="server" />
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
