using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConexaoBancoDeDados
{
    public class SqlController
    {
        public string? Connectionstring;
        public string? Table;
        private List<string> _fields = new List<string>();
        
        public SqlController(string connectionstring, string table)
        {
            Connectionstring = connectionstring;
            Table = table;            
            _fields = AllFields();
        }

        public List<string> Fields()
        {
            return _fields;
        }

        private List<string> AllFields()
        {
            string query = $"SELECT * FROM {Table};";
            
            List<string> lista = new List<string>();

            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();

                using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        
                        /* while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                lista.Add($"{reader.GetName(i)}");
                            }
                        } */
                        for (int i = 0; i < reader.FieldCount; i++)
                            {
                                lista.Add($"{reader.GetName(i)}");
                            }
                    }
                }
            }
        
        return lista;
        }

        public List<string> SelectAll(string whereCond = "")
        {
            string cond = whereCond;
            string query = $"SELECT * FROM {Table} {cond};";
                        
            List<string> listaClientes = new List<string>();

            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();

                using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                                                
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                listaClientes.Add($"{reader.GetName(i)}: {reader[i].ToString()}");
                            }                                                       
                            listaClientes.Add("_________________________________");
                        }
                    }
                }
            }

        return listaClientes;
        }

        public void Update(string? updateValue, string? field = "*", string? where = "")
        {
            string? campo = field;
            string? valor = updateValue;
            string? whr = where;
            string cond = whr != "" ? "WHERE " + whr : "";
            string query = $"UPDATE {Table} SET {campo} = '{valor}' {cond};";            

            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();

                using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                {
                    sqlcmd.ExecuteNonQuery();
                }
                
            }
        }

        public void Insert(List<String?> values)
        {
            List<string?> listavalores = values;
            List<string> lista = Fields();
            string query = $"INSERT iNTO {Table} ";
            string campos = " ";
            string valores = " ";
            for(int i = 0; i < lista.Count;i++)
            {
                if (i == lista.Count - 1)
                {
                    valores += $"'{listavalores[i]}' ";
                    campos += $"{lista[i]} ";
                }
                else
                {
                    campos += $"{lista[i]}, ";
                    valores += $"'{listavalores[i]}', ";
                }
            }
            query += $"({campos})";
            query += $"VALUES ({valores});";

            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();

                using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                {
                    sqlcmd.ExecuteNonQuery();
                }
                
            }

        }

        public void Delete(string where)
        {
            string cond = where;
            string query = $"DELETE FROM {Table} WHERE {cond}";

            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();

                using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                {
                    sqlcmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}
