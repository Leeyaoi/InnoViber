﻿using InnoViber.Domain.Enums;

namespace InnoViber.DAL.Entities;

public class MessageEntity : BaseEntity
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status {  get; set; }
    public Guid UserId{ get; set; }
    public Guid ChatId{ get; set; }
    public UserEntity? User { get; set; }
    public ChatEntity? Chat { get; set; }
}
