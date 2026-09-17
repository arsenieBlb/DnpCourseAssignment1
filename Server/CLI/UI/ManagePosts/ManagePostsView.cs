using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public ManagePostsView(
        IPostRepository postRepository,
        IUserRepository userRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        createPostView = new CreatePostView(postRepository, userRepository);
        listPostsView = new ListPostsView(postRepository);
        singlePostView = new SinglePostView(postRepository, commentRepository);
    }

    public Task CreatePostAsync() => createPostView.CreateAsync();

    public void ShowOverview() => listPostsView.Show();

    public Task ShowSingleAsync() => singlePostView.ShowAsync();

    public async Task AddCommentAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Add comment");

        Console.Write("Commenter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
        {
            Console.WriteLine("Enter a positive whole number.");
            return;
        }

        if (!await EntityExistsAsync(userRepository.GetSingleAsync, userId))
        {
            return;
        }

        Console.Write("Post ID: ");
        if (!int.TryParse(Console.ReadLine(), out int postId) || postId <= 0)
        {
            Console.WriteLine("Enter a positive whole number.");
            return;
        }

        if (!await EntityExistsAsync(postRepository.GetSingleAsync, postId))
        {
            return;
        }

        Console.Write("Comment: ");
        string? body = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Comment is required.");
            return;
        }

        Comment created = await commentRepository.AddAsync(new Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId
        });

        Console.WriteLine($"Comment added with ID {created.Id}.");
    }

    private static async Task<bool> EntityExistsAsync<T>(Func<int, Task<T>> getById, int id)
    {
        try
        {
            await getById(id);
            return true;
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            return false;
        }
    }
}
