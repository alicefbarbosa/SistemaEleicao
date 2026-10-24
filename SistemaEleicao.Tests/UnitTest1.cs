using SistemaEleicao.Console;
using Xunit;

namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_ComNumeroNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();
        var candidato = new Candidato("Ana", 10);

        bool resultado = eleicao.CadastrarCandidato(candidato);

        Assert.True(resultado);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));

        bool resultado = eleicao.CadastrarCandidato(
            new Candidato("Bruno", 10)
        );

        Assert.False(resultado);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveManterCandidatoOriginal()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.CadastrarCandidato(new Candidato("Bruno", 10));

        Assert.Single(eleicao.Candidatos);
        Assert.Equal("Ana", eleicao.Candidatos[0].Nome);
    }

    [Fact]
    public void RegistrarEleitor_ComDocumentoNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();
        var eleitor = new Eleitor("12345678900");

        bool resultado = eleicao.RegistrarEleitor(eleitor);

        Assert.True(resultado);
    }

    [Fact]
    public void Votar_ComEleitorRegistrado_DeveAceitarPrimeiroVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.RegistrarEleitor(new Eleitor("12345678900"));

        bool resultado = eleicao.Votar("12345678900", 10);

        Assert.True(resultado);
    }

    [Fact]
    public void Votar_DuasVezes_ComMesmoEleitor_DeveRecusarSegundoVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.RegistrarEleitor(new Eleitor("12345678900"));

        eleicao.Votar("12345678900", 10);
        bool segundoVoto = eleicao.Votar("12345678900", 10);

        Assert.False(segundoVoto);
    }

    [Fact]
    public void Votar_EmCandidatoInexistente_DeveContabilizarVotoNulo()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor(new Eleitor("12345678900"));

        bool resultado = eleicao.Votar("12345678900", 99);

        Assert.True(resultado);
        Assert.Equal(1, eleicao.VotosNulos);
    }

    [Fact]
    public void Votar_EmCandidatoExistente_DeveContabilizarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.RegistrarEleitor(new Eleitor("111"));

        eleicao.Votar("111", 10);

        Assert.Equal(1, eleicao.ConsultarVotos(10));
    }

    [Fact]
    public void Votar_EmBranco_DeveContabilizarVotoBranco()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor(new Eleitor("222"));

        eleicao.Votar("222", 0);

        Assert.Equal(1, eleicao.VotosBrancos);
    }

    [Fact]
    public void Votar_EmOutroCandidatoInexistente_DeveContabilizarVotoNulo()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor(new Eleitor("333"));

        eleicao.Votar("333", 99);

        Assert.Equal(1, eleicao.VotosNulos);
    }

    [Fact]
    public void ObterVencedor_ComCandidatoMaisVotado_DeveRetornarVencedor()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.CadastrarCandidato(new Candidato("Bruno", 20));

        eleicao.RegistrarEleitor(new Eleitor("101"));
        eleicao.RegistrarEleitor(new Eleitor("102"));
        eleicao.RegistrarEleitor(new Eleitor("103"));

        eleicao.Votar("101", 10);
        eleicao.Votar("102", 10);
        eleicao.Votar("103", 20);

        Candidato? vencedor = eleicao.ObterVencedor();

        Assert.NotNull(vencedor);
        Assert.Equal("Ana", vencedor.Nome);
    }

    [Fact]
    public void ObterVencedor_ComEmpate_DeveRetornarNulo()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato(new Candidato("Ana", 10));
        eleicao.CadastrarCandidato(new Candidato("Bruno", 20));

        eleicao.RegistrarEleitor(new Eleitor("201"));
        eleicao.RegistrarEleitor(new Eleitor("202"));

        eleicao.Votar("201", 10);
        eleicao.Votar("202", 20);

        Candidato? vencedor = eleicao.ObterVencedor();

        Assert.Null(vencedor);
    }
}