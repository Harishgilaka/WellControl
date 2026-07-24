namespace WOCS.Domain.Enums
{
    public enum LynxScheduleActionsTypeEnum
    {
        BaseActionBlock = 0,
        ConsoleCommandAction = 3,
        QueryHistoricDataAction = 4,
        QueryLiveDataAction = 5,
        ControlAction = 6,
        WaitForAction = 7,
        WaitUntilAction = 8,
        RepeatAction = 9,
        StopAction = 10,
        ChangeScheduleAction = 11,
        CallScheduleAction = 12,
        PulseAction = 20
    }
}
