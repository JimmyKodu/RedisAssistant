using RedisAssistant.ViewModels;

namespace RedisAssistant.Views;

public partial class KeysPage : ContentPage
{
	public KeysPage(KeysViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
