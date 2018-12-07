using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using PieDialog.PieEnums;
using PieDialog.PieUtilities;

namespace PieDialog.PieViews
{
    public class PieEditText : PieBaseView
    {
        public PieGravityProperties Gravity { get; set; } = PieGravityProperties.Center;

        public bool SingleLine { get; set; } = true;

        public string PlaceHolder { get; set; } = string.Empty;

        public Color TextColor { get; set; } = new Color(0, 0, 0);

        public Color HintColor { get; set; } = new Color(202, 202, 202);

        public Color UnderlineColor { get; set; } = new Color(202, 202, 202);

        public PieThickness Margin { get; set; } = new PieThickness(10, 10, 10, 10);

        public int TextSize { get; set; } = 15;

        public ComplexUnitType TextSizeFormat { get; set; } = ComplexUnitType.Sp;

        public int Font { get; set; } = -1;

        public string Value { get { return GetValue(); } }

        public int MaxLines { get; set; } = -1;

        public TypefaceStyle FontStyle { get; set; } = TypefaceStyle.Normal;

        private Func<string> _getValue { get; set; }

        public Func<string> GetValue { get; private set; }

        public InputTypes InputType { get; set; } = InputTypes.ClassText;

        public PieEditText()
        {
            GetValue = () =>
            {
                if (_getValue == null)
                    throw new Exception("View Has Not Been Initialized Yet");
                return _getValue();
            };
        }
        internal override View GetView(Context context)
        {
            EditText editText = new EditText(context);
            editText.Hint = PlaceHolder;
            editText.SetTextColor(TextColor);
            editText.SetHintTextColor(HintColor);
            editText.Background.SetColorFilter(UnderlineColor, PorterDuff.Mode.SrcAtop);
            editText.SetTextSize(TextSizeFormat, TextSize);
            editText.InputType = InputType;
            editText.Gravity = (Gravity == PieGravityProperties.Center) ? GravityFlags.Center : (Gravity == PieGravityProperties.Left) ? GravityFlags.Left : GravityFlags.Right;
            editText.SetSingleLine(SingleLine);
            if (MaxLines > 0)
            {
                editText.SetMaxLines(MaxLines);
            }

            if (Font >= 0)
            {
                Typeface face = context.Resources.GetFont(Font);
                editText.SetTypeface(face, FontStyle);
            }

            editText.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            (editText.LayoutParameters as LinearLayout.LayoutParams).SetMargins(PxToDp(Margin.Left), PxToDp(Margin.Top), PxToDp(Margin.Right), PxToDp(Margin.Bottom));

            _getValue = () =>
            {
                return editText.Text;
            };

            return editText;
        }
    }
}