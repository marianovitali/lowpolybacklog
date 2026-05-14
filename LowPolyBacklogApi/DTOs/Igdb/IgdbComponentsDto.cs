using System.Text.Json.Serialization;

namespace LowPolyBacklogApi.DTOs.Igdb
{
    public class IgdbComponentsDto
    {
        public class IgdbCoverDto
        {
            [JsonPropertyName("image_id")]
            public string ImageId { get; set; } = string.Empty;
        }

        public class IgdbGenreDto
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;
        }

        public class IgdbCompanyDto
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;
        }

        public class IgdbInvolvedCompanyDto
        {
            [JsonPropertyName("company")]
            public IgdbCompanyDto? Company { get; set; }

            [JsonPropertyName("developer")]
            public bool IsDeveloper { get; set; }
        }
    }
}
