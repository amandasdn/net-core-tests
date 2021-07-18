using Store.Domain.Entities;
using Store.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private List<Produto> _produtos = new()
        {
            new Produto("Ursinho de Pelúcia", 40, Guid.Parse("4B914540-1B71-4B7D-B163-E65472CF1A9A")),
            new Produto("Moldura Porta Retrato", 10, Guid.Parse("2C495D9A-E01D-4DE6-B4E4-D8530C65F66A")),
            new Produto("Carregador de Celular", 50, Guid.Parse("6DDA02CF-DCE7-4C72-9E80-8AE39DB3E1AC")),
            new Produto("Xícara", 30, Guid.Parse("FBA3EDC1-4503-4E2E-BF96-2A22892498AC")),
            new Produto("Travesseiro", 100, Guid.Parse("F751E3A8-529F-4982-892D-1308B0CD9BE8"))
        };

        public Produto GetProduto(Guid id)
            => _produtos.FirstOrDefault(p => p.Id == id);

        public List<Produto> ListarProdutos()
            => _produtos.OrderBy(p => p.Nome).ToList();
    }
}
