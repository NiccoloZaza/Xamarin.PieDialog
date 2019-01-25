using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PieDialog.CacheUtils;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;
using static Android.Text.TextUtils;

namespace PieDialog.PieViews
{
    public class PieButton : PieBaseView
    {
        public PieButtonImage Image { get; set; } = null;

        public PieGradientDirection GradientDirection { get; set; } = PieGradientDirection.LeftRight;
        public PieAnimations Animation { get; set; } = PieAnimations.NoAnimation;
        public int AnimationDelay { get; set; } = 0;
        public int AnimationDuration { get; set; } = 0;
        public Action Clicked { get; set; }
        public PieGravityProperties TextGravity { get; set; } = PieGravityProperties.Center;
        public PieThickness Padding { get; set; } = new PieThickness(0, 0, 10, 10);
        public string Text { get; set; } = "Button";
        public string Font { get; set; } = string.Empty;
        public int TextSize { get; set; } = 15;
        public ComplexUnitType TextSizeFormat { get; set; } = ComplexUnitType.Sp;
        public int MaxLines { get; set; } = -1;
        public PieEllipsizes Ellipsize { get; set; } = PieEllipsizes.None;
        public Color TextColor { get; set; } = new Color(158, 158, 158);

        public PieThickness Margin { get; set; } = new PieThickness(10, 10, 10, 10);

        public TypefaceStyle FontStyle { get; set; } = TypefaceStyle.Normal;

        public TextAlignment TextAlignment { get; set; } = TextAlignment.Center;

        public List<Color> BackgroundColorSet { get; set; } = new List<Color>() { new Color(68, 68, 68) };

        public int CornerRadius { get; set; } = 0;
        internal override View GetView(Context context)
        {
            CardView card = new CardView(context);
            LinearLayout innerLine = new LinearLayout(context);
            innerLine.Orientation = Orientation.Horizontal;
            innerLine.LayoutParameters = new CardView.LayoutParams(CardView.LayoutParams.MatchParent, CardView.LayoutParams.WrapContent);
            if (Image != null)
            {
                if (Image.Location == PieButtonImageLocation.Left)
                {
                    innerLine.AddView(Image.GetView(context));
                }
            }
            card.Click += (o, e) => { Clicked?.Invoke(); };
            float density = context.Resources.DisplayMetrics.Density;
            int px = (int)(CornerRadius * density);
            card.Radius = px;
            if (BackgroundColorSet == null || BackgroundColorSet.Count == 0)
            {
                card.SetCardBackgroundColor(Color.Rgb(68, 68, 68));
            }
            else
            {
                if (BackgroundColorSet.Count == 1)
                {
                    card.SetCardBackgroundColor(BackgroundColorSet[0]);
                }
                else
                {
                    int[] Colors = new int[] { };
                    foreach (var item in BackgroundColorSet)
                    {
                        Colors = Colors.Append(item).ToArray();
                    }
                    GradientDrawable.Orientation orientation;
                    switch (GradientDirection)
                    {
                        case PieGradientDirection.LeftRight:
                            orientation = GradientDrawable.Orientation.LeftRight;
                            break;
                        case PieGradientDirection.LeftTopRightbottom:
                            orientation = GradientDrawable.Orientation.TlBr;
                            break;
                        case PieGradientDirection.TopBottom:
                            orientation = GradientDrawable.Orientation.TopBottom;
                            break;
                        case PieGradientDirection.RightTopLeftbottom:
                            orientation = GradientDrawable.Orientation.TrBl;
                            break;
                        case PieGradientDirection.RightLeft:
                            orientation = GradientDrawable.Orientation.RightLeft;
                            break;
                        case PieGradientDirection.RightBottomLeftTop:
                            orientation = GradientDrawable.Orientation.BrTl;
                            break;
                        case PieGradientDirection.BottomTop:
                            orientation = GradientDrawable.Orientation.BottomTop;
                            break;
                        case PieGradientDirection.LeftBottomRightTop:
                            orientation = GradientDrawable.Orientation.BlTr;
                            break;
                        default:
                            orientation = GradientDrawable.Orientation.LeftRight;
                            break;
                    }
                    GradientDrawable gd = new GradientDrawable(orientation, Colors);
                    FrameLayout layout = new FrameLayout(context);
                    layout.LayoutParameters = new CardView.LayoutParams(CardView.LayoutParams.MatchParent, CardView.LayoutParams.MatchParent);
                    //ViewCompat.SetBackground(layout, gd);
                    card.AddView(layout);
                }
            }
            card.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            (card.LayoutParameters as LinearLayout.LayoutParams).SetMargins((int)(Margin.Left * density), (int)(Margin.Top * density), (int)(Margin.Right * density), (int)(Margin.Bottom * density));
            TextView text = new TextView(context);
            if (!(string.IsNullOrEmpty(Font) || string.IsNullOrWhiteSpace(Font)))
            {
                try
                {
                    Typeface face = FontCache.Instance.GetFont(Font, context);
                    text.SetTypeface(face, FontStyle);
                }
                catch (Exception) { }
            }
            text.SetPadding((int)(Padding.Left * density), (int)(Padding.Top * density), (int)(Padding.Right * density), (int)(Padding.Bottom * density));
            text.Text = Text;
            text.SetTextSize(TextSizeFormat, TextSize);
            text.SetTextColor(TextColor);
            if (MaxLines > 0)
                text.SetMaxLines(MaxLines);
            text.TextAlignment = TextAlignment;
            text.Gravity = (TextGravity == PieGravityProperties.Center) ? GravityFlags.Center : (TextGravity == PieGravityProperties.Left) ? GravityFlags.Left : GravityFlags.Right;
            text.Ellipsize = (Ellipsize == PieEllipsizes.End) ? TruncateAt.End : null;
            text.LayoutParameters = new CardView.LayoutParams(CardView.LayoutParams.MatchParent, CardView.LayoutParams.WrapContent);
            innerLine.AddView(text);
            if (Image != null)
            {
                if (Image.Location == PieButtonImageLocation.Right)
                {
                    innerLine.AddView(Image.GetView(context));
                }
            }
            card.AddView(innerLine);
            return card;
        }
    }
}