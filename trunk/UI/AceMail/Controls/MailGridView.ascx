<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailGridView.ascx.cs"
    Inherits="AceMail.Controls.MailGridView" %>
<%@ Register Src="ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="uc" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<script type="text/javascript">
    function CheckAll(id)
    {
        var base = document.getElementById('<%= this.gvMessages.ClientID %>');
        var inputs = base.getElementsByTagName("input");
        for(var i = 0; i < inputs.length; i++)
        {
            if(inputs[i].type == 'checkbox' && inputs[i].id.indexOf("chkSelect", 0) >= 0)
            {
                inputs[i].checked = id.checked;
            }
        }
    }
</script>

<div>
    <asp:Label ID="lblEmpty" Visible="false" runat="server" />
    <asp:GridView ID="gvMessages" BackColor="#F0F0F0" BorderColor="Silver"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="right" CaptionAlign="Top"
        GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="emlid"
        AllowSorting="True" OnSorting="gvMessages_Sorting" OnRowDataBound="gvMessages_RowDataBound"
        OnRowCommand="gvMessages_RowCommand" OnPageIndexChanging="gvMessages_PageIndexChanging"
        HorizontalAlign="Center" ShowFooter="True" runat="server">
        <HeaderStyle Font-Bold="True" ForeColor="#150517" HorizontalAlign="Left"></HeaderStyle>
        <SelectedRowStyle BackColor="#CCFF99" Font-Bold="False" ForeColor="#FFFFF" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <Columns>
            <asp:BoundField HeaderText="From" DataField="Fromaddress" ReadOnly="True" SortExpression="From">
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:ButtonField DataTextField="subject" HeaderText="Subject" SortExpression="subject"
                Text="Subject" CommandName="messages">
                <ItemStyle Wrap="False" />
            </asp:ButtonField>
            <asp:BoundField DataField="updateddate" HeaderText="Date" ReadOnly="True" SortExpression="updateddate"
                DataFormatString="{0:d}">
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkAll" onclick="CheckAll(this);" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
