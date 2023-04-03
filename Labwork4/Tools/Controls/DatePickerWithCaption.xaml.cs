using System;
using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools.Controls
{
	public partial class DatePickerWithCaption : UserControl
	{
		public string Caption
		{
			get { return TbCaption.Text; }
			set { TbCaption.Text = value; }
		}

		public DateTime? Date
		{
			get { return (DateTime?)GetValue(DateProperty); }
			set { SetValue(DateProperty, value); }
		}

		public static readonly DependencyProperty DateProperty = DependencyProperty.Register
		(
			"Date",
			typeof(DateTime?),
			typeof(DatePickerWithCaption),
			new PropertyMetadata(null)
		);

		public DatePickerWithCaption()
		{
			InitializeComponent();
		}
	}
}
