namespace InnoViber.BLL.Models;

public class BaseViewModel
{
    public required Guid Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}
