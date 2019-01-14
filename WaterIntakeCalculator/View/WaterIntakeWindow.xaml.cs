using System.Windows;
using WaterIntakeCalculator.ViewModel;

namespace WaterIntakeCalculator.View
{
    /// <summary>
    /// Interaction logic for WaterIntakeWindow.xaml
    /// </summary>
    public partial class WaterIntakeWindow : Window
    {
        public WaterIntakeWindow()
        {
            InitializeComponent();
            this.DataContext = new WaterIntakeWindowViewModel();
        }
    }
}
