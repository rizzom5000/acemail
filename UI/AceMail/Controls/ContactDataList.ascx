<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactDataList.ascx.cs"
    Inherits="AceMail.Controls.ContactDataList" %>
<%@ Register Src="ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
<div>
    <asp:DataList ID="dtlClients" DataKeyField="clientid" RepeatColumns="3" RepeatDirection="Horizontal"
        EnableViewState="true" OnItemCommand="dtlClients_ItemCommand" OnItemDataBound="dtlClients_ItemDataBound"
        CssClass="dtlClients" runat="server">
        <ItemTemplate>
            <div class="tmpClient">
                <div>
                    <asp:Image ID="imgClient" runat="server" />
                </div>
                <br />
                <div>
                    <asp:LinkButton ID="btnName" runat="server" />
                </div>
                <div>
                    <asp:Label ID="lblEmail" runat="server" />
                </div>
                <div>
                    <asp:LinkButton ID="btnDelete" Text="Delete" CommandName="btnDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "clientid") %>'
                        runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <div id="divPage" class="paging" runat="server">
        <div id="prevDiv">
            <asp:Button ID="btnPrev" Text=" << " OnClick="btnPrev_Click" runat="server" />
        </div>
        <div id="divNext">
            <asp:Button ID="btnNext" Text=" >> " OnClick="btnNext_Click" runat="server" />
        </div>
        <div id="divNum">
            <asp:Label ID="lblPage" runat="server" />
        </div>
    </div>
</div>
