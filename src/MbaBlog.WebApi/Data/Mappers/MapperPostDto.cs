using MbaBlog.Domain.Domain;
using MbaBlog.WebApi.Data.Dtos;

namespace MbaBlog.WebApi.Data.Mappers
{
    public class MapperPostDto : IMapperPostDto
    {
        public Post MapPost(PostDto postDto)
        {

            return new Post
            {
                AutorId = postDto.AutorId,
                Titulo = postDto.Titulo,
                Texto = postDto.Texto,
                CriadoEm = DateTime.Now
            };
        }
    }
}
