using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel;

public class FilteredGameProductVM(SelectList genre, string selectedGenreGameProduct, string selectTitleGameProduct)
{
    public SelectList Genres { get; set; } = genre;
    public string SelectedGenreGameProduct { get; set; } = selectedGenreGameProduct;
    public string SelectedTitleGameProduct { get; set; } = selectTitleGameProduct;
}
