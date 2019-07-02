using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeMelhoria : EstadoPrioridade
    {
        public EstadoPrioridadeMelhoria(Chat Contexto) : base(Contexto)
        {
            Mensagens.Add("Okay. Iremos abrir um chamado para registrar sua melhoria. Okay?");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Melhoria;


        protected override List<string> ObterResposta(string resposta)
        {
            SalvarDados("");
            this.PularParaEstadoChamado();
            return Contexto.Estado.Mensagens;
        }

        protected override void ProximoEstadoNao()
        {
            //Não é necessário
        }

        protected override void ProximoEstadoPula()
        {
            //Não é necessário
        }

        protected override void ProximoEstadoSim()
        {
            //Não é necessário
        }
    }
}
