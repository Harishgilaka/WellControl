using Microsoft.EntityFrameworkCore;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Repositories
{
    public class LynxOperationVersionRepository : ILynxOperationVersionRepository
    {
        private readonly WocsContext _context;
        public LynxOperationVersionRepository(WocsContext wocsContext)
        {
            _context = wocsContext;
        }
        public async Task<IEnumerable<OperationVersionDto>> GetAllOperationVersionsWithOperationIdAsync(Guid operationId)
        {
            var query = _context.LynxOperationVersions.Where(ov => ov.OperationId == operationId).AsQueryable();

            if (query.Any())
            {
                return await query.Select(ov => new OperationVersionDto
                {
                    Id = ov.Id,
                    OperationId = ov.OperationId,
                    VersionId = ov.VersionId,
                    Notes = ov.Notes,
                    IsActive = ov.IsActive,
                    IsConfigured = ov.IsConfigured,
                    LynxFrequencyBandId = ov.LynxFrequencyBandId,
                    LynxLrcmTransmissionSpeedId = ov.LynxLrcmTransmissionSpeedId,
                    Depth = ov.Depth,
                    AcousticAddress = ov.AcousticAddress,
                    COMPortP0 = ov.COMPortP0,
                    COMPortP1 = ov.COMPortP1,
                    LastModifiedTime = ov.LastModifiedTime,
                    CreatedTime = ov.CreatedTime,
                    ModifiedBy = ov.ModifiedBy,
                    CreatedBy = ov.CreatedBy,
                    Stations = ov.LynxStations.Select(s => new StationDto
                    {
                        Id = s.Id,
                        OperationVersionId = s.OperationVersionId,
                        Name = s.Name,
                        Position = s.Position,
                        PowerLevel = s.PowerLevel,
                        DbLevel = s.DbLevel,
                        IsConfigured = s.IsConfigured,
                        LastModifiedTime = s.LastModifiedTime,
                        CreatedTime = s.CreatedTime,
                        ModifiedBy = s.ModifiedBy,
                        CreatedBy = s.CreatedBy,
                        Assemblies = s.LynxAssemblies.Select(a => new AssemblyDto
                        {
                            Id = a.Id,
                            AssemblyId = a.AssemblyId,
                            StationId = a.StationId,
                            Name = a.Name,
                            Position = a.Position,
                            IsConfigured = a.IsConfigured,
                            ClassName = a.ClassName,
                            LynxDeviceGroup = a.LynxDeviceGroup,
                            ScheduleStartTime = a.ScheduleStartTime,
                            ProposedScheduleStartTime = a.ProposedScheduleStartTime,
                            ProposedScheduleUpdateSent = a.ProposedScheduleUpdateSent,
                            ProposedScheduleUpdateAkLastQuery = a.ProposedScheduleUpdateAkLastQuery,
                            ProposedScheduleUpdateResponseEta = a.ProposedScheduleUpdateResponseEta,
                            ProposedScheduleUpdateMessageSize = a.ProposedScheduleUpdateMessageSize,
                            ProposedScheduleUpdateAckReceived = a.ProposedScheduleUpdateAckReceived,
                            ProposedScheduleUpdateAckStatus = a.ProposedScheduleUpdateAckStatus,
                            ScheduleEndTime = a.ScheduleEndTime,
                            LastModifiedTime = a.LastModifiedTime,
                            CreatedTime = a.CreatedTime,
                            ModifiedBy = a.ModifiedBy,
                            CreatedBy = a.CreatedBy,
                            AssemblyDevices = a.LynxAssemblyDevices.Select(ad => new AssemblyDeviceDto
                            {
                                Id = ad.Id,
                                AssemblyId = ad.AssemblyId,
                                LynxDeviceId = ad.LynxDeviceId,
                                LynxDeviceTypeId = ad.LynxDeviceTypeId,
                                LynxLocalId = ad.LynxLocalId,
                                IsConfigured = ad.IsConfigured,
                                FirmwareVersion = ad.FirmwareVersion,
                                SamplingRateMS = ad.SamplingRateMS,
                                FrequencyBandId = ad.FrequencyBandId,
                                LynxChirpFrequencyRangeId = ad.LynxChirpFrequencyRangeId,
                                LastModifiedTime = ad.LastModifiedTime,
                                CreatedTime = ad.CreatedTime,
                                ModifiedBy = ad.ModifiedBy,
                                CreatedBy = ad.CreatedBy
                            }),
                            lynxAssemblyScheduleActionBlock = a.LynxAssemblyScheduleActionBlocks.Select(asab => new LynxAssemblyScheduleActionBlockDto
                            {
                                Id = asab.Id,
                                AssemblyId = asab.AssemblyId,
                                AssemblyDeviceReferenceId = asab.AssemblyDeviceReferenceId,
                                BlockNumber = asab.BlockNumber,
                                Name = asab.Name,
                                TransmissionInterval = asab.TransmissionInterval,
                                Duration = asab.Duration,
                                DataInterval = asab.DataInterval,
                                NumberOfRepeats = asab.NumberOfRepeats,
                                RepeatIndefinetly = asab.RepeatIndefinetly,
                                DataFormat = asab.DataFormat,
                                TemperatureResolution = asab.TemperatureResolution,
                                PressureResolution = asab.PressureResolution,
                                ActionBlockTypeId = asab.ActionBlockTypeId,
                                IsProposal = asab.IsProposal,
                                ProposalFor = asab.ProposalFor,
                            })
                        })
                    })
                }).ToListAsync();
            }

            return Enumerable.Empty<OperationVersionDto>();
        }
    }
}
