using TPG.Maria.MapContracts;
using TPG.Maria.MapLayer;

namespace MapApplication
{
    public class LabelsViewModel
    {
        private readonly IMariaLabelsLayer _mariaLabelsLayer;

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