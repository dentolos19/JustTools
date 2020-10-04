using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxImage = AdonisUI.Controls.MessageBoxImage;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace Cto.MassFileRename.Graphics
{

    public partial class WnAnalyze
    {

        public WnAnalyze()
        {
            InitializeComponent();
            UpdateDirectorySelection(null, null);
            ScanRecursivelyOption.IsChecked = true;
        }

        public string[] Directories { get; private set; }

        public bool ScanRecursively { get; private set; }

        private void AddDirectory(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() != true)
                return;
            if (DirectoryList.Items.OfType<string>().Any(directory => directory == dialog.SelectedPath))
            {
                AdonisMessageBox.Show("The directory is already in the list.", "FileSystemMonitor", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Information);
                return;
            }
            DirectoryList.Items.Add(dialog.SelectedPath);
        }

        private void RemoveDirectory(object sender, RoutedEventArgs args)
        {
            var answer = AdonisMessageBox.Show("Are you sure that you want to remove this directory?", "MassFileRename", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (answer == AdonisMessageBoxResult.Yes)
                DirectoryList.Items.Remove(DirectoryList.SelectedItem);
        }

        private void ClearDirectories(object sender, RoutedEventArgs args)
        {
            if (!(DirectoryList.Items.Count >= 1))
                return;
            var answer = AdonisMessageBox.Show("Are you sure that you want to clear all directories?", "MassFileRename", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (answer == AdonisMessageBoxResult.Yes)
                DirectoryList.Items.Clear();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void Analyze(object sender, RoutedEventArgs args)
        {
            if (!(DirectoryList.Items.Count >= 1))
            {
                AdonisMessageBox.Show("You must at least have one directory in the list!", "MassFileRename", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Stop);
                return;
            } 
            Directories = DirectoryList.Items.OfType<string>().ToArray();
            ScanRecursively = ScanRecursivelyOption.IsChecked == true;
            DialogResult = true;
            Close();
        }

        private void UpdateDirectorySelection(object sender, SelectionChangedEventArgs args)
        {
            RemoveButton.IsEnabled = DirectoryList.SelectedItem != null;
        }

    }

}