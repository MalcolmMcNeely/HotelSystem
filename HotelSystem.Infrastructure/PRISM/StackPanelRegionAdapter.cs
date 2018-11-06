using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HotelSystem.Infrastructure.PRISM
{
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        protected StackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) :
           base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
               {
                   if (e.Action == NotifyCollectionChangedAction.Add)
                   {
                       foreach (FrameworkElement element in e.NewItems)
                       {
                           regionTarget.Children.Add(element);
                       }
                   }

                   if (e.Action == NotifyCollectionChangedAction.Remove)
                   {
                       foreach (FrameworkElement element in e.NewItems)
                       {
                           regionTarget.Children.Remove(element);
                       }
                   }
               };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
