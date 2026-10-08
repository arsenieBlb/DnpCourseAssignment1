using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.Write("Post ID: ");
        if (!int.TryParse(Console.ReadLine(), out int postId) || postId <= 0)
        {
            Console.WriteLine("Enter a positive whole number.");
            return;
        }

        try
        {
            Post post = await postRepository.GetSingleAsync(postId);
            Console.WriteLine();
            Console.WriteLine(post.Title);
            Console.WriteLine(post.Body);
            Console.WriteLine("Comments:");

            var comments = commentRepository.GetMany()
                .Where(comment => comment.PostId == post.Id)
                .OrderBy(comment => comment.Id)
                .ToList();

            if (comments.Count == 0)
            {
                Console.WriteLine("No comments yet.");
                return;
            }

            foreach (Comment comment in comments)
            {
                Console.WriteLine(comment.Body);
            }
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
