using System.Collections.ObjectModel;
using System.Linq;
using TPG.GeoFramework.Contracts;
using TPG.GeoFramework.TrackCore.Contracts;
using TPG.GeoFramework.TrackLayer.Contracts.Selection;
using TPG.Maria.Common;
using TPG.Maria.Contracts;
using TPG.Maria.TrackContracts;

namespace MapApplication
{
    public class TrackViewModel
    {
        private readonly IMariaTrackLayer _mariaTracksLayer;
        public ObservableCollection<ITrackData> SelectedTracks { get; private set; } 

        public TrackViewModel(IMariaTrackLayer mariaTracksLayer)
        {
            SelectedTracks = new ObservableCollection<ITrackData>();
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

            //Handle the track selection changed event in order to show information about currently selected track.
            _mariaTracksLayer.ExtendedTrackLayer.TrackSelectionChanged += OnTrackSelectionChanged;
        }

        private void OnTrackSelectionChanged(object sender, TrackSelectionChangedEventArgs args)
        {
            //Remove tracks that has been deselected.
            var deselectedTracksToRemove = SelectedTracks.Where(st => args.DeselectedTracks.Any(dt => st.TrackItemId.InstanceId == dt.InstanceId)).ToList();
            foreach (ITrackData trackDataToRemove in deselectedTracksToRemove)
            {
                SelectedTracks.Remove(trackDataToRemove);
            }

            //Extract the track data objects using selected id.
            string[] selectedIds = args.SelectedTracks.Select(x => x.InstanceId).ToArray();
            ITrackData[] selectedTrackDatas = _mariaTracksLayer.GetTrackData(selectedIds);

            //Push new selected tracks to our list of selected tracks.
            foreach (ITrackData selectedTrackData in selectedTrackDatas)
            {
                SelectedTracks.Add(selectedTrackData);
            }
        }
    }
}