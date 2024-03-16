<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" UICulture="es" Culture="es-MX" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="Act11.RegistroVehicular.Vehiculos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <h1>Registro vehicular</h1>   <!---Solo puede acceder el ro de directivo, los demás no pueden---->
    <br>
    <span class="label label-primary">Placañ</span>
    <asp:TextBox ID="txtPlaca" runat="server" placeholder="Placa"></asp:TextBox>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />

    <asp:DropDownList ID="ddMarca" runat="server" DataSourceID="SqlDataSourceMarcas" DataTextField="marca" DataValueField="cveMarca" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceMarcas" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cveMarca, marca FROM Marcas"></asp:SqlDataSource>
    
    
    <asp:DropDownList ID="ddSubMarca" runat="server" DataSourceID="SqlDataSourceSubMarcas" DataTextField="submarca" DataValueField="cveSubmarca" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceSubMarcas" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cveSubmarca, submarca FROM SubMarcas WHERE (cveMarca = @cveMarca)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddMarca" PropertyName="SelectedValue" Name="cveMarca"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <br>

    <asp:DropDownList ID="ddModelo" runat="server" DataSourceID="SqlDataSourceModelos" DataTextField="modelo" DataValueField="cveModelo"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceModelos" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cveModelo, modelo FROM Modelos"></asp:SqlDataSource>
    
    <asp:DropDownList ID="ddColor" runat="server" DataSourceID="SqlDataSourceColores" DataTextField="colores" DataValueField="cveColor"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceColores" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cveColor, colores FROM Colores"></asp:SqlDataSource>
    
    <asp:DropDownList ID="ddCombustible" runat="server" DataSourceID="SqlDataSourceCombustibles" DataTextField="combustible" DataValueField="cveCombustible"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceCombustibles" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cveCombustible, combustible FROM Combustibles"></asp:SqlDataSource>
    <br>


    <asp:TextBox ID="txtNumSerie" runat="server" placeholder="Número de serie"></asp:TextBox>
    <asp:TextBox ID="txtNumMotor" runat="server" placeholder="Número de motor"></asp:TextBox>
    <br>
    <asp:DropDownList ID="ddEstados" runat="server" DataSourceID="SqlDataSourceEstados" DataTextField="estado" DataValueField="cve_estado" AutoPostBack="True"></asp:DropDownList>

    <asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_estado, estado FROM Estados"></asp:SqlDataSource>
    
    
    <asp:DropDownList ID="ddMunicipios" runat="server" DataSourceID="SqlDataSourceMunicipios" DataTextField="municipio" DataValueField="cve_municipio" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceMunicipios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_municipio, municipio FROM Municipios WHERE (cve_estado = @cve_estado) ORDER BY municipio">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddEstados" PropertyName="SelectedValue" Name="cve_estado"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:DropDownList ID="ddLocalidades" runat="server" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="cve_localidad" OnSelectedIndexChanged="ddLocalidades_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_localidad, localidad,latitud,longitud FROM Localidades WHERE (cve_estado = @cve_estado) AND (cve_municipio = @cve_municipio) ORDER BY localidad">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddEstados" PropertyName="SelectedValue" Name="cve_estado"></asp:ControlParameter>
            <asp:ControlParameter ControlID="ddMunicipios" PropertyName="SelectedValue" Name="cve_municipio"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    
    <br>
    <asp:TextBox ID="txtMatricula" runat="server" placeholder="Matricula"></asp:TextBox>
    <br>
    <!---Falta implementar los radioButtons y un textArea---->
    <asp:RadioButton ID="RBDuenoActual" runat="server" GroupName="grpDueno" Text="Dueño actual" />
    <br>
    <asp:RadioButton ID="RBDuenoAnterior" runat="server" GroupName="grpDueno" Text="Dueño anterior" />
    <br>
    
    <!---botones de acciones---->
    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
    <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
    <asp:Button ID="bntEliminar" runat="server" Text="Eliminar" OnClick="bntEliminar_Click" />
    <br>
    <!---Datos en específico del dueño---->
    <asp:Label ID="Label1" runat="server" Text="Datos del dueño:"></asp:Label>
    <asp:DataList ID="DLInfoDueno" runat="server" DataKeyField="matricula" DataSourceID="SqlDataSourceInfoDuenos" OnItemDataBound="DLInfoDueno_ItemDataBound">
        <ItemTemplate>
            matricula:
            <asp:Label Text='<%# Eval("matricula") %>' runat="server" ID="matriculaLabel" /><br />
            nombre_completo:
            <asp:Label Text='<%# Eval("nombre_completo") %>' runat="server" ID="nombre_completoLabel" /><br />
            curp:
            <asp:Label Text='<%# Eval("curp") %>' runat="server" ID="curpLabel" /><br />
            rfc:
            <asp:Label Text='<%# Eval("rfc") %>' runat="server" ID="rfcLabel" /><br />
            sexo:
            <asp:Label Text='<%# Eval("sexo") %>' runat="server" ID="sexoLabel" /><br />
            estado:
            <asp:Label Text='<%# Eval("estado") %>' runat="server" ID="estadoLabel" /><br />
            municipio:
            <asp:Label Text='<%# Eval("municipio") %>' runat="server" ID="municipioLabel" /><br />
            localidad:
            <asp:Label Text='<%# Eval("localidad") %>' runat="server" ID="localidadLabel" /><br />
            latitud:
            <asp:Label Text='<%# Eval("latitud") %>' runat="server" ID="latitudLabel" /><br />
            longitud:
            <asp:Label Text='<%# Eval("longitud") %>' runat="server" ID="longitudLabel" /><br />
            <br />
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceInfoDuenos" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Usuarios.matricula, Usuarios.nombre + ' ' + Usuarios.paterno + ' ' + Usuarios.materno AS nombre_completo, Usuarios.curp, Usuarios.rfc, Usuarios.sexo, Estados.estado, Municipios.municipio, Localidades.localidad, Localidades.latitud, Localidades.longitud FROM Usuarios INNER JOIN Estados ON Usuarios.cve_estado = Estados.cve_estado INNER JOIN Municipios ON Usuarios.cve_municipio = Municipios.cve_municipio AND Estados.cve_estado = Municipios.cve_estado INNER JOIN Localidades ON Usuarios.cve_localidad = Localidades.cve_localidad AND Estados.cve_estado = Localidades.cve_estado AND Municipios.cve_municipio = Localidades.cve_municipio WHERE (Usuarios.matricula = @matricula)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GVDuenos" PropertyName="SelectedValue" Name="matricula"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <br>
    
    <!---GVVehiculos GVDueños---->
    <asp:Label ID="Label2" runat="server" Text="GRIDVIEW vehiculos:"></asp:Label>
    <asp:GridView ID="GVVehiculos" runat="server" AutoGenerateColumns="False" DataKeyNames="placa,cveMarca,cveSubmarca,cveModelo,cveCombustible,cve_estado,numSerie" DataSourceID="SqlDataSourceVehiculos" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVVehiculos_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            <asp:BoundField DataField="placa" HeaderText="placa" ReadOnly="True" SortExpression="placa"></asp:BoundField>
            <asp:BoundField DataField="numSerie" HeaderText="numSerie" SortExpression="numSerie"></asp:BoundField>
            <asp:BoundField DataField="numMotor" HeaderText="numMotor" SortExpression="numMotor"></asp:BoundField>
            <asp:BoundField DataField="marca" HeaderText="marca" SortExpression="marca"></asp:BoundField>
            <asp:BoundField DataField="submarca" HeaderText="submarca" SortExpression="submarca"></asp:BoundField>
            <asp:BoundField DataField="modelo" HeaderText="modelo" SortExpression="modelo"></asp:BoundField>
            <asp:BoundField DataField="colores" HeaderText="colores" SortExpression="colores"></asp:BoundField>
            <asp:BoundField DataField="combustible" HeaderText="combustible" SortExpression="combustible"></asp:BoundField>
            <asp:BoundField DataField="estado" HeaderText="estado" SortExpression="estado"></asp:BoundField>
            <asp:BoundField DataField="municipio" HeaderText="municipio" SortExpression="municipio"></asp:BoundField>
            <asp:BoundField DataField="localidad" HeaderText="localidad" SortExpression="localidad"></asp:BoundField>
            <asp:BoundField DataField="matricula" HeaderText="matricula" SortExpression="matricula"></asp:BoundField>
            <asp:BoundField DataField="latitud" HeaderText="latitud" SortExpression="latitud"></asp:BoundField>
            <asp:BoundField DataField="longitud" HeaderText="longitud" SortExpression="longitud"></asp:BoundField>
            <asp:BoundField DataField="cveMarca" HeaderText="cveMarca" ReadOnly="True" InsertVisible="False" SortExpression="cveMarca" ></asp:BoundField>
            <asp:BoundField DataField="cveSubmarca" HeaderText="cveSubmarca" ReadOnly="True" InsertVisible="False" SortExpression="cveSubmarca" ></asp:BoundField>
            <asp:BoundField DataField="cveModelo" HeaderText="cveModelo" ReadOnly="True" SortExpression="cveModelo" ></asp:BoundField>
            <asp:BoundField DataField="cveColor" HeaderText="cveColor" SortExpression="cveColor" ></asp:BoundField>
            <asp:BoundField DataField="cveCombustible" HeaderText="cveCombustible" ReadOnly="True" SortExpression="cveCombustible" ></asp:BoundField>
            <asp:BoundField DataField="cve_estado" HeaderText="cve_estado" ReadOnly="True" SortExpression="cve_estado"></asp:BoundField>
            <asp:BoundField DataField="cve_municipio" HeaderText="cve_municipio" SortExpression="cve_municipio"></asp:BoundField>
            <asp:BoundField DataField="cve_localidad" HeaderText="cve_localidad" SortExpression="cve_localidad"></asp:BoundField>
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

    <asp:SqlDataSource runat="server" ID="SqlDataSourceVehiculos" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Vehiculos.placa, Vehiculos.numSerie, Vehiculos.numMotor, Marcas.marca, SubMarcas.submarca, Modelos.modelo, Colores.colores, Combustibles.combustible, Estados.estado, Municipios.municipio, Localidades.localidad, Vehiculos.matricula, Localidades.latitud, Localidades.longitud, Marcas.cveMarca, SubMarcas.cveSubmarca, Modelos.cveModelo, Colores.cveColor, Combustibles.cveCombustible, Estados.cve_estado, Municipios.cve_municipio, Localidades.cve_localidad FROM Vehiculos INNER JOIN Marcas ON Vehiculos.cveMarca = Marcas.cveMarca INNER JOIN SubMarcas ON Vehiculos.cveSubmarca = SubMarcas.cveSubmarca INNER JOIN Modelos ON Vehiculos.cveModelo = Modelos.cveModelo INNER JOIN Colores ON Vehiculos.cveColor = Colores.cveColor INNER JOIN Combustibles ON Vehiculos.cveCombustible = Combustibles.cveCombustible INNER JOIN Estados ON Vehiculos.cve_estado = Estados.cve_estado INNER JOIN Municipios ON Estados.cve_estado = Municipios.cve_estado AND Vehiculos.cve_municipio = Municipios.cve_municipio INNER JOIN Localidades ON Estados.cve_estado = Localidades.cve_estado AND Vehiculos.cve_localidad = Localidades.cve_localidad AND Municipios.cve_municipio = Localidades.cve_municipio"></asp:SqlDataSource>
    
    
    
    <asp:Label ID="Label3" runat="server" Text="GRIDVIEW dueños:"></asp:Label>
    <asp:GridView ID="GVDuenos" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceDuenos" DataKeyNames="matricula" OnSelectedIndexChanged="GVDuenos_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True"></asp:CommandField>
            <asp:BoundField DataField="matricula" HeaderText="matricula" SortExpression="matricula"></asp:BoundField>
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
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status"></asp:BoundField>
            <asp:BoundField DataField="numSerie" HeaderText="numSerie" SortExpression="numSerie"></asp:BoundField>
            <asp:BoundField DataField="cve_estado" HeaderText="cve_estado" SortExpression="cve_estado"></asp:BoundField>
            <asp:BoundField DataField="cve_municipio" HeaderText="cve_municipio" SortExpression="cve_municipio"></asp:BoundField>
            <asp:BoundField DataField="cve_localidad" HeaderText="cve_localidad" SortExpression="cve_localidad"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceDuenos" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT matricula, nombre, paterno, materno, curp, rfc, sexo, estado, municipio, localidad, latitud, longitud, status, numSerie, cve_estado, cve_municipio, cve_localidad FROM GVDuenos"></asp:SqlDataSource>
    <br>
    <!---Mapa de google---->
    <map:googlemap Id="GoogleMaps1" runat="server" MapType="Hybrid" Zoom="16" Latitude="19.9798047" Longitude="-98.6853093" CssClass="Map"></map:googlemap>
</asp:Content>
