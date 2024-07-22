﻿namespace InnoViber.BLL.Models;

public class PaginatedModel<TModel>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? Count { get; set; }
    public int? Total { get; set; }
    public List<TModel>? Items { get; set; }
}
