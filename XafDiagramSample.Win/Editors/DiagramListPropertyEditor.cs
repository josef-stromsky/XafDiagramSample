using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Native;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraDiagram;
using DevExpress.XtraDiagram.Commands;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraMap.Native;
using System.Drawing;
using DevExpress.Diagram.Core.Native.Serialization;
using XafDiagramSample.Module.BusinessObjects.DiagramTest;
using XafDiagramSample.Win.Editors.DiagramItems;
using XAF.Module.Win.MonitoringDesigner.DiagramItems;
using XafDiagramSample.Module.BusinessObjects;

namespace XafDiagramSample.Win.Editors
{
    [ListEditor(typeof(ProcessingNode), true)]
    public class DiagramListPropertyEditor : ListEditor, IComplexListEditor
    {
        private CollectionSourceBase CollectionSource;
        public IObjectSpace ObjectSpace { get; private set; }
        public XafApplication Application { get; private set; }
        public DiagramControl DiagramControl { get; private set; }
        public XtraUserControl MainUserControl { get; private set; }


        public override SelectionType SelectionType => SelectionType.Full;

        public DiagramListPropertyEditor(IModelListView model)
            : base(model)
        {
        }


        public void Setup(CollectionSourceBase collectionSource, XafApplication application)
        {
            CollectionSource = collectionSource;
            ObjectSpace = collectionSource.ObjectSpace;
            Application = application;

            ObjectSpace.Reloaded += ObjectSpace_Reloaded;
        }

        private void ObjectSpace_Reloaded(object sender, EventArgs e)
        {
            if (CollectionSource != null)
            {
                CollectionSource.ResetCollection();
                AssignDataSourceToControl(CollectionSource.Collection);
            }
        }


        public override IList GetSelectedObjects()
        {
            if (DiagramControl != null)
            {
                return DiagramControl.SelectedItems.ToArray();
            }
            return Array.Empty<object>();
        }

        public override void Refresh()
        {
        }

        protected override void AssignDataSourceToControl(object dataSource)
        {
            var proxyCollection = (ProxyCollection)dataSource;

            if (!ReferenceEquals(dataSource, CollectionSource.Collection))
            {
            }

            if (DiagramControl != null)
            {
                Render(proxyCollection);
            }
        }


        public void RemoveDiagramItem(Guid guid)
        {
            var item = FindDiagramItem(guid);
            if (item is DiagramShape diagramShape)
            {
                var attachedConnectors = diagramShape.GetAttachedConnectors()
                    .OfType<ProcessingFlowConnector>();
                foreach (var connector in attachedConnectors)
                {
                    DiagramControl.Items.Remove(connector);
                }
            }
            DiagramControl.Items.Remove((DiagramItem)item);
        }


        internal void UpdateDiagram(IMonitoringBoardMember boardMember)
        {
            if (boardMember is ProcessingNode processingNode)
            {
                if (processingNode.IsDeleted)
                {
                    RemoveDiagramItem(processingNode.Guid);
                }
                else
                {
                    if (!PersistentDiagramItems.TryGetValue(processingNode.Guid,
                        out IPersistentDiagramItem item))
                    {
                        item = new ProcessingNodeShape(processingNode);
                        DiagramControl.Items.Add((DiagramItem)item);
                        var flows = processingNode.GetAllFlows();
                        flows.ForEach(i => UpdateDiagram((IMonitoringBoardMember)i));
                    }
                    item.LoadItemInfo();
                }
            }
            else if (boardMember is ProcessingFlow processingFlow)
            {
                if (processingFlow.IsDeleted)
                {
                    RemoveDiagramItem(processingFlow.Guid);
                }
                else
                {
                    if (!PersistentDiagramItems.TryGetValue(processingFlow.Guid,
                        out IPersistentDiagramItem item))
                    {

                        //item = new ProcessingFlowConnector(
                        //FindDiagramItem<ProcessingNodeShape>(processingFlow.ProcessingFlowOutput.OutputFromNode.Guid),
                        //FindDiagramItem<ProcessingNodeShape>(processingFlow.ProcessingFlowInput.InputToNode.Guid),
                        //processingFlow);

                        //DiagramControl.Items.Add((DiagramItem)item);
                    }
                    item.LoadItemInfo();
                }
            }
            else
            {
                throw new NotSupportedException("not supported / UpdateDiagram");
            }
        }

