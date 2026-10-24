namespace SistemaEleicao.Console;

public class Candidato
{
    public string Nome { get; }
    public int Numero { get; }

    public Candidato(string nome, int numero)
    {
        Nome = nome;
        Numero = numero;
    }
}