using CleanArch.Application.Authentication;
using CleanArch.Domain.Entities;
using Moq;
using System.Threading.Tasks;
using Xunit;
using CleanArch.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using APICleanArch.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CleanArch.Testes
{
    public class CleanArchTestesGenerateToken
    {
        [Fact]
        public void CreateTokenTest()
        {
            string token = TokenService.GenerateToken(new UserMkt()
            {
                Id = 1,
                Username = "Abimael",
                Password = "teste",
                Role = "manager"
            });

            Assert.IsType<string>(token);
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
        [Fact]
        public async void AuthenticationUserTeste()
        {
            var userMktServiceMock = new Mock<IUserMktService>();
            var userValidatorMock = new Mock<IValidator<UserMkt>>();

            userValidatorMock.Setup(validator
                => validator.Validate(It.IsAny<UserMkt>()))
                .Returns(new ValidationResult());

            var controller = new AuthenticationController(userMktServiceMock.Object, userValidatorMock.Object);

            var userAutentication = new UserMkt()
            {
                Id = 1,
                Username = "Abimael",
                Password = "teste",
                Role = "manager"
            };

            userMktServiceMock.Setup(x => x.GetEmployeeByIdAsync(userAutentication.Id))
                .ReturnsAsync(userAutentication);

            var result = await controller.Authenticate(userAutentication);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<dynamic>>(result);
            Assert.NotNull(result.Value);

        }
        //Teste para verficar o Validator: Gera uma objeto inválido e captura o erro que foi retornado.
        [Fact]
        public async void AuthenticationUserExceptionValidation()
        {
            var userMktServiceMock = new Mock<IUserMktService>();
            var userValidatorMock = new Mock<IValidator<UserMkt>>();

            userValidatorMock.Setup(validator
                => validator.Validate(It.IsAny<UserMkt>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                { new ValidationFailure("erro", "erro") }));

            var userMktAutentication = new UserMkt
            {
                Id = 1,
                Username = "testuser",
                Password = "testpassword"
            };

            var result = await new AuthenticationController(
                userMktServiceMock.Object, userValidatorMock.Object)
                .Authenticate(userMktAutentication);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void CheckIfUserExistsInBank()
        {
            var userMktServiceMock = new Mock<IUserMktService>();
            var userValidatorMock = new Mock<IValidator<UserMkt>>();

            userValidatorMock.Setup(validator
                => validator.Validate(It.IsAny<UserMkt>()))
                .Returns(new ValidationResult());
            var userMktAutentication = new UserMkt
            {
                Id = 1,
                Username = "testuser",
                Password = "testpassword"
            };

            userMktServiceMock.Setup(get => 
                 get.GetEmployeeByIdAsync(userMktAutentication.Id))
                .Returns(Task.FromResult<UserMkt>(null));


            var result = await new AuthenticationController(
                userMktServiceMock.Object, userValidatorMock.Object)
                .Authenticate(userMktAutentication);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
