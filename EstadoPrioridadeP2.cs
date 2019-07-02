using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP2 : EstadoPrioridade
    {
        public EstadoPrioridadeP2(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Os dados ficam errados no final? (Se não houver dados para conferir diga pula)");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Alta;

        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q5 = "S";
            Contexto.Estado = new EstadoPrioridadeP2Alt(Contexto);
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q5 = "N";
            Contexto.Estado = new EstadoPrioridadeP3(Contexto);
            this.PularParaEstadoChamado();
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q5 = "P";
            Contexto.Estado = new EstadoPrioridadeP3(Contexto);
            this.PularParaEstadoChamado();
        }
    }
}