        private Dictionary<Guid, IPersistentDiagramItem> PersistentDiagramItems = new();

        private void Render(ProxyCollection dataSource)
        {
            DiagramControl.Items.Clear();
            //PersistentDiagramItems.Clear();
            // TODO: Prefetch InputFlows, OutputFlows
            HashSet<ProcessingFlow> processingFlows = new();

            BoardDiagramContainer container;
            foreach (ProcessingNode node in dataSource)
            {
                if (!PersistentDiagramItems.TryGetValue(node.MonitoringBoard.Guid,
                    out IPersistentDiagramItem persistentDiagramItem))
                {
                    container = new BoardDiagramContainer(node.MonitoringBoard);
                    container.CanSelect = true;
                    container.CanMove = true;
                    container.CanCopy = true;
                    container.CanDelete = true;
                    container.CanEdit = true;
                    container.CanResize = true;
                    container.CanRotate = true;
                    container.CanChangeParent = true;
                    container.ItemsCanAttachConnectorBeginPoint = true;
                    container.ItemsCanAttachConnectorEndPoint = true;
                    container.ItemsCanCopyWithoutParent = true;
                    container.ItemsCanDeleteWithoutParent = true;
                    container.ItemsCanEdit = true;
                    container.ItemsCanMove = true;
                    container.ItemsCanResize = true;
                    container.ItemsCanRotate = true;
                    container.ItemsCanChangeParent = true;
                    container.ItemsCanSnapToOtherItems = true;
                    container.ItemsCanSnapToThisItem = true;
                    container.ItemsCanSelect = true;
                    container.CanAddItems = true;

                    DiagramControl.Items.Add(container);
                }
                else
                {
                    container = (BoardDiagramContainer)persistentDiagramItem;
                }


                processingFlows.AddRange(node.InputFlows.Select(i => i.ProcessingFlow));
                processingFlows.AddRange(node.OutputFlows.Select(i => i.ProcessingFlow));

                var shape = new ProcessingNodeShape(node);



                // If shape is added directly to DiagramControl, connectors are rendered
                // correctly. 
                DiagramControl.Items.Add(shape);

                // If shape is added to container, connectors are not rendered at all.
                //container.Items.Add(shape);

            }

            foreach (var processingFlow in processingFlows)
            {
                container = FindDiagramItem<BoardDiagramContainer>
                    (processingFlow.ProcessingFlowInput.InputToNode.MonitoringBoard.Guid);
                var fromShape = FindDiagramItem<ProcessingNodeShape>(processingFlow.ProcessingFlowOutput.OutputFromNode.Guid);
                var toShape = FindDiagramItem<ProcessingNodeShape>(processingFlow.ProcessingFlowInput.InputToNode.Guid);
                //var connector = new ProcessingFlowConnector(processingFlow);
                //connector.BeginItem = fromShape;
                //connector.EndItem = toShape;
                //connector.AffectedByLayoutAlgorithms = true;
                //connector.CanChangeRoute = true;
                //connector.CanCopy = true;
                //connector.CanDelete = true;
                //connector.CanEdit = true;
                //connector.CanMove = true;
                //connector.CanResize = true;
                //connector.CanRotate = true;
                //connector.CanSelect = true;
                //connector.CanChangeParent = true;

                var c = new DiagramConnector(fromShape, toShape);
                DiagramControl.Items.Add(c);

            }
            DiagramControl.UpdateRoute();

            //DiagramControl.Refresh();
        }

        internal IPersistentDiagramItem FindDiagramItem(Guid guid)
        {
            return FindDiagramItem<IPersistentDiagramItem>(guid);
        }

        internal T FindDiagramItem<T>(Guid guid) where T : IPersistentDiagramItem
        {
            if (PersistentDiagramItems.TryGetValue(guid, out IPersistentDiagramItem item))
            {
                return (T)item;
            }
            return default;
        }

