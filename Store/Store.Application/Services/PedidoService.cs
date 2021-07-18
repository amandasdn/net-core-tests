using Store.Domain.Entities;
using Store.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using static Store.Domain.Entities.Pedido;

namespace Store.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private List<Pedido> _pedidos;

        public PedidoService()
        {
            List<PedidoItem> itens = new()
            {
                new PedidoItem(new Produto("Teclado", 100), 2),
                new PedidoItem(new Produto("Mouse", 50), 1)
            };

            _pedidos = new()
            {
                new Pedido(Guid.Parse("0C28DE05-E125-4B81-8A47-CA179C4B10A5"), itens)
            };
        }

        public bool AdicionarItem(PedidoItem item, Guid pedidoId)
        {
            if(_pedidos.Any(p => p.Id == pedidoId))
            {
                _pedidos.FirstOrDefault(p => p.Id == pedidoId).AdicionarItem(item);
                return true;
            }

            var pedido = PedidoFactory.Rascunho(new Cliente());
            pedido.AdicionarItem(item);
            return true;
        }

        public Pedido GetPedido(Guid id)
            => _pedidos.FirstOrDefault(p => p.Id == id);

        public List<Pedido> ListarPedidos()
            => _pedidos;
    }
}
