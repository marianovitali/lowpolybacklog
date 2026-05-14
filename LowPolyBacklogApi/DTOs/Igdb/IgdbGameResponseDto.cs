using System.Text.Json.Serialization;
using static LowPolyBacklogApi.DTOs.Igdb.IgdbComponentsDto;

namespace LowPolyBacklogApi.DTOs.Igdb
{
    public class IgdbGameResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("first_release_date")]
        public long? FirstReleaseDate { get; set; }

        [JsonPropertyName("cover")]
        public IgdbCoverDto? Cover { get; set; }

        [JsonPropertyName("genres")]
        public List<IgdbGenreDto>? Genres { get; set; }

        [JsonPropertyName("involved_companies")]
        public List<IgdbInvolvedCompanyDto>? InvolvedCompanies { get; set; }
    }
}
