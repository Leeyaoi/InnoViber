using System.Diagnostics.CodeAnalysis;

namespace InnoViber.BLL.Models;

public class UserViewModel : BaseViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
