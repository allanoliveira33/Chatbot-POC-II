using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeInerte : EstadoPrioridade
    {
        public EstadoPrioridadeInerte(Chat contexto) : base(contexto)
        {
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Nenhuma;

        protected override List<string> ObterResposta(string resposta)
        {
            Contexto.Estado = new EstadoPrioridadeInicial(Contexto);
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
