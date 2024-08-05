<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EndSession.aspx.vb" Inherits=".EndSession" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
  <%--   <link href="App_Themes/Estilos/Estilos.css" rel="stylesheet" type="text/css" />--%>
     <script type="text/javascript" src="App_Themes/JavaScript/Funciones.js"></script>
     <script type="text/javascript" src="App_Themes/JavaScript/Utiles.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class= "JER_CONTE_body" id="main" style="overflow:auto;">
          <table  border="0" width="100%">
                    <tr style="width:100%">
                        <td style="width:50%; text-align:left;">
                             <asp:Image ID="Image1" runat="server" 
                              ImageUrl="~/App_Themes/Imagenes/logoOil.png" Width="300px" Height="35px"/></td>
                    </tr>
                    <tr  style="width:100%">
                        <td class="LineaAzul"></td>
                    </tr>
         </table>
              
                
                   <table class="st_Panel" style="width:100%" border="0" >
                            <tr style="width:100%">
                                <td style="text-align:Center;font-size: 16px;" class="st_SubTitulo02" >
                                        Su sesión ha finalizado.
                                        Gracias por su visita.   
                                </td>
                            </tr>
                            <tr>
                             <td style="text-align:Center;font-size: 12px;"> 
                                 <asp:LinkButton ID="lnkInicio" runat="server" PostBackUrl="~/Login.aspx"  style="font-family: Verdana; font-size: XX-Small; font-weight: bold;" >Regresar al Inicio</asp:LinkButton>
                             </td>
                            </tr>
                    </table>
                <div style="height: 2px;">
                </div>

    </div>
    </form>
</body>
</html>
