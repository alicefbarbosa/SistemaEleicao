namespace SistemaEleicao.Console;

public class Eleitor
{
    public string Documento { get; }

    public Eleitor(string documento)
    {
        Documento = documento;
    }
}