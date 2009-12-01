<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="AceMail.Home" Title="Ace Mail Home" %>
<%@ Register Src="Controls/NoticePopUp.ascx" TagName="NoticePopUp" TagPrefix="cc" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <asp:UpdatePanel ID="upnlCalendar" runat="server">
        <ContentTemplate>
            <div id="monthview">
                <asp:Calendar ID="calendar" runat="server" BackColor="#FFFFCC" BorderColor="#D7D2C1"
                    BorderStyle="Solid" ForeColor="Black" CellPadding="4" Height="350px" Width="500px"
                    OnDayRender="calendar_DayRender" OnSelectionChanged="calendar_SelectionChanged"
                    OnVisibleMonthChanged="calendar_VisibleMonthChanged">
                    <SelectedDayStyle BackColor="#FE2E2E" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <DayStyle BorderStyle="Solid" BorderWidth="1px" />
                </asp:Calendar>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlNotices" runat="server">
        <ContentTemplate>
            <div id="notices">
                <label>
                    Notices</label>
                <asp:LinkButton ID="btnNotice" Text="New" OnClick="btnNotice_Click" runat="server" />
                <br />
                <br />
                <asp:Label ID="lblNotice" runat="server" />
                <asp:DataList 
                    ID="dlNotices" 
                    DataKeyField="noticeid" 
                    OnItemCommand="dlNotices_ItemCommand"
                    OnItemDataBound="dlNotices_ItemDataBound" 
                    RepeatDirection="Vertical" 
                    runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="hlNotice" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                            CommandArgument="view" runat="server" />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc:NoticePopUp ID="popNotice" runat="server" />
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
