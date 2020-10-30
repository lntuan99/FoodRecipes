﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodRecipes.Pages
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		public delegate void ShowRecipeDetailPageHandler(int recipeID);
		public event ShowRecipeDetailPageHandler ShowRecipeDetailPage;

		public HomePage()
		{
			InitializeComponent();

			//Test Show snack bar
			notiMessageSnackbar.MessageQueue.Enqueue("Tìm được 4 món ăn thỏa yêu cầu", "CLOSE", () => { });
		}

		public HomePage(bool isFavorite)
		{
			if (isFavorite)
			{

			}	
			
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = this;
			
		}

		private void foodGroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (var item in foodGroupListBox.SelectedItems)
			{
				var selectedButton = ((Button)item);

				selectedButton.Background = (SolidColorBrush) FindResource("MyYellow");
			}
		}

		private void groupButton_Click(object sender, RoutedEventArgs e)
		{
			//Convert current clicked button to list item
			var clickedButton = ((Button)sender);
			var clickedItemIdx = int.Parse(clickedButton.Tag.ToString());
			var clickedItem = foodGroupListBox.Items.GetItemAt(clickedItemIdx);

			//Add this converted item if new else remove it
			if (foodGroupListBox.SelectedItems.Contains(clickedItem))
			{
				foodGroupListBox.SelectedItems.Remove(clickedItem);
				clickedButton.Background = (SolidColorBrush)FindResource("MyLightGray");
			}
			else
			{
				foodGroupListBox.SelectedItems.Add(clickedItem);
			}				
			

			Debug.WriteLine(((Button)sender).Tag.ToString());

			foreach (var item in foodGroupListBox.SelectedItems)
			{
				
				Debug.WriteLine(((Button)item).Name);
			}	
		}

		private void filterButton_Click(object sender, RoutedEventArgs e)
		{
			if (foodGroupListBox.Visibility == Visibility.Visible)
			{
				foodGroupListBox.Visibility = Visibility.Collapsed;
				recipesListView.SetValue(Grid.RowProperty, 1);
				recipesListView.SetValue(Grid.RowSpanProperty, 2);
			}
			else
			{
				foodGroupListBox.Visibility = Visibility.Visible;
				recipesListView.SetValue(Grid.RowProperty, 2);
				recipesListView.SetValue(Grid.RowSpanProperty, 1);
			}
		}

		private void eraserAllFilterButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (var item in foodGroupListBox.SelectedItems)
			{
				var selectedButton = ((Button)item);

				selectedButton.Background = (SolidColorBrush)FindResource("MyLightGray");
			}

			foodGroupListBox.SelectedItems.Clear();
		}

		private void gridTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void sortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void recipesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItemIndex = recipesListView.SelectedIndex;
			string selectedID = "";

			if (selectedItemIndex != -1)
			{
				selectedID = ((Grid)recipesListView.SelectedItem).Tag.ToString();
				Debug.WriteLine(selectedID);
			}

			//Get Id recipe base on item clikced

			ShowRecipeDetailPage?.Invoke(int.Parse(selectedID));	
		}

		private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
		{
			notiMessageSnackbar.IsActive = false;
		}
	}
}