using System;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OneDriveFileAccessRepro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".txt");
            var file = await picker.PickSingleFileAsync();

            string error = "Successfully accessed file!";
            if (file == null)
            {
                error = "Please choose a valid file!";
            }
            else
            {
                string faToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);

                if (!FileAccessWrapper.hasNativeAccessTo(file))
                {
                    error = "Could not access file from C++ code!";
                }
                
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Remove(faToken);
            }
            var dialog = new MessageDialog(error);
            await dialog.ShowAsync();
        }
    }
}
