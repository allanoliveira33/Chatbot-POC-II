using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoComentario : EstadoChamado
    {
        public EstadoChamadoComentario(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Descreva o seu problema/melhoria");
        }

        protected override void ProximoEstado(string resposta)
        {
            Contexto.Chamado.Comentario = resposta;
            Contexto.Estado = new EstadoChamadoExemplo(Contexto); 
        }
    }
}
