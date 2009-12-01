<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true"
    CodeBehind="FileView.aspx.cs" Inherits="AceMail.FileView" Title="File View" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div id="content">
        <div>
            <FCKeditorV2:FCKeditor ID="editor" Height="500px" runat="server">
            </FCKeditorV2:FCKeditor>
        </div>
        <br />
        <div id="bmenu">
            <asp:LinkButton ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" />    </div>
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
