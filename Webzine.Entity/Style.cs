using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Webzine.Entity
{
    public class Style
    {
        [Key]
        [JsonProperty("id")]
        public int IdStyle { get; set; }

        [Required]
        [Display(Name = "Libellé")]
        [MinLength(2)]
        [MaxLength(50)]
        [JsonProperty("name")]
        public string Libelle { get; set; }
        public List<Titre> TitresStyles { get; set; }
    }
}
