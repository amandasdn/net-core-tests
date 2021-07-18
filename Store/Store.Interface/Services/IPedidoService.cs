using Store.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Store.Interface.Services
{
    public interface IPedidoService
    {
        List<Pedido> ListarPedidos();

        Pedido GetPedido(Guid id);

        bool AdicionarItem(PedidoItem item, Guid pedidoId);
    }
}
