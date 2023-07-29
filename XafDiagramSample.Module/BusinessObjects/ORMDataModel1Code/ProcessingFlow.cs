using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XafDiagramSample.Module.BusinessObjects.DiagramTest
{

    public partial class ProcessingFlow : IMonitoringBoardMember
    {
        public ProcessingFlow(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }





        MonitoringBoard IMonitoringBoardMember.MonitoringBoard =>
          ((IMonitoringBoardMember)ProcessingFlowInput)?.MonitoringBoard;


        //
        #region ProcessingFlowInput (ProcessingFlowInput)
        /// <summary>
        /// 
        /// </summary>
        [DevExpress.Xpo.Aggregated]
        [DevExpress.Persistent.Base.EditorAlias(DevExpress.ExpressApp.Editors.EditorAliases.ObjectPropertyEditor)]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        [DevExpress.Xpo.ExplicitLoading(1)]
        public ProcessingFlowInput ProcessingFlowInput
        {
            get { return processingFlowInput; }
            set
            {
                if (processingFlowInput == value) { return; }
                ProcessingFlowInput previousObject = processingFlowInput;
                processingFlowInput = value;
                if (IsLoading) { return; }
                if (previousObject != null && previousObject.ProcessingFlow == this) { previousObject.ProcessingFlow = null; }
                if (processingFlowInput != null) { processingFlowInput.ProcessingFlow = this; }
                OnChanged(ProcessingFlowInputPropertyName, previousObject, processingFlowInput);
            }
        }
        private ProcessingFlowInput processingFlowInput;
        /// <summary>
        /// Name of the property <see cref="ProcessingFlowInput"/>.
        /// </summary>
        public const string ProcessingFlowInputPropertyName = "ProcessingFlowInput";
        //
        //
        #endregion // ProcessingFlowInput (ProcessingFlowInput)
        //
        #region ProcessingFlowOutput (ProcessingFlowOutput)
        /// <summary>
        /// 
        /// </summary>
        [DevExpress.Xpo.Aggregated]
        [DevExpress.Persistent.Base.EditorAlias(DevExpress.ExpressApp.Editors.EditorAliases.ObjectPropertyEditor)]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        [DevExpress.Xpo.ExplicitLoading(1)]
        public ProcessingFlowOutput ProcessingFlowOutput
        {
            get { return processingFlowOutput; }
            set
            {
                if (processingFlowOutput == value) { return; }
                ProcessingFlowOutput previousObject = processingFlowOutput;
                processingFlowOutput = value;
                if (IsLoading) { return; }
                if (previousObject != null && previousObject.ProcessingFlow == this) { previousObject.ProcessingFlow = null; }
                if (processingFlowOutput != null) { processingFlowOutput.ProcessingFlow = this; }
                OnChanged(ProcessingFlowOutputPropertyName, previousObject, processingFlowOutput);
            }
        }
        private ProcessingFlowOutput processingFlowOutput;
        /// <summary>
        /// Name of the property <see cref="ProcessingFlowOutput"/>.
        /// </summary>
        public const string ProcessingFlowOutputPropertyName = "ProcessingFlowOutput";
        //
        //
        #endregion // ProcessingFlowOutput (ProcessingFlowOutput)
        //
    }
}
