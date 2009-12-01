<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true"
    CodeBehind="FileManager.aspx.cs" Inherits="AceMail.FileManager" Title="File Manager" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<%@ Register Src="Controls/FileDataList.ascx" TagName="FileDataList" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <div id="content">
        <cc:FileDataList id="dtlFiles" runat="server" />
        <br />
        <asp:Label ID="lblUpload" Text="Upload File" runat="server" />
        <div>
            <asp:FileUpload ID="FileUpload" runat="server" />
            <br />
            <asp:Button ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" runat="server" />
        </div>
    </div>
   <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
