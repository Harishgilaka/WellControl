using WOCS.Domain.Entities;

namespace WOCS.Common
{
    public class ActionBlockWaitForSchedule : Appointment
    {

        public ActionBlockWaitForSchedule(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime start) : base(actionBlock, start)
        {
            End = start.Add(actionBlock.DurationTs);
            Subject = "Wait";
        }
    }
}
