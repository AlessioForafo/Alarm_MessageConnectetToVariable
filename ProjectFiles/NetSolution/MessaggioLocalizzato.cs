#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.Alarm;
using FTOptix.Modbus;
using FTOptix.CommunicationDriver;
#endregion

public class MessaggioLocalizzato : BaseNetLogic
{
    private IUAVariable stringaPLC;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        stringaPLC = Owner.GetVariable("VarStringa");
        stringaPLC.VariableChange += StringaPLC_VariableChange;
        SetMessage(stringaPLC.Value);
    }

    private void StringaPLC_VariableChange(object sender, VariableChangeEventArgs e)
    {
        SetMessage(e.NewValue);
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        stringaPLC.VariableChange -= StringaPLC_VariableChange;
    }

    [ExportMethod]
    public void SetMessage(string chiave)
    {
        var messaggioAllarme = new LocalizedText(chiave);

        if (InformationModel.LookupTranslation(messaggioAllarme).HasTranslation)
        {
            LogicObject.GetVariable("TestoMessaggioLocalizzato").Value = messaggioAllarme;
        }
    }
}
