using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP1 : EstadoPrioridade
    {
        public EstadoPrioridadeP1(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Você está incapaz de terminar a atividade que está fazendo? (Mesmo que tenha dados errados)");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Alta;

        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q3 = "S";
            Contexto.Estado = new EstadoPrioridadeP2(Contexto);
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q3 = "N";
            Contexto.Estado = new EstadoPrioridadeP1Alt(Contexto);
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q3 = "P";
            Contexto.Estado = new EstadoPrioridadeP2(Contexto);
        }
    }
}
