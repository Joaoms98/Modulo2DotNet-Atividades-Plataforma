using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_posts no banco.</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
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
