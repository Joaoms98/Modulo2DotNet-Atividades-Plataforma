using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
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

        #region métodos
        public void AddIPost(AddPostDTO Post)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePost(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<PostModel> GetAllByPosts()
        {
            throw new System.NotImplementedException();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts
                .FirstOrDefault(u => u.Id == id);
        }

        public List<PostModel> GetPostsbySearch(string title, string descriptiontheme, string namecriator)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePost(UpdatePostDTO Post)
        {
            throw new System.NotImplementedException();
        }
        #endregion métodos
    }
}
