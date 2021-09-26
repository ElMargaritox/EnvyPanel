using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvyPanel
{
    public partial class BusquedaAvanzada : Form
    {
        private MySqlConnection createConnection()
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection("SERVER=198.52.123.82; DATABASE=Clientes; UID=margarita; USERNAME=margarita; PASSWORD=geimer123; PORT=3306;");

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR ADMINISTRATIVO");
            }
            return connection;
        }

        public BusquedaAvanzada()
        {
            InitializeComponent();
        }

        private void BusquedaAvanzada_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conexion = createConnection();
                MySqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "select * from prueba where ClienteId = @ClienteId";
                conexion.Open();
                cmd.Parameters.AddWithValue("@ClienteId", textBox1.Text);
                MySqlDataReader registro = cmd.ExecuteReader();
                string[] ss = { "ClienteId", "Nombre", "Tipo", "Puerto", "Precio", "Moneda", "Tiempo", "Recursos", "Facturacion", "Nodo", "Owner", "Email", "Servicio", "Creado", "Vencimiento" };
                if (registro.Read())
                {
                    foreach (var item in ss)
                    {
                        MyLogger.Text = MyLogger.Text + Environment.NewLine + item.ToString() + ":     " + registro[item].ToString();
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("ERROR GEIMER");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conexion = createConnection();
                MySqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "select * from prueba where Ip = @Ip";
                conexion.Open();
                cmd.Parameters.AddWithValue("@Ip", textBox3.Text);
                MySqlDataReader registro = cmd.ExecuteReader();
                string[] ss = { "ClienteId", "Nombre", "Tipo", "Puerto", "Precio", "Moneda", "Tiempo", "Recursos", "Facturacion", "Nodo", "Owner", "Email", "Servicio", "Creado", "Vencimiento" };
                if (registro.Read())
                {
                    foreach (var item in ss)
                    {
                        MyLogger.Text = MyLogger.Text + Environment.NewLine + item.ToString() + ":     " + registro[item].ToString();
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("ERROR GEIMER");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conexion = createConnection();
                MySqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "select * from prueba where Email = @Email";
                conexion.Open();
                cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                MySqlDataReader registro = cmd.ExecuteReader();
                string[] ss = { "ClienteId", "Nombre", "Tipo", "Puerto", "Precio", "Moneda", "Tiempo", "Recursos", "Facturacion", "Nodo", "Owner", "Email", "Servicio", "Creado", "Vencimiento" };
                if (registro.Read())
                {
                    MyLogger.Text = string.Empty;
                    foreach (var item in ss)
                    {
                        MyLogger.Text = MyLogger.Text + Environment.NewLine + item.ToString() + ":     " + registro[item].ToString();
                    }

                    MyLogger.Text = MyLogger.Text + Environment.NewLine  + "===================================================";

                }
                conexion.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("ERROR GEIMER");
                
            }
        }
    }
}
