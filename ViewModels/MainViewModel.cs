using System.Collections.ObjectModel;

namespace FluentNewsApp_Jasmeet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<string> PredefinedCategories = ["Technology", "Business", "Sports"];
        public ObservableCollection<CategoryViewModel> Categories { get; } = [];

        public MainViewModel()
        {
            foreach (var category in PredefinedCategories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            _ = LoadCategoriesAsync();
        }
        private async Task LoadCategoriesAsync()
        {
            // Get all category results in parallel
            await Task.WhenAll(Categories.Select(category => category.GetArticlesAsync()));
        }
    }

}
