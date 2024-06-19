using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TrabajoFinal
{
    public partial class frmPrincipal : Form
    {
        private List<articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvCatalogo.DataSource = listaArticulos;
                cargarImagen(listaArticulos[0].urlImg);
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string urlImg)
        {
            try
            {
                pbxCatalogo.Load(urlImg);
            }
            catch (Exception ex)
            {

                pbxCatalogo.Load("https://i0.wp.com/thefoodmanager.com/wp-content/uploads/2021/03/placeholder.png?ssl=1");
            }

        }

        private void ocultarColumnas()
        {
            dgvCatalogo.Columns["urlImg"].Visible = false;
            dgvCatalogo.Columns["Id"].Visible = false;
        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCatalogo.CurrentRow != null)
            {
                articulo seleccionado = (articulo)dgvCatalogo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.urlImg);
            }
        }
    }
}
