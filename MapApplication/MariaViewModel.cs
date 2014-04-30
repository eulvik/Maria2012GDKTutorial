using System.Collections.ObjectModel;
using TPG.Maria.Contracts;
using TPG.Maria.MapLayer;

namespace MapApplication
{
    public class MariaViewModel
    {
        public ObservableCollection<IMariaLayer> Layers { get; set; }
        public MapViewModel MapViewModel { get; set; }

        public MariaViewModel()
        {
            Layers = new ObservableCollection<IMariaLayer>();

            var mariaMapLayer = new MapLayer();
            MapViewModel = new MapViewModel(mariaMapLayer);
            Layers.Add(mariaMapLayer);
        }

    }
}