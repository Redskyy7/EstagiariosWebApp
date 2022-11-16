global using CadastroCLI.Data.Entities;
using static CadastroCLI.ValidaCPF;

/* var estagiarios = new List<Estagiario> {
    new ("123.456.789-25", 40, "Antonio Vinicius Carvalho Rezende", Convert.ToDateTime("2002-10-29"), "Gestão de TI", "FANESE", "T.I"),
    new ("987.654.251-32", 60, "Clewerton Lancaster Santos", Convert.ToDateTime("2001-09-12"), "Direito", "UFS", "Jurídico"),
    };
*/

// CPF, CargaSemanal, Nome, DataNascimento, Curso, Instituição de Ensino, Setor

// Lista que Armazena os Estagiários
var estagiarios = new List<Estagiario>();

// NomeSetor, ChefeSetor, CapacidadeSetor

var setores = new List<Setor>();

// Formata o cpf para ao inves de ser por exemplo: 12345678900 ficar 123.456.789-00

string formatarCpf(string cpf) => cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);

// Variável que armazena todo o texto do arquivo

try
{
    var data = File.ReadLines("estagiariosData");

    // Cada linha percorrida pelo loop armazenará esse estagiário em uma variável temporária e irá o adicionar para a lista de estagiários


    foreach (var line in data)
    {
        Estagiario estagiarioTemp = line;
        estagiarios.Add(estagiarioTemp);
    }
}
catch (Exception error) 
{ }


try
{
    var data = File.ReadLines("setoresData");

    foreach (var line in data)
    {
        Setor setorTemp = line;
        setores.Add(setorTemp);
    }
}
catch (Exception error)
{ Console.WriteLine("Arquivo vazio ou com dados inválidos."); }

var op = 1;
var input = "";
bool flag = false;

