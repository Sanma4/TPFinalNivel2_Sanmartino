using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoFinal
{
    
    public partial class frmAgregar : Form
    {
        private articulo articulo = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                if (articulo == null)
                    articulo = new articulo();
                
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.urlImg = txtImg.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (marca)cboMarca.SelectedItem;
                articulo.Categoria = (categoria)cboCategoria.SelectedItem;

                if (articulo.Id != 0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Close();
            }
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            marcaNegocio marcaNegocio = new marcaNegocio();
            categoriaNegocio categoriaNegocio = new categoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    Console.WriteLine($"Marca Id: {articulo.Marca.Id}, Categoria Id: {articulo.Categoria.Id}");
                    txtImg.Text = articulo.urlImg;
                    cargarImagen(articulo.urlImg);
                    txtPrecio.Text = articulo.Precio.ToString();
                }
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
                pbxAgregar.Load(urlImg);
            }
            catch (Exception ex)
            {

                pbxAgregar.Load("https://i0.wp.com/thefoodmanager.com/wp-content/uploads/2021/03/placeholder.png?ssl=1");
            }

        }

        private void txtImg_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImg.Text);
        }
    }
}
