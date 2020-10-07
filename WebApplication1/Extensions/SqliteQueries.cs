using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using WebApplication1.DTO;
using System.Data.Entity.ModelConfiguration.Configuration;

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

        public void CreateTableByArgument(AddNewTableDTO dto)
        {
            string sql = $"CREATE TABLE {dto.TblName}(ID INTEGER PRIMARY KEY AUTOINCREMENT";
            for(int i = 0; i < dto.Column.Count; i++)
            {
                sql += $", {dto.Column[i]} {dto.DataType[i].ToUpper()}";
            }
            sql += ");";
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            conn.Open();
            cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
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

        public DBDTO GetSingle(int ID)
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT Value, ID FROM Example WHERE ID={ID}";
            reader = cmd.ExecuteReader();

            var dto = new DBDTO();

            while (reader.Read())
            {
                dto.Value = reader.GetString(0);
                dto.ID = reader.GetInt32(1);
            }

            conn.Close();

            return dto;
        }

        public List<DBDTO> GetAll()
        {
            conn = new SQLiteConnection("Data Source=semanticdatabase.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT Value, ID FROM Example";
            reader = cmd.ExecuteReader();

            List<DBDTO> valid = new List<DBDTO>();

            while (reader.Read())
            {
                valid.Add(new DBDTO() 
                { 
                    ID = reader.GetInt32(1),
                    Value = reader.GetString(0)
                });
            }

            conn.Close();

            return valid;
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
