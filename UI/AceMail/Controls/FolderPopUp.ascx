<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FolderPopUp.ascx.cs"
    Inherits="AceMail.Controls.FolderPopUp" %>
<%@ Register Src="ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<asp:UpdatePanel ID="upnlFolderPop" runat="server">
    <ContentTemplate>
        <div id="divFolderPop" style="display: none;" runat="server">
            <div align="center" class="moveModal">
                <h3>
                    <asp:Label ID="lblMove" Text="Choose a Folder" runat="server" /></h3>
                <br />
                <asp:Button ID="btnStorage" Text="Storage" OnClick="btnStorage_Click" UseSubmitBehavior="false"
                    CssClass="modalbutton" runat="server" />
                <br />
                <asp:Button ID="btnDelete" Text="Deleted" OnClick="btnDelete_Click" UseSubmitBehavior="false"
                    CssClass="modalbutton" runat="server" />
                <br />
                <asp:Button ID="btnJunk" Text="Junk" OnClick="btnJunk_Click" CssClass="modalbutton"
                    runat="server" />
                <br />
                <asp:Button ID="btnCancel" Text="Cancel" OnClientClick="return hideFolderPop();"
                    CssClass="modalbutton" runat="server" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
