using System.Windows;
using System.Windows.Controls;
using Cto.MassFileRename.Core.Options;

namespace Cto.MassFileRename.Graphics
{

    public partial class WnRename
    {

        public WnRename()
        {
            InitializeComponent();
            IncludeExtensionOption.IsChecked = true;
        }

        public string TargetName { get; private set; }

        public string ReplacementName { get; private set; }

        public RenamingMethodOptions RenamingMethod { get; private set; }

        public bool IncludeFileExtension { get; private set; }

        private void Rename(object sender, RoutedEventArgs args)
        {
            TargetName = TargetNameBox.Text;
            ReplacementName = ReplacementNameBox.Text;
            RenamingMethod = (string)((ComboBoxItem)RenamingMethodBox.SelectedItem).Tag switch
            {
                "MER" => RenamingMethodOptions.MatchExact,
                "MPR" => RenamingMethodOptions.MatchPart,
            };
            IncludeFileExtension = IncludeExtensionOption.IsChecked == true;
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

    }

}