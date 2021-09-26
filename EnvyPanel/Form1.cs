using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EnvyPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Funciones fn = new Funciones();

        public string hora_inicial;
        public string hora_Finalizada;
        public byte contador = 0;
        public bool subirdatos;
        public string datosencontrados;
        public string funcion = string.Empty;
        public int datos;
        public int numerogeimer;
        public Form2 frm = new Form2();
        public BusquedaAvanzada busqueda = new BusquedaAvanzada();
        public static Form1 Instance;

        private MySqlConnection createConnection()
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection("SERVER=191.232.209.179; DATABASE=LICENCIAS; UID=envyhosting; USERNAME=envyhosting; PASSWORD=z7k0ua3Fl1UUp*S@!C41Qf!ylgz2Rc; PORT=3306;");

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR ADMINISTRATIVO");
            }
            return connection;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MessageBox.Show(createConnection() + "xd");

            if (!File.Exists("tmp3.tmp"))
            {
                File.Create("tmp3.tmp");
                MessageBox.Show("Q - AGREGA DATOS" + Environment.NewLine + "R - RECARGA LOS DATOS" + Environment.NewLine + "K - OPACA Y DESOPACA");
                MessageBox.Show("EL BOTON DE ACTUALIZAR INSTANCIA YA FUNCIONA" + Environment.NewLine +
                    "(OJO PORQUE TOMA TODOS LOS DATOS HACER CON CUIDADO XD)" + Environment.NewLine + "Busqueda avanzada geimer para ser mas geimer" + Environment.NewLine + "Verificar Quien No Pago UwU (Toma Tiempo, Aprox 60 Segundos 30 Segundos)");
            }




            if (File.Exists("mfga.txt"))
            {
                try
                {
                    StreamReader sr = new StreamReader("marga.txt");

                    String line; line = sr.ReadLine();
                    sr.Close();

                    numerogeimer = int.Parse(line);
                     textBox1.Text = numerogeimer.ToString();
                    if (line != string.Empty) {}

                }
                catch (Exception)
                {

                    
                }
            }
            else
            {
                textBox1.Text = "1";
            }



            if (!File.Exists("marga.txt"))
            {
                File.Create("marga.txt");
                numerogeimer = 1;
            }
            button2.Enabled = true;
            Instance = this;

            

            
            frm.Show();

            Recargar();




            dateTimePicker1.MinDate = DateTime.Now.Date;
            textBox1.Text = numerogeimer.ToString();
            hora_inicial = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();




        }

        public void llenarcliente()
        {
            numerogeimer++;
            textBox1.Text = numerogeimer.ToString();
        }

        public void ExportarDatos(DataGridView listado)
        {
            Microsoft.Office.Interop.Excel.Application exportarexcel = new Microsoft.Office.Interop.Excel.Application();

            exportarexcel.Application.Workbooks.Add(true);

            int indicecolumna = 0;

            foreach (DataGridViewColumn columna in listado.Columns)
            {
                indicecolumna++;
                exportarexcel.Cells[1, indicecolumna] = columna.Name;
            }

            int indicefila = 0;

            foreach (DataGridViewRow fila in listado.Rows)
            {
                indicefila++;

                indicecolumna = 0;

                foreach (DataGridViewColumn columna in listado.Columns)
                {
                    indicecolumna++;
                    exportarexcel.Cells[indicefila + 1, indicecolumna] = fila.Cells[columna.Name].Value;

                }
            }

            exportarexcel.Calculate();
            
            exportarexcel.Visible = true;
        }

        public void Recargar()
        {
            dataGridView1.DataSource = fn.LlenarGrid("select * from prueba");

            if (dataGridView1.Rows.Count == 0) {
                MessageBox.Show("No Hay Ni Un Cliente Activo :(", "No Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //  string valor1 = (string)dataGridView1.CurrentRow.Cells["Vencimiento"].Value;


         //   for (int i = 0; i < dataGridView1.RowCount; i++)
           // {
             //  string s = dataGridView1.Rows[i].Cells[11].Value.ToString();
             //
               // DateTime.Now.AddMonths(1);
           // }

            



            Form1.Instance.Text = "EnvyPanelSoftware - By Margarita [CLIENTES TOTALES] " + "'" + dataGridView1.Rows.Count + "'";
        }

        public void verificacion_datos()
        {
            textBox2.Text.ToLower();
            if(textBox1.Text == string.Empty || textBox2.Text == "nombre" || comboBox1.Text.ToString() == string.Empty || textBox3.Text == "IP" || textBox4.Text == "Price" || textBox6.Text == "Puerto" || textBox7.Text == "Email" || comboBox2.Text.ToString() == string.Empty)
            {
                MessageBox.Show("Se Ha Detectado Errores. (1)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;    
            }

            if (!textBox3.Text.Contains(".")) { MessageBox.Show("La direccion IP Es Invalida", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (!textBox4.Text.Contains("$")) { textBox4.Text = textBox4.Text + "$"; }

            if (comboBox3.Text.ToString() == string.Empty || comboBox4.Text.ToString() == string.Empty || comboBox5.Text.ToString() == string.Empty || comboBox6.Text.ToString() == string.Empty || textBox5.Text == "Dueño" || textBox5.Text == string.Empty)
            {
                MessageBox.Show("Se Ha Detectado Errores. (2)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            subirdatos = true;
        }

        public void limpiar()
        {
            textBox1.Text = "ClienteId";
            textBox2.Text = "Nombre";
            textBox3.Text = "IP";
            textBox4.Text = "PRECIO";
            textBox5.Text = "Dueño";
            textBox6.Text = "Puerto";
            textBox6.Text = "Email";
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            comboBox6.Text = string.Empty;
            hora_Finalizada = string.Empty;

            textBox1.Focus();
        }


        public void guardardato()
        {
            try
            {
                StreamWriter sw = new StreamWriter("marga.txt");
                sw.WriteLine(textBox1.Text);
                sw.Close();

                llenarcliente();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void geimer(int funcion2)
        {
            switch (funcion2)
            {
                case 1:
                    funcion = "Se Ha Agregado EL Cliente Numero°" + textBox1.Text;
                    
                    timer1.Start();
                    break;
                case 2:
                    funcion = "Se Ha Borrado El Cliente Numero°" + textBox1.Text;
                    timer1.Start();
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            



            verificacion_datos();

           if (subirdatos == false) { return; }


            string agregar = "insert into prueba values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + comboBox1.Text.ToString() + "', '" + textBox3.Text + "', '" + textBox6.Text + "', '" + textBox4.Text + "', '" + comboBox2.Text.ToString() +
                 "', '" + comboBox3.Text.ToString() + "', '" + comboBox4.Text.ToString() + "', '" + comboBox5.Text.ToString() + "', '" + comboBox6.Text.ToString() + "', '" + textBox5.Text + "', '"+ textBox7.Text + "', '" + comboBox7.Text.ToString() + "', '" + hora_inicial + "', '" + hora_Finalizada +
                 "')";
            if (fn.Insertar(agregar))
            {
                int wtf = 1;
                geimer(wtf);
                subirdatos = false;

            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            hora_Finalizada = string.Empty;
            hora_Finalizada = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
        }

        private void button2_Click(object sender, EventArgs e)

        {
            if (textBox1.Text == "0") { return; }

            try
            {
                MySqlConnection conexion = createConnection();
                MySqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "update prueba SET Nombre = @Nombre, Tipo = @Tipo, Ip = @Ip, Puerto = @Puerto, Precio = @Precio, Moneda = @Moneda, Tiempo = @Tiempo, Recursos = @Recursos, Facturacion = @Facturacion, Nodo = @Nodo, Owner = @Owner, Email = @Email, Servicio = @Servicio, Vencimiento = @Vencimiento WHERE ClienteId=" + textBox1.Text;
                conexion.Open();

                cmd.Parameters.AddWithValue("@Nombre", textBox2.Text);
                cmd.Parameters.AddWithValue("@Tipo", comboBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@Ip", textBox3.Text);
                cmd.Parameters.AddWithValue("@Puerto", textBox6.Text);
                cmd.Parameters.AddWithValue("@Precio", textBox4.Text);
                cmd.Parameters.AddWithValue("@Moneda", comboBox2.Text.ToString());
                cmd.Parameters.AddWithValue("@Tiempo", comboBox3.Text.ToString());
                cmd.Parameters.AddWithValue("@Recursos", comboBox4.Text.ToString());
                cmd.Parameters.AddWithValue("@Facturacion", comboBox5.Text.ToString());
                cmd.Parameters.AddWithValue("@Nodo", comboBox6.Text.ToString());
                cmd.Parameters.AddWithValue("@Servicio", comboBox7.Text.ToString());
                cmd.Parameters.AddWithValue("@Email", textBox7.Text);
                cmd.Parameters.AddWithValue("@Owner", textBox5.Text);
                cmd.Parameters.AddWithValue("@Vencimiento", hora_Finalizada);
                cmd.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("La Instancia " + textBox1.Text + "Se Ha Modificiado Correctamnete");
                Recargar();


            }
            catch (Exception x)
            {

                MessageBox.Show("Error Al Conectar A La Base De Datos" + Environment.NewLine + x);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty) { return; }
            if (textBox1.Text == "0") { return; }
            textBox1.Text.ToLower();
            if (textBox1.Text == "clienteid"){return;}

            
            if (textBox1.Text.Contains("c") || textBox1.Text.Contains("a") || textBox1.Text.Contains("e") || textBox1.Text.Contains("b")) { return; }
            string eliminar = "delete from prueba where ClienteId=" + textBox1.Text;

            if (fn.Eliminar(eliminar))
            {
                int wtf = 2;
                geimer(wtf);
            }
            else
            {
                MessageBox.Show("Coloca El ClientId A Borrar");
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = contador;
            if(progressBar1.Value == 100)
            {
                timer1.Stop();
                MessageBox.Show(funcion, "¡Perfecto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar();
                Recargar();
                guardardato();
                contador = 0;
            }
            else
            {
                contador++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
            if (textBox1.Text == string.Empty) { Recargar(); return; }
            if (textBox3.Text == string.Empty) { Recargar(); return; }
 

            dataGridView1.DataSource = fn.MostrarUsuarioPorId("select * from prueba where ClienteId=" + textBox1.Text);

            
            
            if(dataGridView1.Rows.Count >= 1)
            {

                
                return;
            }
            else
            {
                dataGridView1.DataSource = fn.MostrarUsuarioPorId("select * from prueba where Ip='" + textBox3.Text + "'");
                

                if(dataGridView1.Rows.Count >= 1)
                {
                    
                    return;
                }
                else
                {
                    dataGridView1.DataSource = fn.MostrarUsuarioPorId("select * from prueba where Facturacion='" + comboBox5.Text.ToString() + "'");
                    
                    if (dataGridView1.Rows.Count >= 1)
                    {
                        
                        return;
                    }
                    else
                    {
                        dataGridView1.DataSource = fn.MostrarUsuarioPorId("select * from prueba where Nodo='" + comboBox6.Text.ToString() + "'");
                        if (dataGridView1.Rows.Count >= 1)
                        {
                            
                            return;
                        }
                        else
                        {
                            MessageBox.Show("No Se Encontro Ningun Dato", "No Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Recargar();
                        }
                        
                    }
                    
                }
            }
 
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyValue.ToString() == "82")
            {
                Recargar(); return;
            }


            if(e.KeyValue.ToString() == "81")
            {
                verificacion_datos();

                if (subirdatos == false) { return; }


                string agregar = "insert into prueba values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + comboBox1.Text.ToString() + "', '" + textBox3.Text + "', '" + textBox6.Text + "', '" + textBox4.Text + "', '" + comboBox2.Text.ToString() +
                     "', '" + comboBox3.Text.ToString() + "', '" + comboBox4.Text.ToString() + "', '" + comboBox5.Text.ToString() + "', '" + comboBox6.Text.ToString() + "', '" + textBox5.Text + "', '" + textBox7.Text + "', '" + comboBox7.Text.ToString() + "', '" + hora_inicial + "', '" + hora_Finalizada +
                     "')";
                if (fn.Insertar(agregar))
                {
                    int wtf = 1;
                    geimer(wtf);
                    subirdatos = false;
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }

            if(e.KeyValue.ToString() == "75" & Instance.Opacity == 0.85)
            {
                Instance.Opacity = 1;  return;
            }
            else if(e.KeyValue.ToString() == "75" & Instance.Opacity == 1)
            {
                Instance.Opacity = 0.85; return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExportarDatos(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2.Instance.textBox1.Text = Form2.Instance.textBox1.Text + Environment.NewLine + dataGridView1.CurrentCell.Value.ToString();


         
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            busqueda.Show();
            frm.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                int contador = 1;

                while (contador <= dataGridView1.Rows.Count)
                {
                    MySqlConnection conexion = createConnection();
                    MySqlCommand cmd = conexion.CreateCommand();
                    cmd.CommandText = "select * from prueba where ClienteId = @ClienteId";
                    conexion.Open();
                    cmd.Parameters.AddWithValue("@ClienteId", contador);
                    MySqlDataReader registro = cmd.ExecuteReader();
                    if (registro.Read())
                    {
                        string fechalimite = registro["Vencimiento"].ToString();
                        DateTime FechaLimite2;
                        FechaLimite2 = Convert.ToDateTime(fechalimite);

                        if(DateTime.Now.Date > FechaLimite2.Date)
                        {
                            MessageBox.Show("El Cliente Numero " + registro["ClienteId"].ToString() + " No Ha Pagado");
                            conexion.Close();
                            
                            MySqlCommand geimer = conexion.CreateCommand();
                            geimer.CommandText = "update prueba set Vencimiento = @Vencimiento where ClienteId=" + contador;
                           
                            conexion.Open();
                            geimer.Parameters.AddWithValue("@Vencimiento", "VENCIDO");
                            geimer.ExecuteNonQuery();
                            conexion.Close();
                        }

                        
                        
                    }
                    contador++;
                }
                MessageBox.Show("Termino De Buscar Clientes Que No Pagaron");

            }
            catch (Exception x)
            {

                MessageBox.Show("ERROR GEIMER" + x);

            }
        }
    }
}
