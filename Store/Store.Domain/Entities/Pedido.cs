using Store.Core.DomainObjects;
using Store.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Domain.Entities
{
    public class Pedido
    {
        private readonly List<PedidoItem> _itens;

        public Guid Id { get; private set; }

        public double ValorTotal { get; private set; }

        public EPedidoStatus PedidoStatus { get; private set; }

        public Cliente Cliente { get; private set; }

        public IReadOnlyCollection<PedidoItem> Itens => _itens;

        protected Pedido()
        {
            _itens = new List<PedidoItem>();
        }

        private void CalcularValorTotal() => ValorTotal = _itens.Sum(i => i.CalcularValor());

        public bool PedidoItemExistente(PedidoItem item) => _itens.Any(p => p.Produto.Id == item.Produto.Id);

        public void ValidarItemExistente(PedidoItem item)
        {
            if (!PedidoItemExistente(item)) throw new DomainException("O item não pertence ao pedido.");
        }

        private void ValidarQtdItem(PedidoItem item)
        {
            var quantidadeItens = item.Qtd;
            if (PedidoItemExistente(item))
            {
                var itemExistente = _itens.FirstOrDefault(p => p.Produto.Id == item.Produto.Id);
                quantidadeItens += itemExistente.Qtd;
            }

            if (quantidadeItens > Parametros.PEDIDO_MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {Parametros.PEDIDO_MAX_UNIDADES_ITEM} unidades por produto.");
        }

        public void AdicionarItem(PedidoItem item)
        {
            ValidarQtdItem(item);

            // Verifica se o item já existe no pedido.
            if (PedidoItemExistente(item))
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

        public void AtualizarItem(PedidoItem item)
        {
            ValidarItemExistente(item);
            ValidarQtdItem(item);

            var itemExistente = Itens.FirstOrDefault(i => i.Produto.Id == item.Produto.Id);

            _itens.Remove(itemExistente);
            _itens.Add(item);

            CalcularValorTotal();
        }

        public void RemoverItem(PedidoItem item)
        {
            ValidarItemExistente(item);

            _itens.Remove(item);

            CalcularValorTotal();
        }

        #region Factory

        public void TornarRascunho()
        {
            PedidoStatus = EPedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido Rascunho(Cliente cliente)
            {
                var pedido = new Pedido
                {
                    Cliente = cliente,
                    Id = Guid.NewGuid()
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }

        #endregion



        // Apenas para testes:
        public Pedido(Guid id, List<PedidoItem> itens)
        {
            Id = id;
            PedidoStatus = EPedidoStatus.Rascunho;

            foreach(var i in itens)
            {
                AdicionarItem(i);
            }
        }
    }
}
