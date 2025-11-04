using RedisAssistant.ViewModels;

namespace RedisAssistant.Views;

public partial class MonitorPage : ContentPage
{
	public MonitorPage(MonitorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
