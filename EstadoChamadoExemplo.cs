using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoExemplo : EstadoChamado
    {
        public EstadoChamadoExemplo(Chat contexto) : base(contexto)
        {
            this.Mensagens.Add("Caso você tenha um exemplo de nota, pode digitar o ID ou só digitar: -");
            this.MensagemRespostaInvalida = "Você deve digitar um número ou -. Tente novamente.";
        }

        protected override void ProximoEstado(string resposta)
        {
            if (!resposta.Equals("-"))
            {
                Contexto.Chamado.Exemplo = resposta;
            }

            Contexto.Estado = new EstadoChamadoPrioridade(Contexto);
        }

        protected override bool ValidarResposta(string resposta)
        {
            if (int.TryParse(resposta, out int respostaConvertida))
            {
                return true;
            }
            else if (resposta.Equals("-"))
            {
                return true;
            }
            return false;
        }
    }
}
