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

namespace PieDialog.PieUtilities
{
    public class PieThickness
    {
        internal int Left { get; set; } = 0;
        internal int Right { get; set; } = 0;
        internal int Bottom { get; set; } = 0;
        internal int Top { get; set; } = 0;

        internal PieThickness()
        { }

        public PieThickness(int left, int right, int bottom, int top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }
    }
}