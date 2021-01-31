<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mod.aspx.cs" Inherits="mod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>mod</title>
    <script language="javascript" type="text/javascript" src="./config/js/jquery-3.1.0.js"></script>
  <script language="javascript" type="text/javascript" src="./config/js/extend.js"></script>
  <script language="javascript" type="text/javascript" src="./ckeditor/ckeditor.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {


            CKEDITOR.replace('contents', {
                filebrowserUploadUrl: 'uploadCK.ashx'
            });


            //var $uname = $("#uname");
            //var $title = $("#title");
            var $pwd = $("#pwd");
            //var $pwd2 = $("#pwd2");
            //var $content = $("#content");

            /*
            //html4 일때 작성
            //console.log( $uname.getRightPwd() );
            //블라블라 일일이 작성한다
            
            $("#btnOk").on("click",function(e){
                e.preventDefault();
                if (confirm("저장할까요?")) {
                    $("#frmBoard").attr({'action':'write_ok.asp','method':'post'}).submit();
                }
            });
            
            */


            //html5 required 속성 이용
            $("input, textarea").on('focus, keyup', function () {
                $lval = $(this).ltrim();
                $(this).val($lval);
            });

            $("#frmBoard").submit(function (e) {
                //e.preventDefault();
                if (!$pwd.getRightPwd()) {
                    alert("비밀번호가 올바르지 않습니다\n\n1. 영문,숫자,특수문자 조합으로 8~16자이어야합니다\n2. 비밀번호 확인이 다를수 있습니다");
                    $pwd2.focus();
                    return false;
                }
                if (confirm("저장할까요?")) {
                    //$("#frmBoard").attr({ 'action': 'mod_ok.aspx' });
                } else {
                    e.preventDefault();
                }
            });

            $("#btnCancel").on("click", function () {
                history.back();
            });

        });
  </script>
</head>
<body>
    <form id="frmBoard" runat="server">
    <div>
    <table border="1">
		<tr>
		<td align="center"><b>글쓴이</b></td>
		<td>
            <asp:Label ID="lblUname" runat="server" Text="Label"></asp:Label>
            <asp:HiddenField ID="uname" runat="server" />
		</td>
		</tr>
		<tr>
		<td align="center"><b>제목</b></td>
		<td>
            <asp:TextBox ID="title" runat="server" size="30" maxlength="30" placeholder="제목" required oninvalid="this.setCustomValidity('제목을 입력하세요')" oninput="setCustomValidity('')"></asp:TextBox>
		</td>
		</tr>

		<tr>
		<td align="center"><b>비밀번호</b></td>
		<td>
            <asp:TextBox ID="pwd" runat="server" value="12345678#a" size="16" minlength="8" maxlength="16" placeholder="비밀번호" required oninvalid="this.setCustomValidity('비밀번호를 입력하세요')" oninput="setCustomValidity('')" TextMode="Password"></asp:TextBox>
            <asp:Label ID="lblPwdErr" runat="server" Text=""></asp:Label>
		</td>
		</tr>

		<tr>
		<td align="center"><b>내용</b></td>
		<td>
            <asp:TextBox ID="contents" runat="server" TextMode="MultiLine" cols="20" rows="10" required oninvalid="this.setCustomValidity('글내용을 입력하세요')" oninput="setCustomValidity('')"></asp:TextBox>
        </td>
		</tr>

        <tr>
		<td align="center"><b>첨부</b></td>
		<td>
            <asp:Label ID="lblAup" runat="server" Text="Label"></asp:Label>
            <hr />
            <asp:FileUpload ID="files1" runat="server" class="files" /><br>
            <asp:FileUpload ID="files2" runat="server" class="files" /><br />
        </td>
		</tr>

		</table>
		
		<table border="0">
		<tr>
		<td>
            <asp:Button ID="btnOk" runat="server" Text="수정" OnClick="btnOk_Click" />
			<input type="button" value="취소" id="btnCancel">
		</td>
		</tr>
		</table>
    </div>
    </form>
</body>
</html>
