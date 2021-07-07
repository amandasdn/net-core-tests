using Store.Core.DomainObjects;
using Store.Domain.Entities;
using Xunit;

namespace Store.Domain.Tests
{
    public class PedidoItemTests
    {
        private const string TraitName = "Unit Tests";
        private const string TraitValue = "Pedido Item";

        [Fact(DisplayName = "Adicionar Item - Abaixo do mínimo de unidades")]
        [Trait(TraitName, TraitValue)]
        public void AdicionarItemPedido_ItemAbaixoMinimoUnidades_RetornarException()
        {
            // Arrange
            var produto = new Produto("Teste", 100);

            // Act | Assert
            Assert.Throws<DomainException>(() => new PedidoItem(produto, Parametros.MIN_UNIDADES_ITEM - 1));
        }

        [Fact(DisplayName = "Adicionar Item - Acima do máximo de unidades")]
        [Trait(TraitName, TraitValue)]
        public void AdicionarItemPedido_ItemAcimaMaximoUnidades_RetornarException()
        {
            // Arrange
            var produto = new Produto("Teste", 100);

            // Act | Assert
            Assert.Throws<DomainException>(() => new PedidoItem(produto, Parametros.MAX_UNIDADES_ITEM + 1));
        }
    }
}
