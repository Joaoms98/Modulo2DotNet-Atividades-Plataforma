using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo:Classe espelho para criar um novo tema</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AddThemeDTO
    {
        [Required, StringLength(30)]
        public string Description { get; set; }

        public AddThemeDTO(string description)
        {
            Description = description;
        }
     }
    /// <summary>
    /// <para>Resumo:Classe espelho para alterar um novo Tema</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateThemeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Description { get; set; }

        public UpdateThemeDTO(string description)
        {
            Description=description;
        }
    }
}
