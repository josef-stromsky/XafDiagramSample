using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    internal class ProcessingNodeShape : DiagramShape
        , IPersistentDiagramItem
    {
        public ProcessingNode ProcessingNode { get; }

        public Guid Guid => ProcessingNode.Guid;

        public ProcessingNodeShape(ProcessingNode processingNode)
        {
            ProcessingNode = processingNode;
            LoadItemInfo();
        }

        public void SaveItemInfo()
        {
            ProcessingNode.PositionX = Position.X;
            ProcessingNode.PositionY = Position.Y;
            ProcessingNode.Width = Width;
            ProcessingNode.Height = Height;
        }
        public void LoadItemInfo()
        {
            Position = new PointFloat(
                ProcessingNode.PositionX,
                ProcessingNode.PositionY
                );

            Width = ProcessingNode.Width;
            Height = ProcessingNode.Height;

            Shape = BasicShapes.Rectangle;
            Content = ProcessingNode.Code;
        }
    }
}
