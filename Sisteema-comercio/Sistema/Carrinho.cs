using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    public class Carrinho
    {
        StringBuilder StringBuilders = new StringBuilder();
        SqlCommand SqlCommand = new SqlCommand();
        DataTable consulta = new DataTable();

        private string stringConnection = @"Data Source=DESKTOP-PRRVL3A\SERVIÇO_SQL;Initial Catalog=deposito_bebidas;Integrated Security=True";

        //Salvar no carrinho
        public void addNoCarrinho(string nomeProduto, string quantidade, string valor, int id_produto, string quantidade_estoque_anterior)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "INSERT INTO Carrinho(NOME_PRODUTO,QUANTIDADE, VALOR, CODIGO, ESTOQUE_PRODUTO) VALUES (@nomeProduto,@quantidade, @valor ,@id_produto,@quantidade_estoque_anterior)";
                    StringBuilders.Append(script);
                    SqlCommand.Parameters.Add(new SqlParameter("@nomeProduto", nomeProduto));
                    SqlCommand.Parameters.Add(new SqlParameter("@quantidade", quantidade));
                    SqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                    SqlCommand.Parameters.Add(new SqlParameter("@id_produto", id_produto));
                    SqlCommand.Parameters.Add(new SqlParameter("@quantidade_estoque_anterior", quantidade_estoque_anterior));
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


        //Consulta Carrinho
        public DataTable consultaCarrinho()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "SELECT * FROM  Carrinho";
                    StringBuilders.Append(script);
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();
                    
                    consulta.Load(SqlCommand.ExecuteReader());
                    return consulta;

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro para consultar o carrinho");
                return null;
            }
        }



        //Limpar carrinho
        public void limparCarrinho()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "DELETE  FROM  Carrinho";
                    StringBuilders.Append(script);
                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();

                    SqlCommand.ExecuteNonQuery();
                    

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro para DELETAR o carrinho");
                
            }
        }


        public void deletarPorID(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))
                {
                    connection.Open();
                    var script = "DELETE  FROM  Carrinho WHERE ID = @id";
                    StringBuilders.Append(script);
                    SqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    SqlCommand.Connection = connection;
                    SqlCommand.CommandText = StringBuilders.ToString();

                    SqlCommand.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro para DELETAR o carrinho");

            }
        }

    }
}
