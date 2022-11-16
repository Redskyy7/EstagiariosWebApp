using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCLI.Data.Entities
{
    public class Estagiario
    {
        public Estagiario()
        {
        }

        public Estagiario(
                string cpf,
                int cargaSemanal,
                string nome,
                DateTime dataNascimento,
                string cursoGraduacao,
                string instituicaoEnsino,
                string setorAlocado
            )
            {
                Cpf = cpf;
                CargaSemanal = cargaSemanal;
                Nome = nome;
                DataNascimento = dataNascimento;
                CursoGraduacao = cursoGraduacao;
                InstituicaoEnsino = instituicaoEnsino;
                SetorAlocado = setorAlocado;
            }

        public string Cpf { get; set; }

        public int CargaSemanal { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string CursoGraduacao { get; set; }

        public string InstituicaoEnsino { get; set; }

        public string SetorAlocado { get; set; }

        public static implicit operator string (Estagiario estagiario)
            => $"{estagiario.Cpf},{estagiario.CargaSemanal},{estagiario.Nome},{estagiario.DataNascimento.ToString("yyyy-MM-dd")},{estagiario.CursoGraduacao},{estagiario.InstituicaoEnsino},{estagiario.SetorAlocado}";

        public static implicit operator Estagiario (string line)
        {
            var data = line.Split(separator: ",");

            return new Estagiario(
                data[0],
                int.Parse(data[1]),
                data[2],
                data[3].ToDateTime(),
                data[4],
                data[5],
                data[6]
                );

        }

    }

    public static class StringExtension
    {
        public static DateTime ToDateTime (this string value)
        {
            var data = value.Split(separator: "-");
            return new DateTime(
                int.Parse(data[0]),
                int.Parse(data[1]),
                int.Parse(data[2]));
        }
    }
}
