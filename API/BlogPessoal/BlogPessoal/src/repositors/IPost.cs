using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task AddPostAsync(AddPostDTO Post);
        Task UpdatePostAsync(UpdatePostDTO Post);
        Task DeletePostAsync(int id);
        Task <PostModel> GetPostByIdAsync(int id);
        Task <List<PostModel>> GetAllByPostsAsync();
        Task <List<PostModel>> GetPostsbySearchAsync(string title, string theme, string creator);
    }
}
