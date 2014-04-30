using System.Collections.ObjectModel;
using System.Linq;
using TPG.GeoUnits;
using TPG.Maria.MapContracts;

namespace MapApplication
{
    public class MapViewModel
    {
        private readonly IMariaMapLayer _mariaMapLayer;
        public ObservableCollection<string> ActiveMapNames { get; set; }
        
        public GeoPos CenterPosition { get; set; }

        public double Scale
        {
            get
            {
                return _mariaMapLayer.GeoContext.CenterScale;
            }
            set
            {
                _mariaMapLayer.GeoContext.CenterScale = value;
            }
        }

        public MapViewModel(IMariaMapLayer mariaMapLayer)
        {
            ActiveMapNames = new ObservableCollection<string>();
            _mariaMapLayer = mariaMapLayer;
            _mariaMapLayer.LayerInitialized += OnLayerIntialized;
        }

        private void OnLayerIntialized()
        {
            Scale = 100000;
            CenterPosition = new GeoPos(60, 10);

            string mapToShow = _mariaMapLayer.ActiveMapNames.First();
            _mariaMapLayer.ActiveMapName = mapToShow;

            ActiveMapNames.Clear();
            foreach (string activeMapName in _mariaMapLayer.ActiveMapNames)
                ActiveMapNames.Add(activeMapName);
        }
    }
}