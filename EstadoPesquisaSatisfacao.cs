using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    abstract class EstadoPesquisaSatisfacao : Estado
    {
        protected EstadoPesquisaSatisfacao(Chat contexto) : base(contexto)
        {
        }

        protected string MensagemRespostaInvalida { get; set; } = "Resposta Inválida";

        public override List<string> FazerTransicao(string mensagem)
        {
            return ObterResposta(mensagem);
        }

        protected override void SalvarDados(string resposta)
        {
            //
        }

        protected override bool ValidarResposta(string resposta)
        {
            if (!string.IsNullOrWhiteSpace(resposta))
            {
                return true;
            }
            return false;
        }

        protected virtual List<string> ObterResposta(string mensagem)
        {
            if (ValidarResposta(mensagem))
            {
                ProximoEstado(mensagem);
                return Contexto.Estado.Mensagens;
            }
            else
            {
                return new string[] { MensagemRespostaInvalida }.ToList();
            }
        }

        protected abstract void ProximoEstado(string mensagem);
    }
}
