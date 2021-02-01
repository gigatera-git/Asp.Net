<%@ Page Language="C#" AutoEventWireup="true" CodeFile="write_ok.aspx.cs" Inherits="write_ok" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>글쓰기 결과</title>
    <script language="javascript" type="text/javascript" src="./config/js/jquery-3.1.0.js"></script>
    <script language="javascript" type="text/javascript" src="./config/js/extend.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                location.href = "list.aspx";
            }, 5000);
        });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblRes" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
