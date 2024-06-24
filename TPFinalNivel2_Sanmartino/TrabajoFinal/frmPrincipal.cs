﻿using System;
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
            cboCriterio.Items.Add("Codigo");
            cboCriterio.Items.Add("Nombre");
            cboCriterio.Items.Add("Marca");
            cboCriterio.Items.Add("Categoria");
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            articulo seleccionado;
            seleccionado = (articulo)dgvCatalogo.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            articuloNegocio negocio = new articuloNegocio();
            articulo seleccionado = new articulo();

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Estás seguro que quieres eliminarlo? No se podrá recuperar", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (articulo)dgvCatalogo.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cboCriterio.SelectedItem.ToString() == "Codigo")
            {
                cboParametro.Items.Clear();
                cboParametro.Items.Add("Empieza");
                cboParametro.Items.Add("Contiene");
                cboParametro.Items.Add("Termina");
                mostrarFiltro();
            }
            else if (cboCriterio.SelectedItem.ToString() == "Nombre")
            {
                cboParametro.Items.Clear();
                cboParametro.Items.Add("Empieza");
                cboParametro.Items.Add("Contiene");
                cboParametro.Items.Add("Termina");
                mostrarFiltro();

            }
            else if (cboCriterio.SelectedItem.ToString() == "Marca")
            {
                cboParametro.Items.Clear();
                cboParametro.Items.Add("Samsung");
                cboParametro.Items.Add("Apple");
                cboParametro.Items.Add("Sony");
                cboParametro.Items.Add("Huawei");
                cboParametro.Items.Add("Motorola");
                ocultarFiltro();



            }
            else if(cboCriterio.SelectedItem.ToString() == "Categoria")
            {
                cboParametro.Items.Clear();
                cboParametro.Items.Add("Celulares");
                cboParametro.Items.Add("Televisores");
                cboParametro.Items.Add("Media");
                cboParametro.Items.Add("Audio");
                ocultarFiltro();

            }

        }

        private void ocultarFiltro()
        {
            txtFiltro.Visible = false;
            lblFiltro.Visible = false;
        }
        private void mostrarFiltro()
        {
            txtFiltro.Visible = true;
            lblFiltro.Visible = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                string criterio = cboCriterio.SelectedItem.ToString();
                string parametro = cboParametro.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvCatalogo.DataSource = negocio.filtrar(criterio, parametro, filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            articulo detalle;
            detalle = (articulo)dgvCatalogo.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(detalle);
            modificar.ShowDialog();
            cargar();
        }
    }
}
