using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace Ejercicio_Técnico
{
    abstract class ClsDescon
    {
        public abstract string sku { get; set; }
        public abstract string articulo { get; set; }
        public abstract string marca { get; set; }
        public abstract string modelo { get; set; }
        public abstract int departamento { get; set; }
        public abstract int clase { get; set; }
        public abstract int familia { get; set; }
        public abstract int stock { get; set; }
        public abstract int cantidad { get; set; }
        public abstract DateTime fec_alt { get; set; }
        public abstract DateTime fec_baj { get; set; }
        public abstract bool descontinuo { get; set; }

        public abstract bool guardar();
        public abstract bool eliminar(string x);
        public abstract bool actualizar();
        public abstract DataTable mostrar();
    }
}
