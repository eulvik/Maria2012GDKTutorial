using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TPG.DrawObjects.Contracts.DrawObjectState;
using TPG.GeoFramework.Core;
using TPG.Maria.DrawObjectContracts;

namespace MapApplication
{
    public class DrawObjectViewModel
    {
        private readonly IMariaDrawObjectLayer _mariaDrawObjectLayer;
        private const string _dataDir = "data";

        public DrawObjectViewModel(IMariaDrawObjectLayer mariaDrawObjectLayer)
        {
            _mariaDrawObjectLayer = mariaDrawObjectLayer;
            _mariaDrawObjectLayer.LayerInitialized += OnLayerInitialized;
        }

        private void OnLayerInitialized()
        {
            _mariaDrawObjectLayer.ExtendedDrawObjectLayer.LayerChanged += OnLayerChanged;

            //Get all stored draw objects from files in the data directory.
            if (!Directory.Exists(_dataDir))
                Directory.CreateDirectory(_dataDir);

            var files = Directory.GetFiles(_dataDir);
            foreach (var file in files)
            {
                if (Path.GetExtension(file).Contains("xml"))
                {
                    string xml = File.ReadAllText(file);
                    _mariaDrawObjectLayer.UpdateStore(xml);
                }
            }
        }

        private void OnLayerChanged(object sender, DataStoreChangedEventArgs args)
        {
            if (args.DataStoreChangedAction == DataStoreChangeAction.Delete)
            {
                foreach (string affectedDrawObject in args.AffectedDrawObjects)
                {
                    string file = string.Format("{0}/{1}.xml", _dataDir, affectedDrawObject);
                    if (File.Exists(file))
                        File.Delete(file);
                }
            }
            else
            {
                foreach (string affectedDrawObject in args.AffectedDrawObjects)
                {
                    string file = string.Format("{0}/{1}.xml", _dataDir, affectedDrawObject);
                    if (File.Exists(file))
                        File.Delete(file);
                    File.WriteAllText(file, _mariaDrawObjectLayer.GetDrawObjectXMLFromStore(affectedDrawObject));
                }
            }
        }

        public ObservableCollection<ICreationWorkflow> Workflows
        {
            get { return _mariaDrawObjectLayer.GenericCreationWorkflows; }
        }

        private DelegateCommand _creationWorkflowActivatedCommand;
        public ICommand CreationWorkflowActivatedCommand
        {
            get {
                return _creationWorkflowActivatedCommand ??
                       (_creationWorkflowActivatedCommand = new DelegateCommand(CreationWorkflowActivated));
            }
        }

        private void CreationWorkflowActivated(object obj)
        {
            var creationWorkflow = obj as ICreationWorkflow;
            if (creationWorkflow == null)
                return;

            _mariaDrawObjectLayer.ExtendedDrawObjectLayer.ActivateCreationWorkflow(creationWorkflow.ObjectTypeId);
        }

        public ICommand EditPointsCommand
        {
            get { return _mariaDrawObjectLayer.EditPointsCommand; }
        }

        public ICommand DeleteDrawObjectCommand
        {
            get { return _mariaDrawObjectLayer.DeleteDrawObjectCommand; }
        }
    }
}