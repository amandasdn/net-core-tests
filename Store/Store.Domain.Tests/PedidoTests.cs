using Store.Core.DomainObjects;
using Store.Domain.Entities;
using System.Linq;
using Xunit;

namespace Store.Domain.Tests
{
    public class PedidoTests
    {
        private const string TraitName = "Unit Tests";
        private const string TraitValue = "Domain: Pedido";

        [Fact(DisplayName = "Adicionar Item - Novo Pedido")]
        [Trait(TraitName, TraitValue)]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var item = new PedidoItem(produto, 2);

            // Act
            pedido.AdicionarItem(item);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item - Pedido Existente")]
        [Trait(TraitName, TraitValue)]
        public void AdicionarItemPedido_PedidoExistente_DeveAcrescentarUnidades()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var item1 = new PedidoItem(produto, 1);
            var item2 = new PedidoItem(produto, 2);

            // Act
            pedido.AdicionarItem(item1);
            pedido.AdicionarItem(item2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.Itens.Count);
            Assert.Equal(3, pedido.Itens.FirstOrDefault(i => i.Produto.Id == produto.Id).Qtd);
        }

        [Fact(DisplayName = "Atualizar Item - Pedido Inexistente")]
        [Trait(TraitName, TraitValue)]
        public void AtualizarItemPedido_ItemNaoExistente_RetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var itemAtualizado = new PedidoItem(produto, 5);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(itemAtualizado));
        }

        [Fact(DisplayName = "Atualizar Item - Pedido Válido")]
        [Trait(TraitName, TraitValue)]
        public void AtualizarItemPedido_ItemValido_AtualizarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var item = new PedidoItem(produto, 2);
            pedido.AdicionarItem(item);
            var itemAtualizado = new PedidoItem(produto, 5);
            var novaQuantidade = itemAtualizado.Qtd;

            // Act
            pedido.AtualizarItem(itemAtualizado);

            // Assert
            Assert.Equal(novaQuantidade, pedido.Itens.FirstOrDefault(i => i.Produto.Id == produto.Id).Qtd);
        }

        [Fact(DisplayName = "Atualizar Item - Validar Valor Total")]
        [Trait(TraitName, TraitValue)]
        public void AtualizarItemPedido_PedidoProdutosDiferentes_AtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto1 = new Produto("Teste 1", 100);
            var itemExistente1 = new PedidoItem(produto1, 2);
            var itemExistente2 = new PedidoItem(new Produto("Teste 2", 5), 3);
            pedido.AdicionarItem(itemExistente1);
            pedido.AdicionarItem(itemExistente2);

            var itemAtualizado = new PedidoItem(produto1, 5);
            var valorTotalPedido = itemAtualizado.Qtd * itemAtualizado.Produto.Valor
                                 + itemExistente2.Qtd * itemExistente2.Produto.Valor;

            // Act
            pedido.AtualizarItem(itemAtualizado);

            // Assert
            Assert.Equal(valorTotalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item - Acima do máximo de unidades", Skip = "Não é mais necessário, a Exception já está sendo tratada em outro teste.")]
        [Trait(TraitName, TraitValue)]
        public void AtualizarItemPedido_ItemAcimaMaximoUnidades_RetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste 1", 100);
            var itemExistente = new PedidoItem(produto, 2);
            pedido.AdicionarItem(itemExistente);

            var itemAtualizado = new PedidoItem(produto, Parametros.PEDIDO_MAX_UNIDADES_ITEM + 1);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(itemAtualizado));
        }

        [Fact(DisplayName = "Remover Item - Pedido Inexistente")]
        [Trait(TraitName, TraitValue)]
        public void RemoverItemPedido_ItemNaoExistente_RetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste", 100);
            var item = new PedidoItem(produto, 5);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(item));
        }

        [Fact(DisplayName = "Remover Item - Validar Valor Total")]
        [Trait(TraitName, TraitValue)]
        public void RemoverItemPedido_PedidoProdutosDiferentes_AtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste 1", 100);
            var item1 = new PedidoItem(produto, 2);
            var item2 = new PedidoItem(new Produto("Teste 2", 5), 3);
            pedido.AdicionarItem(item1);
            pedido.AdicionarItem(item2);

            var valorTotalPedido = item1.Qtd * item1.Produto.Valor;

            // Act
            pedido.RemoverItem(item2);

            // Assert
            Assert.Equal(valorTotalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Remover Item - Pedido Inexistente")]
        [Trait(TraitName, TraitValue)]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste 1", 100);
            var pedidoItemRemover = new PedidoItem(produto, 2);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItemRemover));
        }


        [Fact(DisplayName = "Remover Item - Deve Calcular Valor Total")]
        [Trait(TraitName, TraitValue)]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.Rascunho(new Cliente());
            var produto = new Produto("Teste 2", 100);
            var pedidoItem1 = new PedidoItem(new Produto("Teste 1", 100), 2);
            var pedidoItem2 = new PedidoItem(produto, 3);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var totalPedido = pedidoItem2.Qtd * pedidoItem2.Produto.Valor;

            // Act
            pedido.RemoverItem(pedidoItem1);

            // Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }
    }
}
