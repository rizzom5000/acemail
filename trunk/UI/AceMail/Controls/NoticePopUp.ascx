<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoticePopUp.ascx.cs"
    Inherits="AceMail.Controls.NoticePopUp" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<script type="text/javascript">
    function pageLoad()
    {
        jQuery('#<%= txtDate.ClientID  %>').datepicker({
            showOn:'button',
            buttonImageOnly:true,
            buttonImage:'images/cal.png',
            dateFormat:'mm/dd/yy', 
            minDate:0,
            showOtherMonths:true,
            navigationAsDateFormat:true,
            currentText:'Now'
            });
    }
</script>

<asp:ScriptManagerProxy ID="smpNoticePopUp" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/ui.datepicker.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upnlNoticePopUp" runat="server">
    <ContentTemplate>
        <div id="divNoticePop" style="display: none;" runat="server">
            <div align="center" class="noticeModal">
                <asp:Label ID="lblEvent" CssClass="lblEvent" runat="server" />
                <label>
                    Name</label>
                <asp:TextBox ID="txtName" CssClass="aspsinputwidth" Columns="30" runat="server" />
                <label>
                    Date</label>
                <asp:TextBox ID="txtDate" CssClass="aspsinputwidth" Columns="27" runat="server" />
                <div id="divCalendar" runat="server">
                </div>
                <label>
                    Note</label>
                <asp:TextBox ID="txtNote" TextMode="MultiLine" Rows="6" Wrap="true" CssClass="aspsinputwidth"
                    Columns="30" runat="server" />
                <asp:Button ID="btnCancel" Text="Cancel" OnClientClick="return hideNoticePop();"
                    CssClass="modalbutton" runat="server" />
                <asp:Button ID="btnAdd" Text="Save" OnClick="btnAdd_Click" CssClass="modalbutton"
                    runat="server" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
