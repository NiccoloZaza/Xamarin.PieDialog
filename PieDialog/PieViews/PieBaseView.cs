using Android.Content;
using Android.Views;

namespace PieDialog.PieViews
{
    public abstract class PieBaseView
    {
        internal abstract View GetView(Context context);

        internal int PxToDp(int px)
        {
            return (int)(PieDialogBuilder.Density * px);
        }
    }
}