        protected override object CreateControlsCore()
        {
            MainUserControl = new XtraUserControl();
            DiagramControl = new DiagramControl();

            DiagramControl.ShowingEditor += DiagramControl_ShowingEditor;
            DiagramControl.SelectionChanged += DiagramControl_SelectionChanged;
            DiagramControl.ItemsMoving += DiagramControl_ItemsMoving;
            DiagramControl.ItemBoundsChanged += DiagramControl_ItemBoundsChanged;
            DiagramControl.Items.ListChanged += Items_ListChanged;
            //DiagramControl.CustomDrawItem += DiagramControl_CustomDrawItem;

            #region PanZoomPanel

            MainUserControl.Controls.Add(DiagramControl);
            DiagramControl.Dock = DockStyle.Fill;
            MainUserControl.Dock = DockStyle.Fill;
            MainUserControl.AutoScaleMode = AutoScaleMode.Font;

            #endregion

            DiagramControl.OptionsView.ShowPageBreaks = false;
            DiagramControl.OptionsView.ShowRulers = false;
            DiagramControl.OptionsView.ShowGrid = false;
            DiagramControl.OptionsView.ZoomFactor = 1.5f;

            // zakazani akci
            DiagramControl.Commands.RegisterHandlers(x => x.RegisterHandlerCore(DiagramCommandsBase.CutCommand,
                (param, diagram, getArgs, baseHandler) => { }, (param, diagram, baseCanExecute) => false));
            DiagramControl.Commands.RegisterHandlers(x => x.RegisterHandlerCore(DiagramCommandsBase.PasteCommand,
                (param, diagram, getArgs, baseHandler) => { }, (param, diagram, baseCanExecute) => false));

            return MainUserControl;
        }



        int pointSize = 8;
        private void DiagramControl_CustomDrawItem(object sender, CustomDrawItemEventArgs e)
        {
            DiagramShape shape = e.Item as DiagramShape;
            e.DefaultDraw();
            if (shape != null)
            {
                foreach (DevExpress.Utils.PointFloat point in e.Item.ActualConnectionPoints)
                {
                    e.GraphicsCache.DrawRectangle(Pens.Red, new RectangleF(point.X * e.Size.Width - pointSize / 2, point.Y * e.Size.Height - pointSize / 2, pointSize, pointSize));
                }
            }
            e.Handled = true;
        }

        private void Items_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                if (DiagramControl.Items[e.NewIndex] is IPersistentDiagramItem diagramItem)
                {
                    PersistentDiagramItems.Add(diagramItem.Guid, diagramItem);
                }
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (DiagramControl.Items[e.NewIndex] is IPersistentDiagramItem diagramItem)
                {
                    PersistentDiagramItems.Remove(diagramItem.Guid);
                }
            }
            else if (e.ListChangedType == ListChangedType.Reset)
            {
                PersistentDiagramItems.Clear();
            }
        }

        private void DiagramControl_ItemBoundsChanged(object sender, DiagramItemBoundsChangedEventArgs e)
        {
            var diagramControl = (DiagramControl)sender;
            if (e.Item is IPersistentDiagramItem diagramItem)
            {
                diagramItem.SaveItemInfo();
            }
        }

        private void DiagramControl_ItemsMoving(object sender, DiagramItemsMovingEventArgs e)
        {
        }

        private void DiagramControl_SelectionChanged(object sender, DiagramSelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }

        //private void DefaultDiagramOptions()
        //{
        //    //DiagramControl.FitToDrawing();
        //    //DiagramControl.ApplySugiyamaLayout();
        //    //DiagramControl.SetZoom(1);
        //}

        private void DiagramControl_ShowingEditor(object sender, DiagramShowingEditorEventArgs e)
        {
            e.Cancel = true;
        }

        public override void Dispose()
        {
            if (ObjectSpace != null)
            {
                ObjectSpace.Reloaded -= ObjectSpace_Reloaded;
            }

            if (DiagramControl != null)
            {
                DiagramControl.ShowingEditor -= DiagramControl_ShowingEditor;
                DiagramControl.SelectionChanged -= DiagramControl_SelectionChanged;
                DiagramControl.Dispose();
            }

            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}