using System;

namespace Store.Domain.Entities
{
    public class Produto
    {
        public Produto(string nome, double valor)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Valor = valor;
        }

        // Apenas para testes:
        public Produto(string nome, double valor, Guid id)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
        }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public double Valor { get; private set; }
    }
}
