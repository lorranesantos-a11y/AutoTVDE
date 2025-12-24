namespace AutoTvde.Api.Auth;

public record FakeUser(
    string Email,
    string Password,
    string Role
);

public static class FakeUserStore
{
    public static readonly List<FakeUser> Users = new()
    {
        new FakeUser(
            Email: "admin@mds.pt",
            Password: "Passw0rd!",
            Role: "Admin"
        ),
        new FakeUser(
            Email: "mediator@mds.pt",
            Password: "Passw0rd!",
            Role: "Mediator"
        )
    };
}
