using System;

namespace Store.Domain
{
    public class PedidoItem
    {
        public PedidoItem(Produto produto, int qtd)
        {
            Id = Guid.NewGuid();
            Produto = produto;
            Qtd = qtd;
        }

        public Guid Id { get; }

        public Produto Produto { get; set; }

        public int Qtd { get; private set; }

        public void AdicionarUnidade(int unidades) => Qtd += unidades;

        internal double CalcularValor() => Produto.Valor * Qtd;
    }
}
