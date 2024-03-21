using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Act11.RegistroVehicular
{
    public partial class Vehiculos : System.Web.UI.Page
    {
        WSCS.WSCSSoapClient WS = new WSCS.WSCSSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentEncoding = System.Text.Encoding.UTF8;
           
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            //aqui va el procedimiento almacenado, tiene que encriptar la matricula y numserie y motor
            string connectionString = "Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa";
            
            //SqlConnection conexion = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertarActualizarDatosVehiculoDuenos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros si es necesario
                    command.Parameters.AddWithValue("@Placa", txtPlaca.Text);
                    command.Parameters.AddWithValue("@NumSerie",WS.Encriptar(txtNumSerie.Text));//cambiar a varchar encriptar
                    command.Parameters.AddWithValue("@NumMotor",WS.Encriptar(txtNumMotor.Text));//varchar encriptar
                    command.Parameters.AddWithValue("@CveMarca", int.Parse(ddMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveSubmarca", int.Parse(ddSubMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveModelo", int.Parse(ddModelo.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveColor", int.Parse(ddColor.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveCombustible", int.Parse(ddCombustible.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveEstado", int.Parse(ddEstados.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveMunicipio", int.Parse(ddMunicipios.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveLocalidad", int.Parse(ddLocalidades.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@Matricula",WS.Encriptar(txtMatricula.Text));//String aqui se tiene que encriptar

                    if (RBDuenoActual.Checked)
                    {
                        command.Parameters.AddWithValue("@IdStatusDuenos",1);//entero

                    }
                    if (RBDuenoAnterior.Checked)
                    {
                        command.Parameters.AddWithValue("@IdStatusDuenos", 2);//entero

                    }
                    /* @Placa NVARCHAR(50),@NumSerie INT,@NumMotor INT,@CveMarca INT,@CveSubmarca INT,@CveModelo INT,@CveColor INT,
                    @CveCombustible INT,@CveEstado INT,@CveMunicipio INT,@CveLocalidad INT,@Matricula NVARCHAR(50),@IdStatusDuenos INT
                     */
                    // Ejecutar el procedimiento almacenado
                    command.ExecuteNonQuery();
                }
            }

            //Console.WriteLine("Procedimiento almacenado ejecutado con éxito.");
            //Console.ReadLine();
            GVVehiculos.DataBind();
            GVDuenos.DataBind();
        }

        protected void GVVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtPlaca.Text = GVVehiculos.SelectedRow.Cells[1].Text.ToString();
            txtNumSerie.Text =WS.Desencriptar(GVVehiculos.SelectedRow.Cells[2].Text.ToString());
            txtNumMotor.Text = WS.Desencriptar(GVVehiculos.SelectedRow.Cells[3].Text.ToString());

            ddMarca.SelectedValue = GVVehiculos.SelectedRow.Cells[15].Text.ToString();
            ddSubMarca.DataBind();
            ddSubMarca.SelectedValue = GVVehiculos.SelectedRow.Cells[16].Text.ToString();
            
            ddModelo.SelectedValue = GVVehiculos.SelectedRow.Cells[17].Text.ToString();
            ddColor.SelectedValue = GVVehiculos.SelectedRow.Cells[18].Text.ToString();
            ddCombustible.SelectedValue = GVVehiculos.SelectedRow.Cells[19].Text.ToString();
            ddEstados.SelectedValue = GVVehiculos.SelectedRow.Cells[20].Text.ToString();
            ddMunicipios.DataBind();
            ddMunicipios.SelectedValue = GVVehiculos.SelectedRow.Cells[21].Text.ToString();
            ddLocalidades.DataBind();
            ddLocalidades.SelectedValue = GVVehiculos.SelectedRow.Cells[22].Text.ToString();
            txtMatricula.Text = WS.Desencriptar(GVVehiculos.SelectedRow.Cells[12].Text.ToString());
            GoogleMaps1.Latitude = double.Parse(GVVehiculos.SelectedRow.Cells[13].Text.ToString());
            GoogleMaps1.Longitude = double.Parse(GVVehiculos.SelectedRow.Cells[14].Text.ToString());

            //mostrar los registros con el mismo numSerie y mostrar los dueños del coche
            //mostrar todos los vehiculos con numSerie
            string numSerie = GVVehiculos.SelectedRow.Cells[2].Text.ToString(); // revisar si se descencripta o no

            // Construir la consulta SQL para buscar el registro específico
            string query = "SELECT Vehiculos.placa, Vehiculos.numSerie, Vehiculos.numMotor, Marcas.marca, SubMarcas.submarca, Modelos.modelo, Colores.colores, Combustibles.combustible, Estados.estado, Municipios.municipio, Localidades.localidad, Vehiculos.matricula, Localidades.latitud, Localidades.longitud, Marcas.cveMarca, SubMarcas.cveSubmarca, Modelos.cveModelo, Colores.cveColor, Combustibles.cveCombustible, Estados.cve_estado, Municipios.cve_municipio, Localidades.cve_localidad FROM Vehiculos INNER JOIN Marcas ON Vehiculos.cveMarca = Marcas.cveMarca INNER JOIN SubMarcas ON Vehiculos.cveSubmarca = SubMarcas.cveSubmarca INNER JOIN Modelos ON Vehiculos.cveModelo = Modelos.cveModelo INNER JOIN Colores ON Vehiculos.cveColor = Colores.cveColor INNER JOIN Combustibles ON Vehiculos.cveCombustible = Combustibles.cveCombustible INNER JOIN Estados ON Vehiculos.cve_estado = Estados.cve_estado INNER JOIN Municipios ON Estados.cve_estado = Municipios.cve_estado AND Vehiculos.cve_municipio = Municipios.cve_municipio INNER JOIN Localidades ON Estados.cve_estado = Localidades.cve_estado AND Vehiculos.cve_localidad = Localidades.cve_localidad AND Municipios.cve_municipio = Localidades.cve_municipio WHERE Vehiculos.numSerie = @numSerie";

            // Establecer el parámetro de la placa en la consulta SQL
            SqlDataSourceVehiculos.SelectCommand = query;
            SqlDataSourceVehiculos.SelectParameters.Clear();
            SqlDataSourceVehiculos.SelectParameters.Add("numSerie",numSerie);

            // Actualizar el GridView con el resultado de la consulta
            GVVehiculos.DataBind();

            //mostrar todos los dueños de x vehiculo por el numSerie
            // Construir la consulta SQL para buscar el registro específico
            string query1 = @"SELECT DISTINCT dbo.Usuarios.matricula, dbo.Usuarios.nombre, dbo.Usuarios.paterno, dbo.Usuarios.materno, dbo.Usuarios.curp, dbo.Usuarios.rfc, dbo.Usuarios.sexo, dbo.Estados.estado, dbo.Municipios.municipio, dbo.Localidades.localidad, dbo.Localidades.latitud, dbo.Localidades.longitud, 
             dbo.StatusDuenos.status, dbo.Vehiculos.numSerie, dbo.Estados.cve_estado, dbo.Municipios.cve_municipio, dbo.Localidades.cve_localidad
FROM   dbo.Vehiculos INNER JOIN
             dbo.Usuarios ON dbo.Vehiculos.matricula = dbo.Usuarios.matricula INNER JOIN
             dbo.Estados ON dbo.Vehiculos.cve_estado = dbo.Estados.cve_estado INNER JOIN
             dbo.Municipios ON dbo.Estados.cve_estado = dbo.Municipios.cve_estado AND dbo.Usuarios.cve_municipio = dbo.Municipios.cve_municipio INNER JOIN
             dbo.Localidades ON dbo.Estados.cve_estado = dbo.Localidades.cve_estado AND dbo.Usuarios.cve_localidad = dbo.Localidades.cve_localidad AND dbo.Municipios.cve_municipio = dbo.Localidades.cve_municipio INNER JOIN
             dbo.Duenos ON dbo.Usuarios.matricula = dbo.Duenos.matricula INNER JOIN
             dbo.StatusDuenos ON dbo.Duenos.IdStatusDuenos = dbo.StatusDuenos.IdStatusDuenos
WHERE (dbo.Vehiculos.numSerie = @numSerie)";

            // Establecer el parámetro de la placa en la consulta SQL
            SqlDataSourceDuenos.SelectCommand = query1;
            SqlDataSourceDuenos.SelectParameters.Clear();
            SqlDataSourceDuenos.SelectParameters.Add("numSerie", numSerie);

            // Actualizar el GridView con el resultado de la consulta
            GVDuenos.DataBind();


        }

        protected void ddLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            string latitud = ddLocalidades.SelectedItem.Attributes["latitud"];
            string longitud = ddLocalidades.SelectedItem.Attributes["longitud"];
            //Console.WriteLine(latitud);
            /*
            GoogleMaps1.Latitude = double.Parse(latitud);//están llegando vacios los parametros revisar porque
            GoogleMaps1.Longitude = double.Parse(longitud);//si lo borro no olvidar el autopstback
             */

        }

        protected void GVDuenos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DLInfoDueno.Enabled = true;
            DLInfoDueno.Visible = true;
            GoogleMaps1.Latitude = double.Parse(GVDuenos.SelectedRow.Cells[11].Text.ToString());
            GoogleMaps1.Longitude = double.Parse(GVDuenos.SelectedRow.Cells[12].Text.ToString());
            //status dueno actual?
            string status = GVDuenos.SelectedRow.Cells[13].Text.Trim();
            
            if (status.Contains("actual"))
            {
                RBDuenoActual.Checked = true;
                RBDuenoAnterior.Checked = false; // Desmarcar el otro RadioButton
            }
            if (status.Contains("anterior"))
            {
                RBDuenoAnterior.Checked = true;
                RBDuenoActual.Checked = false; // Desmarcar el otro RadioButton
            }

            //GVDuenos.DataBind(); 
            // tenemos que poner en el gridWiew de vehiculos los coches que posee actualmente y mostrar solo ese dueño
            string matriculaABuscar = GVDuenos.SelectedRow.Cells[1].Text.ToString(); // revisar si se descencripta o no
            //matriculaABuscar = WS.Encriptar(matriculaABuscar);

            // Construir la consulta SQL para buscar el registro específico
            string query = @"SELECT dbo.Vehiculos.placa, dbo.Vehiculos.numSerie, dbo.Vehiculos.numMotor, dbo.Marcas.marca, dbo.SubMarcas.submarca, dbo.Modelos.modelo, dbo.Colores.colores, dbo.Combustibles.combustible, dbo.Estados.estado, dbo.Municipios.municipio, dbo.Localidades.localidad, 
             dbo.Vehiculos.matricula, dbo.Localidades.latitud, dbo.Localidades.longitud, dbo.Marcas.cveMarca, dbo.SubMarcas.cveSubmarca, dbo.Modelos.cveModelo, dbo.Colores.cveColor, dbo.Combustibles.cveCombustible, dbo.Estados.cve_estado, dbo.Municipios.cve_municipio, 
             dbo.Localidades.cve_localidad
FROM   dbo.Vehiculos INNER JOIN
             dbo.Marcas ON dbo.Vehiculos.cveMarca = dbo.Marcas.cveMarca INNER JOIN
             dbo.SubMarcas ON dbo.Vehiculos.cveSubmarca = dbo.SubMarcas.cveSubmarca INNER JOIN
             dbo.Modelos ON dbo.Vehiculos.cveModelo = dbo.Modelos.cveModelo INNER JOIN
             dbo.Colores ON dbo.Vehiculos.cveColor = dbo.Colores.cveColor INNER JOIN
             dbo.Combustibles ON dbo.Vehiculos.cveCombustible = dbo.Combustibles.cveCombustible INNER JOIN
             dbo.Estados ON dbo.Vehiculos.cve_estado = dbo.Estados.cve_estado INNER JOIN
             dbo.Municipios ON dbo.Estados.cve_estado = dbo.Municipios.cve_estado AND dbo.Vehiculos.cve_municipio = dbo.Municipios.cve_municipio INNER JOIN
             dbo.Localidades ON dbo.Estados.cve_estado = dbo.Localidades.cve_estado AND dbo.Vehiculos.cve_localidad = dbo.Localidades.cve_localidad AND dbo.Municipios.cve_municipio = dbo.Localidades.cve_municipio INNER JOIN
             dbo.Duenos ON dbo.Vehiculos.numSerie = dbo.Duenos.numSerie INNER JOIN
             dbo.StatusDuenos ON dbo.Duenos.IdStatusDuenos = dbo.StatusDuenos.IdStatusDuenos
WHERE(dbo.Vehiculos.matricula = @matricula) AND(dbo.Duenos.IdStatusDuenos = 1)";

            // Establecer el parámetro de la placa en la consulta SQL
            SqlDataSourceVehiculos.SelectCommand = query;
            SqlDataSourceVehiculos.SelectParameters.Clear();
            SqlDataSourceVehiculos.SelectParameters.Add("matricula", matriculaABuscar);

            // Actualizar el GridView con el resultado de la consulta
            GVVehiculos.DataBind();

            //falta mostrar solo el dueño seleccionado al parecer cumple su función bien
            int selectedIndex = GVDuenos.SelectedIndex;

            // Iterar sobre cada fila del GridView
            for (int i = 0; i < GVDuenos.Rows.Count; i++)
            {
                // Verificar si es la fila seleccionada
                if (i == selectedIndex)
                {
                    // Mostrar la fila seleccionada
                    GVDuenos.Rows[i].Visible = true;
                }
                else
                {
                    // Ocultar las filas que no están seleccionadas
                    GVDuenos.Rows[i].Visible = false;
                }
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //modifica todos los valores de el registro pero la placa no porque es la pk
            //aqui va el procedimiento almacenado, tiene que encriptar la matricula y numserie y motor
            string connectionString = "Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa";

            //SqlConnection conexion = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ActualizarDatosVehiculoDuenos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros si es necesario
                    command.Parameters.AddWithValue("@Placa", txtPlaca.Text);
                    command.Parameters.AddWithValue("@NumSerie", WS.Encriptar(txtNumSerie.Text));//cambiar a varchar encriptar
                    command.Parameters.AddWithValue("@NumMotor", WS.Encriptar(txtNumMotor.Text));//varchar encriptar
                    command.Parameters.AddWithValue("@CveMarca", int.Parse(ddMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveSubmarca", int.Parse(ddSubMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveModelo", int.Parse(ddModelo.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveColor", int.Parse(ddColor.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveCombustible", int.Parse(ddCombustible.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveEstado", int.Parse(ddEstados.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveMunicipio", int.Parse(ddMunicipios.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveLocalidad", int.Parse(ddLocalidades.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@Matricula", WS.Encriptar(txtMatricula.Text));//String aqui se tiene que encriptar

                    if (RBDuenoActual.Checked)
                    {
                        command.Parameters.AddWithValue("@IdStatusDuenos", 1);//entero

                    }
                    if (RBDuenoAnterior.Checked)
                    {
                        command.Parameters.AddWithValue("@IdStatusDuenos", 2);//entero

                    }
                    /* @Placa NVARCHAR(50),@NumSerie INT,@NumMotor INT,@CveMarca INT,@CveSubmarca INT,@CveModelo INT,@CveColor INT,
                    @CveCombustible INT,@CveEstado INT,@CveMunicipio INT,@CveLocalidad INT,@Matricula NVARCHAR(50),@IdStatusDuenos INT
                     */
                    // Ejecutar el procedimiento almacenado
                    command.ExecuteNonQuery();
                }
            }

            //Console.WriteLine("Procedimiento almacenado ejecutado con éxito.");
            //Console.ReadLine();
            GVVehiculos.DataBind();
            GVDuenos.DataBind();
        }

        protected void bntEliminar_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("BorrarDatosVehiculoDuenos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros si es necesario
                    command.Parameters.AddWithValue("@NumSerie",WS.Encriptar(txtNumSerie.Text));//cambiar a varchar encriptar
                    
                    command.ExecuteNonQuery();
                }
            }

            //Console.WriteLine("Procedimiento almacenado ejecutado con éxito.");
            //Console.ReadLine();
            GVVehiculos.DataBind();
            GVDuenos.DataBind();

        }

        protected void DLInfoDueno_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtener los datos encriptados del DataList
                string matriculaEncriptada = DataBinder.Eval(e.Item.DataItem, "matricula").ToString();
                string nombre = GVDuenos.SelectedRow.Cells[2].Text.ToString();
                string paterno = GVDuenos.SelectedRow.Cells[3].Text.ToString();
                string materno = GVDuenos.SelectedRow.Cells[4].Text.ToString();

                nombre = WS.Desencriptar(nombre);
                paterno = WS.Desencriptar(paterno);
                materno = WS.Desencriptar(materno);

                string curpEncriptado = DataBinder.Eval(e.Item.DataItem, "curp").ToString();
                string rfcEncriptado = DataBinder.Eval(e.Item.DataItem, "rfc").ToString();
                string sexoEncriptado = DataBinder.Eval(e.Item.DataItem, "sexo").ToString();


                // y así sucesivamente para cada columna encriptada

                // Desencriptar los datos
                string matriculaDesencriptada = WS.Desencriptar(matriculaEncriptada); // Llama a tu método de desencriptación
                string curpDesencriptado = WS.Desencriptar(curpEncriptado);
                string rfcDesencriptado = WS.Desencriptar(rfcEncriptado);
                string sexoDesencriptado = WS.Desencriptar(sexoEncriptado);

                // y así sucesivamente para cada valor encriptado

                // Actualizar los controles dentro del DataList con los datos desencriptados
                ((Label)e.Item.FindControl("matriculaLabel")).Text = matriculaDesencriptada;
                ((Label)e.Item.FindControl("nombre_completoLabel")).Text = nombre + " " + paterno + " " + materno;
                ((Label)e.Item.FindControl("curpLabel")).Text = curpDesencriptado;
                ((Label)e.Item.FindControl("rfcLabel")).Text = rfcDesencriptado;
                ((Label)e.Item.FindControl("sexoLabel")).Text = sexoDesencriptado;

                // y así sucesivamente para cada control dentro del DataList
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string placaABuscar = txtPlaca.Text.Trim(); // Obtener la placa ingresada por el usuario

            // Construir la consulta SQL para buscar el registro específico
            string query = "SELECT Vehiculos.placa, Vehiculos.numSerie, Vehiculos.numMotor, Marcas.marca, SubMarcas.submarca, Modelos.modelo, Colores.colores, Combustibles.combustible, Estados.estado, Municipios.municipio, Localidades.localidad, Vehiculos.matricula, Localidades.latitud, Localidades.longitud, Marcas.cveMarca, SubMarcas.cveSubmarca, Modelos.cveModelo, Colores.cveColor, Combustibles.cveCombustible, Estados.cve_estado, Municipios.cve_municipio, Localidades.cve_localidad FROM Vehiculos INNER JOIN Marcas ON Vehiculos.cveMarca = Marcas.cveMarca INNER JOIN SubMarcas ON Vehiculos.cveSubmarca = SubMarcas.cveSubmarca INNER JOIN Modelos ON Vehiculos.cveModelo = Modelos.cveModelo INNER JOIN Colores ON Vehiculos.cveColor = Colores.cveColor INNER JOIN Combustibles ON Vehiculos.cveCombustible = Combustibles.cveCombustible INNER JOIN Estados ON Vehiculos.cve_estado = Estados.cve_estado INNER JOIN Municipios ON Estados.cve_estado = Municipios.cve_estado AND Vehiculos.cve_municipio = Municipios.cve_municipio INNER JOIN Localidades ON Estados.cve_estado = Localidades.cve_estado AND Vehiculos.cve_localidad = Localidades.cve_localidad AND Municipios.cve_municipio = Localidades.cve_municipio WHERE Vehiculos.placa = @placa";

            // Establecer el parámetro de la placa en la consulta SQL
            SqlDataSourceVehiculos.SelectCommand = query;
            SqlDataSourceVehiculos.SelectParameters.Clear();
            SqlDataSourceVehiculos.SelectParameters.Add("placa", placaABuscar);

            // Actualizar el GridView con el resultado de la consulta
            GVVehiculos.DataBind();

            GVVehiculos.SelectedIndex = 0;
            MostrarPropietarioActual();
            string numSerie = GVVehiculos.SelectedRow.Cells[2].Text.Trim();
        }

        protected void MostrarPropietarioActual()
        {
            string numSerie = GVVehiculos.SelectedRow.Cells[2].Text.Trim();

            // Construir la consulta SQL para buscar los dueños del vehículo con el número de serie especificado
            string query = @"SELECT dbo.Usuarios.matricula, dbo.Usuarios.nombre, dbo.Usuarios.paterno, dbo.Usuarios.materno, dbo.Usuarios.curp, dbo.Usuarios.rfc, dbo.Usuarios.sexo, dbo.Estados.estado, dbo.Municipios.municipio, dbo.Localidades.localidad, dbo.Localidades.latitud, dbo.Localidades.longitud, 
             dbo.StatusDuenos.status, dbo.Vehiculos.numSerie, dbo.Estados.cve_estado, dbo.Municipios.cve_municipio, dbo.Localidades.cve_localidad
             FROM   dbo.Vehiculos INNER JOIN
             dbo.Usuarios ON dbo.Vehiculos.matricula = dbo.Usuarios.matricula INNER JOIN
             dbo.Estados ON dbo.Vehiculos.cve_estado = dbo.Estados.cve_estado INNER JOIN
             dbo.Municipios ON dbo.Estados.cve_estado = dbo.Municipios.cve_estado AND dbo.Usuarios.cve_municipio = dbo.Municipios.cve_municipio INNER JOIN
             dbo.Localidades ON dbo.Estados.cve_estado = dbo.Localidades.cve_estado AND dbo.Usuarios.cve_localidad = dbo.Localidades.cve_localidad AND dbo.Municipios.cve_municipio = dbo.Localidades.cve_municipio INNER JOIN
             dbo.Duenos ON dbo.Usuarios.matricula = dbo.Duenos.matricula INNER JOIN
             dbo.StatusDuenos ON dbo.Duenos.IdStatusDuenos = dbo.StatusDuenos.IdStatusDuenos

             where dbo.Vehiculos.numSerie = @numSerie and dbo.Duenos.IdStatusDuenos = 1";

            // Establecer el parámetro del número de serie en la consulta SQL
            SqlDataSourceDuenos.SelectCommand = query;
            SqlDataSourceDuenos.SelectParameters.Clear();
            SqlDataSourceDuenos.SelectParameters.Add("numSerie", numSerie);

            // Actualizar el GridView con el resultado de la consulta
            GVDuenos.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            DLInfoDueno.Visible= false;
            DLInfoDueno.Enabled = false;
            GVVehiculos.DataBind();
            GVDuenos.DataBind();
        }

        protected void btnBuscarMatricula_Click(object sender, EventArgs e)
        {
            string matriculaABuscar = txtMatricula.Text.Trim(); // revisar si se descencripta o no
            matriculaABuscar = WS.Encriptar(matriculaABuscar);
         
            // Construir la consulta SQL para buscar el registro específico
            string query = @"SELECT dbo.Vehiculos.placa, dbo.Vehiculos.numSerie, dbo.Vehiculos.numMotor, dbo.Marcas.marca, dbo.SubMarcas.submarca, dbo.Modelos.modelo, dbo.Colores.colores, dbo.Combustibles.combustible, dbo.Estados.estado, dbo.Municipios.municipio, dbo.Localidades.localidad, 
             dbo.Vehiculos.matricula, dbo.Localidades.latitud, dbo.Localidades.longitud, dbo.Marcas.cveMarca, dbo.SubMarcas.cveSubmarca, dbo.Modelos.cveModelo, dbo.Colores.cveColor, dbo.Combustibles.cveCombustible, dbo.Estados.cve_estado, dbo.Municipios.cve_municipio, 
             dbo.Localidades.cve_localidad
FROM   dbo.Vehiculos INNER JOIN
             dbo.Marcas ON dbo.Vehiculos.cveMarca = dbo.Marcas.cveMarca INNER JOIN
             dbo.SubMarcas ON dbo.Vehiculos.cveSubmarca = dbo.SubMarcas.cveSubmarca INNER JOIN
             dbo.Modelos ON dbo.Vehiculos.cveModelo = dbo.Modelos.cveModelo INNER JOIN
             dbo.Colores ON dbo.Vehiculos.cveColor = dbo.Colores.cveColor INNER JOIN
             dbo.Combustibles ON dbo.Vehiculos.cveCombustible = dbo.Combustibles.cveCombustible INNER JOIN
             dbo.Estados ON dbo.Vehiculos.cve_estado = dbo.Estados.cve_estado INNER JOIN
             dbo.Municipios ON dbo.Estados.cve_estado = dbo.Municipios.cve_estado AND dbo.Vehiculos.cve_municipio = dbo.Municipios.cve_municipio INNER JOIN
             dbo.Localidades ON dbo.Estados.cve_estado = dbo.Localidades.cve_estado AND dbo.Vehiculos.cve_localidad = dbo.Localidades.cve_localidad AND dbo.Municipios.cve_municipio = dbo.Localidades.cve_municipio INNER JOIN
             dbo.Duenos ON dbo.Vehiculos.numSerie = dbo.Duenos.numSerie INNER JOIN
             dbo.StatusDuenos ON dbo.Duenos.IdStatusDuenos = dbo.StatusDuenos.IdStatusDuenos
WHERE(dbo.Vehiculos.matricula = @matricula) AND(dbo.Duenos.IdStatusDuenos = 1)";

            // Establecer el parámetro de la placa en la consulta SQL
            SqlDataSourceVehiculos.SelectCommand = query;
            SqlDataSourceVehiculos.SelectParameters.Clear();
            SqlDataSourceVehiculos.SelectParameters.Add("matricula", matriculaABuscar);

            // Actualizar el GridView con el resultado de la consulta
            GVVehiculos.DataBind();



        }

        protected void btnLimpiarMatricular_Click(object sender, EventArgs e)
        {
            GVVehiculos.DataBind();
            GVDuenos.DataBind();
        }
    }
}