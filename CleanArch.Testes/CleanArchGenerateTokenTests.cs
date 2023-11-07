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
    public class CleanArchGenerateTokenTests
    {

        //Criar Token
        [Fact]
        public void CreateToken_Jwt_String()
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
        //Autenticação completa do usuário
        [Fact]
        public async void AuthenticationUser_Sucess()
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
        public async void AuthenticationUser_ErroValidationModel_ExceptionValidation()
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
        //Lança um erro ao constatá que o usuário não existe no banco.
        [Fact]
        public async void AuthenticationUser_CheckIfUserExistsInBank_NotFoundObjectResult()
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
