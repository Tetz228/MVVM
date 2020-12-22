using MVVMWPF.ViewModels.Base;

namespace MVVMWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна

        private string _Title = "Анализ статистики MVVM";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status : string - Статус программы

        /// <summary>Статус программы</summary>
        private string _Status = "Готово!";

        /// <summary>Статус программы</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion
    }
}
