<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopudBuscarEmpleado.aspx.vb" Inherits="NOVDOC.BuscarEmpleado" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar Empleado</title>

<%--     <link href="../App_Themes/Estilos/Estilos.css" rel="stylesheet" type="text/css" />--%>

     <script type="text/javascript" src="../App_Themes/JavaScript/Funciones.js"></script>
     <script type="text/javascript" src="../App_Themes/JavaScript/Utiles.js"></script>

      <script type="text/javascript">
          function closeWin() {
              GetRadWindow().close();
              top.location.href = top.location.href; 
          }

          function GetRadWindow() {
              var oWindow = null; if (window.radWindow)
                  oWindow = window.radWindow; else if (window.frameElement.radWindow)
                  oWindow = window.frameElement.radWindow; return oWindow;
          }

          function ValidarIngresoCampos() {
              return true;
          }

        </script>

</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="true"  Skin="Sunset" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="true"    Behaviors="Close"     ></telerik:RadWindowManager>

   <asp:Panel ID="pnlFormulario" runat="server" Width="100%" DefaultButton="btnBuscar">
   
    <div class= "JER_CONTE_body" id="main" style="overflow:auto;">
          <table id="tTitulo" border="0" width="100%">
                    <tr>
                        <td class="stlTituloPagina">
                             Buscar Empleado</td>
                    </tr>
                    <tr>
                        <td class="LineaAzul"></td>
                    </tr>
                </table>
                <div style="height: 2px;">
                </div>
                     <asp:UpdatePanel ID="upCriterios" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                                 <table border="0" cellspacing="0" class="st_Panel" width="100%">
                                        <tr>
                                            <td class="st_SubTitulo02" colspan="2">
                                                &nbsp;Criterios de Búsqueda
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                  <asp:DropDownList ID="ddlTipoBusqueda" runat="server" AutoPostBack="true">
                                                  </asp:DropDownList>
                                                  <asp:DropDownList ID="ddlTipoDocumento" runat="server" Visible="false">
                                                  </asp:DropDownList>
                                                  <asp:TextBox ID="txtBusqueda" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                   <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" >
                                                   <ContentTemplate>
                                                   <asp:Button ID="btnBuscar" runat="server"  Text=""   
                                                           ToolTip="Buscar" Width="0px" Height="0px" BorderWidth="0px"  BorderColor="White" ForeColor="White" BackColor="White" />
                                                   </ContentTemplate>
                                                  </asp:UpdatePanel>

                                                  <asp:Button ID="btnAceptar" runat="server" SkinID="BotonEstandar" Text="Aceptar" Width="107px" />
                                            </td>
                                        </tr>
                                    </table>
                          </ContentTemplate> 
                     </asp:UpdatePanel> 
                <div style="height: 2px;">
                </div>
                <asp:UpdatePanel ID="upDatos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                            <table class="st_Panel" border="0">
                                    <tr>
                                        <td class="st_SubTitulo02">
                                            Resultados de la Búsqueda :&nbsp;
                                            
                                            <asp:Label ID="lbContador" runat="server" Font-Bold="False"></asp:Label>
                                        </td>
                                    </tr>
                            </table>

                            <%--ANIMACION PARA EL EFECTO DE CARGANDO--%>

                           <div style="display:none; padding-left:35%"   id="div_Cargando_01">
                             <table width="100%">
                                <tr>
                                    <td style="vertical-align: middle; font-style: italic" align="left">
                                       Buscando Información...&nbsp;<img alt="Autenticando Usuario..." src="../App_Themes/Imagenes/Loading_01.gif" align="absMiddle" />
                                    </td>
                                </tr>
                             </table>
                           </div>

                           <div style="height: 2px">
                           </div>

                           <div style="vertical-align: top;overflow: auto; left: 0px; width: 100%; height: 400px" class="HeaderGV">
                            <telerik:RadGrid ID="rgMantenimiento" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="true">
                                <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowFilteringByColumn="True" ShowFooter="True" EditMode="InPlace"
                                 AutoGenerateColumns="false" DataKeyNames="codigo">
                                  <CommandItemSettings  RefreshText="Actualizar" />
                                  <RowIndicatorColumn>
                                     <HeaderStyle Width="20px" />
                                  </RowIndicatorColumn>
                                  <ExpandCollapseColumn>
                                   <HeaderStyle Width="20px" />
                                  </ExpandCollapseColumn>
                                  <Columns>
                                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                        <telerik:GridBoundColumn HeaderText="CodEmpleado"     DataField="codigo"             UniqueName="codigo"     AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nombre"          DataField="Nombre"             UniqueName="Nombre"          AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="ApellidoPaterno" DataField="ApellidoPaterno"    UniqueName="ApellidoPaterno" AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="ApellidoMaterno" DataField="ApellidoMaterno"    UniqueName="ApellidoMaterno" AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Cargo"           DataField="Cargo"              UniqueName="Cargo"           AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="numdocide"       DataField="numdocide"          UniqueName="numdocide"        AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="pasaporte"       DataField="pasaporte"          UniqueName="pasaporte"        AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="CarnetExt"       DataField="CarnetExt"          UniqueName="CarnetExt"        AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Dirección"       DataField="Direccion"          UniqueName="Direccion"    HeaderStyle-Width="250px" AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="TextoGrilla">
                                        </telerik:GridBoundColumn>
                                  </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            </div>

                        </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                
                 
                 
                   <table class="st_Panel" border="0">
                                    <tr style="width:100%">
                                        <td style="text-align:right;" class="st_SubTitulo02">
                                            &nbsp;</td>
                                    </tr>
                    </table>
                </div>
                <div style="height: 2px;">
                </div>

    </div>
    
    </asp:Panel> 
    </form>
</body>
</html>
