<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MantEmail.aspx.vb" Inherits="NOVDOC.MantEmail" %>
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

  <asp:Panel ID="pnlFormulario" runat="server" Width="100%" DefaultButton="btnBuscar">
   
    <div class= "JER_CONTE_body" id="main" style="overflow:auto;">
                <table id="tTitulo" border="0" width="100%">
                    <tr>
                        <td class="stlTituloPagina">
                             Mantenimiento de Correos notificados por alerta de Provisión de Documentos</td>
                    </tr>
                    <tr>
                        <td class="LineaAzul"></td>
                    </tr>
                </table>
                <div style="height: 2px;">
                </div>
                     <asp:UpdatePanel ID="upCriterios" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                              <table border="0" class="st_Panel" >
                                    <tr>
                                        <td class="st_SubTitulo02" colspan="2">
                                            &nbsp;Criterios de Búsqueda
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width:35%;">
                                             <table>
                                               <tr> 
                                                 <td  align="center" style="  height: 20px;width: 80px;" class="EstiloDorado"> 
                                                     &nbsp;
                                                 </td>                                  
                                                 <td style=" text-align:right;"> 
                                                       <telerik:RadTextBox ID="RadTxtOrden" Width="200px" runat="server"  Visible="false">
                                                       </telerik:RadTextBox>
                                                 </td>     
                                               </tr>
                                             </table> 
                                        </td> 
                                        <td align="left" style="width: 50%; text-align:left;" valign="top">
                                            <table>
                                              <tr> 
                                                 <td style="width: 90px; height: 20px;"> 
                                                      <asp:Button ID="btnBuscar" runat="server" SkinID="BotonEstandar" Text="Buscar"   ToolTip="Buscar" Visible="false" />
                                                 </td>                                  
                                              </tr>
                                             </table> 
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
                            <div style="height: 2px">
                            </div>
                            <div style="vertical-align: top;overflow: auto; left: 0px; width: 100%; height: 500px"
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
                                       <telerik:GridBoundColumn HeaderText="Nombre de Contacto"   ItemStyle-Font-Size="8pt" DataField="nomfunc" UniqueName="nomfunc"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderText="Email de Contacto"   ItemStyle-Font-Size="8pt" DataField="empfunc" UniqueName="empfunc"    AllowFiltering="false" CurrentFilterFunction="Contains" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                       <telerik:GridTemplateColumn Visible="false" HeaderText="Referencia"  UniqueName="Referencia" AllowFiltering="false"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="#B94A08" HeaderStyle-Font-Bold="true"> 
                                        <ItemTemplate>
                                          <asp:ImageButton ID="ImgDatos" runat="server" ImageUrl="~/App_Themes/Imagenes/Validacion01.gif" ToolTip ="Asignar datos adicionales" CommandName="AgregarReferencia" CommandArgument='<%# Eval("Coddoc")  & "," &  Eval("nomfunc")  & "," &  Eval("empfunc")%>'/>
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
