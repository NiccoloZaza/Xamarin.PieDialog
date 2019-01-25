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
using PieDialog.CacheUtils;
using PieDialog.PieUtilities;
using static Android.App.DatePickerDialog;

namespace PieDialog.PieViews
{
    public class PieDatePicker : PieBaseView
    {
        public DateTime DefaultDate { get; set; } = DateTime.Now;

        public string PlaceHolder { get; set; } = string.Empty;

        public Color TextColor { get; set; } = new Color(0, 0, 0);

        public Color HintColor { get; set; } = new Color(202, 202, 202);

        public Color UnderlineColor { get; set; } = new Color(202, 202, 202);

        public PieThickness Margin { get; set; } = new PieThickness(10, 10, 10, 10);

        public string Font { get; set; } = string.Empty;

        public DateTime Value { get { return GetValue(); } }

        public ComplexUnitType TextSizeFormat { get; set; } = ComplexUnitType.Sp;

        public int TextSize { get; set; } = 15;

        private Func<DateTime> _getValue { get; set; }

        public Func<DateTime> GetValue { get; private set; }

        public TypefaceStyle FontStyle { get; set; } = TypefaceStyle.Normal;

        public PieDatePicker()
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
            editText.Gravity = GravityFlags.Left;
            editText.SetSingleLine(true);
            editText.Focusable = true;
            if (!(string.IsNullOrEmpty(Font) || string.IsNullOrWhiteSpace(Font)))
            {
                try
                {
                    Typeface face = FontCache.Instance.GetFont(Font, context);
                    editText.SetTypeface(face, FontStyle);
                }
                catch (Exception) { }
            }

            editText.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            (editText.LayoutParameters as LinearLayout.LayoutParams).SetMargins(PxToDp(Margin.Left), PxToDp(Margin.Top), PxToDp(Margin.Right), PxToDp(Margin.Bottom));

            editText.Click += (u, e) =>
            {
                if (DateTime.TryParse(editText.Text, out DateTime result))
                    DefaultDate = result;

                DatePickerDialog dialog = new DatePickerDialog(
                    context,
                    new OnDateSetListener()
                    {
                        DateTimeSet = (date) =>
                        {
                            editText.Text = date.ToShortDateString();
                        }
                    },
                    DefaultDate.Year,
                    DefaultDate.Month,
                    DefaultDate.Day);
                dialog.Show();
            };

            _getValue = () =>
            {
                return DateTime.Parse(editText.Text);
            };

            return editText;
        }

        class OnDateSetListener : Java.Lang.Object, IOnDateSetListener
        {
            public Action<DateTime> DateTimeSet { get; set; }

            public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
            {
                DateTimeSet.Invoke(new DateTime(year, month, dayOfMonth));
            }
        }
    }
}