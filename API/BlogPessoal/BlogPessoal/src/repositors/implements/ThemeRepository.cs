using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors.implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ITheme</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class ThemeRepository : ITheme
    {
        #region atributes
        private readonly AppBlogContext _context;
        #endregion atributes

        #region Constructors
        public ThemeRepository(AppBlogContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region métodos
        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo Tema</para>
        /// </summary>
        /// <param name="theme">AddThemeDTO</param>
        public async Task AddThemeAsync(AddThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description,
            });
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um tema</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um tema pela descrição</para>
        /// </summary>
        /// <param name="description">Descrição do tema</param>
        /// <return>ThemeModel</return>
        public async Task <List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                        .Where(t => t.Description.Contains(description))
                        .ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um tema pelo id</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        /// <return>ThemeModel</return>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return  await _context.Themes
                        .FirstOrDefaultAsync(t => t.Id == id);
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um tema</para>
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        public async Task UpdateThemeAsync(UpdateThemeDTO theme)
        {
            var themeUpdate = await GetThemeByIdAsync(theme.Id);
            themeUpdate.Description = theme.Description;
            _context.Themes.Update(themeUpdate);
            await _context.SaveChangesAsync();
        }
        #endregion métodos
    }
}
