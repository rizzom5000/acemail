<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="MailView.aspx.cs"
    Inherits="AceMail.Message" AspCompat="true" Title="Mail View" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div id="menu">
        <ul>
            <li>
                <asp:LinkButton ID="btnReplyAll" Text="Reply All" OnClick="btnReplyAll_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnReply" Text="Reply" OnClick="btnReply_Click" runat="server" /></li>
        </ul>
    </div>
    <div id="content">
        <div id="messageheader">
            <div class="right-justify">
                <asp:Label ID="lblDate" CssClass="right-justify" runat="server" />
            </div>
            <br />
            <div>
                <asp:Label ID="lblFrom" CssClass="boldlabel"  Text="From:" runat="server"/>
                <asp:Label ID="lblFromAddress" CssClass="aspdisplay" runat="server" />
            </div>
            <br />
            <div>
                <asp:Label ID="lblTo" CssClass="boldlabel"  Text="To:" runat="server"/>
                <asp:Label ID="lblToAddress" CssClass="aspdisplay" runat="server" />
            </div>
            <br />
            <div>
                <asp:Label ID="lblCC" CssClass="boldlabel"  Text="CC:" runat="server"/>
                <asp:Label ID="lblCCAddress" CssClass="aspdisplay" runat="server" />
            </div>
            <br />
            <br />
            <div>
                <asp:Label ID="lblSubjectText" CssClass="subjectdisplay" runat="server" />
            </div>
        </div>
        <div id="scroll" runat="server" class="scroll">
            <asp:DataList ID="dtlAtts" runat="server" RepeatDirection="Vertical" OnItemCommand="dtlAtts_ItemCommand">
                <HeaderStyle Font-Bold="true" Font-Size="Larger" />
                <HeaderTemplate>
                    Attachments
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDownload" Text="Download" CommandName="download" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Key")%>'
                        runat="server" />
                    <%# DataBinder.Eval(Container.DataItem, "Value")%>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div id="messagebody" runat="server" class="messagebody" />
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
