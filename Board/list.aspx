<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript" src="./config/js/jquery-3.1.0.js"></script>
  <script language="javascript" type="text/javascript" src="./config/js/extend.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#frmBoard").submit(function (e) {
                ////e.preventDefault(); //This can interrupt html5 form check system.
                ////if (confirm("저장할까요?")) {

                $("#frmBoard").attr({ 'action': 'list.aspx' });

               ////}
            });

            $(document).on("click", "#btnInit", function () { //for dynamic event...
                location.href = 'list.aspx';
                //alert('test');
            });

        });
  </script>
</head>
<body>

    <form id="frmBoard" runat="server">
    <div>
        <div id="write">
		[<a href="write.aspx">글등록</a>] ... asp.net board with mssql/ckeditor/file upload
	    </div>

        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <table border="1">
	            <tr>
	            <td align="center"><b>번호</b></td>
	            <td align="center"><b>제목</b></td>
	            <td align="center"><b>작성자</b></td>
	            <td align="center"><b>클릭수</b></td>
	            <td align="center"><b>작성일</b></td>
	            </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
		        <td align="center">
                    <asp:Label ID="lblNum" runat="server" Text=""></asp:Label></td>
		        <td align="left">
                    
                    <asp:Image ID="img_lvl" runat="server" border="0" align="absmiddle" ImageUrl="./images/common/level.gif" />
                    <asp:Image ID="img_re" runat="server" border="0" align="absmiddle" ImageUrl="./images/common/ico_reply.gif" />

                    <asp:HyperLink ID="linkView" runat="server"><%#Eval("title")%></asp:HyperLink>

		        </td>
		        <td align="center"><%#Eval("uname")%></td>
		        <td align="center"><%#Eval("count")%></td>
		        <td align="center"><%#Eval("reg_date")%></td>
		        </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>


        <div id="search">
            <asp:DropDownList ID="searchOpt" runat="server" required oninvalid="this.setCustomValidity('검색옵션을 선택하세요')" oninput="setCustomValidity('')">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="title">제목</asp:ListItem>
                <asp:ListItem Value="contents">내용</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="searchVal" runat="server" maxlength="10" minlength="2" required oninvalid="this.setCustomValidity('검색어를 입력하세요')" oninput="setCustomValidity('')"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="검색" />

            <asp:Button ID="btnInit" runat="server" Text="처음" UseSubmitBehavior="False" />

        </div>

        <asp:Label ID="paging" runat="server" Text="paging"></asp:Label>
    </form>
</body>
</html>
