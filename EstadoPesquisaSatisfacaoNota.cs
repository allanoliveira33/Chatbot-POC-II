using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPesquisaSatisfacaoNota : EstadoPesquisaSatisfacao
    {
        public EstadoPesquisaSatisfacaoNota(Chat Contexto, List<string> MensagensEstadoAnterior) : base(Contexto)
        {
            this.Mensagens.AddRange(MensagensEstadoAnterior);
            this.Mensagens.Add("Para finalizarmos, vou pedir que você avalie o atendimento. Dê uma nota de 1 a 5.");
            this.MensagemRespostaInvalida = "Nota inválida. Digite uma nota entra e 1 e 5 por favor.";            
        }

        protected override void ProximoEstado(string mensagem)
        {
            Contexto.Avaliacao.SatisfacaoNota = Convert.ToInt32(mensagem);
            Contexto.Estado = new EstadoPesquisaSatisfacaoComentario(Contexto);
        }

        protected override bool ValidarResposta(string resposta)
        {
            if(int.TryParse(resposta, out int respostaConvertida))
            {
                if(respostaConvertida >= 1 && respostaConvertida <= 5)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
