using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    abstract class EstadoPrioridade : Estado
    {
        internal enum Prioridade : int
        {
             Melhoria = 6,
             Baixa = 4,
             Media = 3,
             Alta = 2,
             Urgente = 0,
             Nenhuma = 5
        }

        internal enum TipoRespostaMensagem
        {
            S = 0,
            N = 1,
            P = 2,
            I = 3
        }

        protected abstract Prioridade PrioridadeApontada { get; }

        protected virtual string MensagemRespostaInvalida { get; } = "A resposta não é válida. Favor responder com sim/não/pula.";

        private readonly List<string> respostaAceitasPositivas = new List<string>(new string[] {
            "SIM",
            "S",
            "Y",
            "YES"
        });

        private readonly List<string> respostaAceitasNegativas = new List<string>(new string[] {
            "NÃO",
            "NAO",
            "N",
            "NO"
        });

        private readonly List<string> respostaAceitasPula = new List<string>(new string[]
            {
                "pula",
                "P",
            });

        protected  EstadoPrioridade(Chat contexto) : base(contexto)
        {
            Contexto.Avaliacao.Prioridade = (int)this.PrioridadeApontada;
        }

        public override List<string> FazerTransicao(string mensagem)
        {
            return ObterResposta(mensagem);
        }

        protected virtual List<string> ObterResposta(string resposta)
        {
            if (ValidarResposta(resposta))
            {
                TipoRespostaMensagem tipo = TipoResposta(resposta);
                switch (tipo)
                {
                    case TipoRespostaMensagem.S:
                        ProximoEstadoSim();
                        break;
                    case TipoRespostaMensagem.N:
                        ProximoEstadoNao();
                        break;
                    case TipoRespostaMensagem.P:
                        ProximoEstadoPula();
                        break;
                    default:
                        ProximoEstadoErro();
                        break;
                }
                return Contexto.Estado.Mensagens;
            }
            else
            {
                return new string[] { this.MensagemRespostaInvalida }.ToList();
            }
        }

        protected virtual void PularParaEstadoChamado()
        {
            Contexto.Estado = new EstadoChamadoInicial(Contexto, this.Mensagens);
        }

        protected abstract void ProximoEstadoSim();

        protected abstract void ProximoEstadoNao();

        protected abstract void ProximoEstadoPula();

        protected virtual void ProximoEstadoErro()
        {
            //Esse estado não deveria ser chamado nunca. Teste de insanidade
        }

        protected override bool ValidarResposta(string resposta)
        {
            if (respostaAceitasPositivas.Any(item => item.ToUpper().Equals(resposta.ToUpper())) ||
                respostaAceitasNegativas.Any(item => item.ToUpper().Equals(resposta.ToUpper())) ||
                respostaAceitasNegativas.Any(item => item.ToUpper().Equals(resposta.ToUpper())))
            {
                return true;
            }
            return false;
        }

        protected override void SalvarDados(string resposta)
        {
            //Não precisa
        }

        protected virtual TipoRespostaMensagem TipoResposta(string resposta)
        {
            if (respostaAceitasPositivas.Any(item => item.ToUpper().Equals(resposta.ToUpper())))
            {
                return TipoRespostaMensagem.S;
            }
            else if (respostaAceitasNegativas.Any(item => item.ToUpper().Equals(resposta.ToUpper())))
            {
                return TipoRespostaMensagem.N;
            }
            else if (respostaAceitasPula.Any(item => item.ToUpper().Equals(resposta.ToUpper())))
            {
                return TipoRespostaMensagem.P;
            }
            return TipoRespostaMensagem.I;
        }
    }
}
