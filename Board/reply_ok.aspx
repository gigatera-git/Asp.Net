<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reply_ok.aspx.cs" Inherits="reply_ok" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript" src="./config/js/jquery-3.1.0.js"></script>
  <script language="javascript" type="text/javascript" src="./config/js/extend.js"></script>
  <script language="javascript" type="text/javascript">
      $(document).ready(function () {
          setTimeout(function () {
              var qs = $("#queryString").val();
              location.href = "list.aspx?" + qs;
		}, 5000);
	});
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblRes" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="queryString" runat="server" />
    </div>
    </form>
</body>
</html>
