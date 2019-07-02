using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoTitulo : EstadoChamado
    {
        public EstadoChamadoTitulo(Chat Contexto) : base(Contexto)
        {
            this.Mensagens.Add("Digite um título para o chamado");
        }

        protected override void ProximoEstado(string resposta)
        {
            Contexto.Chamado.Titulo = resposta;
            Contexto.Estado = new EstadoChamadoComentario(Contexto);
        }
    }
}
