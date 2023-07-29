using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using XafDiagramSample.Module.BusinessObjects.DiagramTest;

namespace XAF.Module.Win.MonitoringDesigner.DiagramItems
{
    internal class BoardDiagramContainer : DiagramContainer
        , IPersistentDiagramItem

    {
        public MonitoringBoard MonitoringBoard { get; }
        public Guid Guid => MonitoringBoard.Guid;

        public BoardDiagramContainer(MonitoringBoard monitoringBoard)
        {
            MonitoringBoard = monitoringBoard;
            LoadItemInfo();
        }



        public void LoadItemInfo()
        {
            this.Header = MonitoringBoard.Code;
            this.Shape = StandardContainers.Corners;
            Position = new PointFloat(100, 50);
            Width = 1000;
            Height = 600;
            ShowHeader = true;
            CanCollapse = true;
            IsCollapsed = false;
            CanCopy = true;
            CanMove = true;
        }

        public void SaveItemInfo()
        {
        }
    }
}
