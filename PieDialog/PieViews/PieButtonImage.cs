using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;

namespace PieDialog.PieViews
{
    public class PieButtonImage
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string PlaceHolder { get; set; }
        private int Id { get; set; } = -1;
        private string Image { get; set; }
        private bool IsUrl { get; set; } = false;
        public PieThickness Margin { get; set; } = new PieThickness(10, 0, 0, 0);
        public PieThickness Padding { get; set; } = new PieThickness(0, 0, 0, 0);
        public ImageView.ScaleType ScaleType { get; set; } = ImageView.ScaleType.FitXy;
        public PieButtonImageLocation Location { get; set; } = PieButtonImageLocation.Left;

        public PieButtonImage(int Id)
        {
            this.Id = Id;
        }

        public PieButtonImage(string ImageUrl)
        {
            Image = ImageUrl;
        }

        public View GetView(Context context)
        {
            var Density = context.Resources.DisplayMetrics.Density;
            ImageViewAsync image = new ImageViewAsync(context);
            if (Id == -1)
            {
                if (IsUrl)
                    ImageService.
                                Instance.
                                LoadUrl(Image).
                                LoadingPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).
                                ErrorPlaceholder(PlaceHolder, FFImageLoading.Work.ImageSource.CompiledResource).
                                IntoAsync(image);
                else
                    ImageService.
                                Instance.
                                LoadCompiledResource(Image).
                                LoadingPlaceholder(Image).
                                ErrorPlaceholder(Image).
                                IntoAsync(image);
            }
            else
            {
                image.SetImageResource(Id);
            }
            image.LayoutParameters = new LinearLayout.LayoutParams((Width == 0 || Height == 0 ? (int)(40 * Density) : Width), LinearLayout.LayoutParams.MatchParent);
            (image.LayoutParameters as LinearLayout.LayoutParams).SetMargins((int)(Margin.Left * Density), (int)(Margin.Top * Density), (int)(Margin.Right * Density), (int)(Margin.Bottom * Density));
            image.SetPadding((int)(Padding.Left * Density), (int)(Padding.Top * Density), (int)(Padding.Right * Density), (int)(Padding.Bottom * Density));
            return image;
        }
    }
}