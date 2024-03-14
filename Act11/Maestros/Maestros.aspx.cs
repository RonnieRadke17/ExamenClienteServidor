using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Act11.Maestros
{
    public partial class Maestros : System.Web.UI.Page
    {
        WSCS.WSCSSoapClient WS = new WSCS.WSCSSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            WS.Insersion(txtMatricula.Text, txtNombre.Text, txtPaterno.Text, txtMaterno.Text, txtCurp.Text, txtRfc.Text, txtSexo.Text, int.Parse(ddEstados.SelectedValue.ToString()), int.Parse(ddMunicipios.SelectedValue.ToString()), int.Parse(ddLocalidades.SelectedValue.ToString()), 2);
            GVMaestros.DataBind();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            WS.Modificacion(txtMatricula.Text, txtNombre.Text, txtPaterno.Text, txtMaterno.Text, txtCurp.Text, txtRfc.Text, txtSexo.Text, int.Parse(ddEstados.SelectedValue.ToString()), int.Parse(ddMunicipios.SelectedValue.ToString()), int.Parse(ddLocalidades.SelectedValue.ToString()));
            GVMaestros.DataBind();
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            WS.Eliminacion(txtMatricula.Text);
            GVMaestros.DataBind();
        }

        protected void GVMaestros_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtMatricula.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[1].Text.ToString());
            txtNombre.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[2].Text.ToString());
            txtPaterno.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[3].Text.ToString());
            txtMaterno.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[4].Text.ToString());
            txtRfc.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[5].Text.ToString());
            txtCurp.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[6].Text.ToString());
            txtSexo.Text = WS.Desencriptar(GVMaestros.SelectedRow.Cells[7].Text.ToString());
            GoogleMaps1.Latitude = Double.Parse(GVMaestros.SelectedRow.Cells[11].Text.ToString());
            GoogleMaps1.Longitude = Double.Parse(GVMaestros.SelectedRow.Cells[12].Text.ToString());
        }
    }
}