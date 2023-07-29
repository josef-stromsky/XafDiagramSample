using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using System;
using System.Drawing;
using XAF.Module.Win.MonitoringDesigner.DiagramItems;
using XafDiagramSample.Module.BusinessObjects.DiagramTest;

namespace XafDiagramSample.Win.Editors.DiagramItems
{
    internal class ProcessingFlowConnector
        : DiagramConnector
        , IPersistentDiagramItem
    {
        public ProcessingFlowConnector(ProcessingFlow processingFlow)
        {
            ProcessingFlow = processingFlow;
        }
        //public ProcessingFlowConnector(ProcessingNodeShape beginItem, ProcessingNodeShape endItem, ProcessingFlow processingFlow)
        //    : base(beginItem, endItem)
        //{
        //    ProcessingFlow = processingFlow;
        //}

        public ProcessingFlow ProcessingFlow { get; }

        public Guid Guid => ProcessingFlow.Guid;


        public void SaveItemInfo()
        {
        }
        public void LoadItemInfo()
        {
            BeginArrow = ArrowDescriptions.ClosedDot;
            BeginArrowSize = new SizeF(5, 5);
            BeginLeftLabel = "OUT";
            BeginRightLabel = "Out-1";

            Content = ProcessingFlow.FlowType.ToString();


            EndArrow = ArrowDescriptions.Filled90;
            EndArrowSize = new SizeF(15, 15);
            EndLeftLabel = "IN";
            EndRightLabel = "In-1";
        }

    }
}
