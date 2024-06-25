using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace TrabajoFinal
{
    
    public partial class frmAgregar : Form
    {
        private articulo articulo = null;
        private bool modoDetalle = false;
        private OpenFileDialog archivo = null;


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
        public frmAgregar(articulo articulo, bool modoDetalle)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalle artículo";
            this.modoDetalle = modoDetalle;
        }

        private void configurarOnlyRead()
        {
            cboMarca.Enabled = false;
            cboCategoria.Enabled = false;
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtImg.Enabled = false;
            txtPrecio.Enabled = false;
            btnAceptar.Enabled = false;
            btnCancelar.Text = "Cerrar";
            cargarImagen(txtImg.Text);
            btnAgregarImg.Visible = false;
            lblObligatorio.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
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

                if (!ValidarCampos())
                {
                    MessageBox.Show("Completa todos los campos");
                }
                else
                {

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

                    if(archivo != null && !(txtImg.Text.ToLower().Contains("http")))
                    {
                        string destinationPath = Path.Combine(ConfigurationManager.AppSettings["images-folder"], Path.GetFileName(archivo.FileName));
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath); 
                        }
                        File.Copy(archivo.FileName, destinationPath);
                    }
                }
                
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if(!ValidarCampos() == false)
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
                    txtImg.Text = articulo.urlImg;
                    txtPrecio.Text = articulo.Precio.ToString();
                }
                if(modoDetalle)
                {
                    configurarOnlyRead();
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
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                return false;
            return true;
        }

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "JPEG|*.JPEG;|JPG|*.jpg;|png|*.png;|Todos los archivos| *.JPEG; *.jpg; *.png";
            List<OpenFileDialog> lista = new List<OpenFileDialog>();
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtImg.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                lista.Add(archivo);
            }
        }
    }
}
