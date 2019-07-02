using MaisSistemas.abax.Core.Domain.DomChamadoCategoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoInicial : EstadoChamado
    {
        public EstadoChamadoInicial(Chat Contexto, List<string> MensagensAnteriores = null) : base(Contexto)
        {
            this.Mensagens.AddRange(MensagensAnteriores ?? new List<string>());
            this.Mensagens.Add("Favor, digite a opção correspondente à categoria do chamado");
            int contador = 0;
            string MensagemListaCategorias = "";
            foreach (string categoria in Contexto.Chamado.CategoriasNome)
            {
                MensagemListaCategorias += $"{contador + 1} - {categoria}</br>";
                contador++;
            }
            this.Mensagens.Add(MensagemListaCategorias);
        }

        public EstadoChamadoInicial(Chat Contexto) : base(Contexto)
        {
            Contexto.Avaliacao.Prioridade = Contexto.Avaliacao.Prioridade;
            this.Mensagens.Add("Favor, digite a opção correspondente à categoria do chamado");
            int contador = 0;
            string MensagemListaCategorias = "";
            foreach (string categoria in Contexto.Chamado.CategoriasNome)
            {
                MensagemListaCategorias += $"{contador + 1} - {categoria}</br>";
                contador++;
            }
            this.Mensagens.Add(MensagemListaCategorias);
        }

        protected override void ProximoEstado(string resposta)
        {
            Contexto.Chamado.Categoria = resposta;
            Contexto.Estado = new EstadoChamadoTitulo(Contexto);
        }

        protected override bool ValidarResposta(string resposta)
        {
            if (int.TryParse(resposta, out int respostaConvertida))
            {
                if (respostaConvertida > 0 && respostaConvertida <= Contexto.Chamado.CategoriasNome.Count)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
