using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUserView;

    public ManageUsersView(IUserRepository userRepository)
    {
        createUserView = new CreateUserView(userRepository);
    }

    public Task CreateUserAsync()
    {
        return createUserView.CreateAsync();
    }
}
