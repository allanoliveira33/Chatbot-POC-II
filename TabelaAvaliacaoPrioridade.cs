using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSistemas.abax.Core.TelegramBot
{
    class TabelaAvaliacaoPrioridade
    {
        public TabelaAvaliacaoPrioridade()
        {
            using (SqlConnection con = new SqlConnection("Server=smartnfe.database.windows.net;Database=SMARTNFECLOUD;User Id=smart.nfe;Password=Amor1010"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO avaliacaodeprioridade output INSERTED.ID VALUES(@Q1, @Q2, @Q3, @Q4, @Q5, @Q6, @prioridade, @idAbax, @idGlpi, @nota, @Qcomentario)", con))
                {
                    cmd.Parameters.AddWithValue("@Q1", Q1);
                    cmd.Parameters.AddWithValue("@Q2", Q2);
                    cmd.Parameters.AddWithValue("@Q3", Q3);
                    cmd.Parameters.AddWithValue("@Q4", Q4);
                    cmd.Parameters.AddWithValue("@Q5", Q5);
                    cmd.Parameters.AddWithValue("@Q6", Q6);
                    cmd.Parameters.AddWithValue("@prioridade", Prioridade);
                    cmd.Parameters.AddWithValue("@idAbax", ChamadoAbaxId);
                    cmd.Parameters.AddWithValue("@idGlpi", ChamadoGlpiId);
                    cmd.Parameters.AddWithValue("@nota", SatisfacaoNota);
                    cmd.Parameters.AddWithValue("@Qcomentario", SatisfacaoComentario);
                    con.Open();

                    Id = (int)cmd.ExecuteScalar();

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }

        private int id = 0;
        private string q1 = "";
        private string q2 = "";
        private string q3 = "";
        private string q4 = "";
        private string q5 = "";
        private string q6 = "";
        private int prioridade = 0;
        private int chamadoAbaxId = 0;
        private int chamadoGlpiId = 0;
        private int satisfacaoNota = 0;
        private string satisfacaoComentario = "";

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Q1
        {
            get { return q1; }
            set { q1 = value; }
        }

        public string Q2
        {
            get { return q2; }
            set { q2 = value; }
        }

        public string Q3
        {
            get { return q3; }
            set { q3 = value; }
        }

        public string Q4
        {
            get { return q4; }
            set { q4 = value; }
        }

        public string Q5
        {
            get { return q5; }
            set { q5 = value; }
        }

        public string Q6
        {
            get { return q6; }
            set { q6 = value; }
        }

        public int Prioridade
        {
            get { return prioridade; }
            set { Salvar(); prioridade = value; }
        }

        public int ChamadoAbaxId
        {
            get { return chamadoAbaxId; }
            set { Salvar(); chamadoAbaxId = value; }
        }

        public int ChamadoGlpiId
        {
            get { return chamadoGlpiId; }
            set { Salvar(); chamadoGlpiId = value; }
        }

        public int SatisfacaoNota
        {
            get { return satisfacaoNota; }
            set { Salvar(); satisfacaoNota = value; }
        }

        public string SatisfacaoComentario
        {
            get { return satisfacaoComentario; }
            set { Salvar(); satisfacaoComentario = value; }
        }

        private void Salvar()
        {
            using (SqlConnection con = new SqlConnection("Server=smartnfe.database.windows.net;Database=SMARTNFECLOUD;User Id=smart.nfe;Password=Amor1010"))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE avaliacaodeprioridade SET Q1 = @Q1, Q2 = @Q2, Q3 = @Q3, Q4 = @Q4, Q5 = @Q5, Q6 = @Q6, " +
                    "Prioridade = @prioridade, ChamadoAbaxId = @idAbax, ChamadoGlpiId = @idGlpi, SatisfacaoNota = @nota, SatisfacaoComentario = @Qcomentario " +
                    "WHERE Id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@Q1", Q1);
                    cmd.Parameters.AddWithValue("@Q2", Q2);
                    cmd.Parameters.AddWithValue("@Q3", Q3);
                    cmd.Parameters.AddWithValue("@Q4", Q4);
                    cmd.Parameters.AddWithValue("@Q5", Q5);
                    cmd.Parameters.AddWithValue("@Q6", Q6);
                    cmd.Parameters.AddWithValue("@prioridade", Prioridade);
                    cmd.Parameters.AddWithValue("@idAbax", ChamadoAbaxId);
                    cmd.Parameters.AddWithValue("@idGlpi", ChamadoGlpiId);
                    cmd.Parameters.AddWithValue("@nota", SatisfacaoNota);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Qcomentario", SatisfacaoComentario);
                    con.Open();

                    cmd.ExecuteNonQuery();

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }
    }
}
