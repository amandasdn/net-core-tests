using Newtonsoft.Json;
using System;

namespace Store.Domain.Result
{
    public class Resposta<T>
    {
        public Resposta(T conteudo)
        {
            Data = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Conteudo = conteudo;
        }

        public string Data { get; private set; }

        public bool Sucesso { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Conteudo { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Mensagem { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MensagemDesenvolvedor { get; private set; }

        public void SetStatus(bool sucesso, string mensagem, string mensagemDesenvolvedor)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            MensagemDesenvolvedor = mensagemDesenvolvedor;
        }
    }
}
