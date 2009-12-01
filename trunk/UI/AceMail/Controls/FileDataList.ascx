<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileDataList.ascx.cs"
    Inherits="AceMail.Controls.FileDataList" %>
<%@ Register Src="ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
<link rel="Stylesheet" type="text/css" href="~/Style.css" id="style" runat="server" visible="false" />

<div>
    <table class="dtlMain" border="0" cellpadding="0" cellspacing="1" align="center"
        width="800px">
        <asp:DataList ID="dtlFiles" runat="server" DataKeyField="fileid" RepeatDirection="Vertical"
            RepeatLayout="Flow" RepeatColumns="5" OnItemCommand="dtlFiles_ItemCommand">
            <SelectedItemStyle CssClass="dtlSelected" />
            <HeaderTemplate>
                <tr>
                    <th align="center">
                        <b>File</b>
                    </th>
                    <th align="center">
                        <b>Last Updated</b>
                    </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%# DataBinder.Eval(Container.DataItem, "Filename")%>
                    </td>
                    <td align="center">
                        <%# DataBinder.Eval(Container.DataItem, "updateddate")%>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "fileid")%>'
                            runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="btnDownload" Text="Download" CommandName="download" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "fileid")%>'
                            runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="btnDelete" Text="Delete" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "fileid")%>'
                            runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
    </table>
    <div id="divPage" class="paging" runat="server">
        <div id="divPrev">
                <asp:Button ID="btnPrev" OnClick="btnPrev_Click" Text=" << " runat="server" />
        </div>
        <div id="divNext">
                <asp:Button ID="btnNext" OnClick="btnNext_Click" Text=" >> " runat="server" />
        </div>
        <div id="divNum">
            <asp:Label ID="lblPage" runat="server" />
        </div>
    </div>
</div>
