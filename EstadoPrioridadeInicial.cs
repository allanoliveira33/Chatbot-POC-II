using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoPrioridadeInicial : EstadoPrioridade
    {
        public EstadoPrioridadeInicial(Chat contexto) : base(contexto)
        {
            Mensagens.AddRange(new string[] {
                "Bem Vindo ao Chat Abax. Sou um robô que irá te ajudar a reportar seu problema!",
                "Vamos fazer assim: Vou te fazer algumas perguntas de 'sim' ou 'não', mas caso você não saiba responder, basta dizer 'pula'. =D",
                "Vamos Começar?",
                "Você deseja propor uma nova melhoria ou funcionalidade?"
            });
        }

        protected override Prioridade PrioridadeApontada => Prioridade.Nenhuma;

        protected override void ProximoEstadoSim()
        {
            Contexto.Avaliacao.Q1 = "S";
            Contexto.Estado = new EstadoChamadoInicial(Contexto);
        }

        protected override void ProximoEstadoNao()
        {
            Contexto.Avaliacao.Q1 = "N";
            Contexto.Estado = new EstadoPrioridadeP0(Contexto);
        }

        protected override void ProximoEstadoPula()
        {
            Contexto.Avaliacao.Q1 = "P";
            Contexto.Estado = new EstadoPrioridadeP0(Contexto);
        }
    }
}
