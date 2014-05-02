using System.Collections.ObjectModel;
using TPG.Maria.Common;
using TPG.Maria.Contracts;
using TPG.Maria.TrackContracts;

namespace MapApplication
{
    public class TrackViewModel
    {
        private readonly IMariaTrackLayer _mariaTracksLayer;

        public TrackViewModel(IMariaTrackLayer mariaTracksLayer)
        {
            _mariaTracksLayer = mariaTracksLayer;
            _mariaTracksLayer.LayerInitialized += OnLayerInitialized;
        }

        private void OnLayerInitialized()
        {
            //We first create the track lists that we are interested in.
            _mariaTracksLayer.TrackLists = new ObservableCollection<string> { "ais.test" };

            //Tell the track layer which service fot get tracks from.
            //The name "TrackService" corresponds to the entry for the endpoint in the App.config.
            _mariaTracksLayer.TrackServices = new ObservableCollection<IMariaService> { new MariaService("TrackService") };
            _mariaTracksLayer.ActiveTrackService = _mariaTracksLayer.TrackServices[0];
            
            //Finally we tell which track list to display tracks from.
            _mariaTracksLayer.ActiveTrackList = _mariaTracksLayer.TrackLists[0];
        }
    }
}