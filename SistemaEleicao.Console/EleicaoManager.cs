using System.Collections.Generic;
using System.Linq;

namespace SistemaEleicao.Console;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly List<Eleitor> _eleitores = new();
    private readonly HashSet<string> _eleitoresQueVotaram = new();
    private readonly Dictionary<int, int> _votosPorCandidato = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos.AsReadOnly();

    public int VotosNulos { get; private set; }

    public int VotosBrancos { get; private set; }

    public bool CadastrarCandidato(Candidato candidato)
    {
        bool numeroJaExiste = _candidatos.Any(
            c => c.Numero == candidato.Numero
        );

        if (numeroJaExiste)
        {
            return false;
        }

        _candidatos.Add(candidato);
        _votosPorCandidato[candidato.Numero] = 0;

        return true;
    }

    public bool RegistrarEleitor(Eleitor eleitor)
    {
        bool eleitorJaExiste = _eleitores.Any(
            e => e.Documento == eleitor.Documento
        );

        if (eleitorJaExiste)
        {
            return false;
        }

        _eleitores.Add(eleitor);
        return true;
    }

    public bool Votar(string documentoEleitor, int numeroCandidato)
    {
        bool eleitorRegistrado = _eleitores.Any(
            e => e.Documento == documentoEleitor
        );

        if (!eleitorRegistrado)
        {
            return false;
        }

        if (_eleitoresQueVotaram.Contains(documentoEleitor))
        {
            return false;
        }

        _eleitoresQueVotaram.Add(documentoEleitor);

        if (numeroCandidato == 0)
        {
            VotosBrancos++;
            return true;
        }

        bool candidatoExiste = _candidatos.Any(
            c => c.Numero == numeroCandidato
        );

        if (!candidatoExiste)
        {
            VotosNulos++;
            return true;
        }

        _votosPorCandidato[numeroCandidato]++;

        return true;
    }

    public int ConsultarVotos(int numeroCandidato)
    {
        if (!_votosPorCandidato.ContainsKey(numeroCandidato))
        {
            return 0;
        }

        return _votosPorCandidato[numeroCandidato];
    }

    public Candidato? ObterVencedor()
    {
        if (_candidatos.Count == 0)
        {
            return null;
        }

        int maiorNumeroDeVotos = _votosPorCandidato.Values.Max();

        var candidatosMaisVotados = _candidatos
            .Where(c => _votosPorCandidato[c.Numero] == maiorNumeroDeVotos)
            .ToList();

        if (candidatosMaisVotados.Count != 1)
        {
            return null;
        }

        return candidatosMaisVotados[0];
    }
}