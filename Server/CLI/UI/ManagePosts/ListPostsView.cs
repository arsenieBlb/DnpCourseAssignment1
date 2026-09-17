using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine("Posts");

        var posts = postRepository.GetMany().OrderBy(post => post.Id).ToList();
        if (posts.Count == 0)
        {
            Console.WriteLine("No posts found.");
            return;
        }

        foreach (var post in posts)
        {
            Console.WriteLine($"[{post.Id}] {post.Title}");
        }
    }
}
