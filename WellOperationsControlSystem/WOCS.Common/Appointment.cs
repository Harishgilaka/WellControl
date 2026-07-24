using WOCS.Domain.Entities;

namespace WOCS.Common
{
    public abstract class Appointment
    {
        protected Appointment(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime start)
        {
            ActionBlock = actionBlock ?? throw new ArgumentNullException(nameof(actionBlock));
            UniqueId = actionBlock.Id.ToString();
            Start = start;
        }
        public LynxAssemblyScheduleActionBlockDto ActionBlock { get; }
        public string UniqueId { get; set; }
        public DateTime Start { get; }
        public virtual DateTime End { get; protected set; }
        public virtual string Subject { get; protected set; } = string.Empty;
        public override string ToString() =>
            $"{Subject} : {Start} - {End}";
    }
}
