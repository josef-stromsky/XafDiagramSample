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
