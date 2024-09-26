using FluentAssertions;
using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.Util.Exceptions;
using MbaBlog.Util.Users;
using MbaBlog.Util.Users.Dtos;
using Moq;

namespace MbaBlog.Util.Tests.Users;

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
    public void QuandoUsuaarioNaoLogado_DeveRetornarNotFoundException()
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


        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _repositoryUserRole.Setup(x => x.GetRole(id)).Returns("admin");


        //When
        var result = _service.HasAthorization(id);

        //Then
        result.Should().BeTrue();

    }

    [Fact]
    public void QuandoUsuarioNaoAutorizado_DeveRetornarFalse()
    {
        //Given
        var id = Guid.NewGuid();

        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _repositoryUserRole.Setup(x => x.GetRole(id)).Returns(string.Empty);

        //When
        var result = _service.HasAthorization(Guid.NewGuid());

        //Then
        result.Should().BeFalse();

    }

    [Fact]
    public void QuandoUsuarioAutorizadoNaoAdmin_DeveRetornarTrue()
    {
        //Given
        var id = Guid.NewGuid();

        _appIdentityUser.Setup(x => x.GetUserId()).Returns(id);
        _repositoryUserRole.Setup(x => x.GetRole(id)).Returns(string.Empty);


        //When
        var result = _service.HasAthorization(id);

        //Then
        result.Should().BeTrue();

    }

    [Fact]
    public void QuandoUsuarioEncontrado_DeveRetornarTrue()
    {
        //Given
        var id = Guid.NewGuid();
        _repositoryUser.Setup(x => x.GetUser(id)).Returns(new Infrastructure.Dtos.UserDto());

        //When
        var result = _service.IsUser(id);

        //Then
        result.Should().BeTrue();

    }

    [Fact]
    public void QuandoUsuarioNaoEncontrado_DeveRetornarFalse()
    {
        //Given
        var id = Guid.NewGuid();
        _repositoryUser.Setup(x => x.GetUser(id)).Returns(new Infrastructure.Dtos.UserDto());

        //When
        var result = _service.IsUser(Guid.NewGuid());

        //Then
        result.Should().BeFalse();

    }
}
