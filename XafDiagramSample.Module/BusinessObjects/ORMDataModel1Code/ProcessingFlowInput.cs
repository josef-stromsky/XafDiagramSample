using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XafDiagramSample.Module.BusinessObjects.DiagramTest
{

    public partial class ProcessingFlowInput
    {
        public ProcessingFlowInput(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }






        //
        #region ProcessingFlow (ProcessingFlow)
        /// <summary>
        /// 
        /// </summary>
        //
        [DevExpress.Persistent.Base.NonCloneable]
        [DevExpress.Persistent.Base.EditorAlias(DevExpress.ExpressApp.Editors.EditorAliases.ObjectPropertyEditor)]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        [DevExpress.Xpo.ExplicitLoading(1)]
        public ProcessingFlow ProcessingFlow
        {
            get { return processingFlow; }
            set
            {
                if (processingFlow == value) { return; }
                ProcessingFlow previousObject = processingFlow;
                processingFlow = value;
                if (IsLoading) { return; }
                if (previousObject != null && previousObject.ProcessingFlowInput == this) { previousObject.ProcessingFlowInput = null; }
                if (processingFlow != null) { processingFlow.ProcessingFlowInput = this; }
                OnChanged(ProcessingFlowPropertyName, previousObject, processingFlow);
            }
        }
        private ProcessingFlow processingFlow;
        /// <summary>
        /// Name of the property <see cref="ProcessingFlow"/>.
        /// </summary>
        public const string ProcessingFlowPropertyName = "ProcessingFlow";
        //
        //
        #endregion // ProcessingFlow (ProcessingFlow)
        //
    }

}
