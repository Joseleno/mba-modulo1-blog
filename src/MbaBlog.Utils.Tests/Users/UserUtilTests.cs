using FluentAssertions;
using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.Utils.Exceptions;
using MbaBlog.Utils.Users;
using MbaBlog.Utils.Users.Dtos;
using Moq;

namespace MbaBlog.Utils.Tests.Users;

public class UserUtilTests
{
    private readonly Mock<IAppIdentityUser> _appIdentityUser = new();
    private readonly Mock<IRepositoryUserRole> _repositoryUserRole = new();
    private readonly Mock<IRepositoryUser> _repositoryUser = new();

    private readonly UserUtil _service;

    public UserUtilTests()
    {
        _service = new UserUtil(_appIdentityUser.Object, _repositoryUserRole.Object, _repositoryUser.Object);
    }

    [Fact]
    public void QuandoUsuaarioLogado_DeveRetornarUsuario()
    {
        //Given
        var id = Guid.NewGuid();
        var nome = "nome@nome";
        
        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _appIdentityUser.Setup(x => x.GetUsername()).Returns(nome);

        var uesExpected = new UserDto { UserId = id, UserEmail= nome };

        //When
        var result = _service.GetUser();
        
        //Then
        result.Should().BeEquivalentTo(uesExpected);

    }

    [Fact]
    public void QuandoUsuaarioLogado_DeveRetornarNotFoundException()
    {
        //Given

        _appIdentityUser.Setup(x => x.GetUserId());
        _appIdentityUser.Setup(x => x.GetUsername());

        //Then

        Assert.Throws<NotFoundException>(() => _service.GetUser());
    }

    [Fact]
    public void QuandoUsuarioAdmin_DeveRetornarTrue()
    {
        //Given
        var id = Guid.NewGuid();


        //var usertId = _appIdentityUser.GetUserId();
        //var result = usertId == id || IsAdmin(usertId);
        //return result;

        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _repositoryUserRole.Setup(x => x.GetRole(id)).Returns("admin");


        //When
        var result = _service.HasAthorization(id);

        //Then
        result.Should().BeTrue();

    }

    [Fact]
    public void QuandoUsuaarioNaoAutorizado_DeveRetornarFalse()
    {
        //Given
        var id = Guid.NewGuid();
        var nome = "nome@nome";

        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _appIdentityUser.Setup(x => x.GetUsername()).Returns(nome);

        var uesExpected = new UserDto { UserId = id, UserEmail = nome };

        //When
        var result = _service.GetUser();

        //Then
        result.Should().BeEquivalentTo(uesExpected);

    }
}
