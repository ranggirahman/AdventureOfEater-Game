using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //tambahan utnuk bisa mengkoneksi ke database
using System.Windows.Forms; //tambahan

namespace TMDProvis
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private MySqlDataReader dataReader;

        //konstruktor
        public DBConnection()
        {
            server = "localhost";
            database = "adventure";
            username = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //mengambil data adapter
        public MySqlDataAdapter getAdapter()
        {
            return adapter;
        }

        //mengambil data adapter
        public MySqlDataReader getReader()
        {
            return dataReader;
        }

        //membuaka koneksi database
        public bool openConnection()
        {
            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                adapter = new MySqlDataAdapter(cmd);
                //MessageBox.Show("KOneksi Berhasil");
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0: //tidak dapat terkoneksi dengan server
                        MessageBox.Show("Tidak datap terkoneksi dengan server " + server);
                        break;
                    case 1045:  //username atau password tidak valid
                        MessageBox.Show("username atau password tidak valid");
                        break;
                }
                return false;
            }
        }

        //menutup koneksi database
        public bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //mengeksekusi query tanpa dimasukkan hasilnya ke reader
        public bool executeQuery(string query)
        {
            cmd.CommandText = query;
            //mengeksekusi query
            try
            {
                cmd.BeginExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show("Query Gagal");
                return false;
            }
        }

        //mengeksekusi query yang hasilnya dimsukkan hasilnya ke reader
        public bool executeQueryReader(string query)
        {
            cmd.CommandText = query;
            try
            {
                dataReader = cmd.ExecuteReader();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show("Query gagal!");
            }
            return false;
        }

        //mengembalikan hasil
        public List<string>[] getResult(int number)
        {
            List<string>[] list = new List<string>[number];
            for (int i=0; i<number; i++)
            {
                list[i] = new List<string>();
            }

            while (dataReader.Read())
            {
                for (int i=0; i<number; i++)
                {
                    for(int j=0; j<number; j++)
                    {
                        list[i].Add(dataReader.GetString(j) + "");
                    }
                }
            }

            //close Data reader
            dataReader.Close();

            return list;
        }
    }
}
