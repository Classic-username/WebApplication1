using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace WebApplication1.Extensions
{
    public class SqliteQueries
    {
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataReader reader;

        public void CreateDB()
        {
            if (!File.Exists("semanticdatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("semanticdatabase.sqlite");
                string sql = "CREATE TABLE Example(ID INTEGER PRIMARY KEY AUTOINCREMENT, Value TEXT);";
                conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
                conn.Open();
                cmd = new SQLiteCommand(sql, conn);//This is one way to handle passing commands
                cmd.ExecuteNonQuery();
                conn.Close();
            } else
            {
                conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            }
        }

        public void AddValues(string value)
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            //this is another
            cmd.Connection = conn;
            cmd.CommandText = $"INSERT INTO Example(Value) VALUES ('{value}')";
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public string GetSingle(int ID)
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT Value FROM Example WHERE ID={ID}";
            reader = cmd.ExecuteReader();

            string val = "";

            while (reader.Read())
            {
                val = reader.GetString(0);
            }

            conn.Close();

            return val;
        }

        public List<string> GetAll()
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT Value FROM Example";
            reader = cmd.ExecuteReader();

            List<string> val = new List<string>();

            while (reader.Read())
            {
                val.Add(reader.GetString(0));
            }

            conn.Close();

            return val;
        }

        public void UpdateValue(int ID, string value)
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE Example SET Value='{value}' WHERE ID={ID}";
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void DeleteValue(int ID)
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"DELETE FROM Example WHERE ID={ID}";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
