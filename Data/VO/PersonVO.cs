

using System.Text.Json.Serialization;

namespace APIrestASP_NETudemy.Data.VO
{
    
    public class PersonVO
    {

        [JsonPropertyName("Código")]
        public long Id { get; set; }

        [JsonPropertyName("Nome")]
        public string FirstName { get; set; }

        [JsonPropertyName("sobrenome")]
        public string LastName { get; set; }

        [JsonPropertyName("Endereço")]
        public string Adress { get; set; }

        [JsonIgnore]
        public string Gender { get; set; }
        



    }
}
