using Store.Core.DomainObjects;
using System;

namespace Store.Domain.Entities
{
    public class PedidoItem
    {
        public PedidoItem(Produto produto, int qtd)
        {
            ValidarQuantidade(qtd);

            Id = Guid.NewGuid();
            Produto = produto;
            Qtd = qtd;
        }

        public Guid Id { get; }

        public Produto Produto { get; set; }

        public int Qtd { get; private set; }

        public void AdicionarUnidade(int unidades) => Qtd += unidades;

        internal double CalcularValor() => Produto.Valor * Qtd;

        private void ValidarQuantidade(int qtd)
        {
            if (qtd < Parametros.MIN_UNIDADES_ITEM) throw new DomainException($"Mínimo de {Parametros.MIN_UNIDADES_ITEM} unidade(s) por item.");
            if (qtd > Parametros.MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {Parametros.MAX_UNIDADES_ITEM} unidades por item.");
        }
    }
}
