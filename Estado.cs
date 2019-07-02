using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    abstract class Estado
    {
        protected Estado(Chat contexto)
        {
            Contexto = contexto;
        }

        public Chat Contexto { get; }

        public virtual List<string> Mensagens { get; } = new List<string>();

        protected abstract void SalvarDados(string resposta);

        protected abstract bool ValidarResposta(string resposta);

        public abstract List<string> FazerTransicao(string mensagem);

        public List<string> IniciarEstado()
        {
            return Contexto.Estado.FazerTransicao("");
        }

        public void AtualizarPrioridadeDoContexto()
        {
        }
    }
}
