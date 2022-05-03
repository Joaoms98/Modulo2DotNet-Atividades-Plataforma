using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo:Classe espelho para criar um novo Posts</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AddPostDTO
    {
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [Required,StringLength(30)]
        public string EmailCreator { get; set; }
       
        [Required]
        public string DescriptionTheme { get; set; }

        public AddPostDTO(string title,string description,string photograph,string emailCreator,string descriptionTheme)
        {
            Title = title;
            Description = description;
            Photograph = photograph;
            EmailCreator = emailCreator;
            DescriptionTheme = descriptionTheme;
        }
    }
    /// <summary>
    /// <para>Resumo:Classe espelho para alterar um novo Posts</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(string title, string description, string photograph, string emailCreator, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Photograph = photograph;
            DescriptionTheme = descriptionTheme;
        }
    }
}
