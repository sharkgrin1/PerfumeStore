using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnWebAPI.Models;

public class MinPagination
{
    private const int MaxSize = 10;
    private int _size = 5;
    private int _page = 1;

    [Range(1, MaxSize)]
    public int? Size
    {
        get => _size;
        set => _size = value ?? _size;
    }

    public int? Page
    {
        get => _page;
        set => _page = value ?? _page;
    }
}