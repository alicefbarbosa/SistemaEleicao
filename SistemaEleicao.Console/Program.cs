using SistemaEleicao.Console;

var eleicao = new EleicaoManager();

bool executando = true;

while (executando)
{
    System.Console.WriteLine("\n=== SISTEMA DE ELEIÇÃO ===");
    System.Console.WriteLine("1 - Cadastrar candidato");
    System.Console.WriteLine("2 - Registrar eleitor");
    System.Console.WriteLine("3 - Votar");
    System.Console.WriteLine("4 - Apurar resultados");
    System.Console.WriteLine("0 - Sair");
    System.Console.Write("Escolha uma opção: ");

    string? opcao = System.Console.ReadLine();

    switch (opcao)
    {
        case "1":
            System.Console.Write("Nome do candidato: ");
            string nome = System.Console.ReadLine() ?? "";

            System.Console.Write("Número do candidato: ");

            if (int.TryParse(System.Console.ReadLine(), out int numero))
            {
                var candidato = new Candidato(nome, numero);

                if (eleicao.CadastrarCandidato(candidato))
                {
                    System.Console.WriteLine("Candidato cadastrado com sucesso!");
                }
                else
                {
                    System.Console.WriteLine("Já existe um candidato com esse número.");
                }
            }
            else
            {
                System.Console.WriteLine("Número inválido.");
            }

            break;

        case "2":
            System.Console.Write("Número do título ou CPF simulado: ");
            string documento = System.Console.ReadLine() ?? "";

            var eleitor = new Eleitor(documento);

            if (eleicao.RegistrarEleitor(eleitor))
            {
                System.Console.WriteLine("Eleitor registrado com sucesso!");
            }
            else
            {
                System.Console.WriteLine("Esse eleitor já está registrado.");
            }

            break;

        case "3":
            System.Console.Write("Documento do eleitor: ");
            string documentoVotante = System.Console.ReadLine() ?? "";

            System.Console.Write("Número do candidato (0 para branco): ");

            if (int.TryParse(System.Console.ReadLine(), out int numeroVoto))
            {
                if (eleicao.Votar(documentoVotante, numeroVoto))
                {
                    System.Console.WriteLine("Voto registrado com sucesso!");
                }
                else
                {
                    System.Console.WriteLine("Não foi possível registrar o voto. Verifique se o eleitor está registrado e se ainda não votou.");
                }
            }
            else
            {
                System.Console.WriteLine("Número inválido.");
            }

            break;

        case "4":
            System.Console.WriteLine("\n=== APURAÇÃO DOS RESULTADOS ===");

            foreach (var candidatoApurado in eleicao.Candidatos)
            {
                int votos = eleicao.ConsultarVotos(candidatoApurado.Numero);

                System.Console.WriteLine(
                    $"{candidatoApurado.Nome} (número {candidatoApurado.Numero}): {votos} voto(s)"
                );
            }

            System.Console.WriteLine($"Votos brancos: {eleicao.VotosBrancos}");
            System.Console.WriteLine($"Votos nulos: {eleicao.VotosNulos}");

            var vencedor = eleicao.ObterVencedor();

            if (vencedor is null)
            {
                System.Console.WriteLine("Não há um vencedor único. Pode haver empate ou nenhum candidato cadastrado.");
            }
            else
            {
                System.Console.WriteLine(
                    $"Candidato vencedor: {vencedor.Nome}, número {vencedor.Numero}!"
                );
            }

            break;

        case "0":
            executando = false;
            System.Console.WriteLine("Encerrando o sistema. Até mais!");
            break;

        default:
            System.Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}