<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maestros.aspx.cs" Inherits="Act11.Maestros.Maestros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Página Maestros</h1>
    <asp:TextBox ID="txtMatricula" runat="server" placeholder="Matricula"></asp:TextBox>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
    <asp:TextBox ID="txtPaterno" runat="server" placeholder="Paterno"></asp:TextBox>
    <asp:TextBox ID="txtMaterno" runat="server" placeholder="Materno"></asp:TextBox>
    <asp:TextBox ID="txtRfc" runat="server" placeholder="Rfc"></asp:TextBox>
    <asp:TextBox ID="txtCurp" runat="server" placeholder="curp"></asp:TextBox>
    <asp:TextBox ID="txtSexo" runat="server" placeholder="Sexo"></asp:TextBox>

    <asp:DropDownList ID="ddEstados" runat="server" DataSourceID="SqlDataSourceEstados" DataTextField="estado" DataValueField="cve_estado" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_estado, estado FROM Estados"></asp:SqlDataSource>

    <asp:DropDownList ID="ddMunicipios" runat="server" DataSourceID="SqlDataSourceMunicipios" DataTextField="municipio" DataValueField="cve_municipio" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceMunicipios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_municipio, municipio FROM Municipios WHERE (cve_estado = @cveEstado)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddEstados" PropertyName="SelectedValue" Name="cveEstado"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:DropDownList ID="ddLocalidades" runat="server" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="cve_localidad"></asp:DropDownList>

    <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_localidad, localidad FROM Localidades WHERE (cve_estado = @cveEstado) AND (cve_municipio = @cveMunicipio)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddEstados" PropertyName="SelectedValue" Name="cveEstado"></asp:ControlParameter>
            <asp:ControlParameter ControlID="ddMunicipios" PropertyName="SelectedValue" Name="cveMunicipio"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="btnInsertar_Click" />
    <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
    <asp:Button ID="btneliminar" runat="server" Text="Eliminar" OnClick="btneliminar_Click" />

    <asp:GridView ID="GVMaestros" runat="server" AutoGenerateColumns="False" DataKeyNames="matricula" DataSourceID="SqlDataSourceMaestros" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVMaestros_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            <asp:BoundField DataField="matricula" HeaderText="matricula" ReadOnly="True" SortExpression="matricula"></asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre"></asp:BoundField>
            <asp:BoundField DataField="paterno" HeaderText="paterno" SortExpression="paterno"></asp:BoundField>
            <asp:BoundField DataField="materno" HeaderText="materno" SortExpression="materno"></asp:BoundField>
            <asp:BoundField DataField="curp" HeaderText="curp" SortExpression="curp"></asp:BoundField>
            <asp:BoundField DataField="rfc" HeaderText="rfc" SortExpression="rfc"></asp:BoundField>
            <asp:BoundField DataField="sexo" HeaderText="sexo" SortExpression="sexo"></asp:BoundField>
            <asp:BoundField DataField="estado" HeaderText="estado" SortExpression="estado"></asp:BoundField>
            <asp:BoundField DataField="municipio" HeaderText="municipio" SortExpression="municipio"></asp:BoundField>
            <asp:BoundField DataField="localidad" HeaderText="localidad" SortExpression="localidad"></asp:BoundField>
            <asp:BoundField DataField="latitud" HeaderText="latitud" SortExpression="latitud"></asp:BoundField>
            <asp:BoundField DataField="longitud" HeaderText="longitud" SortExpression="longitud"></asp:BoundField>
            <asp:BoundField DataField="rol" HeaderText="rol" SortExpression="rol"></asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#999999"></EditRowStyle>

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceMaestros" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Usuarios.matricula, Usuarios.nombre, Usuarios.paterno, Usuarios.materno, Usuarios.curp, Usuarios.rfc, Usuarios.sexo, Estados.estado, Municipios.municipio, Localidades.localidad, Localidades.latitud, Localidades.longitud, Roles.rol FROM Usuarios INNER JOIN Estados ON Usuarios.cve_estado = Estados.cve_estado INNER JOIN Municipios ON Usuarios.cve_municipio = Municipios.cve_municipio AND Estados.cve_estado = Municipios.cve_estado INNER JOIN Localidades ON Usuarios.cve_localidad = Localidades.cve_localidad AND Estados.cve_estado = Localidades.cve_estado AND Municipios.cve_municipio = Localidades.cve_municipio INNER JOIN Roles ON Usuarios.idRol = Roles.idRol WHERE (Usuarios.idRol = 2)"></asp:SqlDataSource>
    <br />

    <map:GoogleMap Id="GoogleMaps1" runat="server" MapType="Hybrid" Zoom="16" Latitude="19.9798047" Longitude="-98.6853093" CssClass="Map"></map:GoogleMap>
</asp:Content>
