using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sistema
{
    public class Estoque
    {
        StringBuilder StringBuilders = new StringBuilder();
        SqlCommand SqlCommand = new SqlCommand();
        DataTable consulta = new DataTable();

        private string stringConnection = @"Data Source=DESKTOP-PRRVL3A\SERVIÇO_SQL;Initial Catalog=deposito_bebidas;Integrated Security=True";

        //Salvar estoque
        public void gravar(string nomeProduto,string tipoProduto, string marca, string quantidade, string valor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "INSERT INTO Estoque(NOME_PRODUTO,TIPO, MARCA, QUANTIDADE,VALOR) VALUES (@nomeProduto,@tipoProduto, @marca ,@quantidade, @valor)";
                    StringBuilders.Append(script);
                    SqlCommand.Parameters.Add(new SqlParameter("@nomeProduto", nomeProduto));
                    SqlCommand.Parameters.Add(new SqlParameter("@tipoProduto", tipoProduto));
                    SqlCommand.Parameters.Add(new SqlParameter("@marca", marca));
                    SqlCommand.Parameters.Add(new SqlParameter("@quantidade", quantidade));
                    SqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    SqlCommand.ExecuteNonQuery();
                    


                }
            }
            catch (Exception)
            {

                Console.WriteLine("Erro para salvar");
            }
        }


        //Atualiza estoque
        public void atualizar(int codigo,string nomeProduto, string tipoProduto, string marca, string quantidade, string valor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "UPDATE Estoque SET NOME_PRODUTO = @nomeProduto,TIPO = @tipoProduto, MARCA = @marca, QUANTIDADE = @quantidade ,VALOR = @valor WHERE ID = @id";
                    StringBuilders.Append(script);
                    SqlCommand.Parameters.Add(new SqlParameter("@id", codigo));
                    SqlCommand.Parameters.Add(new SqlParameter("@nomeProduto", nomeProduto));
                    SqlCommand.Parameters.Add(new SqlParameter("@tipoProduto", tipoProduto));
                    SqlCommand.Parameters.Add(new SqlParameter("@marca", marca));
                    SqlCommand.Parameters.Add(new SqlParameter("@quantidade", quantidade));
                    SqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    SqlCommand.ExecuteNonQuery();



                }
            }
            catch (Exception)
            {

                Console.WriteLine("Erro para salvar");
            }
        }

        //SELECT ALL
        public DataTable consultaEstoque()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "SELECT * FROM Estoque";
                    StringBuilders.Append(script);
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;
                   


                }
            }
            catch (Exception)
            {
                return null;
                Console.WriteLine("Erro de consulta");
            }
        }

        //delete 
        public DataTable deletarEstoqueId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "DELETE  FROM Estoque WHERE ID = @id";
                    StringBuilders.Append(script);

                    SqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;



                }
            }
            catch (Exception)
            {
                return null;
                Console.WriteLine("Erro de Delete");
            }
        }


        //SELECT por Nome
        public DataTable consultaEstoqueNome(string nomeProduto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "SELECT * FROM Estoque WHERE (NOME_PRODUTO  LIKE '%' + @nomeProduto + '%')";
                    StringBuilders.Append(script);

                    SqlCommand.Parameters.Add(new SqlParameter("@nomeProduto", nomeProduto));

                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;



                }
            }
            catch (Exception)
            {
                return null;
                Console.WriteLine("Erro de consulta");
            }
        }

        //select por tipo
        //SELECT por Nome
        public DataTable consultaEstoqueTipo(string tipoProduto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "SELECT * FROM Estoque WHERE (TIPO  LIKE '%' + @tipoProduto + '%')";
                    StringBuilders.Append(script);

                    SqlCommand.Parameters.Add(new SqlParameter("@tipoProduto", tipoProduto));

                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;



                }
            }
            catch (Exception)
            {
                return null;
                Console.WriteLine("Erro de consulta");
            }
        }

        //select por marca
        public DataTable consultaEstoqueMarca(string marca)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "SELECT * FROM Estoque WHERE (MARCA  LIKE '%' + @marca + '%')";
                    StringBuilders.Append(script);

                    SqlCommand.Parameters.Add(new SqlParameter("@marca", marca));

                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;



                }
            }
            catch (Exception)
            {
                return null;
                Console.WriteLine("Erro de consulta");
            }
        }



        //Atualiza estoque
        public void updateEstoqueQuantidade(int codigo,  string quantidade)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "UPDATE Estoque SET  QUANTIDADE = @quantidade WHERE ID = @id";
                    StringBuilders.Append(script);
                    SqlCommand.Parameters.Add(new SqlParameter("@id", codigo));
                    SqlCommand.Parameters.Add(new SqlParameter("@quantidade", quantidade));
                   
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    SqlCommand.ExecuteNonQuery();



                }
            }
            catch (Exception)
            {

                Console.WriteLine("Erro para salvar");
            }
        }




    }
}
