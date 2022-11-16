using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstagiariosWebApp.Models
{
    public class SetorModel
    {
        public static List<SetorModel> setorLista = new List<SetorModel>();
        public SetorModel()
        {
        }

        public SetorModel (
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

        public static implicit operator string (SetorModel setor)
            => $"{setor.NomeSetor}, {setor.ChefeSetor}, {setor.CapacidadeSetor}";

        public static implicit operator SetorModel (string line)
        {
            var data = line.Split(separator: ",");

            return new SetorModel(
                data[0],
                data[1],
                int.Parse(data[2])
                );
        }
    }
}


