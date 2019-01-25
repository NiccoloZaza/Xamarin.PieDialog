using Android.App;
using Android.Widget;
using Android.OS;
using PieDialog;
using PieDialog.PieViews;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;
using Android.Graphics;
using System;

namespace Demo
{
    [Activity(Label = "Demo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            PieDialogBuilder builder = new PieDialogBuilder(this);
            builder.SetIcon(new PieIcon()
            {
                CornerRadius = 35,
                Gravity = PieGravityProperties.Center,
                Image = "download.jpg",
                IsUrl = false,
                Location = PieLocations.Top,
                PlaceHolder = "download.jpg",
                ScaleType = ImageView.ScaleType.CenterCrop,
                StrokeThickness = 10,
                Width = 0,
                Height = 0,
                WithStroke = true
            });
            builder.SetTitle(new PieTextArea()
            {
                Animation = PieAnimations.NoAnimation,
                Padding = new PieThickness(0, 0, 10, 0),
                Ellipsize = PieEllipsizes.None,
                Gravity = PieGravityProperties.Center,
                Font = "bpgmrgvlovani.ttf",
                MaxLines = 0,
                FontStyle = Android.Graphics.TypefaceStyle.Normal,
                Text = "რეგისტრაცია",
                TextColor = new Android.Graphics.Color(115, 116, 115),
                TextSize = 19,
                TextSizeFormat = Android.Util.ComplexUnitType.Dip
            });
            builder.SetSubTitle(new PieTextArea()
            {
                Padding = new PieThickness(0, 0, 20, 10),
                Animation = PieAnimations.NoAnimation,
                Ellipsize = PieEllipsizes.None,
                Gravity = PieGravityProperties.Center,
                MaxLines = 0,
                Font = "bpgmrgvlovani.ttf",
                FontStyle = Android.Graphics.TypefaceStyle.Normal,
                Text = "შეიყვანეთ ინფორმაცია",
                TextColor = new Android.Graphics.Color(115, 116, 115),
                TextSize = 15,
                TextSizeFormat = Android.Util.ComplexUnitType.Dip
            });

            builder.AddDateTimePicker(new PieDatePicker()
            {
                Font = "bpgmrgvlovani.ttf",
                FontStyle = TypefaceStyle.Normal,
                PlaceHolder = "Date Of Birth",
                DefaultDate = DateTime.Now,
            }, out var BirthDateGetter);

            builder.AddEditText(new PieEditText()
            {
                Font = "bpgmrgvlovani.ttf",
                FontStyle = TypefaceStyle.Normal,
                PlaceHolder = "Name",
                Gravity = PieGravityProperties.Left,
                SingleLine = true
            }, out var NameGetter);

            builder.AddButton(new PieButton()
            {
                BackgroundColorSet = new System.Collections.Generic.List<Android.Graphics.Color>()
                {
                    Color.Rgb(3, 183, 111)
                },
                Clicked = () =>
                {
                    var Date = BirthDateGetter();
                    var Name = NameGetter();
                    FindViewById<TextView>(Resource.Id.nameTxt).Text = Name;

                    FindViewById<TextView>(Resource.Id.selectedDateTxt).Text = Date.ToShortDateString();
                    builder.DismissDialog();
                },
                GradientDirection = PieGradientDirection.LeftRight,
                CornerRadius = 5,
                Ellipsize = PieEllipsizes.None,
                MaxLines = 0,
                Margin = new PieThickness(10, 10, 10, 0),
                FontStyle = TypefaceStyle.Normal,
                Padding = new PieThickness(10, 10, 10, 10),
                Text = "დადასტურება",
                Font = "bpgmrgvlovani.ttf",
                TextAlignment = Android.Views.TextAlignment.Center,
                TextColor = new Android.Graphics.Color(255, 255, 255),
                TextGravity = PieGravityProperties.Center,
                TextSize = 13,
                TextSizeFormat = Android.Util.ComplexUnitType.Dip
            });
            FindViewById<Button>(Resource.Id.showDlgBtn).Click += (u, e) =>
            {
                builder.ShowDialog();
            };
        }
    }
}

