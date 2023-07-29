using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using XafDiagramSample.Module.BusinessObjects.DiagramTest;

namespace XafDiagramSample.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater
{
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion)
    {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();

        var monitoringBoard = ObjectSpace.FirstOrDefault<MonitoringBoard>(i => i.Code == "DEFAULT");
        if (monitoringBoard != null)
        {
            return;
        }

        monitoringBoard = ObjectSpace.CreateObject<MonitoringBoard>();
        monitoringBoard.Code = "DEFAULT";

        for (int i = 0; i < 10; i++)
        {
            var obj = ObjectSpace.CreateObject<ProcessingNode>();
            obj.Code = $"Code {i}";
            obj.MonitoringBoard = monitoringBoard;
            obj.Guid = Guid.NewGuid();
        }
        ObjectSpace.CommitChanges();

        //ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema()
    {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
