using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoAnexos : EstadoChamado
    {
        public EstadoChamadoAnexos(Chat contexto) : base(contexto)
        {
            this.Mensagens.Add("Coloque um comentário para seus anexos (se não houver anexos, apenas diga \"pula\").");
            this.Mensagens.Add("Mas não se esqueça de anexar os arquivos.");
            this.MensagemRespostaInvalida = "Parece que você não anexou nada... Se não tiver anexos apenas diga \"pula\".";
        }

        protected override void ProximoEstado(string resposta)
        {
            Contexto.Chamado.ComentarioAnexo = resposta;
            Contexto.Chamado.Anexos = Contexto.Arquivos;
            this.Contexto.Estado = new EstadoChamadoFinal(Contexto);
            PularParaEstadoPesquisaSatisfacao();
        }

        protected override bool ValidarResposta(string resposta)
        {
            if (resposta.Trim().ToUpper().Equals("PULA"))
            {
                return true;
            }
            else if(Contexto.Arquivos.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
