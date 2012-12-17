<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageUpload.aspx.cs" Inherits="GCR.Web.ImageUpload" %>
<%@ Register src="~/piczardUserControls/simpleImageUploadUserControl/SimpleImageUpload.ascx" tagname="SimpleImageUpload" tagprefix="ccPiczardUC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-color:#020202;margin:0;padding:0">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#020202;margin:0;padding:0">
    <form id="uploadForm" runat="server">
    <div>
            <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
      
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>        

                <ccPiczardUC:SimpleImageUpload ID="Picture1" runat="server" Width="830px" AutoOpenImageEditPopupAfterUpload="true" />
                          
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
