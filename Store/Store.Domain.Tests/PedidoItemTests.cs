using Store.Core.DomainObjects;
using Store.Domain.Entities;
using Xunit;

namespace Store.Domain.Tests
{
    public class PedidoItemTests
    {
        private const string TraitName = "Unit Tests";
        private const string TraitValue = "Domain: Pedido Item";

        [Fact(DisplayName = "Novo Pedido Item - Abaixo do mínimo de unidades")]
        [Trait(TraitName, TraitValue)]
        public void NovoItemPedido_ItemAbaixoMinimoUnidades_RetornarException()
        {
            // Arrange
            var produto = new Produto("Teste", 100);

            // Act | Assert
            Assert.Throws<DomainException>(() => new PedidoItem(produto, Parametros.PEDIDO_MIN_UNIDADES_ITEM - 1));
        }

        [Fact(DisplayName = "Novo Pedido Item - Acima do máximo de unidades")]
        [Trait(TraitName, TraitValue)]
        public void NovoItemPedido_ItemAcimaMaximoUnidades_RetornarException()
        {
            // Arrange
            var produto = new Produto("Teste", 100);

            // Act | Assert
            Assert.Throws<DomainException>(() => new PedidoItem(produto, Parametros.PEDIDO_MAX_UNIDADES_ITEM + 1));
        }
    }
}
