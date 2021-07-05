using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Domain
{
    public class Pedido
    {
        private readonly List<PedidoItem> _itens;

        public double ValorTotal { get; private set; }

        public EPedidoStatus PedidoStatus { get; private set; }

        public Cliente Cliente { get; private set; }

        public IReadOnlyCollection<PedidoItem> Itens => _itens;

        protected Pedido()
        {
            _itens = new List<PedidoItem>();
        }

        public void CalcularValorTotal()
        {
            ValorTotal = _itens.Sum(i => i.CalcularValor());
        }

        public void AdicionarItem(PedidoItem item)
        {
            // Verifica se o item já existe no pedido.
            if (_itens.Any(i => i.Produto.Id == item.Produto.Id))
            {
                // Caso exista, acrescenta unidade em sua quantidade.
                var itemExistente = _itens.FirstOrDefault(i => i.Produto.Id == item.Produto.Id);

                itemExistente.AdicionarUnidade(item.Qtd);
                item = itemExistente;

                _itens.Remove(itemExistente);
            }

            _itens.Add(item);

            CalcularValorTotal();
        }

        #region Status

        public void TornarRascunho()
        {
            PedidoStatus = EPedidoStatus.Rascunho;
        }

        #endregion

        public static class PedidoFactory
        {
            public static Pedido Rascunho(Cliente cliente)
            {
                var pedido = new Pedido
                {
                    Cliente = cliente
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
