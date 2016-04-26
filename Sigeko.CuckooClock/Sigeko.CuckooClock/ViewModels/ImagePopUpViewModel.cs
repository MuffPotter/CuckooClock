namespace Sigeko.CuckooClock.ViewModels
{
	public class ImagePopUpViewModel : PopUpBaseViewModel
	{
		private string _headerText;

		private string _imageSource;

		public ImagePopUpViewModel(string headerText, string imageSource)
		{
			this.HeaderText = headerText;
			this.ImageSource = imageSource;
		}

		public string HeaderText
		{
			get { return _headerText; }
			set { SetProperty(ref _headerText, value); }
		}

		public string ImageSource
		{
			get { return _imageSource; }
			set { SetProperty(ref _imageSource, value); }
		}
	}
}
