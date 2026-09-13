using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly ManageUsersView manageUsersView;
    private readonly ManagePostsView managePostsView;

    public CliApp(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        IPostRepository postRepository)
    {
        manageUsersView = new ManageUsersView(userRepository);
        managePostsView = new ManagePostsView(postRepository, userRepository, commentRepository);
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Welcome to the forum CLI!");

        while (true)
        {
            PrintMainMenu();
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUsersView.CreateUserAsync();
                    break;
                case "2":
                    await managePostsView.CreatePostAsync();
                    break;
                case "3":
                    await managePostsView.AddCommentAsync();
                    break;
                case "4":
                    managePostsView.ShowOverview();
                    break;
                case "5":
                    await managePostsView.ShowSingleAsync();
                    break;
                case "0":
                case null:
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid menu choice. Enter a number from 0 to 5.");
                    break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Main menu");
        Console.WriteLine("1. Create user");
        Console.WriteLine("2. Create post");
        Console.WriteLine("3. Add comment");
        Console.WriteLine("4. View posts overview");
        Console.WriteLine("5. View specific post");
        Console.WriteLine("0. Exit");
        Console.Write("Select an option: ");
    }
}
