using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Extensions;
using Store.Domain.Result;
using Store.Interface.Services;
using System;

namespace Store.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [ProducesResponseType(typeof(Resposta<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<object>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult GetProduto()
        {
            try
            {
                var result = _produtoService.ListarProdutos();

                return this.ActOk(result);
            }
            catch(Exception e)
            {
                return this.ActInternalServerError(e);
            }
        }
    }
}
