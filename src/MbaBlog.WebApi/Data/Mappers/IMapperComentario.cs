using MbaBlog.Data.Domain;
using MbaBlog.WebApi.Data.Dtos;

namespace MbaBlog.WebApi.Data.Mappers;

public interface IMapperComentario
{
    ComentarioPost Mapcomentario(ComentarioDto comentario);
}
