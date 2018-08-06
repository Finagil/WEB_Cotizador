<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="PaginaMasterX.Master" CodeBehind="CotizadorAF.aspx.vb" Inherits="WEB_Cotizador.WebFormAF" 
    title="Cotizador" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
    
    <%@ MasterType virtualpath="~/Cotizadores/PaginaMasterX.Master" %>
    
 <script runat="server">

    

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%">
        <tr>
        <td style="width: 14.28%" align=center>
            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                            Text="Monto de las Facturas"></asp:Label>
                        <br />
                        <asp:TextBox ID="TxtFact" runat="server"
                                Font-Bold="True" MaxLength="10" Width="100px" style="margin-left: 0px">1,000,000.00</asp:TextBox>
                </td>
            </td>
            <td align=center  style="width: 7.14%">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                    Text="Iva de Capital"></asp:Label>
                <br />
                <asp:CheckBox ID="ckIVA" runat="server" AutoPostBack="True" />
                <td align=center  style="width: 14.28%">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                    Text="Tipo Persona"></asp:Label>
                <br />
                <asp:DropDownList ID="CmbTipoPers" runat="server" Width="156px">
                    <asp:ListItem Value="E">Fisica con Act. Empr.</asp:ListItem>
                    <asp:ListItem Value="M">Persona Moral</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td align=center  style="width: 10.28%">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                    Text="Plazo"></asp:Label>
                        <br />
                <asp:DropDownList ID="CmbPlazo" runat="server" Width="98px" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="12">12 Meses</asp:ListItem>
                    <asp:ListItem Value="24">24 Meses</asp:ListItem>
                    <asp:ListItem Value="36">36 Meses</asp:ListItem>
                </asp:DropDownList>
                        <br />
                        </td>
            <td style="width: 14.28%" align=center>
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                            Text="Valor Recidual%"></asp:Label>
                        <br />
                        <asp:TextBox ID="TxtOpcion" runat="server"
                                Font-Bold="True" MaxLength="10" Width="36px" style="margin-left: 0px" Enabled="False">20.0</asp:TextBox>
                <br />
                </td>
                    <td style="width: 10.28%" align=center>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                    Text="Tasa anual %"></asp:Label>
                        <br />
                <asp:TextBox ID="TxtTasa" runat="server" Enabled="False" Font-Bold="True" MaxLength="10"
                    ReadOnly="True" Width="38px">16.0</asp:TextBox><br />
                        </td>
                    <td style="width: 7.14%" align=center>
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6600"
                            Text="% Comisión"></asp:Label>
                        <br />
                        <asp:TextBox ID="TxtComision" runat="server" Enabled="False"
                                Font-Bold="True" MaxLength="10" Width="100px">3.00</asp:TextBox>
                        </td>
                                 <td style="width: 14.28%" align=center>
                                 </td>
        </tr>
        <tr>
            
            <td align=center colspan=8>
                
                <asp:Label ID="LbError" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red"
                    Text="Error en el monto financiado." Visible="False"></asp:Label>
                <br />
                <cc1:BotonEnviar ID="BotonEnviar1" runat="server" BackColor="#FF6600" Font-Bold="True"
                    ForeColor="White" Text="Calcular Crédito" TextoEnviando="Calculando..." Width="136px" /><br />
                <br />
                </td>
            
        </tr>
        <tr>
            <td align=center colspan=8>
                <cc1:BotonEnviar ID="BotonImp" runat="server" BackColor="#FF6600" Font-Bold="True"
                    ForeColor="White" Text="Imprimir" TextoEnviando="Imprimiendo..." Width="136px" Visible="False" />
                <br />
                <asp:GridView ID="GridAmortizaciones" runat="server" CellPadding="4" Font-Names="Arial"
                    ForeColor="#333333" GridLines="None" PageSize="50" Width="80%" ShowFooter="True" EnableModelValidation="True">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
            </td>
            
        </tr>
    </table>
</asp:Content>
