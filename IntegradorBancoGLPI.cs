using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaisSistemas.abax.Core.Domain.DomChamadoCategoria;
using MaisSistemas.abax.Core.Domain.DomChamadoStatus;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class IntegradorBancoGLPI : IntegradorBanco
    {
        protected override TipoBanco Tipo => TipoBanco.GLPI;

        public override List<ChamadoCategoria> RecuperarCategoriaDeChamado()
        {
            throw new NotImplementedException();
        }

        public override List<ChamadoStatus> RecuperarStatusChamados()
        {
            throw new NotImplementedException();
        }
    }
}
