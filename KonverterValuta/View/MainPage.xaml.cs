﻿using KonverterValuta.ViewModel;

namespace KonverterValuta.View;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;;
	}

}

