using MbaBlog.Domain.Domain;
using MbaBlog.WebApi.Data.Dtos;

namespace MbaBlog.WebApi.Data.Mappers
{
    public interface IMapperPostDto
    {
        Post MapPost(PostDto post);
    }
}
