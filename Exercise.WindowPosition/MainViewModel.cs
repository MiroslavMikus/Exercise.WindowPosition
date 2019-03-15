using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exercise.WindowPosition
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Process> _processes;
        public ObservableCollection<Process> Processes { get => _processes; set => Set(ref _processes, value); }

        private Process _selectedProcess;
        public Process SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                if (Set(ref _selectedProcess, value) && value != null)
                {
                    RefreshPosition();
                }
            }
        }

        public ICommand SetPositionCommand { get; set; }
        public ICommand RefreshProcessesCommand { get; set; }
        public ICommand RefreshPositionCommand { get; set; }

        private int _top;

        public int Top
        {
            get { return _top; }
            set { Set(ref _top, value); }
        }

        private int _left;
        public int Left
        {
            get { return _left; }
            set { Set(ref _left, value); }
        }

        private int _right;

        public int Right
        {
            get { return _right; }
            set { Set(ref _right, value); }
        }
        private int _bottom;
        public int Bottom
        {
            get { return _bottom; }
            set { Set(ref _bottom, value); }
        }

        public MainViewModel()
        {
            RefreshProcess();

            RefreshProcessesCommand = new RelayCommand(RefreshProcess);
            RefreshPositionCommand = new RelayCommand(RefreshPosition);

            SetPositionCommand = new RelayCommand(() =>
            {
                if (SelectedProcess == null) return;

                WindowPositionManager.MoveWindow(SelectedProcess.Id, new Rect
                {
                    Top = this.Top,
                    Bottom = this.Bottom,
                    Left = this.Left,
                    Right = this.Right
                });
            });
        }

        private void RefreshProcess() => Processes = new ObservableCollection<Process>(Process.GetProcesses().OrderBy(a => a.ProcessName));

        private void RefreshPosition()
        {
            if (SelectedProcess == null) return;

            var position = WindowPositionManager.GetWindowPosition(SelectedProcess.Id);

            Top = position.Top;
            Bottom = position.Bottom;
            Left = position.Left;
            Right = position.Right;
        }
    }
}
