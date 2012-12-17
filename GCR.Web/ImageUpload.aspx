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

                <ccPiczardUC:SimpleImageUpload ID="ImageUploader" runat="server" Width="830px" AutoOpenImageEditPopupAfterUpload="true" />
                <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="cancel" UseSubmitBehavior="true" CssClass="hide" /> 
                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="save" UseSubmitBehavior="true" CssClass="hide"  /> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script type="text/javascript">

        function Upload_Notify() {
            var notifier = window.parent.GCR.Notifier;
            if (notifier != null) {
                notifier.notify.apply(null, arguments);
            }
         }

        function Upload_Ready() {
             window.setTimeout(function () {
                 Upload_Notify("ready");
             }, 25);
         }

         function Upload_SaveSuccess(path) {
             Upload_Notify("saved", path);
         }
    </script>
</body>
</html>
