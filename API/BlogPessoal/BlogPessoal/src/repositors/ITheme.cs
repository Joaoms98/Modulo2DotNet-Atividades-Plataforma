using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositors
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Tema</para>
    /// <para>Criado por:Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface ITheme
    {
        void AddTheme(AddThemeDTO theme);
        void UpdateTheme(UpdateThemeDTO theme);
        void DeleteTheme(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetThemeByDescription(string description);
    }
}
