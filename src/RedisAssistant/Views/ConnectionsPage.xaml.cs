using RedisAssistant.ViewModels;

namespace RedisAssistant.Views;

public partial class ConnectionsPage : ContentPage
{
	public ConnectionsPage(ConnectionsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
