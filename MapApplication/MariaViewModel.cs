using System.Collections.ObjectModel;
using System.Windows.Input;
using TPG.Maria.Contracts;
using TPG.Maria.DrawObjectLayer;
using TPG.Maria.GridLayer;
using TPG.Maria.MapLayer;
using TPG.Maria.TrackLayer;

namespace MapApplication
{
    public class MariaViewModel
    {
        public ObservableCollection<IMariaLayer> Layers { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public GridViewModel GridViewModel { get; set; }
        public LabelsViewModel LabelsViewModel { get; set; }
        public TrackViewModel TrackViewModel { get; set; }
        public DrawObjectViewModel DrawObjectViewModel { get; set; }

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

            var mariaTracksLayer = new TrackLayer();
            TrackViewModel = new TrackViewModel(mariaTracksLayer);
            Layers.Add(mariaTracksLayer);

            var mariaDrawObjectLayer = new DrawObjectLayer {InitializeGenericCreationWorkflows = true};
            DrawObjectViewModel = new DrawObjectViewModel(mariaDrawObjectLayer);
            Layers.Add(mariaDrawObjectLayer);
        }
    }
}