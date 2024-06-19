using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class articuloNegocio
    {
        public List<articulo> listar()
        {
            List<articulo> lista = new List<articulo>();
            acessoDatos datos = new acessoDatos();
            try
            {
                datos.setarConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as  Dispositivo, A.ImagenUrl, A.Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca And C.Id = A.IdCategoria");
                datos.ejecutarLector();
                while (datos.Lector.Read())
                {
                    articulo aux = new articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca  = new marca();
                    aux.Marca.descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new categoria();
                    aux.Categoria.descripcion = (string)datos.Lector["Dispositivo"];
                    aux.urlImg = (string)datos.Lector["imagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

    }
}
