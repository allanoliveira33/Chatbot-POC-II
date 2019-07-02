using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP2Alt : EstadoPrioridade
    {
        public EstadoPrioridadeP2Alt(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Tem alguma forma diferente de você fazer essa atividade que os dados estejam corretos, ou alguma forma de modificar eles corretamente?");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Alta;

        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q6 = "S";
            Contexto.Estado = new EstadoPrioridadeP3(Contexto);
            this.PularParaEstadoChamado();
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q6 = "N";
            Contexto.Estado = new EstadoChamadoInicial(Contexto);
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q6 = "P";
            Contexto.Estado = new EstadoPrioridadeP3(Contexto);
            this.PularParaEstadoChamado();
        }
    }
}
