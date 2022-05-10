using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Resources;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArcMvc.Domain.Tests
{
    public class ProductUnitTest
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Produto", "Procuto 01", 10.65M, 10, "Image 01");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Produto", "Procuto 01", 10.65M, 10, "Image 01");

            string msg = string.Format(MessagesValidation.msgValueInvalid, "Id");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Procuto 01", 10.65M, 10, "Imagem");

            string msg = String.Format(MessagesValidation.msgFieldMinCharacter, "Nome", 3);

            action.Should()
                    .Throw<DomainExceptionValidation>()
                    .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(1, null, "Procuto 01", 10.65M, 10, "Imagem");

            string msg = String.Format(MessagesValidation.msgFieldRequired, "Nome");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_ShortDescriptionValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(1, "Procuto 01", "Pr", 10.65M, 10, "Imagem");

            string msg = String.Format(MessagesValidation.msgFieldMinCharacter, "Descrição", 5);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_WithNullDescriptionValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, "Procuto 01", null, 10.65M, 10, "Imagem");

            string msg = String.Format(MessagesValidation.msgFieldRequired, "Descrição");

            action.Should()
                .Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_LongImageValue_DomainExceptionImageMaxCharactere()
        {
            string image = "çalskdflçkas~çlkdfçlaksdflkçãçlskfdlçsãklfçkasçlkflçaksdlfç~kaçslkdfçalskdflçkas~çlkdfçlaksdflkçãçlskfdlçsãklfçkasçlkflçaksdlfç~kaçslkdfçalskdflçkas~çlkdfçlaksdflkçãçlskfdlçsãklfçkasçlkflçaksdlfç~kaçslkdfçalskdflçkas~çlkdfçlaksdflkçãçlskfdlçsãklfçkasçlkflçaksdlfç~kaçslkdf";

            Action action = () => new Product(1, "Produto", "Procuto 01", 10.65M, 10, image);

            string msg = string.Format(MessagesValidation.msgFieldMaxCharacter, "Imagem", 250);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_ExceptionDomain()
        {
            Action action = () => new Product(1, "Procuto 01", "Procuto 01", -2.55M, 10, "Imagem");

            string msg = string.Format(MessagesValidation.msgValueInvalid, "Preço");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int qtdeStock)
        {
            Action action = () => new Product(1, "Procuto 01", "Procuto 01", 10.65M, qtdeStock, "Imagem");
            
            string msg = string.Format(MessagesValidation.msgValueInvalid, "Estoque");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Produto", "Procuto 01", 10.65M, 10, null);
            action.Should().NotThrow<DomainExceptionValidation>();
        }
    }
}
