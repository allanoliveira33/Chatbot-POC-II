using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP1Alt : EstadoPrioridade
    {
        public EstadoPrioridadeP1Alt(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Você consegue fazer o que está tentando de alguma outra forma?");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Alta;


        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q4 = "S";
            Contexto.Estado = new EstadoPrioridadeP2(Contexto);
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q4 = "N";
            Contexto.Estado = new EstadoChamadoInicial(Contexto);
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q4 = "P";
            Contexto.Estado = new EstadoPrioridadeP2(Contexto);
        }
    }
}
