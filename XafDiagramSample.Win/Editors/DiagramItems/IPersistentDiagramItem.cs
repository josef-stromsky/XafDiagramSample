using DevExpress.Diagram.Core;
using System;

namespace XAF.Module.Win.MonitoringDesigner.DiagramItems
{
    internal interface IPersistentDiagramItem : IDiagramItem
    {
        void SaveItemInfo();
        void LoadItemInfo();

        Guid Guid { get; }
    }
}
