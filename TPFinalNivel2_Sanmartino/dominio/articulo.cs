using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class articulo
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public marca idMarca { get; set; }
        public categoria idCategoria { get; set; }
        public string urlImg { get; set; }
        public double precio { get; set; }
    }
}
