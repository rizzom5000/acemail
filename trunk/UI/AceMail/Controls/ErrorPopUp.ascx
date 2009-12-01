<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorPopUp.ascx.cs"
    Inherits="AceMail.Controls.ErrorPopUp" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<asp:UpdatePanel ID="upnlErrorPop" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div id="divErrorPop" style="display: none;">
            <div class="errorModal" align="center">
                <asp:Label ID="lblError" CssClass="lblError" runat="server" />
                <br />
                <asp:Button ID="btnOK" Text="OK" OnClientClick="return hideErrorPop();" CssClass="modalbutton"
                    runat="server" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
