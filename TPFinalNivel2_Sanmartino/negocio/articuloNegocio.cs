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
                datos.setarConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, M.Descripcion as Marca, C.Descripcion as  Dispositivo, A.ImagenUrl, A.Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca And C.Id = A.IdCategoria");
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
                    aux.Marca.Id = (int)datos.Lector["idMarca"];
                    aux.Categoria = new categoria();
                    aux.Categoria.descripcion = (string)datos.Lector["Dispositivo"];
                    aux.Categoria.Id = (int)datos.Lector["idCategoria"];
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
        
        public void agregar(articulo nuevo)
        {
            acessoDatos datos = new acessoDatos();
            try
            {
                datos.setarConsulta("insert into ARTICULOS values (@codigo, @titulo, @descripcion, @idMarca, @idCategoria, @urlImagen, @precio)");
                
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@titulo", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@idMarca", nuevo.Marca.Id);
                datos.setearParametros("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@urlImagen", nuevo.urlImg);
                datos.setearParametros("@precio", nuevo.Precio);

                datos.ejecutarAccion();
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

        public void Modificar(articulo nuevo)
        {
            acessoDatos datos = new acessoDatos();
            try
            {
                datos.setarConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @marca, IdCategoria = @categoria, ImagenUrl = @img, Precio = @precio where Id = @id");
                
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@marca", nuevo.Marca.Id);
                datos.setearParametros("@categoria", nuevo.Categoria.Id);
                datos.setearParametros("@img", nuevo.urlImg);
                datos.setearParametros("@precio", nuevo.Precio);
                datos.setearParametros("@id", nuevo.Id);
                
                datos.ejecutarAccion();
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
        

        public void eliminar(int id)
        {
            acessoDatos datos = new acessoDatos();
            try
            {
                datos.setarConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
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
