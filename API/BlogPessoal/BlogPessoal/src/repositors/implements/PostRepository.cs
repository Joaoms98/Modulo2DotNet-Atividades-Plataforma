using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public void AddPost(AddPostDTO Post)
        {
                _context.Posts.Add(new PostModel
                {
                    Title = Post.Title,
                    Description = Post.Description,
                    Photograph = Post.Photograph,
                    Creator = _context.Users.FirstOrDefault(
                u => u.Email == Post.Creator),
                    Theme = _context.Themes.FirstOrDefault(
                t => t.Description == Post.Theme)
                });
                _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllByPosts()
        {
            return _context.Posts.ToList();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts
                .FirstOrDefault(u => u.Id == id);
        }

        public List<PostModel> GetPostsbySearch(string title, string descriptiontheme, string namecriator)
        {
            switch (title, descriptiontheme, namecriator)
            {
                case (null, null, null):
                    return GetAllByPosts();
                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(namecriator))
                    .ToList();
                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(descriptiontheme))
                    .ToList();
                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();
                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(descriptiontheme))
                    .ToList();
                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(descriptiontheme) &
                    p.Creator.Name.Contains(namecriator))
                    .ToList();
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(namecriator))
                    .ToList();
                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(descriptiontheme) |
                    p.Creator.Name.Contains(namecriator))
                    .ToList();
            }
        }

        public void UpdatePost(UpdatePostDTO Post)
        {
            var existingpost = GetPostById(Post.Id);
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
