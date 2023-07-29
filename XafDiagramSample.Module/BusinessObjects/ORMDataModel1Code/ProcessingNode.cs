using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XafDiagramSample.Module.BusinessObjects.DiagramTest
{

    public partial class ProcessingNode : IMonitoringBoardMember
    {
        public ProcessingNode(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }



        public IEnumerable<ProcessingFlow> GetAllFlows()
        {
            var result = new HashSet<ProcessingFlow>();
            result.AddRange(InputFlows.Select(i => i.ProcessingFlow));
            result.AddRange(OutputFlows.Select(i => i.ProcessingFlow));
            return result;
        }
    }

}
