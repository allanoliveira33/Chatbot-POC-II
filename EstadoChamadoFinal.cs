using MaisSistemas.abax.Core.Service.ServiceChamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class EstadoChamadoFinal : EstadoChamado
    {
        private int ChamadoID { get; set; }

        public EstadoChamadoFinal(Chat Contexto) : base(Contexto)
        {
            SalvarDados("");
            this.Mensagens.Add($"Chamado criado! ID: {ChamadoID}");
        }

        protected override void ProximoEstado(string resposta)
        {
            //Não é necessário
        }

        protected override void SalvarDados(string resposta)
        {
            ChamadoID = Contexto.Chamado.SalvarChamadoModel();
            Contexto.Avaliacao.ChamadoAbaxId = ChamadoID;
        }
    }
}
