using MaisSistemas.abax.Core.Domain.DomChamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaisSistemas.abax.Core.TelegramBot.EstadoPrioridade;
using static MaisSistemas.abax.Core.TelegramBot.IntegradorBanco;

namespace MaisSistemas.abax.Core.TelegramBot
{
    public class Chat
    {
        public bool EsperandoArquivos
        {
            get
            {
                if (Estado is EstadoChamadoAnexos)
                {
                    return true;
                }
                return false;
            }
        }

        public bool BotIniciado
        {
            get
            {
                if (Estado is null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Finalizado
        {
            get { return finalizado; }
        }

        internal bool finalizado = false;

        internal Dictionary<string, string> Arquivos { get; set; } = new Dictionary<string, string>();

        private string Usuario { get; }

        internal TabelaAvaliacaoPrioridade Avaliacao { get; set; }

        internal ChamadoModel Chamado { get; set; }

        internal TipoBanco Banco { get; }

        public Chat(string Usuario, TipoBanco banco)
        {
            this.Banco = banco;
            this.Usuario = Usuario;
            this.Chamado = new ChamadoModel(TipoBanco.Abax, Usuario);
        }

        internal Estado Estado { get; set; }

        public List<string> ProximaIteracao(string mensagem)
        {
            try
            {
                return Estado.FazerTransicao(mensagem);
            }
            catch
            {
                throw;
            }
        }

        public List<string> IniciarBot()
        {
            IniciarAnalise();
            return Estado.IniciarEstado();
        }

        private void IniciarAnalise()
        {
            finalizado = false;
            Avaliacao = new TabelaAvaliacaoPrioridade();
            this.Estado = new EstadoPrioridadeInerte(this);
        }

        public void AnexarAqruivos(Dictionary<string, string> arquivos)
        {
            Arquivos = arquivos;
        }

        public void CancelarArquivos()
        {
            Arquivos = new Dictionary<string, string>();
        }

        public void ReiniciarChat()
        {
            this.Chamado = new ChamadoModel(TipoBanco.Abax, Usuario);
            CancelarArquivos();
            Avaliacao = null;
            Estado = null;
        }
    }
}
