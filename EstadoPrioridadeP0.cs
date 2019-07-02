using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP0 : EstadoPrioridade
    {
        public EstadoPrioridadeP0(Chat contexto) : base(contexto)
        {
            this.Mensagens.Add("O sistema está inoperante?");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Urgente;

        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q2 = "S";
            Contexto.Estado = new EstadoChamadoInicial(Contexto);
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q2 = "N";
            Contexto.Estado = new EstadoPrioridadeP1(Contexto);
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q2 = "P";
            Contexto.Estado = new EstadoPrioridadeP1(Contexto);
        }
    }
}
