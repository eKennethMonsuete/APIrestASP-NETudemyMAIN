

using APIrestASP_NETudemy.Hypermedia;
using APIrestASP_NETudemy.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace APIrestASP_NETudemy.Data.VO
{
    
    public class PersonVO : ISupportHyperMedia
    {

       // [JsonPropertyName("Código")]
        public long Id { get; set; }

       // [JsonPropertyName("Nome")]
        public string FirstName { get; set; }

       // [JsonPropertyName("sobrenome")]
        public string LastName { get; set; }

       // [JsonPropertyName("Endereço")]
        public string Adress { get; set; }

       // [JsonIgnore]
        public string Gender { get; set; }
        public List<HyperMediaLink> Links
        {
            get;
            set;
        } = new List<HyperMediaLink>();
    }
}
