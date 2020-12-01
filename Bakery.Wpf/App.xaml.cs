using Bakery.Wpf.Common;
using Bakery.Wpf.ViewModels;
using System.Windows;

namespace Bakery.Wpf
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private async void Application_Startup(object sender, StartupEventArgs e)
    {
      var controller = new WindowController();
      var viewModel = await MainWindowViewModel.Create(controller);
      controller.ShowWindow(viewModel);
    }
  }
}
