using MbaBlog.Data.Domain;
using MbaBlog.WebApi.Data.Dtos;

namespace MbaBlog.WebApi.Data.Mappers;

public class MapperComentarioDto : IMapperComentario
{
    public ComentarioPost Mapcomentario(ComentarioDto comentario)
    {
        return new ComentarioPost
        {
            PostId = comentario.PostId,
            AutorId = comentario.AutorId,
            Comentario = comentario.Comentario
        };
    }
}
