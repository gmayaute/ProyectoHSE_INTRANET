<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ErrorNOV.aspx.vb" Inherits="ErrorNOV_aspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <%--  <link href="App_Themes/Estilos/Estilos.css" rel="stylesheet" type="text/css" />--%>

    <style type="text/css">
        .style1
        {
            width: 10px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    

    <div class= "JER_CONTE_body" id="main" style="overflow:auto;">
        
    <p align="center">
    </p>
    <p align="center">
      &nbsp;</p>
    <p align="center">
      <table   height="20" cellspacing="0" cellpadding="0" align="center" border="0" runat="server" style="border-bottom: slategray 2px solid">
        <tr>
          <td align="center" colspan="2" height="27" style="border-top: slategray 2px solid">
          </td>
        </tr>
        <tr>
          <th align="center">
            <img src="App_Themes/Imagenes/Advertencia.JPG" style="width: 76px; height: 72px" /></th>
          <th width="91%">
            <div align="left">
              ADVERTENCIA</div>
          </th>
        </tr>
        <tr>
          <td align="center">
          </td>
          <td align="left" width="91%">
            <p>
              <br>
              <font face="Verdana" size="1">El sistema a encontrado un conflicto&nbsp;interno.</font></p>
            <p>
              <font face="Verdana" size="1">Intentelo más tarde o comunique el detalle técnico al
                área de&nbsp;Sistemas.</font></p>
            <p>
              <asp:TextBox ID="txtDetalle" runat="server" Height="150px" TextMode="MultiLine"
                                Width="50%" Visible="False" ReadOnly="True"></asp:TextBox></p>
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <asp:linkbutton runat="server" Text="Regresar" ID="lnkRegresar"  style="font-family: Verdana; font-size: XX-Small; font-weight: bold;" />
          </td>
        </tr>
      </table>
    </p>

    </div>

    </form>
</body>
</html>
