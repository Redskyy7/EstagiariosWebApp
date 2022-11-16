using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCLI.Data.Entities
{
    public class Setor
    {
        public Setor()
        {
        }

        public Setor (
            string nomeSetor,
            string chefeSetor,
            int capacidadeSetor
            )
            {
                NomeSetor = nomeSetor;
                ChefeSetor = chefeSetor;
                CapacidadeSetor = capacidadeSetor;
            }
        public string NomeSetor { get; set; }
        public string ChefeSetor { get; set; } 
        public int CapacidadeSetor { get; set; }

        public static implicit operator string (Setor setor)
            => $"{setor.NomeSetor},{setor.ChefeSetor},{setor.CapacidadeSetor}";

        public static implicit operator Setor (string line)
        {
            var data = line.Split(separator: ",");

            return new Setor(
                data[0],
                data[1],
                int.Parse(data[2])
                );
        }
    }
}


