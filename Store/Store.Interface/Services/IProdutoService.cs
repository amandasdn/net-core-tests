using Store.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Store.Interface.Services
{
    public interface IProdutoService
    {
        List<Produto> ListarProdutos();

        Produto GetProduto(Guid id);
    }
}
