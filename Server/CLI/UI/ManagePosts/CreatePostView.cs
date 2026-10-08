using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task CreateAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Create post");

        Console.Write("Author user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
        {
            Console.WriteLine("Enter a positive whole number.");
            return;
        }

        if (!await UserExistsAsync(userId))
        {
            return;
        }

        Console.Write("Title: ");
        string? title = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title is required.");
            return;
        }

        Console.Write("Body: ");
        string? body = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Body is required.");
            return;
        }

        Post created = await postRepository.AddAsync(new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        });

        Console.WriteLine($"Post created with ID {created.Id}.");
    }

    private async Task<bool> UserExistsAsync(int userId)
    {
        try
        {
            await userRepository.GetSingleAsync(userId);
            return true;
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            return false;
        }
    }
}
