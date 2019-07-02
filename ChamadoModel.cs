using MaisSistemas.abax.Core.Domain.DomChamado;
using MaisSistemas.abax.Core.Domain.DomChamadoAnexos;
using MaisSistemas.abax.Core.Domain.DomChamadoCategoria;
using MaisSistemas.abax.Core.Domain.DomChamadoStatus;
using MaisSistemas.abax.Core.Service.ServiceChamado;
using MaisSistemas.abax.Core.Service.ServiceChamadoAnexos;
using MaisSistemas.abax.Infraestrutura;
using MaisSistemas.abax.Infraestrutura.Configuracao;
using MaisSistemas.abax.Infraestrutura.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaisSistemas.abax.Core.TelegramBot.IntegradorBanco;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class ChamadoModel
    {
        TipoBanco Banco { get; set; }

        public string Titulo { get; set; }

        public string Comentario { get; set; }

        public int Prioridade{ get; set; }

        public string ComentarioAnexo { get; set; }

        public string Categoria
        {
            get { return categoria; }
            set
            {
                categoria = value;
                if (Banco == TipoBanco.Abax) {
                    chamado.CHAMADOCATEGORIAIDCATEGORIA = Categorias[Convert.ToInt32(categoria) - 1];
                    chamado.EXEMPLO_TIPO = Categorias[Convert.ToInt32(categoria) - 1].EXIGEEXEMPLO;
                }
            }
        }

        private Dictionary<int, string> PrioridadesAbax { get; } = new Dictionary<int, string>() {
            {0, "Urgente" },
            {1, "Muito Alta" },
            {2, "Alta" },
            {3, "Média" },
            {4, "Baixa" },
            {5, "Nenhuma" }
        };

        public Dictionary<int, string> Prioridades
        {
            get
            {
                switch (Banco)
                {
                    case TipoBanco.Abax:
                        return PrioridadesAbax;
                    case TipoBanco.GLPI:
                        return PrioridadesGLPI;
                    default:
                        return null;
                }
            }
        }

        private Dictionary<int, string> PrioridadesGLPI { get; } = new Dictionary<int, string>();

        public string Exemplo { get; set; }

        private Chamado chamado = new Chamado(0)
        {
            CLIENTE = Config_Local.Cliente,
            TIPO = "C"
        };

        private List<ChamadoCategoria> Categorias { get; set; } = new List<ChamadoCategoria>();

        public List<string> CategoriasNome
        {
            get
            {
                switch (Banco)
                {
                    case TipoBanco.Abax:
                        return Categorias.Select(item => item.NOME).ToList();
                    case TipoBanco.GLPI:
                        return null;
                    default:
                        return null;
                }
            }
        }

        private string Usuario { get; }

        private string categoria;

        public Dictionary<string, string> Anexos { get; set; } = new Dictionary<string, string>();

        public ChamadoModel(TipoBanco banco, string usuario)
        {
            Banco = banco;
            Usuario = usuario;
        }

        public int SalvarChamadoModel()
        {
            switch (Banco)
            {
                case TipoBanco.Abax:
                    return SalvarChamadoAbax();
                case TipoBanco.GLPI:
                    return SalvarChamadoGLPI();
                default:
                    return 0;
            }
        }

        private int SalvarChamadoAbax()
        {
            chamado.TITULO = Titulo;
            chamado.COMENTARIO = Comentario;
            chamado.USUARIOCRIACAO = Usuario;
            chamado.PRIORIDADE = Prioridade.ToString();
            chamado.EXEMPLO_NOTA = Exemplo;
            chamado.DATACRIACAO = DateTime.Now;

            IntegradorBanco integrador = GetIntegrador(Banco);
            Categorias = integrador.RecuperarCategoriaDeChamado();
            chamado.CHAMADOSTATUSIDSTATUS = integrador.RecuperarStatusChamados().Single(item => item.ID == 8);

            SalvarChamado salvarChamado = new SalvarChamado(null, chamado, Infraestrutura.Enumerador.Enumerador.FONTEDEDADOS.CLOUD);
            salvarChamado.ApenasGerarInsert = true;
            salvarChamado.ApenasGerarInsert_COMID = false;
            salvarChamado.DrivePadrao = new MSSqlAdapter(null);
            salvarChamado.Execute();
            ExecutarComand(null, salvarChamado.ComandoInsert);

            foreach (KeyValuePair<string, string> file in Anexos)
            {
                ChamadoAnexos chamadoAnexos = new ChamadoAnexos(0, salvarChamado.Chamado, null, Usuario, ComentarioAnexo, DateTime.Now, file.Key);
                SalvarChamadoAnexos SalvarChamadoAnexos = new SalvarChamadoAnexos(null, chamadoAnexos, Infraestrutura.Enumerador.Enumerador.FONTEDEDADOS.ABAXINBOUND);
                SalvarChamadoAnexos.ApenasGerarInsert = true;
                SalvarChamadoAnexos.ApenasGerarInsert_COMID = false;
                SalvarChamadoAnexos.DrivePadrao = new MSSqlAdapter(null);
                SalvarChamadoAnexos.Execute();

                int sid = InserirDados(null, SalvarChamadoAnexos.ComandoInsert);

                AbaxAdmOuMSSqlCloud AbaxAdmOuMSSqlCloud1 = new AbaxAdmOuMSSqlCloud(null);
                AbaxAdmOuMSSqlCloud1.SalvarArquivo(null, "ChamadoAnexos", "Anexo", "ID", sid.ToString(), file.Value);
            }
            return salvarChamado.Chamado.ID;
        }

        private int InserirDados(DeployProgram DeployProgram, string sql)
        {
            AbaxAdmOuMSSqlCloud AbaxAdmOuMSSqlCloud = new AbaxAdmOuMSSqlCloud(DeployProgram);
            return AbaxAdmOuMSSqlCloud.InserirDados(sql, 1614);
        }

        private int SalvarChamadoGLPI()
        {
            return 0;
        }

        private void ExecutarComand(DeployProgram DeployProgram, string sql)
        {
            AbaxAdmOuMSSqlCloud AbaxAdmOuMSSqlCloud = new AbaxAdmOuMSSqlCloud(DeployProgram);
            AbaxAdmOuMSSqlCloud.ExecuteCommand(sql, 9999, new List<string>());
        }
    }
}
