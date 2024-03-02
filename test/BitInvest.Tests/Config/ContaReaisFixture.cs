
using System;
using BitInvest.Tests.Config;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Bitinvest.Domain.Entities.ContaReais.Tests
{

    [CollectionDefinition(nameof(BitInvestCollection2))]

    public class BitInvestCollection2 : ICollectionFixture<ContaReaisFixture> { }

    public class ContaReaisFixture
    {

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoSemIdInvalida()
        {
            // Sem Id Inválido
            return new ContaCorrenteReais(
                Guid.Empty,
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                200,
                500);
        }


        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoSemOperacaoInvalida()
        {
            // Sem TipoOperação Inválido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                0,
                200,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoSemDescricaoInvalida()
        {
            // Sem Descrição Inválido
            return new ContaCorrenteReais(
               Guid.NewGuid(),
               DateTime.Now,
               string.Empty,
               TipoOperacao.Credito,
               200,
               500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoSemDataMovInvalida()
        {
            // Sem DataMov Inválido
            return new ContaCorrenteReais(
               Guid.NewGuid(),
               DateTime.MinValue,
               "Operacao de Teste",
               TipoOperacao.Credito,
               200,
               500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoCreditoValida()
        {
            // Movimento de Conta em Reais de Crédtito Válido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                200,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoDebitoValida()
        {
            // Movimento de Conta em Reais de Débito Válido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Debito,
                200,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoCreditoNegativoInvalida()
        {
            // Movimento de Conta em Reais de Crédtito Negativo - Inválido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                -1,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoDebitoNegativoInvalida()
        {
            // Movimento de Conta em Reais de Débito Negativo - Inválido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Debito,
                -1,
                500);
        }


        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoCreditoZeradoInvalida()
        {
            // Criando um movimento de Conta Real de Crédtito Valor Zerado InVálido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                0,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoDebitoZeradoInvalida()
        {
            // Criando um movimento de Conta Real de Crédtito Negativo InVálido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                0,
                500);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoSaqueMaiorQSaldoInvalido()
        {
            // Criando um movimento de Conta Real de Saque Inválido por valor de Saque maior que Saldo
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Debito,
                500,
                0);
        }


        public ContaCorrenteReais CriarContaCorrenteReaisComValorAbaixoDoMinimo()
        {
            // Configurar uma instância com valor abaixo do mínimo para testes 50 mínimo
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                49,
                0);
        }


        public ContaCorrenteReais CriarContaCorrenteReaisSaqueAcimadoSaldo()
        {
            // Configurar uma instância com valor abaixo do mínimo para testes 50 mínimo
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Debito,
                150,
                100);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoCreditoAbaixoMinimoInvalido()
        {
            // Movimento de Crédito abaixo do Mínimo - Inválido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Credito,
                10,
                100);
        }

        public ContaCorrenteReais CriarContaCorrenteReaisMovimentoDebitoAbaixoMinimoInvalido()
        {
            // Movimento de Débito abaixo do Mínimo - Inválido
            return new ContaCorrenteReais(
                Guid.NewGuid(),
                DateTime.Now,
                "Operacao de Teste",
                TipoOperacao.Debito,
                10,
                100);
        }


        // Adicione mais métodos conforme necessário para criar instâncias com configurações específicas
    }


}