using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_Themes no banco.</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_Themes")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Description { get; set; }

        [JsonIgnore]
        public List<PostModel> Posts { get; set; }
    }
}
