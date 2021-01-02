using MVVMWPF.Infrastructure.Commands;
using MVVMWPF.Models;
using MVVMWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MVVMWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region SelectedPageIndex : int - Номер выбранной вкладки

        /// <summary>Номер выбранной вкладки</summary>
        private int _SelectedPageIndex;

        /// <summary>Номер выбранной вкладки</summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region TestDataPoint : IEnumerable<DataPoint> - DESCRIPTION

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }

        #endregion

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

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecuted(object p) => true;
        #endregion

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) 
                return;
            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));

            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;

                var y = Math.Sin(2 * Math.PI * x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }
    }
}
