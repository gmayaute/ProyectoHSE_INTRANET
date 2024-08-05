<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AlertaSuministroLicencia.aspx.vb" Inherits="NOVDOC.AlertaSuministroLicencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
      <script type="text/javascript">
          function confirmAspUpdatePanelPostback(button, mensaje) {
              function aspUpdatePanelCallbackFn(arg) {
                  if (arg) {
                      __doPostBack(button.name, "");
                  }
              }
              radconfirm(mensaje, aspUpdatePanelCallbackFn, 330, 110, null, "Confirmar Proceso");
          }
       </script>
        
        <script type="text/javascript" src="../App_Themes/JavaScript/Funciones.js"></script>
        <script type="text/javascript" src="../App_Themes/JavaScript/Utiles.js"></script>

  <asp:Panel ID="pnlFormulario" runat="server" Width="100%">
   
    <div class= "JER_CONTE_body" id="main" style=" overflow:hidden;">
                <table id="tTitulo" border="0" width="100%">
                    <tr>
                        <td class="stlTituloPagina">
                             Establecimientos  National Oilwell Varco</td>
                    </tr>
                    <tr>
                        <td class="LineaAzul"></td>
                    </tr>
                </table>
                <div style="height: 2px;">
                </div>
                     <asp:UpdatePanel ID="upCriterios" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                              
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
                            <div style="height: 2px">
                            </div>
                            <div style="vertical-align: top;overflow: auto; left: 0px; width: 100%; height: 800px; overflow-x:hidden;"
                                class="HeaderGV">

                             <telerik:RadGrid ID="rgContactos" runat="server" AutoGenerateColumns="False" AllowSorting="True"  
                             GridLines="None" AlternatingItemStyle-BackColor="#D6D6D6" 
                             AllowPaging="True" PageSize="10" AllowAutomaticUpdates="true" AllowAutomaticInserts="True">
                              
                              <ClientSettings>
                                <%--<Scrolling  UseStaticHeaders="true" />--%>
                                   <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top"
                         CommandItemSettings-AddNewRecordText="Agregar"
                         CommandItemSettings-RefreshText="Actualizar"  EditMode="EditForms">
                          
                                   <%-- <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>--%>
                                    <Columns>
                                       <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="50px" CancelText="Cancelar" EditText="Editar"    InsertText="Insertar" UpdateText="Actualizar"></telerik:GridEditCommandColumn>
                                       <telerik:GridBoundColumn HeaderText="Nombre"   ItemStyle-Font-Size="8pt" DataField="nombre" UniqueName="nombre"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Dirección"   ItemStyle-Font-Size="8pt" DataField="direccion" UniqueName="direccion"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Codigo Licencia"   ItemStyle-Font-Size="8pt" DataField="codlicencia" UniqueName="codlicencia"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Inicio"   ItemStyle-Font-Size="8pt" DataField="LicenFecIni" UniqueName="LicenFecIni"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Fin"   ItemStyle-Font-Size="8pt" DataField="LicenFecFin" UniqueName="LicenFecFin"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Codigo Defensa Civil"   ItemStyle-Font-Size="8pt" DataField="coddefensa" UniqueName="coddefensa"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Inicio"   ItemStyle-Font-Size="8pt" DataField="DefenFecIni" UniqueName="DefenFecIni"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Fin"   ItemStyle-Font-Size="8pt" DataField="DefenFecFin" UniqueName="DefenFecFin"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Codigo Ambiental"   ItemStyle-Font-Size="8pt" DataField="codcertambiental" UniqueName="codcertambiental"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Inicio"   ItemStyle-Font-Size="8pt" DataField="AmbienFecIni" UniqueName="AmbienFecIni"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Fin"   ItemStyle-Font-Size="8pt" DataField="AmbienFecFin" UniqueName="AmbienFecFin"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Codigo Fumigación"   ItemStyle-Font-Size="8pt" DataField="codfumigacion" UniqueName="codfumigacion"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Inicio"   ItemStyle-Font-Size="8pt" DataField="FumigFecIni" UniqueName="FumigFecIni"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Fecha Fin"   ItemStyle-Font-Size="8pt" DataField="FumigFecFin" UniqueName="FumigFecFin"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       
                                       <telerik:GridTemplateColumn Visible="false" HeaderText="Referencia"  UniqueName="Referencia" AllowFiltering="false"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true"> 
                                       <ItemTemplate>
                                          <asp:ImageButton ID="ImgDatos" runat="server" ImageUrl="~/App_Themes/Imagenes/Validacion01.gif" ToolTip ="Asignar datos adicionales" CommandName="AgregarReferencia" CommandArgument='<%# Eval("Id")  & "," &  Eval("Estado")  & "," &  Eval("FecEntregaIni")  & "," &  Eval("FecEntregaFin")  & "," &  Eval("FecVentaPago") & "," &  Eval("NroOrden") & "," &  Eval("TipoServ")%>'/>
                                       </ItemTemplate>
                                       </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="height: 2px;">
                </div>

    </div>
    
    </asp:Panel> 

             

</asp:Content>
