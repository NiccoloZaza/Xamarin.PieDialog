using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using PieDialog.PieEnums;
using PieDialog.PieViews;

namespace PieDialog
{
    public class PieDialogBuilder
    {
        internal static float Density { get; private set; }

        private PDialog Dialog { get; set; } = new PDialog();
        Dialog dialog { get; set; }

        private Activity _activity;

        public PieDialogBuilder(Activity activity)
        {
            _activity = activity;
            Density = _activity.Resources.DisplayMetrics.Density;
        }

        public void AddDateTimePicker(PieDatePicker datePicker, out Func<DateTime> ValueGetter)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(datePicker);
            ValueGetter = datePicker.GetValue;
        }

        public void AddDateTimePicker(PieDatePicker datePicker)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(datePicker);
        }

        public void AddEditText(PieEditText editText, out Func<string> ValueGetter)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(editText);
            ValueGetter = editText.GetValue;
        }

        public void AddEditText(PieEditText editText)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(editText);
        }

        public void SetTitle(PieTextArea text)
        {
            Dialog.Title = text;
        }

        public void SetIcon(PieIcon icon)
        {
            Dialog.Icon = icon;
        }

        public void SetSubTitle(PieTextArea text)
        {
            Dialog.Subtitle = text;
        }

        public void AddButton(PieButton row)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(row);
        }

        public void AddTextArea(PieTextArea row)
        {
            if (Dialog.Rows == null)
            {
                Dialog.Rows = new List<PieBaseView>();
            }
            Dialog.Rows.Add(row);
        }

        public void SetDialogAnimation(PieAnimations animation)
        {
            Dialog.DialogAnimation = animation;
        }

        public void SetDialogColor(Color clr)
        {
            Dialog.DialogColor = clr;
        }

        public void ShowDialog()
        {
            dialog = new Dialog(_activity);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            dialog.SetCancelable(true);
            var content = Dialog.GetView(_activity);
            dialog.SetContentView(content);
            dialog.Show();
        }

        public void DismissDialog()
        {
            if (dialog.IsShowing)
                dialog.Dismiss();
        }
    }
}