using System.ComponentModel.DataAnnotations.Schema;

namespace WOCS.Domain.Entities
{
    public class LynxAssemblyScheduleActionBlockDto
    {
        public Guid Id { get; set; }
        public Guid AssemblyId { get; set; }
        public Guid AssemblyDeviceReferenceId { get; set; }
        public int BlockNumber { get; set; }
        public string? Name { get; set; }
        public int TransmissionInterval { get; set; }
        public int Duration { get; set; }
        public int DataInterval { get; set; }
        public int NumberOfRepeats { get; set; }
        public bool RepeatIndefinetly { get; set; }
        public TimeSpan TimeOfFlight { get; set; }
        public int DataFormat { get; set; }
        public int TemperatureResolution { get; set; }
        public int PressureResolution { get; set; }
        public TimeSpan TransmissionIntervalTs
        {
            get
            {
                return TimeSpan.FromSeconds(TransmissionInterval);
            }
        }

        public TimeSpan DataIntervalTs
        {
            get
            {
                return TimeSpan.FromSeconds(DataInterval);
            }
        }

        public TimeSpan DurationTs
        {
            get
            {
                return TimeSpan.FromMinutes(Duration);
            }
        }

        public int ActionBlockTypeId { get; set; }
        public bool IsProposal { get; set; }
        public Guid? ProposalFor { get; set; }
        public int StationPosition { get; set; }
        public DateTime ScheduleStartTime { get; set; }
    }
}
