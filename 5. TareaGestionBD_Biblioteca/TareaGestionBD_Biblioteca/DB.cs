using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaGestionBD_Biblioteca
{
    public static class DB
    {
        // Ajusta aquí tu base de datos si no es "BibliotecaDB"
        public static readonly string ConnectionString =
            @"Server=JESUS-DIAZ\SQLEXPRESS;Database=Biblioteca;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
