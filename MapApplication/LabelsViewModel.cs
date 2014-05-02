using TPG.Maria.MapContracts;
using TPG.Maria.MapLayer;

namespace MapApplication
{
    public class LabelsViewModel
    {
        private readonly IMariaLabelsLayer _mariaLabelsLayer;

        public double Opacity
        {
            get { return _mariaLabelsLayer.Opacity; }
            set { _mariaLabelsLayer.Opacity = value; }
        }

        public LabelsViewModel(IMariaLabelsLayer mariaLabelsLayer)
        {
            _mariaLabelsLayer = mariaLabelsLayer;
            _mariaLabelsLayer.LayerInitialized += OnLayerInitialized;
        }

        private void OnLayerInitialized()
        {
        }
    }
}