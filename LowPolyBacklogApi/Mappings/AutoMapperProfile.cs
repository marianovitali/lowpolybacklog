using AutoMapper;
using LowPolyBacklogApi.DTOs.Backlog;
using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Domain: Game
            // GET
            CreateMap<Game, GameResponseDto>()
                .ForMember(dto => dto.Genres,
                            options => options.MapFrom(game => game.Genres.Select(g => g.Name).ToList()));

            // ADD
            CreateMap<GameCreateDto, Game>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            // EDIT
            CreateMap<GameUpdateDto, Game>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
            #endregion


            #region Domain: Backlog
            // GET
            CreateMap<BacklogEntry, BacklogResponseDto>()
                .ForMember(dto => dto.GameTitle,
                            options => options.MapFrom(game => game.Game.Title))
                .ForMember(dto => dto.CoverImageUrl,
                            options => options.MapFrom(game => game.Game.CoverImageUrl));

            // ADD
            CreateMap<BacklogCreateDto, BacklogEntry>();

            // EDIT
            CreateMap<BacklogUpdateDto, BacklogEntry>();
            #endregion


        }
    }
}
