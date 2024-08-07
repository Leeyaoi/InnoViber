﻿using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.Message;

public class MessageShortViewModel
{
    public string Text { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
}
