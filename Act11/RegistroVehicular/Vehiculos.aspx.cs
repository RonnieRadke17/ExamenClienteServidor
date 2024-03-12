using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {

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
                    command.Parameters.AddWithValue("@NumSerie",int.Parse(txtNumSerie.Text));//cambiar a varchar
                    command.Parameters.AddWithValue("@NumMotor", int.Parse(txtNumMotor.Text));//entero
                    command.Parameters.AddWithValue("@CveMarca", int.Parse(ddMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveSubmarca", int.Parse(ddSubMarca.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveModelo", int.Parse(ddModelo.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveColor", int.Parse(ddColor.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveCombustible", int.Parse(ddCombustible.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveEstado", int.Parse(ddEstados.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveMunicipio", int.Parse(ddMunicipios.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@CveLocalidad", int.Parse(ddLocalidades.SelectedValue.ToString()));//entero
                    command.Parameters.AddWithValue("@Matricula",txtMatricula.Text);//String

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
        }

        private int ObtenerIdMarcaPorNombre(string nombreMarca)
        {
            int idMarca = 0;
            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la marca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cveMarca FROM Marcas WHERE marca = @NombreMarca";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreMarca", nombreMarca);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idMarca = Convert.ToInt32(result);
                    }
                }
            }
            return idMarca;
        }

        private int ObtenerIdSubMarcaPorNombre(string nombreSubMarca, int idMarca)
        {
            int idSubMarca = 0;

            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la submarca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cveSubmarca FROM SubMarcas WHERE submarca = @NombreSubMarca AND cveMarca = @IdMarca";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreSubMarca", nombreSubMarca);
                    command.Parameters.AddWithValue("@IdMarca", idMarca);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idSubMarca = Convert.ToInt32(result);
                    }
                }
            }

            return idSubMarca;
        }

        private int ObtenerIdModeloPorNombre(string nombreModelo)
        {
            int idModelo = 0;
            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la marca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cveModelo FROM Modelos WHERE modelo = @nombreModelo";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreModelo", nombreModelo);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idModelo = Convert.ToInt32(result);
                    }
                }
            }
            return idModelo;
        }

        private int ObtenerIdColorPorNombre(string nombreColor)
        {
            int idColor = 0;
            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la marca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cveColor FROM Colores WHERE colores = @nombreColor";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreColor", nombreColor);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idColor = Convert.ToInt32(result);
                    }
                }
            }
            return idColor;
        }

        private int ObtenerIdCombustiblePorNombre(string nombreCombustible)
        {
            int idCombustible = 0;
            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la marca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cveCombustible FROM Combustibles WHERE combustible = @nombreCombustible";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreCombustible", nombreCombustible);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idCombustible = Convert.ToInt32(result);
                    }
                }
            }
            return idCombustible;
        }

        private int ObtenerIdEstadoPorNombre(string nombreEstado)
        {
            int idEstado = 0;
            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la marca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cve_estado FROM Estados WHERE estado = @nombreEstado";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEstado", nombreEstado);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idEstado = Convert.ToInt32(result);
                    }
                }
            }
            return idEstado;
        }

        private int ObtenerIdMunicipioPorNombre(string nombreMunicipio, int idEstado)
        {
            int idMunicipio = 0;

            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la submarca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cve_municipio FROM Municipios WHERE municipio = @nombreMunicipio AND cve_estado = @idEstado";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreMunicipio", nombreMunicipio);
                    command.Parameters.AddWithValue("@idEstado", idEstado);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idMunicipio = Convert.ToInt32(result);
                    }
                }
            }

            return idMunicipio;
        }

        private int ObtenerIdLocalidadPorNombre(string nombreLocalidad, int idEstado,int idMunicipio)
        {
            int idLocalidad = 0;

            // Utiliza tu conexión y tu consulta SQL para obtener el ID de la submarca
            using (SqlConnection connection = new SqlConnection("Data Source=CR7\\SQLEXPRESS;Initial Catalog=2231122109;User ID=sa;Password=aaa"))
            {
                connection.Open();

                string query = "SELECT cve_localidad FROM Localidades WHERE localidad = @nombreLocalidad AND cve_estado = @idEstado AND cve_municipio = @idMunicipio";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@idEstado", idEstado);
                    command.Parameters.AddWithValue("@idMunicipio", idMunicipio);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Si se encuentra un resultado, conviértelo a entero
                        idLocalidad = Convert.ToInt32(result);
                    }
                }
            }

            return idLocalidad;
        }

        protected void GVVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumSerie.Text = GVVehiculos.SelectedRow.Cells[1].Text.ToString();
            txtPlaca.Text = GVVehiculos.SelectedRow.Cells[2].Text.ToString();
            txtNumMotor.Text = GVVehiculos.SelectedRow.Cells[3].Text.ToString();
            ddMarca.SelectedValue = ObtenerIdMarcaPorNombre(GVVehiculos.SelectedRow.Cells[4].Text.ToString()).ToString();
            int marca = ObtenerIdMarcaPorNombre(GVVehiculos.SelectedRow.Cells[4].Text.ToString());
            ddSubMarca.DataBind();//poner el dataBind es lo que hace que aparesca la submarca porque las actualiza a las de la marca
            ddSubMarca.SelectedValue = ObtenerIdSubMarcaPorNombre(GVVehiculos.SelectedRow.Cells[5].Text.ToString(), ObtenerIdMarcaPorNombre(GVVehiculos.SelectedRow.Cells[4].Text.ToString())).ToString();
            ddModelo.SelectedValue = ObtenerIdModeloPorNombre(GVVehiculos.SelectedRow.Cells[6].Text.ToString()).ToString();
            ddColor.SelectedValue = ObtenerIdColorPorNombre(GVVehiculos.SelectedRow.Cells[7].Text.ToString()).ToString();
            ddCombustible.SelectedValue = ObtenerIdCombustiblePorNombre(GVVehiculos.SelectedRow.Cells[8].Text.ToString()).ToString();
            ddEstados.SelectedValue = ObtenerIdEstadoPorNombre(GVVehiculos.SelectedRow.Cells[9].Text.ToString()).ToString();
            int estado = ObtenerIdEstadoPorNombre(GVVehiculos.SelectedRow.Cells[9].Text.ToString());
            ddMunicipios.DataBind();
            //nombre municipio y id estado
            ddMunicipios.SelectedValue = ObtenerIdMunicipioPorNombre(GVVehiculos.SelectedRow.Cells[10].Text.ToString(),estado).ToString();
            int municipio = ObtenerIdMunicipioPorNombre(GVVehiculos.SelectedRow.Cells[10].Text.ToString(), estado);
            ddLocalidades.DataBind();
            ddLocalidades.SelectedValue = ObtenerIdLocalidadPorNombre(GVVehiculos.SelectedRow.Cells[11].Text.ToString(),estado,municipio).ToString();
            //tengo que hacer una consulta por cada campo porque me da el nombre 
            txtMatricula.Text= GVVehiculos.SelectedRow.Cells[12].Text.ToString();
            //aqui va la latitud y longitud de donde se regristró
            GoogleMaps1.Latitude = double.Parse(GVVehiculos.SelectedRow.Cells[13].Text.ToString());
            GoogleMaps1.Longitude = double.Parse(GVVehiculos.SelectedRow.Cells[14].Text.ToString());

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
            GVDuenos.DataBind();
        }
    }
}