using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPesquisaSatisfacaoComentario : EstadoPesquisaSatisfacao
    {
        public EstadoPesquisaSatisfacaoComentario(Chat contexto) : base(contexto)
        {
            this.Mensagens.Add("Agora deixe um comentário/sugestão quanto ao atendimento, caso queira " +
                "(você pode digitar qualquer coisa, mas ficamos agradecidos com seu feedback):");
        }

        protected override void ProximoEstado(string mensagem)
        {
            Contexto.Avaliacao.SatisfacaoComentario = mensagem;
            Contexto.ReiniciarChat();
            Contexto.finalizado = true;
        }

        protected override bool ValidarResposta(string resposta)
        {
            return base.ValidarResposta(resposta);
        }

        protected override List<string> ObterResposta(string mensagem)
        {
            if (ValidarResposta(mensagem))
            {
                ProximoEstado(mensagem);
                return new List<string>() { "Muito obrigado pelo feedback!", "Sinta-se livre para me chamar sempre que precisar!" };
            }
            return new List<string>() { MensagemRespostaInvalida };
        }
    }
}
