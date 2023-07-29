using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XafDiagramSample.Module.BusinessObjects.DiagramTest
{

    public partial class ProcessingFlowOutput
    {
        public ProcessingFlowOutput(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }




        //
        #region ProcessingFlow (ProcessingFlow)
        /// <summary>
        /// 
        /// </summary>
        #region RequiredField (RuleRequiredFieldAttribute)
        [DevExpress.Persistent.Validation.RuleRequiredFieldAttribute(
            "VR_ProcessingFlowOutput_ProcessingFlow_RequiredField"
            , "Save"
            , TargetCriteria = ""
            , ResultType = DevExpress.Persistent.Validation.ValidationResultType.Error
            , InvertResult = false
            , SkipNullOrEmptyValues = false
            , Name = "RequiredField"
            , CustomMessageTemplate = ""
        )]
        #endregion
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
                if (previousObject != null && previousObject.ProcessingFlowOutput == this) { previousObject.ProcessingFlowOutput = null; }
                if (processingFlow != null) { processingFlow.ProcessingFlowOutput = this; }
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
