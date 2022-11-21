using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ejercicio_Técnico
{
    public partial class ABCC : Form
    {
        public ABCC()
        {
            InitializeComponent();
        }

        ClsSku s = new ClsSku();

        
        private void ABCC_Load(object sender, EventArgs e)
        {
            
        }
        private void btnguardar_Click(object sender, EventArgs e)
        {
            s.sku = txtsku.Text;
            s.articulo = txtarticulo.Text;
            s.marca = txtmarca.Text;
            s.modelo = txtmodelo.Text;
            s.departamento = int.Parse(cbdepartamento.SelectedValue.ToString());
            s.clase = int.Parse(cbclase.SelectedValue.ToString());
            s.familia = int.Parse(cbfamilia.SelectedValue.ToString());
            s.cantidad = int.Parse(txtcantidad.Text);
            s.stock = int.Parse(txtstock.Text);
            s.fec_alt = Convert.ToDateTime(dtfec_alt.Text = DateTime.Now.ToString("dd/MM/yyyy"));
            s.fec_baj = Convert.ToDateTime(dtfec_baj.Value = new DateTime(1900, 01, 01));
            s.descontinuo = cbdescontinuado.Checked;

            if (int.Parse(txtcantidad.Text) > int.Parse(txtstock.Text))
            {
                if (s.guardar() == true)
                {
                    MessageBox.Show("Guardado con éxito");
                    limpia();
                    deshabilita();
                    btnguardar.Enabled = false;
                    txtsku.Clear(); txtsku.Enabled = true; txtsku.Focus();
                }
            }
            else
                MessageBox.Show("La Cantidad no debe ser mayor al Stock");
        }
        public void habilita()
        {
            txtarticulo.Enabled = true;
            txtmarca.Enabled = true;
            txtmodelo.Enabled = true;
            cbdepartamento.Enabled = true;
            txtcantidad.Enabled = true;
            txtstock.Enabled = true;
            dtfec_alt.Enabled = true;
            dtfec_baj.Enabled = true;
            cbdescontinuado.Enabled = true;

        }
        public void deshabilita()
        {
            txtarticulo.Enabled = false;
            txtmarca.Enabled = false;
            txtmodelo.Enabled = false;
            cbdepartamento.Enabled = false;
            cbclase.Enabled = false;
            cbfamilia.Enabled = false;
            txtcantidad.Enabled = false;
            txtstock.Enabled = false;
            dtfec_alt.Enabled = false;
            dtfec_baj.Enabled = false;
            cbdescontinuado.Enabled = false;
        }
        public void limpia()
        {
            txtarticulo.Clear();
            txtmarca.Clear();
            txtmodelo.Clear();
            cbdepartamento.Text = "Seleccione una Opción";
            cbclase.Text = "Seleccione una Opción";
            cbfamilia.Text = "Seleccione una Opción";
            txtcantidad.Clear();
            txtstock.Clear();
            dtfec_alt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtfec_baj.Value = new DateTime(1900, 01, 01);
            cbdescontinuado.Checked = false;
        }
        private void cargarDepartamento()
        {
            cbdepartamento.DataSource = null;
            cbdepartamento.Items.Clear();
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand("SELECT Cod_Dep, Nom_Dep FROM departamentos ORDER BY Cod_Dep ASC;", cnn);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable dt = new DataTable();
                data.Fill(dt);
                cnn.Close();

                DataRow fila = dt.NewRow();
                fila["Nom_Dep"] = "Selecione una Opción";
                dt.Rows.InsertAt(fila,0);

                cbdepartamento.ValueMember = "Cod_Dep";
                cbdepartamento.DisplayMember = "Nom_Dep";
                cbdepartamento.DataSource = dt;
            }catch(Exception ex)
            {
                MessageBox.Show("Error al cargar Departamentos " + ex.Message);
            }
        }

        private void cargarClase(string cod_dep)
        {
            cbclase.Enabled = true;
            cbclase.DataSource = null;
            cbclase.Items.Clear();
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand("SELECT Cod_Cla, Nom_Cla FROM clases WHERE Departamentos_Cod_Dep= @cod_dep ORDER BY Cod_Cla ASC", cnn);
                comando.Parameters.AddWithValue("cod_dep", cod_dep);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable dt = new DataTable();
                data.Fill(dt);
                cnn.Close();

                DataRow fila = dt.NewRow();
                fila["Nom_Cla"] = "Selecione una Opción";
                dt.Rows.InsertAt(fila, 0);

                cbclase.ValueMember = "Cod_Cla";
                cbclase.DisplayMember = "Nom_Cla";
                cbclase.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Clases " + ex.Message);
            }
        }

        private void cargarFamilia(string cod_cla)
        {
            cbfamilia.Enabled = true;
            cbfamilia.DataSource = null;
            cbfamilia.Items.Clear();
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand("SELECT Cod_Fam, Nom_Fam FROM familias WHERE Clases_Cod_Cla= @cod_cla ORDER BY Cod_Fam ASC", cnn);
                comando.Parameters.AddWithValue("cod_cla", cod_cla);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable dt = new DataTable();
                data.Fill(dt);
                cnn.Close();

                DataRow fila = dt.NewRow();
                fila["Nom_Fam"] = "Selecione una Opción";
                dt.Rows.InsertAt(fila, 0);

                cbfamilia.ValueMember = "Cod_Fam";
                cbfamilia.DisplayMember = "Nom_Fam";
                cbfamilia.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Familias " + ex.Message);
            }
        }
        public string traerdepartamento(int cd)
        {
            string nom = "";
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Nom_Dep FROM departamentos WHERE Cod_Dep=" + cd, cnn);
            MySqlDataReader myreader = comando.ExecuteReader();
            comando.Dispose();
            if (myreader.HasRows)
                while (myreader.Read())
                    nom = myreader["Nom_Dep"].ToString();
            myreader.Close(); cnn.Close(); cnn.Dispose();
            return nom;
        }
        public string traerclase(int cc)
        {
            string nom = "";
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Nom_Cla FROM clases WHERE Cod_Cla=" + cc, cnn);
            MySqlDataReader myreader = comando.ExecuteReader();
            comando.Dispose();
            if (myreader.HasRows)
                while (myreader.Read())
                    nom = myreader["Nom_Cla"].ToString();
            myreader.Close(); cnn.Close(); cnn.Dispose();
            return nom;
        }
        public string traerfamilia(int cf)
        {
            string nom = "";
            MySqlConnection cnn = new MySqlConnection(miclase.conexion);
            cnn.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Nom_Fam FROM familias WHERE Cod_Fam=" + cf, cnn);
            MySqlDataReader myreader = comando.ExecuteReader();
            comando.Dispose();
            if (myreader.HasRows)
                while (myreader.Read())
                    nom = myreader["Nom_Fam"].ToString();
            myreader.Close(); cnn.Close(); cnn.Dispose();
            return nom;
        }
        private void txtsku_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27) this.Close();
            if (e.KeyChar == 13 && txtsku.Text.Length > 0 && txtsku.Text.Length <= 6)
            {
                MySqlConnection cnn = new MySqlConnection(miclase.conexion);
                cnn.Open();
                MySqlCommand comando = new MySqlCommand("SELECT Artículo, Marca, Modelo, Stock, Cantidad, Fec_Alt, Fec_Baj, Departamentos_Cod_Dep, Clases_Cod_Cla, Familias_Cod_Fam, Descontinuado FROM skus WHERE SKU = '" + txtsku.Text + "';", cnn);
                MySqlDataReader myreader = comando.ExecuteReader();
                comando.Dispose();
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        txtarticulo.Text = myreader["Artículo"].ToString();
                        txtmarca.Text = myreader["Marca"].ToString();
                        txtmodelo.Text = myreader["Modelo"].ToString();
                        cbdepartamento.Text = traerdepartamento(int.Parse(myreader["Departamentos_Cod_Dep"].ToString()));
                        cbclase.Text = traerclase(int.Parse(myreader["Clases_Cod_Cla"].ToString()));
                        cbfamilia.Text = traerfamilia(int.Parse(myreader["Familias_Cod_Fam"].ToString()));
                        txtcantidad.Text = myreader["Cantidad"].ToString();
                        txtstock.Text = myreader["Stock"].ToString();
                        dtfec_alt.Text = myreader["Fec_Alt"].ToString();
                        dtfec_baj.Text = myreader["Fec_Baj"].ToString();
                        cbdescontinuado.Checked = Convert.ToBoolean(myreader["Descontinuado"]);
                        
                    }
                    btneliminar.Enabled = true;
                    btnactualizar.Enabled = true;
                }
                else
                {
                    btnguardar.Enabled = true;
                }
                txtsku.Enabled = false;
                habilita();
                txtarticulo.Focus();
                myreader.Close(); cnn.Close(); cnn.Dispose();
            }
        }

        private void cbclase_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbclase.SelectedValue.ToString() != null)
            {
                string cod_cla = cbclase.SelectedValue.ToString();
                cargarFamilia(cod_cla);
            }
        }

        private void txtarticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtmarca.Focus();
            }
        }

        private void txtmarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtmodelo.Focus();
            }
        }

        private void txtmodelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbdepartamento.Focus();
                cargarDepartamento();
            }
        }

        private void cbclase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbfamilia.Focus();
            }
        }

        private void cbfamilia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtstock.Focus();
            }
        }

        private void txtstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtcantidad.Focus();
            }
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtfec_alt.Focus();
            }
        }

        private void cbfec_alt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtfec_baj.Focus();
            }
        }

        private void cbfec_baj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (btnguardar.Enabled)
                    btnguardar.Focus();
                else
                    btnactualizar.Focus();
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ABCC_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ABRIR EL MENU PRINCIPAL
            //menu fm = new menu();
            //fm.Show;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (s.eliminar(txtsku.Text) == true)
            {
                MessageBox.Show("Eliminado con éxito");
                limpia();
                deshabilita();
                txtsku.Clear();
                btneliminar.Enabled = false;
                btnactualizar.Enabled = false;
                txtsku.Focus();
                txtsku.Enabled = true;
            }
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtcantidad.Text) >= int.Parse(txtstock.Text))
            {
                s.sku = txtsku.Text;
                s.articulo = txtarticulo.Text;
                s.marca = txtmarca.Text;
                s.modelo = txtmodelo.Text;
                s.departamento = int.Parse(cbdepartamento.SelectedValue.ToString());
                s.clase = int.Parse(cbclase.SelectedValue.ToString());
                s.familia = int.Parse(cbfamilia.SelectedValue.ToString());
                s.stock = int.Parse(txtstock.Text);
                s.cantidad = int.Parse(txtcantidad.Text);
                if (cbdescontinuado.Checked == true)
                {
                    s.descontinuo = cbdescontinuado.Checked;
                    dtfec_baj.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    s.fec_baj = Convert.ToDateTime(dtfec_baj.Text = DateTime.Now.ToString("yyyy/MM/dd"));
                }
                else
                {
                    s.descontinuo = cbdescontinuado.Checked;
                    s.fec_baj = Convert.ToDateTime(dtfec_baj.Value = new DateTime(1900, 01, 01));
                }
                if (s.actualizar() == true)
                {
                    MessageBox.Show("Actualizado con éxito");
                    limpia();
                    deshabilita();
                    btnactualizar.Enabled = false;
                    btneliminar.Enabled = false;
                    txtsku.Clear(); txtsku.Focus();
                    txtsku.Enabled = true;
                }
            }
            else
                 MessageBox.Show("La Cantidad no debe ser mayor al Stock");
        }

        private void cbdepartamento_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            if (cbdepartamento.SelectedValue.ToString() != null)
            {
                string cod_dep = cbdepartamento.SelectedValue.ToString();
                cargarClase(cod_dep);
            }
        }

        private void cbdepartamento_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbclase.Focus();
            }
        }
    }
}
