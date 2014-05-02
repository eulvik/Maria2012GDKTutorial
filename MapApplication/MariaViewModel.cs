using System.Collections.ObjectModel;
using TPG.Maria.Contracts;
using TPG.Maria.GridLayer;
using TPG.Maria.MapLayer;

namespace MapApplication
{
    public class MariaViewModel
    {
        public ObservableCollection<IMariaLayer> Layers { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public GridViewModel GridViewModel { get; set; }
        public LabelsViewModel LabelsViewModel { get; set; }

        public MariaViewModel()
        {
            Layers = new ObservableCollection<IMariaLayer>();

            var mariaMapLayer = new MapLayer();
            MapViewModel = new MapViewModel(mariaMapLayer);
            Layers.Add(mariaMapLayer);

            var mariaGridLayer = new GridLayer();
            GridViewModel = new GridViewModel(mariaGridLayer);
            Layers.Add(mariaGridLayer);

            var mariaLabelsLayer = new LabelsLayer(mariaMapLayer);
            LabelsViewModel = new LabelsViewModel(mariaLabelsLayer);
            Layers.Add(mariaLabelsLayer);
        }
    }
}