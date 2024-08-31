namespace CribQuest.Backend.Services.Interfaces;

public interface IJwtAuth
{
    string Authentication(User user = null);
}