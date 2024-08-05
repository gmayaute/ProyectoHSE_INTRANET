<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="NOVDOC.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
<HEAD>
  <TITLE>Ejemplo de imagenes</TITLE> 
 
  <SCRIPT LANGUAGE="JavaScript">
      if (document.images) {
          var activado = new Image();
          activado.src = "graphics/encendido.gif";
          var desactivado = new Image();
          desactivado.src = "graphics/apagado.gif";
      }
      function activar(nombreImagen) {
          if (document.images) {
              document[nombreImagen].src = activado.src;
          }
      }
      function desactivar(nombreImagen) {
          if (document.images) {
              document[nombreImagen].src = desactivado.src;
          }
      }
  </SCRIPT>

</HEAD>

<BODY>

<a href="mipagina.html" onclick="activar('prueba');"  onmouseover="activar('prueba');"  onmouseout="desactivar('prueba');">
  <IMG name="prueba" SRC="apagado.gif" BORDER=0>
</a>

</BODY>
</HTML>
