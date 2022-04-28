using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(30)]
        public string Title { get; set; }

        [Required,StringLength(100)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [ForeignKey("Fk_Users")]
        public UserModel Creator { get; set; }

        [ForeignKey("Fk_Themes")]
        public ThemeModel Theme { get; set; }
    }
}
