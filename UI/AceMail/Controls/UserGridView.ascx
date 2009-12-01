<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserGridView.ascx.cs"
    Inherits="AceMail.Controls.UserGridView" %>
<%@ Register Src="ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="uc" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->

<div class="gvUsers">
    Existing System Users
    <asp:GridView 
        ID="gvUsers" 
        BackColor="#F0F0F0" 
        BorderColor="Silver" 
        BorderStyle="None"
        BorderWidth="1px" 
        CellPadding="3" 
        CaptionAlign="Top" 
        GridLines="Horizontal" 
        AutoGenerateColumns="False"
        AllowPaging="True" 
        DataKeyNames="userid" 
        AllowSorting="True"
        OnRowDataBound="gvUsers_RowDataBound" 
        OnRowCommand="gvUsers_RowCommand" 
        OnPageIndexChanging="gvUsers_PageIndexChanging"
        HorizontalAlign="Center" 
        CssClass="gvUsers" 
        runat="server">
        <HeaderStyle Font-Bold="True" ForeColor="#150517" HorizontalAlign="Left"></HeaderStyle>
        <SelectedRowStyle BackColor="#CCFF99" Font-Bold="False" ForeColor="#FFFFF" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <Columns>
            <asp:BoundField HeaderText="Login" DataField="login" ReadOnly="True" SortExpression="login">
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="createddate" HeaderText="Date Enrolled" ReadOnly="True"
                SortExpression="createddate" DataFormatString="{0:d}">
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:ButtonField CommandName="delete" Text="Delete" />
        </Columns>
    </asp:GridView>
</div>
