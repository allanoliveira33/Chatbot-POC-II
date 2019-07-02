using MaisSistemas.abax.Core.Domain.DomChamado;
using MaisSistemas.abax.Core.Domain.DomChamadoCategoria;
using MaisSistemas.abax.Core.Domain.DomChamadoStatus;
using MaisSistemas.abax.Core.Service.ServiceChamadoCategoria;
using MaisSistemas.abax.Core.Service.ServiceChamadoStatus;
using MaisSistemas.abax.Infraestrutura;
using MaisSistemas.abax.Infraestrutura.Configuracao;
using MaisSistemas.abax.Infraestrutura.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class IntegradorBancoAbax : IntegradorBanco
    {
        protected override TipoBanco Tipo => TipoBanco.Abax;

        public override List<ChamadoCategoria> RecuperarCategoriaDeChamado()
        {
            List<ChamadoCategoria> listaDeCategorias = new List<ChamadoCategoria>();
            ObterListaDeChamadoCategoria obterListaDeChamadoCategoria = new ObterListaDeChamadoCategoria(null, Infraestrutura.Enumerador.Enumerador.FONTEDEDADOS.CLOUD);
            obterListaDeChamadoCategoria.ApenasGerarInsert = true;
            obterListaDeChamadoCategoria.ApenasGerarInsert_COMID = false;
            obterListaDeChamadoCategoria.DrivePadrao = new MSSqlAdapter(null);
            obterListaDeChamadoCategoria.Execute();
            DataTable categoriasDt = GetDataChamadoCategoria(null, obterListaDeChamadoCategoria.ComandoInsert);
            foreach (DataRow row in categoriasDt.Rows)
            {
                listaDeCategorias.Add(new ChamadoCategoria(Convert.ToInt32(row["id"]))
                {
                    CODIGO = row["Codigo"].ToString(),
                    EXIGEEXEMPLO = row["ExigeExemplo"].ToString(),
                    NOME = row["Nome"].ToString()
                });
            }
            return listaDeCategorias;
        }

        public override List<ChamadoStatus> RecuperarStatusChamados()
        {
            List<ChamadoStatus> status = new List<ChamadoStatus>();
            ObterListaDeChamadoStatus obterListaDeChamadoStatus = new ObterListaDeChamadoStatus(null, Infraestrutura.Enumerador.Enumerador.FONTEDEDADOS.CLOUD);
            obterListaDeChamadoStatus.ApenasGerarInsert = true;
            obterListaDeChamadoStatus.ApenasGerarInsert_COMID = false;
            obterListaDeChamadoStatus.DrivePadrao = new MSSqlAdapter(null);
            obterListaDeChamadoStatus.Execute();
            DataTable statusDt = GetDataChamadoStatus(null, obterListaDeChamadoStatus.ComandoInsert);
            foreach (DataRow row in statusDt.Rows)
            {
                status.Add(new ChamadoStatus(Convert.ToInt32(row["id"]))
                {
                    CODIGO = row["Codigo"].ToString(),
                    FLUXO = row["Fluxo"].ToString(),
                    NOME = row["Nome"].ToString(),
                    USUARIORESPONSAVEL = row["UsuarioResponsavel"].ToString()
                });
            }
            return status;
        }

        private DataTable GetDataChamadoStatus(DeployProgram DeployProgram, string sql)
        {
            AbaxAdmOuMSSqlCloud AbaxAdmOuMSSqlCloud = new AbaxAdmOuMSSqlCloud(DeployProgram);
             return AbaxAdmOuMSSqlCloud.GetData(sql, 9999);
        }

        private DataTable GetDataChamadoCategoria(DeployProgram DeployProgram, string sql)
        {
            AbaxAdmOuMSSqlCloud AbaxAdmOuMSSqlCloud = new AbaxAdmOuMSSqlCloud(DeployProgram);
            return AbaxAdmOuMSSqlCloud.GetData(sql, 9999);
        }
    }
}
