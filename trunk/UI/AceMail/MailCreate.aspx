<%@ Page Language="C#" MasterPageFile="~/AceMail.Master" AutoEventWireup="true" CodeBehind="MailCreate.aspx.cs"
    Inherits="AceMail.Mail" Title="Mail Create" AspCompat="true" %>
<%@ Register Src="Controls/ErrorPopUp.ascx" TagName="ErrorPopUp" TagPrefix="cc" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<!--
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
-->
    <script type="text/javascript">
        function setAddress()
        {
           var listBox = document.getElementById('<%= lbxAddr.ClientID %>');
           var boxType = document.getElementById('<%= hdnFld.ClientID %>').value;
           var textBox;
           if(boxType == "to")
           {
                textBox = document.getElementById('<%= txtTo.ClientID %>');
           }
           if(boxType == "cc")
           {
                textBox = document.getElementById('<%= txtCC.ClientID %>');
           }
           if(boxType == "bcc")
           {
                textBox = document.getElementById('<%= txtBCC.ClientID %>');
           }
           var selectedIndex = listBox.selectedIndex;
           textBox.value += listBox.options[selectedIndex].value + "; ";
        }
    	
        function getAddr(type)
        {
           var boxType = document.getElementById('<%= hdnFld.ClientID %>');
           boxType.value = type;
           $pop = jQuery('#divAddressPop');
           $pop.dialog({modal:false, position:'center', autoOpen:false, overlay:{opacity:0.5, background:'gray'}});
           $pop.parent().appendTo(jQuery('form:first')); 
           $pop.show(); 
           $pop.dialog('open'); 
           return false;
        }
        
        function hideAddressPop()
        {
            $pop.dialog('close'); 
            return false;
        }
    </script>

    <div id="menu">
        <ul>
            <li>
                <asp:LinkButton ID="btnCancel" Text="Cancel" PostBackUrl="~/MailManager.aspx" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" /></li>
            <li>
                <asp:LinkButton ID="btnSend" Text="Send" OnClick="btnSend_Click" runat="server" /></li>
        </ul>
    </div>
    <br />
    <div id="content">
        <asp:UpdatePanel ID="upnlHeader" runat="server">
            <ContentTemplate>
                <div id="mailheader">
                    <div>
                        <label>
                            To:</label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtTo" CssClass="aspinput" runat="server" />
                        <button id="btnTo" onclick="getAddr('to')">
                            ...</button>
                    </div>
                    <br />
                    <div>
                        <label>
                            CC:</label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtCC" CssClass="aspinput" runat="server" />
                        <button id="btnCC" onclick="getAddr('cc')">
                            ...</button>
                    </div>
                    <br />
                    <div>
                        <label>
                            BCC:</label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtBCC" CssClass="aspinput" runat="server" />
                        <button id="btnBCC" onclick="getAddr('bcc')">
                            ...</button>
                    </div>
                    <br />
                    <div>
                        <label>
                            Subject:</label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtSubject" CssClass="aspinput" runat="server" />
                    </div>
                    <br />
                    <div>
                        <label>
                            HTML?</label>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkHTML" CssClass="aspinput" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="scroll" runat="server" class="scroll">
            <asp:DataList ID="dtlAtts" runat="server" RepeatDirection="Vertical" OnItemCommand="dtlAtts_ItemCommand">
                <HeaderStyle Font-Bold="true" Font-Size="Larger" />
                <HeaderTemplate>
                    Attachments
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnRemove" Text="Remove" CommandName="remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'
                        runat="server" />
                    <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'
                        runat="server" />
                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div>
            <br />
            <asp:Label ID="lblUpload" Text="Add Attachment" runat="server" />
        </div>
        <div>
            <asp:FileUpload ID="FileUpload" runat="server" />
            <br />
            <asp:Button ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" runat="server" />
        </div>
        <br />
        <div>
            <FCKeditorV2:FCKeditor ID="editor" Height="400px" runat="server" />
        </div>
        <div id="bmenu">
            <asp:LinkButton ID="btnSend2" Text="Send" OnClick="btnSend_Click" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdnFld" runat="server" />
    <div id="divAddressPop" style="display: none;">
        <div class="addressModal" align="center">
            <asp:Label ID="lblAddr" Text="Choose Contacts" runat="server" />
            <br />
            <asp:ListBox ID="lbxAddr" runat="server" />
            <br />
            <asp:Button ID="btnClose" Text="Close" OnClientClick="return hideAddressPop();" CssClass="modalbutton"
                runat="server" />
        </div>
    </div>
    <cc:ErrorPopUp ID="popError" runat="server" />
</asp:Content>
