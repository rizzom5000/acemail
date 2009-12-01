<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="ContactView.aspx.cs"
    Inherits="AceMail.ContactView" Title="Contact View" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div id="content">
        <asp:Label ID="lblClient" CssClass="toplabel" Font-Bold="true" Width="450" runat="server" />
        <br />
        <br />
        <div>
            <asp:Image ID="imgClient" Width="100px" Height="75px" Visible="false" runat="server" />
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <label class="toplabel">
                            First Name</label>
                        <asp:TextBox ID="txtFirst" Columns="30" CssClass="aspinputwidth"  runat="server" />
                    </td>
                    <td>
                        <label class="custlabel" style="width: 50px">
                            Middle</label>
                        <asp:TextBox ID="txtMiddle" Columns="1" CssClass="aspinputwidth" runat="server" />
                    </td>
                    <td>
                        <label class="custlabel" style="width: 75px">
                            Last Name</label>
                        <asp:TextBox ID="txtLast" Columns="30" CssClass="aspinputwidth" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <label class="toplabel">
                Address 1</label>
            <asp:TextBox ID="txtAddr1" Columns="30" CssClass="aspinputwidth" runat="server" />
            <br />
            <label class="toplabel">
                Address 2</label>
            <asp:TextBox ID="txtAddr2" Columns="30" CssClass="aspinputwidth" runat="server" />
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <label class="toplabel">
                            City</label>
                        <asp:TextBox ID="txtCity" Columns="30" CssClass="aspinputwidth" runat="server" />
                    </td>
                    <td>
                        <label class="custlabel" style="width: 45px">
                            State</label>
                        <asp:TextBox ID="txtState" Columns="2" CssClass="aspinputwidth" runat="server" />
                    </td>
                    <td>
                        <label class="custlabel" style="width: 30px">
                            Zip</label>
                        <asp:TextBox ID="txtPostal" Columns="8" CssClass="aspinputwidth" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <label class="toplabel">
                Phone Primary</label>
            <asp:TextBox ID="txtPhone1" Columns="10" CssClass="aspinputwidth" runat="server" />
            <br />
            <label class="toplabel">
                Phone Second</label>
            <asp:TextBox ID="txtPhone2" Columns="10" CssClass="aspinputwidth" runat="server" />
            <br />
            <label class="toplabel">
                Phone Mobile</label>
            <asp:TextBox ID="txtMobile" Columns="10"  CssClass="aspinputwidth" runat="server" />
        </div>
        <div>
            <label class="toplabel">
                Email</label>
            <asp:TextBox ID="txtEmail" CssClass="aspshortinput" runat="server" />
        </div>
        <div>
            <asp:Button ID="btnUpdate" Text="Save" OnClick="btnUpdate_Click" CssClass="submitclient" runat="server" />
        </div>
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
