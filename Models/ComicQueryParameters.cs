public class ComicQueryParameters
{
  
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Type { get; set; }

    // Ordenación
    public string? SortBy { get; set; } //Precio y año publicación
    public bool SortDescending { get; set; } = false;
}
