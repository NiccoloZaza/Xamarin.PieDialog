using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;
using static Android.Text.TextUtils;

namespace PieDialog.PieViews
{
    public class PieTextArea : PieBaseView
    {
        public PieGravityProperties Gravity { get; set; } = PieGravityProperties.Center;
        public PieAnimations Animation { get; set; }
        public int AnimationDelay { get; set; } = 0;
        public int AnimationDuration { get; set; } = 0;
        public string Text { get; set; } = "Text";
        public int Font { get; set; } = -1;
        public int TextSize { get; set; } = 15;
        public ComplexUnitType TextSizeFormat { get; set; } = ComplexUnitType.Sp;
        public int MaxLines { get; set; } = -1;
        public PieEllipsizes Ellipsize { get; set; } = PieEllipsizes.None;
        public Color TextColor { get; set; } = new Color(158, 158, 158);

        public PieThickness Margin { get; set; } = new PieThickness(10, 10, 10, 10);

        public PieThickness Padding { get; set; } = new PieThickness();

        public TypefaceStyle FontStyle { get; set; } = TypefaceStyle.Normal;

        internal override View GetView(Context context)
        {
            TextView text = new TextView(context);
            float density = context.Resources.DisplayMetrics.Density;
            text.Text = Text;
            text.Gravity = (Gravity == PieGravityProperties.Center) ? GravityFlags.Center : (Gravity == PieGravityProperties.Left) ? GravityFlags.Left : GravityFlags.Right;
            text.SetTextColor(TextColor);

            text.SetPadding((int)(density * Padding.Left), (int)(Padding.Top), (int)(Padding.Right), (int)(Padding.Bottom));

            text.Ellipsize = (Ellipsize == PieEllipsizes.End) ? TruncateAt.End : null;

            if (MaxLines > 0)
            {
                text.SetMaxLines(MaxLines);
            }

            text.SetTextSize(TextSizeFormat, TextSize);
            try
            {
                if (Font >= 0)
                {
                    Typeface face = context.Resources.GetFont(Font);
                    text.SetTypeface(face, FontStyle);
                }
            }
            catch (Exception)
            { }
            text.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);

            return text;
        }
    }
}