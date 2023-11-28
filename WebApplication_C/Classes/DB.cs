using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using WebApplication_C.Classes;
using System.Runtime.ConstrainedExecution;
using System.Web.Helpers;

namespace WebApplication_C.Classes
{
    public static class DB
    {
        private static MySqlConnection conn = null;
        private static string server = "";
        private static string database = "";
        private static string uid = "";
        private static string password = "";


        //Constructor
        static DB()
        {
            Initialize();
        }

        /// <summary>
        /// Initializar as configurações de conexão com o banco de dados
        /// </summary>
        private static void Initialize()
        {
            //server = "localhost";
            server = "10.200.116.197";
            //database = "connectcsharptomysql";
            database = "loja-online";
            //uid = "username";
            uid = "admin";
            //password = "password";
            password = "senai";
            string myConnectionString;
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                //conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Abrir a conexão com o banco de dados
        /// </summary>
        /// <returns></returns>
        private static bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }

        }

        /// <summary>
        /// Fechar a conexão com o Banco de dados
        /// </summary>
        /// <returns></returns>
        private static bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //Metodos SCRUD para a tabela Usuarios

        /// <summary>
        /// Inserir um Usuario no Banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        public static void Insert_Usuario(Usuario usuario)
        {
            string query = "INSERT INTO Usuarios (cpf,nome,sobrenome,senha,email,genero,enderecoRua,cep,telefone,admin,enderecoNumero,enderecoComplemento) VALUES('" + usuario.CPF + "','" + usuario.Nome + "','" + usuario.Sobrenome + "','" + usuario.Senha + "','" + usuario.Email + "','" + usuario.Genero + "','" + usuario.EnderecoRua + "','" + usuario.CEP + "','" + usuario.Telefone + "','" + usuario.Admin + "','" + usuario.EnderecoNumero + "','" + usuario.EnderecoComplemento + "')";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Atualizar os dados de um Usuário no banco de dados
        /// </summary>
        /// <param name="usuario" ></param>
        public static void Update_usuário(Usuario usuario)
        {
            string query = "UPDATE Usuarios SET  nome = '" + usuario.Nome + "',sobrenome = '" + usuario.Sobrenome + "',senha = '" + usuario.Senha + "','" + usuario.Email + "',genero = '" + usuario.Genero + "', enderecoRua = '" + usuario.EnderecoRua + "', cep = '" + usuario.CEP + "', telefone = '" + usuario.Telefone + "', admin = '" + usuario.Admin + "', enderecoNumero = '" + usuario.EnderecoNumero + "', enderecoComplemento = '" + usuario.EnderecoComplemento + "' WHERE cpf='" + usuario.CPF + "'";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = conn;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Excluir um Usuário do Banco de Dados
        /// </summary>
        /// <param name="cpf"></param>
        public static void DeleteUsuario(long cpf)
        {
            string query = "DELETE FROM Usuarios WHERE cpf='" + cpf + "'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        /// <summary>
        /// Retorna o Id(tipo long) do usuário que se autenticou (Retorna "0" para erro de login)
        /// </summary>
        /// <returns></returns>
        /// <param name="CPF">Identificação do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        public static long Login_Usuarios(string CPF, string senha)
        {
            string query = "SELECT cpf FROM Usuarios WHERE cpf='"+CPF+"' AND senha = '"+senha+"'";

            //Cria a verificação de login
            long login = 0;

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    login = long.Parse(dataReader["cpf"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return login;
            }
            else
            {
                return login;
            }
        }


        /// <summary>
        /// Retorna os dados de todos os Usuarios do DB em uma Lista
        /// </summary>
        /// <returns></returns>
        public static List<Usuario> Lista_Usuarios()
        {
            string query = "SELECT * FROM Usuarios";

            //Create a list to store the result
            List<Usuario> Usuarios = new List<Usuario>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    bool tem;
                    Boolean adm = Boolean.TryParse(dataReader["Admin"] + "", out tem);
                    Usuario Usuario = new Usuario(long.Parse(dataReader["cpf"] + ""), dataReader["Nome"] + "", dataReader["Sobrenome"] + "", dataReader["Senha"] + "", dataReader["Email"] + "", dataReader["Genero"] + "", dataReader["EnderecoRua"] + "", int.Parse(dataReader["EnderecoNumero"] + ""), dataReader["EnderecoComplemento"] + "", long.Parse(dataReader["CEP"] + ""), long.Parse(dataReader["Telefone"] + ""), adm);
                    Usuarios.Add(Usuario);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return Usuarios;
            }
            else
            {
                return Usuarios;
            }
        }

        /// <summary>
        /// Retorna os dados de um Usuário do Banco de Dados
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>Usuario</returns>
        public static Usuario Busca_Usuario(long cpf)
        {
            string query = "SELECT * FROM Usuarios WHERE cpf = '" + cpf + "'";
            Usuario Usuario = new Usuario();
            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Usuario.CPF = long.Parse(dataReader["cpf"] + "");
                    Usuario.Nome = dataReader["nome"] + "";
                    Usuario.Sobrenome = dataReader["Sobrenome"] + "";
                    Usuario.Senha = dataReader["Senha"] + "";
                    Usuario.Email = dataReader["Email"] + "";
                    Usuario.Genero = dataReader["Genero"] + "";
                    Usuario.EnderecoRua = dataReader["EnderecoRua"] + "";
                    Usuario.EnderecoNumero = int.Parse(dataReader["EnderecoNumero"] + "");
                    Usuario.EnderecoComplemento = dataReader["EnderecoComplemento"] + "";
                    Usuario.CEP = long.Parse(dataReader["CEP"] + "");
                    Usuario.Telefone = long.Parse(dataReader["Telefone"] + "");
                    bool tem = false;
                    Boolean adm = Boolean.TryParse(dataReader["Admin"] + "", out tem);
                    Usuario.Admin = adm;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return Usuario;
            }
            else
            {
                return Usuario;
            }
        }

        /// <summary>
        /// Conta quantos Usuários estão cadastradas no Banco de dados
        /// </summary>
        /// <returns>Int</returns>
        public static int Count_Usuario()
        {
            string query = "SELECT Count(*) FROM Usuarios";
            int Count = -1;

            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        /// <summary>
        /// Buscar o CPF de um Usuário a partir do nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>string</returns>
        public static string Get_CPF_Usuario(string nome)
        {
            string query = "SELECT cpf FROM Usuarios " +
                "WHERE nome ='" + nome + "'";
            String cpf_Usuario = "";

            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                cpf_Usuario = cmd.ExecuteScalar() + "";

                //close Connection
                CloseConnection();

                return cpf_Usuario;
            }
            else
            {
                return cpf_Usuario;
            }
        }


        //Metodos SCRUD para a tabela Produtos

        /// <summary>
        /// Inserir um Produto no Banco de dados.
        /// </summary>
        /// <param name="produto"></param>
        public static void Insert_Produto(Produto produto)
        {
            string query = "INSERT INTO Produtos (quantidade,categoria,nome,descricao,valor,peso,largura,altura,profundidade,imagem) VALUES('" + produto.Quantidade + "','" + produto.Categoria + "','" + produto.Nome + "','" + produto.Descricao + "','" + produto.Valor + "','" + produto.Peso + "','" + produto.Largura + "','" + produto.Altura + "','" + produto.Profundidade + "','" + produto.Imagem + "')";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Atualizar os dados do produto no Banco de dados
        /// </summary>
        /// <param name="produto"></param>
        public static void Update_Produto(Produto produto)
        {
            string query = "UPDATE Produtos SET  quantidade = '" + produto.Quantidade + "', categoria = '" + produto.Categoria + "', nome = '" + produto.Nome + "', descricao = '" + produto.Descricao + "', valor = '" + produto.Valor + "', peso = '" + produto.Peso + "', largura = '" + produto.Largura + "', altura = '" + produto.Altura + "', profundidade = '" + produto.Profundidade + "', imagem = '" + produto.Imagem + "' WHERE id='" + produto.Id + "'";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = conn;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Atualiza a imagem de um produto no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imagem"></param>
        public static void Update_Produto_Imagem(int id, string imagem)
        {
            string query = "UPDATE Produtos SET imagem = '" + imagem + "' WHERE id='" + id + "'";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = conn;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Excluir um Produto do Banco de Dados
        /// </summary>
        /// <param name="cpf"></param>
        public static void DeleteProduto(int id)
        {
            string query = "DELETE FROM Produtos WHERE id='" + id + "'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        /// <summary>
        /// Retorna os dados de todos os produtos do DB em uma Lista
        /// </summary>
        /// <returns></returns>
        public static List<Produto> Lista_Produtos()
        {
            string query = "SELECT * FROM produtos";

            //Create a list to store the result
            List<Produto> produtos = new List<Produto>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Produto produto = new Produto(dataReader["Nome"] + "", int.Parse(dataReader["Quantidade"]+""), int.Parse(dataReader["Categoria"]+""), dataReader["Descricao"]+"", dataReader["Imagem"] + "", float.Parse(dataReader["Valor"]+""), float.Parse(dataReader["Peso"]+""), float.Parse(dataReader["Largura"]+""), float.Parse(dataReader["Altura"]+""), float.Parse(dataReader["Profundidade"]+""));
                    produtos.Add(produto);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return produtos;
            }
            else
            {
                return produtos;
            }
        }

        /// <summary>
        /// Retorna os dados de um produto do Banco de Dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Usuario</returns>
        public static Produto Busca_Produto_por_id(int id)
        {
            string query = "SELECT * FROM Produtos WHERE id = '" + id + "'";
            Produto produto = new Produto();
            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    produto.Id = int.Parse(dataReader["id"] + "");
                    produto.Quantidade = int.Parse(dataReader["quantidade"] + "");
                    produto.Categoria = int.Parse(dataReader["categoria"] + "");
                    produto.Nome = dataReader["nome"] + "";
                    produto.Descricao = dataReader["descricao"] + "";
                    produto.Valor = float.Parse(dataReader["valor"] + "");
                    produto.Peso = float.Parse(dataReader["peso"] + "");
                    produto.Largura = float.Parse(dataReader["largura"] + "");
                    produto.Altura = float.Parse(dataReader["altura"] + "");
                    produto.Profundidade = long.Parse(dataReader["profundidade"] + "");
                    produto.Imagem = dataReader["imagem"] + "";
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return produto;
            }
            else
            {
                return produto;
            }
        }

        /// <summary>
        /// Retorna os dados dos produtos que contenham a "busca" no nome
        /// </summary>
        /// <param name="busca"></param>
        /// <returns>List</Usuario></returns>
        public static List<Produto> Busca_Produto_por_Nome(String busca)
        {
            string query = "SELECT * FROM Produtos WHERE nome LIKE '%" + busca + "'";
            List<Produto> produtos = new List<Produto>();
            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Produto produto = new Produto(int.Parse(dataReader["id"] + ""), dataReader["nome"] + "", int.Parse(dataReader["quantidade"] + ""), int.Parse(dataReader["categoria"] + ""), dataReader["descricao"] + "", dataReader["imagem"] + "", float.Parse(dataReader["valor"] + ""),float.Parse(dataReader["peso"] + ""), float.Parse(dataReader["largura"] + ""), float.Parse(dataReader["altura"] + ""), long.Parse(dataReader["profundidade"] + ""));
                    produtos.Add(produto);
                }
                
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return produtos;
            }
            else
            {
                return produtos;
            }
        }

        /// <summary>
        /// Conta quantos produtos estão cadastradas no Banco de dados
        /// </summary>
        /// <returns>Int</returns>
        public static int Count_Produtos()
        {
            string query = "SELECT Count(*) FROM Produtos";
            int Count = -1;

            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

    }
}