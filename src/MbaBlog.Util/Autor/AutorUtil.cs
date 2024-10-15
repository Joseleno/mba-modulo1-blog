using MbaBlog.Data.Repositories.Comentarios;
using MbaBlog.Data.Repositories.Posts;

namespace MbaBlog.Util.Autor;

public class AutorUtil(IRepositoryComentario repositoryComentario, IRepositoryPost repositoryPost) : IAutorUtil
{
    private readonly IRepositoryComentario _repositoryComentario = repositoryComentario;
    private readonly IRepositoryPost _repositoryPost = repositoryPost;

    public bool IsAutorComentario(Guid idComentario, Guid idAutor)
    {
        var result = _repositoryComentario.GetById(idComentario).Result;
        return result?.AutorId == idAutor; 
    }

    public bool IsAutorPost(Guid idPost, Guid idAutor)
    {
        var result = _repositoryPost.GetById(idPost).Result;
        return result?.AutorId == idAutor;
    }
}
