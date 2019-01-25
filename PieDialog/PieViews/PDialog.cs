using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;

namespace PieDialog.PieViews
{
    public class PDialog
    {
        public PieAnimations DialogAnimation { get; set; }
        public PieTextArea Title { get; set; } = null;
        public PieIcon Icon { get; set; } = null;
        public PieThickness SeparatorPadding { get; set; } = new PieThickness(0, 0, 0, 0);
        public PieTextArea Subtitle { get; set; } = null;
        public Color DialogColor { get; set; } = new Color(255, 255, 255);
        public int CornerRadius { get; set; } = 5;
        public List<PieBaseView> Rows { get; set; } = null;

        internal FrameLayout GetView(Context context)
        {
            FrameLayout mainFrame = new FrameLayout(context);
            mainFrame.FocusableInTouchMode = true;
            mainFrame.Focusable = true;
            mainFrame.SetClipChildren(false);
            mainFrame.SetClipToPadding(false);
            mainFrame.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent, GravityFlags.Center);
            float density = context.Resources.DisplayMetrics.Density;
            CardView card = new CardView(context);
            card.Radius = density * 8;
            card.SetClipToPadding(false);
            card.SetClipChildren(false);
            card.ClipToOutline = false;
            card.SetCardBackgroundColor(DialogColor);
            card.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent, GravityFlags.CenterVertical);
            LinearLayout body = new LinearLayout(context);
            var space = new Space(context);
            body.LayoutParameters = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            body.SetVerticalGravity(GravityFlags.Center);
            body.SetClipToPadding(false);
            body.SetClipChildren(false);
            body.Orientation = Orientation.Vertical;
            FrameLayout varFrame = new FrameLayout(context);
            varFrame.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent);
            varFrame.AddView(card);
            mainFrame.AddView(varFrame);
            if (Icon != null)
            {
                var icon = Icon.GetView(context);
                icon.SetCardBackgroundColor(DialogColor);
                if (Icon.Location == PieLocations.Top)
                {
                    GravityFlags gravity = GravityFlags.CenterHorizontal;
                    if (Icon.Gravity == PieGravityProperties.Left)
                        gravity = GravityFlags.Left;
                    else if (Icon.Gravity == PieGravityProperties.Right)
                        gravity = GravityFlags.Right;
                    (icon.LayoutParameters as FrameLayout.LayoutParams).Gravity = GravityFlags.Top | gravity;
                    varFrame.SetPadding(0, (int)(density * Icon.Height / 2), 0, 0);
                    mainFrame.AddView(icon);
                    card.SetContentPadding((int)(density * 8), (int)(density * (Icon.Height / 2 + 4)), (int)(density * 8), (int)(density * 8));
                }
                else if (Icon.Location == PieLocations.Right)
                {
                    GravityFlags gravity = GravityFlags.CenterVertical;
                    if (Icon.Gravity == PieGravityProperties.Left)
                        gravity = GravityFlags.Top;
                    else if (Icon.Gravity == PieGravityProperties.Right)
                        gravity = GravityFlags.Bottom;
                    (icon.LayoutParameters as FrameLayout.LayoutParams).Gravity = GravityFlags.Right | gravity;
                    varFrame.SetPadding(0, 0, (int)(density * Icon.Width / 2), 0);
                    mainFrame.AddView(icon);
                    card.SetContentPadding((int)(density * 8), (int)(density * 8), (int)(density * (Icon.Width / 2 + 4)), (int)(density * 8));
                    card.Post(() =>
                    {
                        if (card.Height <= Icon.Height * density)
                        {
                            card.LayoutParameters = new FrameLayout.LayoutParams(card.Width, (int)(((Icon.Height + 30) * density)), GravityFlags.CenterVertical);
                            body.LayoutParameters = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.MatchParent);
                        }
                    });
                }
                else if (Icon.Location == PieLocations.Left)
                {
                    GravityFlags gravity = GravityFlags.CenterVertical;
                    if (Icon.Gravity == PieGravityProperties.Left)
                        gravity = GravityFlags.Bottom;
                    else if (Icon.Gravity == PieGravityProperties.Right)
                        gravity = GravityFlags.Top;
                    (icon.LayoutParameters as FrameLayout.LayoutParams).Gravity = GravityFlags.Left | gravity;
                    varFrame.SetPadding((int)(density * Icon.Width / 2), 0, 0, 0);
                    mainFrame.AddView(icon);
                    card.SetContentPadding((int)(density * (Icon.Width / 2 + 4)), (int)(density * 8), (int)(density * 8), (int)(density * 8));
                    card.Post(() =>
                    {
                        if (card.Height <= Icon.Height * density)
                        {
                            card.LayoutParameters = new FrameLayout.LayoutParams(card.Width, (int)(((Icon.Height + 30) * density)), GravityFlags.CenterVertical);
                            body.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent);
                        }
                    });
                }
                else
                {
                    GravityFlags gravity = GravityFlags.CenterHorizontal;
                    if (Icon.Gravity == PieGravityProperties.Left)
                        gravity = GravityFlags.Left;
                    else if (Icon.Gravity == PieGravityProperties.Right)
                        gravity = GravityFlags.Right;
                    (icon.LayoutParameters as FrameLayout.LayoutParams).Gravity = GravityFlags.Bottom | gravity;
                    varFrame.SetPadding(0, 0, 0, (int)(density * Icon.Height / 2));
                    mainFrame.AddView(icon);
                    card.SetContentPadding((int)(density * 8), (int)(density * 8), (int)(density * 8), (int)(density * (Icon.Height / 2 + 4)));
                }
            }

            if (Title != null)
            {
                var title = Title.GetView(context);
                body.AddView(title);
            }

            if (Subtitle != null)
            {
                var subtitle = Subtitle.GetView(context);
                body.AddView(subtitle);
            }

            if (Rows != null && Rows.Count > 0)
            {
                foreach (var item in Rows)
                {
                    body.AddView(item.GetView(context));
                }
            }
            ScrollView scroll = new ScrollView(context);
            scroll.LayoutParameters = new CardView.LayoutParams(CardView.LayoutParams.MatchParent, CardView.LayoutParams.MatchParent);
            scroll.VerticalScrollBarEnabled = false;
            scroll.AddView(body);
            scroll.OverScrollMode = OverScrollMode.Never;
            card.AddView(scroll);
            return mainFrame;
        }
    }
}