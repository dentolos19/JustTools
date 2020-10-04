using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cto.MassFileRename.Core.Bindings;
using Cto.MassFileRename.Core.Options;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxImage = AdonisUI.Controls.MessageBoxImage;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace Cto.MassFileRename.Graphics
{

    public partial class WnMain
    {

        public WnMain()
        {
            InitializeComponent();
            UpdateFileSelection(null, null);
            StartButton.IsEnabled = false;
        }

        private string[] _directories;

        private bool _scanRecursively;

        private void StartAnalyzing(object sender, RoutedEventArgs args)
        {
            var dialog = new WnAnalyze { Owner = this };
            if (dialog.ShowDialog() != true)
                return;
            _directories = dialog.Directories;
            _scanRecursively = dialog.ScanRecursively;
            var files = AnalyzeDirectories(_directories, _scanRecursively);
            if (!(files.Length >= 1))
            {
                AdonisMessageBox.Show("No files found.", "MassFileRename", AdonisMessageBoxButton.OK, AdonisMessageBoxImage.Information);
            }
            else
            {
                FileList.Items.Clear();
                foreach (var file in files)
                    FileList.Items.Add(file);
                StartButton.IsEnabled = true;
            }
        }

        private AnalyzedFileBinding[] AnalyzeDirectories(IEnumerable<string> directories, bool recursive = false)
        {
            var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return (from directory in directories from file in Directory.GetFiles(directory, "*.*", option) select new AnalyzedFileBinding { FileName = Path.GetFileName(file), FileLocation = file }).ToArray();
        }

        private void StartRenaming(object sender, RoutedEventArgs args)
        {
            var dialog = new WnRename { Owner = this };
            if (dialog.ShowDialog() != true)
                return;
            var answer = AdonisMessageBox.Show($"This will rename all {FileList.Items.Count} file(s) and it is irreversible! Do you want to continue?", "MassFileRename", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (answer != AdonisMessageBoxResult.Yes)
                return;
            foreach (var file in FileList.Items.OfType<AnalyzedFileBinding>())
            {
                var homeDirectory = Path.GetDirectoryName(file.FileLocation);
                var fileName = dialog.IncludeFileExtension ? Path.GetFileName(file.FileLocation) : Path.GetFileNameWithoutExtension(file.FileLocation);
                var fileNameExt = dialog.IncludeFileExtension ? string.Empty : Path.GetExtension(file.FileLocation);
                switch (dialog.RenamingMethod)
                {
                    case RenamingMethodOptions.MatchExact:
                        fileName = (fileName == dialog.TargetName ? dialog.ReplacementName : fileName) + fileNameExt;
                        break;
                    case RenamingMethodOptions.MatchPart:
                        fileName = fileName!.Replace(dialog.TargetName!, dialog.ReplacementName + fileNameExt);
                        break;
                }
                File.Move(file.FileLocation, Path.Combine(homeDirectory!, fileName!));
            }
            var files = AnalyzeDirectories(_directories, _scanRecursively);
            FileList.Items.Clear();
            foreach (var file in files)
                FileList.Items.Add(file);
        }

        private void RemoveFile(object sender, RoutedEventArgs args)
        {
            var answer = AdonisMessageBox.Show("Are you sure that you want to remove this file?", "MassFileRename", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (answer == AdonisMessageBoxResult.Yes)
                FileList.Items.Remove(FileList.SelectedItem);
            if (!(FileList.Items.Count >= 1))
                StartButton.IsEnabled = false;
        }

        private void ClearFiles(object sender, RoutedEventArgs args)
        {
            if (!(FileList.Items.Count >= 1))
                return;
            var answer = AdonisMessageBox.Show("Are you sure that you want to clear all files?", "MassFileRename", AdonisMessageBoxButton.YesNo, AdonisMessageBoxImage.Question);
            if (answer == AdonisMessageBoxResult.Yes)
                FileList.Items.Clear();
            StartButton.IsEnabled = false;
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void UpdateFileSelection(object sender, SelectionChangedEventArgs args)
        {
            if (FileList.SelectedItem == null)
            {
                RemoveButton.IsEnabled = false;
                CopyLocationButton.IsEnabled = false;
            }
            else
            {
                RemoveButton.IsEnabled = true;
                CopyLocationButton.IsEnabled = true;
            }
        }

        private void CopyFileLocation(object sender, RoutedEventArgs args)
        {
            Clipboard.SetText(((AnalyzedFileBinding)FileList.SelectedItem).FileLocation);
        }

    }

}