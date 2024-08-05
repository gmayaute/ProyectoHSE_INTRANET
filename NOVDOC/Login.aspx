<%@ Page Title="Sistema Control Documentario NOVPeru" Language="vb" AutoEventWireup="false"  CodeBehind="Login.aspx.vb" Inherits="NOVDOC.login" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

   <%--   <link href="App_Themes/Estilos/Estilos.css" type="text/css" rel="stylesheet" />--%>
   <style type="text/css">
    #UpdatePanel1, #UpdatePanel2, #UpdateProgress1 { 
      border-right: white 1px solid; border-top: white 1px solid; 
      border-left: white 1px solid; border-bottom: white 1px solid;
    }
    #UpdatePanel1, #UpdatePanel2 { 
      width:80px; height:10px; position: relative;
      float: left; margin-left: 80px; margin-top: 10px;
     }
     #UpdateProgress1 {
      width: 400px; background-color: #FFC080; 
      bottom: 0%; left: 0px; position: absolute;
     }
       .style1
       {
           font: 11px Arial, Helvetica,sans-serif;
           color: #676767;
           width: 80px;
           height: 21px;
       }
       .style2
       {
           width: 150px;
           height: 21px;
       }
    </style>


      <script type="text/javascript" src="App_Themes/JavaScript/Utiles.js">   </script>     
      <script type="text/javascript" src="App_Themes/JavaScript/Funciones.js"></script>

      <script type="text/javascript">
          function ValidarIngresoCampos() {
              var usuario = document.getElementById('txtUsuario').value;
              var clave = document.getElementById('txtPass').value;
              if (usuario == "") {
                  document.getElementById('lblMensajeError').innerHTML = "Ingrese usuario."
                  return false;
              }
              else {
                  if (clave == "") {
                      document.getElementById('lblMensajeError').innerHTML = "Ingrese clave."
                      return false;
                  }
              
              }
              return true;
          }

   </script>

</head>
<body style="background-color:White" >


    <form id="form1" runat="server">
    <div id="Divform">  
        <div style="height: 4px; width: 100%">
            &nbsp;
        </div>
        <div style="width: 100%; padding-left:28%;">
            <img alt="" src="App_Themes/Imagenes/logoOil.png"/>
        </div>

        <div style="height: 4px; width: 100%" class="division">
            &nbsp;
        </div>

        <br style="clear: both" />

        <div style="height: 4px; width: 100%">
            &nbsp;</div>
        
        <div id="main">
             <div style="width: 100%;" >
                   <p style="text-align:center;padding:0px" class="main_login_text">
                       &nbsp;</p>
             </div>
             <div style="width:100%; text-align:center;">
                <img alt="" src="App_Themes/Imagenes/Documentacion.jpg"  style="padding-top:0px; padding-bottom:0px; padding-right:0px; padding-left:0px"   />
             </div>
            <div id="main_login_titulo" text-align:center;">
                    <p class="main_login_text" align="center">
                        <b>Login</b>
                    </p>
             </div>
             <div style="height: 2px; width: 100%">
                 &nbsp;</div>
            <div id="main_login">
     
            <div  style="width:100%; text-align:center;">

                          <table align=center style="height: 95px">   
                                 <tr>
                                  <td align="left" class ="style1" >
                                     <asp:Label ID="lblusuario" runat="server" Text="Usuario"></asp:Label>
                                  </td>
                                   <td class="style2">
                                     <asp:TextBox ID="txtUsuario" runat="server" class ="stlPaginaTexto">
                                     </asp:TextBox> 
                                   </td>
                                 </tr>
                                 <tr>
                                   <td style="width:80px" align="left" class ="stlPaginaTexto" >
                                      <asp:Label ID="lblPass" runat="server" Text="Contraseña" class ="stlPaginaTexto"></asp:Label> 
                                   </td>
                                   <td style="width:150px"> 
                                       <asp:TextBox ID="txtPass" runat="server" class ="stlPaginaTexto" TextMode="Password"></asp:TextBox> 
                                   </td>
                                 </tr>
                                
                                 <tr>

                                  <td colspan="2"  style="text-align:right; vertical-align: bottom">  

                                  <asp:Button ID="btnIngresar" runat="server" Text="Iniciar sesión" 
                                          SkinID ="BotonEstandar"  Width="104px"/>
                                   </td>
                                 
                                 </tr>
                                </table>

                                <div  class="Espacio" >  
                                </div>

                                <div style=" text-align:center;">
                                   <asp:Label ID="lblMensajeError" class ="stlPaginaTexto"  runat="server" 
                                        ForeColor="Red" Width="600px"></asp:Label>              
                                </div>

                          </div>


            </div>

        </div> 
    
    
    </div>
        


    <%--ANIMACION PARA EL EFECTO DE CARGANDO--%>

    <div style="display:none; padding-left:35%"   id="div_Cargando_01">
        <table width="100%">
            <tr>
                <td style="vertical-align: middle; font-style: italic" align="left">
                    Autenticando Usuario...&nbsp;<img alt="Autenticando Usuario..." src="/finance/App_Themes/Imagenes/Loading_01.gif" align="absMiddle" />
                </td>
            </tr>
        </table>
    </div> 

  

</form>


</body>
</html>
