using MySqlX.XDevAPI;
using System;
using System.Runtime.InteropServices;

namespace WebApplication_C.Classes
{
    /// <summary>
    /// Classe com todos os métodos dos produtos do sistema
    /// </summary>
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public float Valor { get; set; }
        public float Peso { get; set; }
        public float Largura { get; set; }
        public float Altura { get; set; }
        public float Profundidade { get; set; }

        /// <summary>
        /// Construtor completo de produtos
        /// </summary>
        /// <param name="Nome"></param>
        /// <param name="Quantidade"></param>
        /// <param name="Categoria"></param>
        /// <param name="Descricao"></param>
        /// <param name="Imagem"></param>
        /// <param name="Valor"></param>
        /// <param name="Peso"></param>
        /// <param name="Largura"></param>
        /// <param name="Altura"></param>
        /// <param name="Profundidade"></param>
        public Produto(string Nome, int Quantidade, int Categoria, string Descricao, string Imagem, float Valor, float Peso, float Largura, float Altura, float Profundidade)
        {
            this.Id = 0;
            this.Nome = Nome;
            this.Quantidade = Quantidade;
            this.Categoria = Categoria;
            this.Descricao = Descricao;
            this.Imagem = Imagem;
            this.Valor = Valor;
            this.Peso = Peso;
            this.Altura = Altura;
            this.Largura = Largura;
            this.Profundidade = Profundidade;
        }
        /// <summary>
        /// Construtor Completo com ID de produtos
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Nome"></param>
        /// <param name="Quantidade"></param>
        /// <param name="Categoria"></param>
        /// <param name="Descricao"></param>
        /// <param name="Imagem"></param>
        /// <param name="Valor"></param>
        /// <param name="Peso"></param>
        /// <param name="Largura"></param>
        /// <param name="Altura"></param>
        /// <param name="Profundidade"></param>
        public Produto(int Id, string Nome, int Quantidade, int Categoria, string Descricao, string Imagem, float Valor, float Peso, float Largura, float Altura, float Profundidade)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Quantidade = Quantidade;
            this.Categoria = Categoria;
            this.Descricao = Descricao;
            this.Imagem = Imagem;
            this.Valor = Valor;
            this.Peso = Peso;
            this.Altura = Altura;
            this.Largura = Largura;
            this.Profundidade = Profundidade;
        }

        /// <summary>
        /// Construtor básico de produto
        /// </summary>
        /// <param name="Nome"></param>
        /// <param name="Quantidade"></param>
        /// <param name="Categoria"></param>
        /// <param name="Valor"></param>
        public Produto(string Nome, int Quantidade, int Categoria, float Valor)
        {
            this.Id = 0;
            this.Nome = Nome;
            this.Quantidade = Quantidade;
            this.Categoria = Categoria;
            this.Descricao = "";
            this.Imagem = "";
            this.Valor = Valor;
            this.Peso = 0;
            this.Altura = 0;
            this.Largura = 0;
            this.Profundidade = 0;
        }

        /// <summary>
        /// Construtor sem parâmetros
        /// </summary>
        public Produto()
        {
            this.Id = 0;
            this.Nome = "";
            this.Quantidade = 0;
            this.Categoria = 0;
            this.Descricao = "";
            this.Imagem = "";
            this.Valor = 0;
            this.Peso = 0;
            this.Altura = 0;
            this.Largura = 0;
            this.Profundidade = 0;
        }
    }
}
