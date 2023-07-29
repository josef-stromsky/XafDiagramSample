using DevExpress.Diagram.Core;

namespace XafDiagramSample.Module.BusinessObjects
{
    internal interface IPersistentDiagramItem : IDiagramItem
    {
        void SaveItemInfo();
        void LoadItemInfo();

        Guid Guid { get; }
    }
}
