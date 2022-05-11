using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositors
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Posts</para>
    /// <para>Criado por:Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPost
    {
        void AddPost(AddPostDTO Post);
        void UpdatePost(UpdatePostDTO Post);
        void DeletePost(int id);
        PostModel GetPostById(int id);
        List<PostModel> GetAllByPosts();
        List<PostModel> GetPostsbySearch(string title, string theme, string creator);
    }
}
