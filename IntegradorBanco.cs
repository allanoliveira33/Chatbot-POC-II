using MaisSistemas.abax.Core.Domain.DomChamadoCategoria;
using MaisSistemas.abax.Core.Domain.DomChamadoStatus;
using MaisSistemas.abax.Core.Service.ServiceChamadoCategoria;
using MaisSistemas.abax.Core.Service.ServiceChamadoStatus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    public abstract class IntegradorBanco
    {
        public struct Categoria
        {
            public int Id;
            public string Nome;
            public string Exemplo;
        }

        public enum TipoBanco
        {
            Abax,
            GLPI
        }

        protected abstract TipoBanco Tipo { get; }

        private static List<IntegradorBanco> ListaDeIntegradores { get; } = new List<IntegradorBanco>(new IntegradorBanco[] {
            new IntegradorBancoAbax(),
            //new IntegradorBancoGLPI()
        });

        public static IntegradorBanco GetIntegrador(TipoBanco banco)
        {
            return ListaDeIntegradores.Single(integrador => integrador.Tipo == banco) ?? new IntegradorBancoAbax();
        }

        public abstract List<ChamadoStatus> RecuperarStatusChamados();

        public abstract List<ChamadoCategoria> RecuperarCategoriaDeChamado();
    }
}
