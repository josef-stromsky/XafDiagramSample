using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafDiagramSample.Module.BusinessObjects.DiagramTest;

namespace XafDiagramSample.Module.BusinessObjects
{
    public interface IMonitoringBoardMember
    {
        MonitoringBoard MonitoringBoard { get; }
        Guid Guid { get; }
    }
}
