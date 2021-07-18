using Microsoft.AspNetCore.Mvc;
using Store.Domain.Result;
using System;

namespace Store.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ActOk(this ControllerBase controllerBase, string message = null)
        {
            var resposta = new Resposta<object>(null);

            resposta.SetStatus(
                true,
                string.IsNullOrEmpty(message) ? "Operação realizada com sucesso." : message,
                null
            );

            return controllerBase.StatusCode(200, resposta);
        }

        public static IActionResult ActOk<T>(this ControllerBase controllerBase, T content, string message = null)
        {
            var resposta = new Resposta<T>(content);

            resposta.SetStatus(
                true,
                string.IsNullOrEmpty(message) ? "Operação realizada com sucesso." : message,
                null
            );

            return controllerBase.StatusCode(200, resposta);
        }

        public static IActionResult ActBadRequest(this ControllerBase controllerBase, string message = null)
        {
            var resposta = new Resposta<object>(null);

            resposta.SetStatus(
                false,
                string.IsNullOrEmpty(message) ? "Requisição inválida." : message,
                null
            );

            return controllerBase.StatusCode(400, resposta);
        }

        public static IActionResult ActInternalServerError(this ControllerBase controllerBase, Exception exception, string message = null)
        {
            var resposta = new Resposta<object>(null);

            resposta.SetStatus(
                false,
                string.IsNullOrEmpty(message) ? "Ocorreu um erro interno." : message,
                exception?.Message
            );

            return controllerBase.StatusCode(500, resposta);
        }
    }
}
