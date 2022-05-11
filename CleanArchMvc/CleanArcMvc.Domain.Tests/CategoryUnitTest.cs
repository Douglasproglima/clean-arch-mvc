using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Resources;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArcMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Categoria Teste");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Categoria Teste");

            string msg = String.Format(MessagesValidation.msgValueInvalid, "Id");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");

            string msg = String.Format(MessagesValidation.msgFieldMinCharacter, "Nome", 3);

            action.Should()
                    .Throw<DomainExceptionValidation>()
                    .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, string.Empty);

            string msg = String.Format(MessagesValidation.msgFieldRequired, "Nome");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(msg);
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);

            string msg = String.Format(MessagesValidation.msgFieldRequired, "Nome");

            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}
