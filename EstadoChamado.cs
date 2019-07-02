using MaisSistemas.abax.Core.Domain.DomChamado;
using MaisSistemas.abax.Core.Domain.DomChamadoStatus;
using MaisSistemas.abax.Infraestrutura.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaisSistemas.abax.Core.TelegramBot.IntegradorBanco;

namespace MaisSistemas.abax.Core.TelegramBot
{
    abstract class EstadoChamado : Estado
    {
        protected EstadoChamado(Chat contexto) : base(contexto)
        {
        }

        public override List<string> FazerTransicao(string mensagem)
        {
            return ObterResposta(mensagem);
        }

        protected override bool ValidarResposta(string resposta)
        {
            return true;
        }

        protected override void SalvarDados(string resposta)
        {
            //
        }

        protected string MensagemRespostaInvalida { get; set; } = "Resposta Inválida. Digite uma opção válida por favor.";

        protected virtual List<string> ObterResposta(string resposta)
        {
            if (ValidarResposta(resposta))
            {
                ProximoEstado(resposta);
                return Contexto.Estado.Mensagens;
            }
            else
            {
                return new string[] { this.MensagemRespostaInvalida }.ToList();
            }
        }

        protected abstract void ProximoEstado(string resposta);

        protected virtual void PularParaEstadoPesquisaSatisfacao()
        {
            Contexto.Estado = new EstadoPesquisaSatisfacaoNota(Contexto, Contexto.Estado.Mensagens);
        }
    }
}
