using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;
using PieDialog.PieEnums;

namespace PieDialog.PieViews
{
    public class PieIcon
    {
        private int _width = 0;
        private int _hieght = 0;
        public int Width { get { if (WithStroke) return _width + StrokeThickness; return _width; } set { _width = value; } }
        public int Height { get { if (WithStroke) return _hieght + StrokeThickness; return _hieght; } set { _hieght = value; } }
        public PieGravityProperties Gravity { get; set; } = PieGravityProperties.Center;
        public PieLocations Location { get; set; } = PieLocations.Top;
        public int CornerRadius { get; set; } = 0;
        public bool WithStroke { get; set; } = true;
        public int StrokeThickness { get; set; } = 0;
        public string PlaceHolder { get; set; }
        public string Image { get; set; }
        public bool IsUrl { get; set; } = false;

        public ImageView.ScaleType ScaleType { get; set; } = ImageView.ScaleType.FitXy;

        internal CardView GetView(Context context)
        {
            CardView StrokeCard = new CardView(context);
            StrokeCard.CardElevation = 0;
            float density = context.Resources.DisplayMetrics.Density;
            CardView Card = new CardView(context);
            Card.CardElevation = 0;
            ImageViewAsync Image = new ImageViewAsync(context);
            Image.SetScaleType(ScaleType);
            if (IsUrl)
            {
                ImageService.Instance.LoadUrl(this.Image).LoadingPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).ErrorPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).IntoAsync(Image);
            }
            else
            {ImageService.Instance.LoadCompiledResource(this.Image).LoadingPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).ErrorPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).IntoAsync(Image);
            }
            Image.LayoutParameters = new CardView.LayoutParams(CardView.LayoutParams.MatchParent, CardView.LayoutParams.MatchParent);
            if (Width - StrokeThickness <= 0 || Height - StrokeThickness <= 0)
            {
                Width = (int)((int)(context.Resources.DisplayMetrics.WidthPixels / 4.5) / density);
                Height = (int)((int)(context.Resources.DisplayMetrics.WidthPixels / 4.5) / density);
                Card.LayoutParameters = new CardView.LayoutParams((int)(_width * density), (int)(_hieght * density), GravityFlags.Center);
                CornerRadius = _hieght / 2;
            }
            else
            {
                Card.LayoutParameters = new CardView.LayoutParams((int)(density * (Width - StrokeThickness)), (int)(density * (Height - StrokeThickness)), GravityFlags.Center);
            }
            var cornerradiusscale = CornerRadius / (_width * 1.0);
            StrokeCard.LayoutParameters = new FrameLayout.LayoutParams((int)(density * Width), (int)(density * Height));
            StrokeCard.Radius = (int)(density * (Width * cornerradiusscale));
            Card.Radius = CornerRadius * density;
            Card.AddView(Image);
            StrokeCard.AddView(Card);
            return StrokeCard;
        }
    }
}