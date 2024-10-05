namespace MbaBlog.Util.Autor;

public interface IAutorUtil
{
    bool IsAutorPost(Guid idPost, Guid idAutor);

    bool IsAutorComentario(Guid idPost, Guid idAutor);

}
