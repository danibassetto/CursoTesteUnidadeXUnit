using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemConstrutor
{
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
    [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
    [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
    //código do teste omitido

    public void RetornaEhValidoDeAcordoComDadosDeEntrada(string? origem, string? destino, string dataIda, string dataVolta, double preco, bool validacao)
    {
        // Padrão AAA
        //arrange
        Rota rota = new(origem, destino);
        Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

        //act
        OfertaViagem oferta = new(rota, periodo, preco);

        //assert
        Assert.Equal(validacao, oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoRotaNula()
    {
        Rota? rota = null;
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100;

        OfertaViagem oferta = new(rota, periodo, preco);

        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-250)]
    public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
    {
        //arrange
        Rota rota = new("Origem1", "Destino1");
        Periodo periodo = new(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));

        //act
        OfertaViagem oferta = new(rota, periodo, preco);

        //assert
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }
}