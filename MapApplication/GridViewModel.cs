using System.Collections.ObjectModel;
using System.Linq;
using TPG.GeoFramework.GridLayer.Contracts;
using TPG.Maria.GridContracts;

namespace MapApplication
{
    public class GridViewModel
    {
        private readonly IMariaGridLayer _gridLayer;

        public ObservableCollection<IGridRenderer> Grids { get; set; }
 
        public GridViewModel(IMariaGridLayer gridLayer)
        {
            Grids = new ObservableCollection<IGridRenderer>();
            _gridLayer = gridLayer;
            _gridLayer.LayerInitialized += OnLayerIntitialized;
        }

        private void OnLayerIntitialized()
        {
            _gridLayer.Grids.First().Visible = true;

            //Make a copy of available grids.
            foreach (IGridRenderer gridRenderer in _gridLayer.Grids)
            {
                Grids.Add(gridRenderer);
            }
        }
    }
}