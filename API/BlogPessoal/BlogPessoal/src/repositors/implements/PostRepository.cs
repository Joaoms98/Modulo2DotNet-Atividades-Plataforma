using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors.implements
{
    public class PostRepository : IPost
    {
        #region atributes
        private readonly AppBlogContext _context;
        #endregion atributes

        #region Constructors
        public PostRepository(AppBlogContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region methods
        public async Task AddPostAsync(AddPostDTO Post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = Post.Title,
                Description = Post.Description,
                Photograph = Post.Photograph,
                Creator = _context.Users.FirstOrDefault(
                u => u.Email == Post.Creator),
                Theme = _context.Themes.FirstOrDefault(
                 t => t.Description == Post.Theme)
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task <List<PostModel>> GetAllByPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts
                .Include(u => u.Creator)
                .Include(u => u.Theme)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<PostModel>> GetPostsbySearchAsync(string title, string descriptiontheme, string namecriator)
        {
            switch (title, descriptiontheme, namecriator)
            {
                case (null, null, null):
                    return await GetAllByPostsAsync();
                case (null, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(namecriator))
                    .ToListAsync();
                case (null, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(descriptiontheme))
                    .ToListAsync();
                case (_, null, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();
                case (_, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(descriptiontheme))
                    .ToListAsync();
                case (null, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(descriptiontheme) &
                    p.Creator.Name.Contains(namecriator))
                    .ToListAsync();
                case (_, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(namecriator))
                    .ToListAsync();
                case (_, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(descriptiontheme) |
                    p.Creator.Name.Contains(namecriator))
                    .ToListAsync();
            }
        }

        public async Task UpdatePostAsync(UpdatePostDTO Post)
        {
            var existingpost = await GetPostByIdAsync(Post.Id);
            existingpost.Title = Post.Title;
            existingpost.Description = Post.Description;
            existingpost.Photograph = Post.Photograph;
            existingpost.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == Post.Theme);
            _context.Posts.Update(existingpost);
            _context.SaveChanges();
        }
        #endregion
    }
}
