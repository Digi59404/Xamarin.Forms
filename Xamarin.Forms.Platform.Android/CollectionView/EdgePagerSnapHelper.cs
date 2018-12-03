using Android.Support.V7.Widget;
using AView = Android.Views.View;

namespace Xamarin.Forms.Platform.Android
{
	internal abstract class EdgePagerSnapHelper : PagerSnapHelper
	{
		// CurrentTargetPosition will have this value until the user scrolls around
		protected int CurrentTargetPosition = -1;

		protected static OrientationHelper CreateOrientationHelper(RecyclerView.LayoutManager layoutManager)
		{
			return layoutManager.CanScrollHorizontally()
				? OrientationHelper.CreateHorizontalHelper(layoutManager)
				: OrientationHelper.CreateVerticalHelper(layoutManager);
		}

		protected static bool IsLayoutReversed(RecyclerView.LayoutManager layoutManager)
		{
			if (layoutManager is LinearLayoutManager linearLayoutManager)
			{
				return linearLayoutManager.ReverseLayout;
			}

			return false;
		}

		public override AView FindSnapView(RecyclerView.LayoutManager layoutManager)
		{
			if (layoutManager.ItemCount == 0)
			{
				return null;
			}

			if (!(layoutManager is LinearLayoutManager linearLayoutManager))
			{
				// Don't snap to anything if this isn't a LinearLayoutManager;
				return null;
			}

			var targetItemPosition = CurrentTargetPosition;

			if (targetItemPosition != -1)
			{
				return linearLayoutManager.FindViewByPosition(targetItemPosition);
			}

			return null;
		}

		public override int FindTargetSnapPosition(RecyclerView.LayoutManager layoutManager, int velocityX, int velocityY)
		{
			CurrentTargetPosition = base.FindTargetSnapPosition(layoutManager, velocityX, velocityY);
			return CurrentTargetPosition;
		}
	}
}