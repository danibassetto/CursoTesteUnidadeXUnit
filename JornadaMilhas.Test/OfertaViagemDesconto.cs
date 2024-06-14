using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemDesconto
{
    [Fact]
    public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
    {
        //arrange
        Rota rota = new("OrigemA", "DestinoB");
        Periodo periodo = new(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.00;
        double desconto = 20.00;
        double precoComDesconto = precoOriginal - desconto;
        OfertaViagem oferta = new(rota, periodo, precoOriginal)
        {
            //act
            Desconto = desconto
        };

        //assert
        Assert.Equal(precoComDesconto, oferta.Preco);
    }

    [Theory]
    [InlineData(120, 30)]
    [InlineData(100, 30)]
    public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualAoPreco(double desconto, double precoComDesconto)
    {
        //arrange
        Rota rota = new("OrigemA", "DestinoB");
        Periodo periodo = new(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.00;
        OfertaViagem oferta = new(rota, periodo, precoOriginal)
        {
            //act
            Desconto = desconto
        };

        //assert
        Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
    }

    [Fact]
    public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPRecoSaoInvalidos()
    {
        //arrange
        int quantidadeEsperada = 3;
        Rota rota = null;
        Periodo periodo = new(new DateTime(2024, 6, 1), new DateTime(2024, 5, 10));
        double preco = -100;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
    }
}