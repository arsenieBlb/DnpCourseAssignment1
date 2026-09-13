using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task CreateAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Create user");

        Console.Write("Username: ");
        string? userName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine("Username is required.");
            return;
        }

        Console.Write("Password: ");
        string? password = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password is required.");
            return;
        }

        User created = await userRepository.AddAsync(new User
        {
            UserName = userName,
            Password = password
        });

        Console.WriteLine($"User created with ID {created.Id}.");
    }
}
