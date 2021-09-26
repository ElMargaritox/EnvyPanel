using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EnvyPanel
{
    class Funciones
    {
        MySqlConnection conexion = new MySqlConnection("SERVER=IP; DATABASE=DATABASE_NAME; UID=ROOT_NAME; USERNAME=USERNAME; PASSWORD=PASSWORD; PORT=PORT;");
        private MySqlCommand cmd;
        private MySqlCommandBuilder cmbuilder;
        private MySqlDataAdapter da;
        private DataSet ds;
        private DataTable dt;

        public bool Conectar()
        {
            bool conectado = false;

            try
            {
                conexion.Open();
                conectado = true;
            }
            catch (MySqlException ex)
            {
                conectado = false; System.Windows.Forms.MessageBox.Show("ERROR " + ex);
            }
            finally
            {
                conexion.Close(); 
            }

            return conectado;
        }

        public bool Insertar(string consulta)
        {
            bool agregado = false;
            int rows = 0;

            conexion.Open();

            cmd = new MySqlCommand(consulta, conexion);
            rows = cmd.ExecuteNonQuery();

            if(rows > 0)
            {
                agregado = true;
            }

            conexion.Close();


            return agregado;
        }

        public bool Actualizar(string consulta)
        {
            bool actualizado = false;
            int rows = 0;

            conexion.Open();
            cmd = new MySqlCommand(consulta, conexion);
            rows = cmd.ExecuteNonQuery();

            if(rows > 0)
            {
                actualizado = true;
            }

            conexion.Close();

            return actualizado;
        }

        public DataTable LlenarGrid(string consulta)
        {
            conexion.Open();
            cmd = new MySqlCommand(consulta, conexion);
            da = new MySqlDataAdapter(cmd);

            dt = new DataTable();

            da.Fill(dt);

            conexion.Close();

            return dt;
        }


        public bool Eliminar(string consulta)
        {
            bool eliminado = false;
            int rows = 0;

            conexion.Open();
            cmd = new MySqlCommand(consulta, conexion);
            rows = cmd.ExecuteNonQuery();

            if(rows > 0)
            {
                eliminado = true;
            }


            conexion.Close();


            return eliminado;
        }

        public DataTable MostrarUsuarioPorId(string consulta)
        {
            conexion.Open();
            cmd = new MySqlCommand(consulta, conexion);
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable(); 
            da.Fill(dt);
            conexion.Close();
            return dt;
        }
    }
}
