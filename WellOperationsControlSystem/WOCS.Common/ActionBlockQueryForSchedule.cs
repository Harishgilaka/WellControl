using WOCS.Domain.Entities;

namespace WOCS.Common
{
    public class ActionBlockQueryForSchedule : Appointment
    {
        public ActionBlockQueryForSchedule(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime start) : base(actionBlock, start)
        {
            Subject = "Query";
            End = start.Add(actionBlock.TimeOfFlight);
        }
    }
}
