using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoPrioridade : EstadoChamado
    {
        public EstadoChamadoPrioridade(Chat Contexto) : base(Contexto)
        {
            string MensagemListaPrioridades = "";
            this.Mensagens.Add("Me informe a prioridade do seu chamado (na sua opinião)");
            foreach(KeyValuePair<int, string> prioridade in Contexto.Chamado.Prioridades)
            {
                MensagemListaPrioridades += $"{prioridade.Key} - {prioridade.Value}</br>";
            }
            this.Mensagens.Add(MensagemListaPrioridades);
        }

        protected override void ProximoEstado(string resposta)
        {
            Contexto.Chamado.Prioridade = Convert.ToInt32(resposta);
            Contexto.Estado = new EstadoChamadoAnexos(Contexto);
        }

        protected override bool ValidarResposta(string resposta)
        {
            if(int.TryParse(resposta, out int respostaConvertida))
            {
                if (Contexto.Chamado.Prioridades.Keys.Contains(respostaConvertida))
                {
                    return true;
                }
            }
            return false;
        }

        protected override List<string> ObterResposta(string resposta)
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
    }
}
