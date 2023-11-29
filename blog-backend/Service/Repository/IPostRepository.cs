using blog_backend.DAO.Model;
using blog_backend.Entity;

namespace blog_backend.Service.Repository;

public interface IPostRepository
{
    //getPostDetails 
    
    public Task<Post?> GetPostDetails(Guid postId);

    public Task<List<Tag>> GetTags(CreatePostDTO postDto);
    
    public Task CreatePost(Post post);

    public Task LikePost(Post post, User user);
    
    public Task UnlikePost(Post post, User user);


}