using System;
using System.Linq;
using Xunit;

namespace Store.Domain.Tests
{
    public class PedidoTests
    {
        private const string TraitName = "Categoria";
        private const string TraitValue = "Pedido Tests";

        [Fact(DisplayName = "Adicionar Item - Novo Pedido")]
        [Trait(TraitName, TraitValue)]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var pedidoItem = new PedidoItem(produto, 2);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item - Pedido Existente")]
        [Trait(TraitName, TraitValue)]
        public void Trocar_Nome_Metodo()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var pedidoItem1 = new PedidoItem(produto, 1);
            var pedidoItem2 = new PedidoItem(produto, 2);

            // Act
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.Itens.Count);
            Assert.Equal(3, pedido.Itens.FirstOrDefault(i => i.Produto.Id == produto.Id).Qtd);
        }
    }
}