while (op != 0)
{
    Console.WriteLine(
        "Digite o número da opção desejada (Os dados serão salvos após o fechamento do programa)\n\n" +
        "1 - Cadastrar Novo Estagiário\n" +
        "2 - Cadastrar Novo Setor\n" +
        "3 - Remover Estagiário\n" +
        "4 - Remover Setor\n" +
        "5 - Listar Estagiários\n" +
        "6 - Listar Setores\n" +
        "0 - Fechar o Programa\n"
        );
    input = Console.ReadLine();
    op = int.Parse(input);

    switch (op)
    {

        // Cadastra um Estagiário caso a opção escolhida seja 1
        case 1:
            Estagiario estagiarioTemp = new Estagiario();

            do
            {                
                Console.WriteLine("Digite o número do CPF do Estagiário");
                estagiarioTemp.Cpf = formatarCpf(Console.ReadLine().PadLeft(11, '0')); // Lê o cpf, completa com zeros a esquerda pra evitar problemas e formata pra colocar " . " e " - ".
                if (IsCpf(estagiarioTemp.Cpf) == true)
                    break;
                Console.WriteLine("CPF Inválido.");
            } while (true);
            
            do {
                Console.WriteLine("Digite a Carga Horária Semanal do Estagiário");
                if (int.TryParse(Console.ReadLine(), out int num)) {
                    estagiarioTemp.CargaSemanal = num;
                    break;
                }             
                Console.WriteLine("Carga Semanal Inválida.");
            } while (true);

            Console.WriteLine("Digite o Nome do Estagiário");
            estagiarioTemp.Nome = Console.ReadLine();

            do
            {
                Console.WriteLine("Digite a Data de Nascimento no Formato (AAAA-MM-DD)");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
                {
                    estagiarioTemp.DataNascimento = data;
                    break;
                }
                Console.WriteLine("Data Inválida");
            } while (true);
            Console.WriteLine("Digite o Curso de Graduação do Estagiário");
            estagiarioTemp.CursoGraduacao = Console.ReadLine();
            Console.WriteLine("Digite a Instituição de Ensino do Estagiário");
            estagiarioTemp.InstituicaoEnsino = Console.ReadLine();

            do
            {
                flag = false;
                Console.WriteLine("Digite o Setor que o Estagiário está alocado");
                estagiarioTemp.SetorAlocado = Console.ReadLine();
                foreach (Setor setor in setores)
                {
                    if (estagiarioTemp.SetorAlocado.Equals(setor.NomeSetor) == true)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                    break;
                Console.WriteLine("Setor especificado não existe. Lembre-se de checar a acentuação");
            } while (true);
            File.AppendAllText("estagiariosData", "\n" + estagiarioTemp);
            estagiarios.Add(estagiarioTemp);

            break;

        // Cadastra um Setor caso a opção escolhida seja 2
        case 2:
            Setor setorTemp = new Setor();
            Console.WriteLine("Digite o Nome do Setor");
            setorTemp.NomeSetor = Console.ReadLine();
            Console.WriteLine("Digite o Chefe do Setor");
            setorTemp.ChefeSetor = Console.ReadLine();
            do
            {
                Console.WriteLine("Digite a Capacidade do Setor");
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    setorTemp.CapacidadeSetor = num;
                    break;
                }
                Console.WriteLine("Capacidade Inválida");
            } while (true);
            setores.Add(setorTemp);
            File.AppendAllText("setoresData", "\n" + setorTemp);
            break;

        case 3:
            var cpfDeletar = "";
            var estagiarioDeletar = "";
            flag = false;
            var count = 0;
            do
            {
                Console.WriteLine("Digite o CPF do Estagiário que deseja remover. Sem pontuação Exemplo : 12345678900 ");
                cpfDeletar = formatarCpf(Console.ReadLine().PadLeft(11, '0')); // Lê o cpf, completa com zeros a esquerda pra evitar problemas e formata pra colocar " . " e " - ".
                if (IsCpf(cpfDeletar) == true)
                {
                    foreach (Estagiario estagiario in estagiarios)
                    {
                        if (cpfDeletar == estagiario.Cpf)
                        {
                            flag = true;
                            estagiarioDeletar = estagiario;
                            File.WriteAllLines("estagiariosData", File.ReadLines("estagiariosData").Where(l => l != estagiarioDeletar).ToList());
                            estagiarios.Remove(estagiario);
                            break;
                        }
                        count++;
                    }
                    if (flag == true) break;
                }
                Console.WriteLine("CPF Inválido. Tente remover a pontuação.");
            } while (true);

            break;

        case 4:
            var nomeSetorDeletar = "";
            var setorDeletar = "";
            flag = false;
            do
            {
                Console.WriteLine("Digite o Nome do Setor que deseja remover.");
                nomeSetorDeletar = Console.ReadLine().ToLower();
                foreach (Setor setor in setores)
                {
                    if (nomeSetorDeletar.Equals(setor.NomeSetor.ToLower()))
                    {
                        Console.WriteLine(nomeSetorDeletar + setor.NomeSetor + "\n" + setor);
                        flag = true;
                        setorDeletar = setor;
                        File.WriteAllLines("setoresData", File.ReadLines("setoresData").Where(l => l != setorDeletar).ToList());
                        setores.Remove(setor);
                        break;
                    }
                    
                }
                Console.WriteLine("O setor digitado não existe");
            } while (flag != true);



            break;

        case 5:
            foreach (Estagiario estagiario in estagiarios)
            {
                Console.WriteLine("CPF: " + estagiario.Cpf);
                Console.WriteLine("Carga Semanal: " + estagiario.CargaSemanal);
                Console.WriteLine("Nome: " + estagiario.Nome);
                Console.WriteLine("Data de Nascimento: " + estagiario.DataNascimento);
                Console.WriteLine("Curso de Graduação: " + estagiario.CursoGraduacao);
                Console.WriteLine("Instituição de Ensino: " + estagiario.InstituicaoEnsino);
                Console.WriteLine("Setor Alocado: " + estagiario.SetorAlocado);
                Console.WriteLine("--");
            }
            break;

        case 6:
            foreach (Setor setor in setores)
            {
                Console.WriteLine("Nome do Setor: " + setor.NomeSetor);
                Console.WriteLine("Chefe do Setor: " + setor.ChefeSetor);
                Console.WriteLine("Capacidade do Setor: " + setor.CapacidadeSetor);
                Console.WriteLine("--");
            }
            break;

        case 0:
            break;

        default:
            Console.WriteLine("Opção Inválida");
            break;


    }
}




// Escreve a lista de estagiários em um arquivo chamado "estagiariosData" e a lista de Setores em um arquivo chamado "setoresData"

// File.WriteAllLines("estagiariosData", estagiarios.Select(estagiario => (string)estagiario).ToList());
// File.WriteAllLines("setoresData", setores.Select(setor => (string)setor).ToList());





