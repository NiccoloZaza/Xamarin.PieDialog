using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PieDialog.CacheUtils
{
    public class FontCache
    {
        private static FontCache _instance = null;
        public static FontCache Instance { get { return _instance ?? (_instance = new FontCache()); } }

        private FontCache() { }

        private Dictionary<string, Typeface> cache = new Dictionary<string, Typeface>();

        public Typeface GetFont(string name, Context context)
        {
            if (cache.TryGetValue(name, out var font))
            {
                return font;
            }
            try
            {
                Typeface tmp = Typeface.CreateFromAsset(context.Assets, name);
                cache.TryAdd(name, tmp);
                return tmp;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}