using FluentNewsApp_Jasmeet.Models;
using FluentNewsApp_Jasmeet.Services;
using System.Collections.ObjectModel;

namespace FluentNewsApp_Jasmeet.ViewModels
{
    public class CategoryViewModel(string categoryName) : ViewModelBase
    {
        private bool _isLoading;
        private bool _isError;
        private NewsApiService _newsApiService = new();

        public string CategoryName { get; } = categoryName;
        public ObservableCollection<Article> Articles { get; set; } = [];

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsError
        {
            get => _isError;
            private set
            {
                if (_isError != value)
                {
                    _isError = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task GetArticlesAsync()
        {
            try
            {
                IsError = false;
                IsLoading = true;

                // Clearing the list before loading new
                Articles.Clear();

                Random rnd = new();
                await Task.Delay(rnd.Next(1, 10) * 1000);

                var articles = await _newsApiService.GetNewsAsync(CategoryName);
                foreach (var article in articles)
                {
                    Articles.Add(article);
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsError = true;
            }
        }
    }
}
