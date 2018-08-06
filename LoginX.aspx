<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginX.aspx.vb" Inherits="WEB_Cotizador.LoginX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cotizadores Finagil</title>
</head>
<body background="IMG/fondoconteblanco.png" >
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <table style="width: 100%">
            <tr align =center>
                <td>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Img/LOGO FINAGIL.JPG" Height="204px" Width="434px" /><br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="COTIZADORES" Font-Names="Arial,Verdana,Geneva,sans-serif" ForeColor="#FF6401"></asp:Label><br />
                    <br />
                    <br />
                    <asp:Label ID="LabelUser" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6401"
                        Text="Usuario"></asp:Label><br />
                    <asp:TextBox ID="Txtuser" runat="server" Width="150px"></asp:TextBox><br />
                    <asp:Label ID="LabelPass" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6401"
                        Text="Contraseña"></asp:Label><br />
                    <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" Width="150px"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="BtnAceptar" runat="server" BackColor="#FF6401" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Medium" ForeColor="White" Height="30px" Text="Entrar" Width="136px" /></td>                
            </tr>
            <tr align =center>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="XX-Large"
                        ForeColor="#FF6401" Width="640px"></asp:Label></td>                
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
