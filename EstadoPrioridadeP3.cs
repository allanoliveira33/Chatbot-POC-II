using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeP3 : EstadoPrioridade
    {
        public EstadoPrioridadeP3(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Okay! Já tenho as informações que precisava.");
            this.Mensagens.Add("Vamos abrir um chamado para tratar o seu problema. Okay?");
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Alta;

        protected override List<string> ObterResposta(string resposta)
        {
            //SalvarDados("");
            //this.PularParaEstadoChamado();
            //return Contexto.Estado.Mensagens;
            return null;
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
