using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Ejercicio_Técnico
{
    class ClsSku : ClsDescon
    {
        private string sk;
        private string ar;
        private string ma;
        private string mo;
        private int de;
        private int cl;
        private int fa;
        private int ca;
        private int st;
        private DateTime fea;
        private DateTime feb;
        private int des;

        public override string sku { get { return sk; } set { sk = value; } }
        public override string articulo { get { return ar; } set { ar = value; } }
        public override string marca { get { return ma; } set { ma = value; } }
        public override string modelo { get { return mo; } set { mo = value; } }
        public override int departamento { get { return de; } set { de = value; } }
        public override int clase { get { return cl; } set { cl = value; } }
        public override int familia { get { return fa; } set { fa = value; } }
        public override int cantidad { get { return ca; } set { ca = value; } }
        public override int stock { get { return st; } set { st = value; } }
        public override DateTime fec_alt { get { return fea; } set { fea = value; } }
        public override DateTime fec_baj { get { return feb; } set { feb = value; } }
        public override bool descontinuo { get { return Convert.ToBoolean(des); } set { des = Convert.ToInt32(value); } }
        public override bool guardar()
        {
            MySqlConnection cnn = new MySqlConnection();
            MySqlCommand comando = new MySqlCommand();
            cnn.ConnectionString = miclase.conexion;
            comando.Connection = cnn;
            cnn.Open();
            comando.Parameters.AddWithValue("@sk", this.sk);
            comando.Parameters.AddWithValue("@ar", this.ar);
            comando.Parameters.AddWithValue("@ma", this.ma);
            comando.Parameters.AddWithValue("@mo", this.mo);
            comando.Parameters.AddWithValue("@de", this.de);
            comando.Parameters.AddWithValue("@cl", this.cl);
            comando.Parameters.AddWithValue("@fa", this.fa);
            comando.Parameters.AddWithValue("@ca", this.ca);
            comando.Parameters.AddWithValue("@st", this.st);
            comando.Parameters.AddWithValue("@fea", this.fea);
            comando.Parameters.AddWithValue("@feb", this.feb);
            comando.Parameters.AddWithValue("@des", this.des);
            comando.CommandText = "Insert into skus values (@sk,@ar,@ma,@mo,@st,@ca,@fea,@feb,@de,@cl,@fa,@des);";
            int resultado;
            resultado = comando.ExecuteNonQuery();
            cnn.Close(); cnn.Dispose();
            if (resultado > 0) return true; else return false;
        }

        public override bool eliminar(string x)
        {
            MySqlConnection cnn = new MySqlConnection();
            MySqlCommand comando = new MySqlCommand();
            cnn.ConnectionString = miclase.conexion;
            comando.Connection = cnn;
            cnn.Open();
            comando.Parameters.AddWithValue("@sk", x);
            comando.CommandText = "Delete from skus where sku=@sk";
            int resultado;
            resultado = comando.ExecuteNonQuery();
            cnn.Close(); cnn.Dispose();
            if (resultado > 0) return true; else return false;
        }
        public override bool actualizar()
        {
            MySqlConnection cnn = new MySqlConnection();
            MySqlCommand comando = new MySqlCommand();
            cnn.ConnectionString = miclase.conexion;
            comando.Connection = cnn;
            cnn.Open();
            comando.Parameters.AddWithValue("@sk", this.sk);
            comando.Parameters.AddWithValue("@ar", this.ar);
            comando.Parameters.AddWithValue("@ma", this.ma);
            comando.Parameters.AddWithValue("@mo", this.mo);
            comando.Parameters.AddWithValue("@de", this.de);
            comando.Parameters.AddWithValue("@cl", this.cl);
            comando.Parameters.AddWithValue("@fa", this.fa);
            comando.Parameters.AddWithValue("@ca", this.ca);
            comando.Parameters.AddWithValue("@st", this.st);
            comando.Parameters.AddWithValue("@fea", this.fea);
            comando.Parameters.AddWithValue("@feb", this.feb);
            comando.Parameters.AddWithValue("@des", this.des);
            comando.CommandText = "UPDATE skus SET Artículo=@ar, Marca=@ma, Modelo=@mo, Cantidad=@ca, Stock=@st, Fec_Alt=@fea, Fec_Baj=@feb, Departamentos_Cod_Dep=@de, Clases_Cod_Cla=@cl, Familias_Cod_Fam=@fa, Descontinuado=@des WHERE SKU=@sk;";
            int resultado;
            resultado = comando.ExecuteNonQuery();
            cnn.Close(); cnn.Dispose();
            if (resultado > 0) return true; else return false;
        }
        public override DataTable mostrar()
        {
            MySqlConnection cnn = new MySqlConnection();
            MySqlCommand comando = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            cnn.ConnectionString = miclase.conexion;
            comando.Connection = cnn;
            cnn.Open();
            comando.CommandText = "SELECT * FROM skus ORDER BY SKU ASC";
            da.SelectCommand = comando;
            da.Fill(dt);
            cnn.Close();
            cnn.Dispose();
            return dt;
        }
    }
}
