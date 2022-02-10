<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  >	
	<head>
		<title>Integrated Accounting System</title>
	</head>
	<frameset border="0" framespacing="0" rows="85,*,24" frameborder="0">
		<frame name="HeaderFrame" src="header.aspx" noresize="noresize" scrolling="no" target="ContentFrame">
		    <frameset border="0" framespacing="0" cols="250,*" frameborder="0" framewidth="0">
		        <frame name="MenuFrame" src="menu.aspx" noresize="noresize" scrolling="auto" target="ContentFrame" />
		        <frame name="ContentFrame" src="blank.aspx" noresize="noresize" scrolling="auto" target="ContentFrame" />
		    </frameset>
		<frame name="FooterFrame" src="footer.aspx" noresize="noresize" scrolling="no" target="ContentFrame" />
		<noframes>
			<body>
				<p>This page uses frames, but your browser doesn't support them.</p>
			</body>
		</noframes>
	</frameset>
</html>