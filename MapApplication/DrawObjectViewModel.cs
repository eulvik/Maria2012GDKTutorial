using System.Collections.ObjectModel;
using System.Windows.Input;
using TPG.GeoFramework.Core;
using TPG.Maria.DrawObjectContracts;

namespace MapApplication
{
    public class DrawObjectViewModel
    {
        private readonly IMariaDrawObjectLayer _mariaDrawObjectLayer;

        public DrawObjectViewModel(IMariaDrawObjectLayer mariaDrawObjectLayer)
        {
            _mariaDrawObjectLayer = mariaDrawObjectLayer;
            _mariaDrawObjectLayer.LayerInitialized += OnLayerInitialized;
        }

        private void OnLayerInitialized()
        {
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