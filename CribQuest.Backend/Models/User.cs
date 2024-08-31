using Microsoft.AspNetCore.Identity;

namespace CribQuest.Backend.Models;

public class User : IdentityUser<Guid>
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string WhatsappLink { get; set; }  = string.Empty;
    public string ProfileImageUrl { get; set; }  = string.Empty;
    public AccountType AccountType { get; set; }
}