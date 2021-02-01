<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript" src="./config/js/jquery-3.1.0.js"></script>
  <script language="javascript" type="text/javascript" src="./config/js/jquery.bpopup.min.js"></script>
  <script language="javascript" type="text/javascript" src="./config/js/extend.js?v=2020-07-23-001"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            var $idx = $("#idx_h").val();
            var $intPage = $("#intPage_h").val();
            var $SearchOpt = $("#SearchOpt_h").val();
            var $SearchVal = $("#SearchVal_h").val();
            var $ref = $("#ref_h").val();
            var $re_step = $("#re_step_h").val();
            var $re_lvl = $("#re_lvl_h").val();

            $("#btnList").on("click", function () {
                location.href = "list.aspx?intPage=" + $intPage + "&SearchOpt=" + $SearchOpt + "&SearchVal=" + $SearchVal;
            });

            $("#btnReply").on("click", function () {
                location.href = "reply.aspx?idx=" + $idx + "&intPage=" + $intPage + "&SearchOpt=" + $SearchOpt + "&SearchVal=" + $SearchVal + "&ref_=" + $ref + "&re_step=" + $re_step + "&re_lvl=" + $re_lvl;
            });

            $("#btnDel").on("click", function () {
                $('#popPwd').bPopup(
                    { modalClose: true },
                    function () { $("#pwdChk").val('').focus(); }
                );
            });

            $("#btnMod").on("click", function () {
                location.href = "mod.aspx?idx=" + $idx + "&intPage=" + $intPage + "&SearchOpt=" + $SearchOpt + "&SearchVal=" + $SearchVal;
            });

            $("#btnPwdChkOk").on("click", function (e) {
                var $res = "";
                var $pwd = $("#pwdChk");
                if (!$pwd.getRightPwd()) {
                    alert("비밀번호가 올바르지 않습니다\n\n- 영문,숫자,특수문자 조합으로 8~16자이어야합니다");
                    $pwd.focus();
                    return false;
                }
                //alert("idx="+$idx+"&pwd="+escape($pwd.val()));
                //location.href = "pwdChk.jsp?idx="+$idx+"&pwd="+escape($pwd.val());
                //return;
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "view.aspx/getChkPwd",
                    data: "{idx:'" + $idx + "',pwd:'" + escape($pwd.val()) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).fail(function (request, status, error) {  //error
                    alert("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);
                }).done(function (msg) {
                    $res = String(msg.d);
                });

                //alert($res);return;
                if ($res != 'ok') {
                    alert("error : " + $res);
                } else {
                    alert("비밀번호가 확인되었습니다")
                    $("#pwd_h").val($("#pwdChk").val());
                    $("#frmBoard").attr({ 'action': 'del_ok.aspx', 'method': 'post' }).submit();
                }
            });

        });
  </script>
  <style type="text/css">
  #popPwd {
	width:500px;
	height:160px;
	border:1px solid gray;
	display:none;
	background-color:white;
	position:relative;
  }
  #popPwd #bClose {
	position:absolute;
	right:-10px;
	top:-30px;
	font:arial-black;
	font-size:36px;
	font-weight:bold;
	color:black;
	background-color:yellow;
	width:40px;
	height:40px;
	line-height:40px;
	text-align:center;
	cursor:pointer;
  }

  #popPwd #pwdcheckbody {
	margin-left:20px;
	margin-top:20px;
  }

  .attach {
	display:block;
  }
  .attach img {
	width:100px;
	border:1px solid gray;
	margin-right:5px;
  }
  </style>
</head>
<body>
    <form id="frmBoard" runat="server">
        <asp:HiddenField ID="idx_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="intPage_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="SearchOpt_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="SearchVal_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="ref_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="re_step_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="re_lvl_h" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="pwd_h" runat="server"></asp:HiddenField>

        <table border="1">
		<tr>
		<td align="center"><b>작성자</b></td><td>
            <asp:Label ID="lblUname" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>제목</b></td><td>
            <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>내용</b></td><td>
            <asp:Label ID="lblContents" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>클릭수</b></td><td>
            <asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>아이피</b></td><td>
            <asp:Label ID="lblReg_ip" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>아이피(m)</b></td><td>
            <asp:Label ID="lblMod_ip" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>등록일</b></td><td>
            <asp:Label ID="lblReg_Date" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>수정일</b></td><td>
            <asp:Label ID="lblMod_Date" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
		<td align="center"><b>첨부파일</b></td><td>
            <asp:Label ID="lblAup" runat="server" Text="Label"></asp:Label>
		</td>
		</tr>
		</table>


        <div>
			<input type="button" value="리스트" id="btnList" />
			<input type="button" value="답글" id="btnReply" />
			<input type="button" value="수정" id="btnMod" alt="수정" />
			<input type="button" value="삭제" id="btnDel" alt="삭제" />
		</div>

		<div id="popPwd" class="b-close">
			<div id="bClose" class="b-close">
				X
			</div>
			<div id="pwdcheckbody">
				<b>● 비밀번호 확인</b> <br><br>
				해당글 삭제를 위해 글 비밀번호를 입력하세요<br><br>

				<input type="password" name="pwdChk" id="pwdChk" value="" placeholder="비밀번호" minlength="8" maxlength="16">
				<input type="button" value="확인" id="btnPwdChkOk">
			</div>
		</div>

    <div>
    
    </div>
    </form>
</body>
</html>
