using Newtonsoft.Json;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;
using WOCS.Domain.Enums;

namespace WOCS.Application.Services
{
    public class LynxAssemblyScheduleService : ILynxAssemblyScheduleService
    {
        private readonly ILynxAssemblyScheduleRepository _repository;

        //const double DataSpace = 235;
        //const int BitsPerByte = 8;
        //const int WorstCaseProcessingTime = 3;
        //const int PartialMessageHeaderValue = 144;
        //const int FullMesageHeaderValue = 152;
        public LynxAssemblyScheduleService(ILynxAssemblyScheduleRepository repository)
        {
            _repository = repository;
        }

        //public TimeSpan GetTimeofFlight(TimeSpan duration, TimeSpan scheduleDataInterval, int stationLevel, int dFormat)
        //{
        //    int ExpectedSamples = 0;

        //    if (duration != TimeSpan.Zero && scheduleDataInterval != TimeSpan.Zero)
        //    {
        //        ExpectedSamples = (int)(duration.TotalMinutes / scheduleDataInterval.TotalMinutes);
        //        ExpectedSamples += 1; // samples include the first and last sample of the period requested duration.
        //    }
        //    else
        //    {
        //        return TimeSpan.FromSeconds(0);
        //    }

        //    /* How many full messages are being sent */

        //    uint dataFormat = (uint)dFormat;

        //    var temperaturePrecision = dataFormat & 0x0FU;
        //    bool includeTemperature = temperaturePrecision <= 4U;
        //    uint pressurePrecision = dataFormat >> 4;
        //    bool includePressure = pressurePrecision <= 4U;

        //    int BytesPerTempVal = 0;
        //    int BytesPerPressVal = 0;

        //    if (includeTemperature)
        //    {
        //        if (temperaturePrecision == 0)
        //        {
        //            BytesPerTempVal = 2;
        //        }
        //        else
        //        {
        //            BytesPerTempVal = 3;
        //        }
        //    }

        //    if (includePressure)
        //    {
        //        if (pressurePrecision == 0)
        //        {
        //            BytesPerPressVal = 2;
        //        }
        //        else if (pressurePrecision <= 2)
        //        {
        //            BytesPerPressVal = 3;
        //        }
        //        else
        //        {
        //            BytesPerPressVal = 4;
        //        }
        //    }

        //    int BytesPerRecord = (BytesPerPressVal + BytesPerTempVal);

        //    int fullMessages = (int)(ExpectedSamples / (int)(DataSpace / BytesPerRecord));

        //    double messages = ExpectedSamples / (DataSpace / BytesPerRecord);

        //    int partMessages = (int)Math.Ceiling(messages - fullMessages);

        //    /* How many samples in partial the message */

        //    int samplesPerMessage = (int)(DataSpace / BytesPerRecord);
        //    int remainingSamples = ExpectedSamples - (samplesPerMessage * fullMessages);

        //    /* How many bits in partial the message */
        //    int pBits = remainingSamples * BytesPerRecord * BitsPerByte;

        //    int fullMessageBits = 0;
        //    int partMessageBits = 0;

        //    if (fullMessages > 0)
        //    {
        //        fullMessageBits = (FullMesageHeaderValue + ((int)DataSpace * BitsPerByte)) * fullMessages;
        //    }

        //    if (partMessages > 0)
        //    {
        //        partMessageBits = PartialMessageHeaderValue + pBits;
        //    }

        //    double totalDuration = (fullMessageBits + partMessageBits) * 6.75 * stationLevel + (WorstCaseProcessingTime * stationLevel);

        //    //return TimeSpan.FromSeconds(totalDuration);

        //    // TODO round up!

        //    // add DP variance

        //    TimeSpan originalTimespan = TimeSpan.FromSeconds(totalDuration);

        //    int roundBy = 15; // Round up to nearest 15 minutes

        //    int minutes = (int)Math.Ceiling(originalTimespan.TotalMinutes);
        //    int roundedMinutes = ((minutes + (roundBy - 1)) / roundBy) * roundBy;

        //    TimeSpan roundedTimespan = TimeSpan.FromMinutes(roundedMinutes);

        //    return roundedTimespan;
        //}
        public async Task<IEnumerable<LynxAssemblyScheduleActionBlockDto>> GetActionBlockByIdAsync(Guid assemblyId)
        {
            var actionBlocksResponse = await _repository.GetAllAsyncWith(assemblyId);

            //actionBlocksResponse = actionBlocksResponse.Select(x =>
            //{
            //    x.TimeOfFlight = TimeofFlight.GetTimeofFlight(
            //        x.DurationTs,
            //        x.DataIntervalTs,
            //        x.StationPosition,
            //        x.DataFormat);

            //    return x;
            //}).ToList();

            //var response = ValidateSchedleTime(actionBlocksResponse);
            //var filter = actionBlocksResponse;

            return actionBlocksResponse;
        }

        //private bool ValidateSchedleTime(IEnumerable<LynxAssemblyScheduleActionBlockDto> lynxAssemblyScheduleActionBlockDto)
        //{
        //    List<LynxAssemblyScheduleActionBlockDto> actionBlock = new List<LynxAssemblyScheduleActionBlockDto>();
        //    var JsonData = new List<string>();
        //    var commandNow = DateTime.Now;
        //    var commandDuration = TimeSpan.FromHours(1);

        //    foreach (var action in lynxAssemblyScheduleActionBlockDto.OrderBy(e => e.BlockNumber))
        //    {
        //        switch (action.ActionBlockTypeId)
        //        {
        //            case (int)LynxScheduleActionsTypeEnum.QueryHistoricDataAction:
        //                actionBlock.Add(action);
        //                JsonData.Add(JsonConvert.SerializeObject(actionBlock));
        //                break;
        //            case (int)LynxScheduleActionsTypeEnum.WaitForAction:
        //                actionBlock.Add(action);
        //                JsonData.Add(JsonConvert.SerializeObject(actionBlock));
        //                break;
        //        }
        //    }

        //    List<(DateTime Start, DateTime End, string Action)> windows = new();

        //    // ✅ initial start from first block
        //    DateTime currentStart = actionBlock.First().ScheduleStartTime;

        //    foreach (var block in actionBlock.OrderBy(b => b.BlockNumber))
        //    {
        //        for (int i = 0; i <= block.NumberOfRepeats; i++)
        //        {
        //            DateTime start;

        //            if (i == 0)
        //            {
        //                // ✅ first iteration → use currentStart
        //                start = currentStart;
        //            }
        //            else
        //            {
        //                // ✅ next iterations → respect interval from previous end
        //                start = currentStart.Add(block.DataIntervalTs);
        //            }

        //            var end = start.Add(block.DurationTs);

        //            windows.Add((start, end, block.Name ?? ""));

        //            // ✅ update for chaining
        //            currentStart = end;
        //        }
        //    }


        //    bool isQueryHistoricDataRunning = windows.Any(w => w.Action == "QueryHistoricDataAction" && commandNow >= w.Start && commandNow <= w.End);

        //    if (isQueryHistoricDataRunning)
        //    {
        //        Console.WriteLine("commandNow overlaps with QueryHistoricDataAction window.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("No QueryHistoricDataAction running at commandNow.");
        //    }

        //    bool isCommandDurationHistoricDataRunning = windows.Any(w => w.Action == "QueryHistoricDataAction" && commandDuration <= (w.End - w.Start));

        //    if (isCommandDurationHistoricDataRunning)
        //    {
        //        Console.WriteLine("commandDuration overlaps with isCommandDurationHistoricDataRunning window.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("No isCommandDurationHistoricDataRunning running at commandDuration.");
        //    }

        //    bool isCommandDurationValid = windows.Any(w => w.Action == "WaitForAction" && commandDuration <= (w.End - w.Start));

        //    if (isCommandDurationValid)
        //    {
        //        Console.WriteLine("commandDuration is less than WaitForAction duration.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("commandDuration exceeds WaitForAction duration.");
        //    }

        //    return true;
        //}

        private bool ValidateSchedleTime(IEnumerable<LynxAssemblyScheduleActionBlockDto> lynxAssemblyScheduleActionBlockDto)
        {
            var commandNow = DateTime.Now;
            var commandDuration = TimeSpan.FromHours(1);

            List<LynxAssemblyScheduleActionBlockDto> actionBlock = lynxAssemblyScheduleActionBlockDto
                .Where(a => a.ActionBlockTypeId == (int)LynxScheduleActionsTypeEnum.QueryHistoricDataAction
                         || a.ActionBlockTypeId == (int)LynxScheduleActionsTypeEnum.WaitForAction)
                .OrderBy(a => a.BlockNumber)
                .ToList();

            List<(DateTime Start, DateTime End, string Action)> windows = new();

            // ✅ global start
            DateTime currentStart = actionBlock.First().ScheduleStartTime;

            foreach (var block in actionBlock)
            {
                for (int i = 0; i <= block.NumberOfRepeats; i++)
                {
                    DateTime start;

                    if (i == 0)
                        start = currentStart;
                    else
                        start = currentStart.Add(block.DataIntervalTs);

                    var end = start.Add(block.DurationTs);

                    windows.Add((start, end, block.Name ?? ""));

                    currentStart = end;
                }
            }

            // ✅ FILTER only QueryHistoricDataAction
            //var blockingWindows = windows
            //    .Where(w => w.Action == "QueryHistoricDataAction" || w.Action == "WaitForAction")
            //    .OrderBy(w => w.Start)
            //    .ToList();

            //// ✅ STEP 1: Check current running
            //bool isRunningNow = blockingWindows.Any(w =>
            //    commandNow >= w.Start && commandNow < w.End);

            //if (isRunningNow)
            //{
            //    Console.WriteLine("❌ Command blocked: QueryHistoricDataAction currently running.");
            //    return false;
            //}

            //// ✅ STEP 2: Find next window
            //var nextWindow = blockingWindows
            //    .Where(w => w.Start > commandNow)
            //    .OrderBy(w => w.Start)
            //    .FirstOrDefault();

            //var commandEnd = commandNow.Add(commandDuration);

            //// ✅ STEP 3: Future clash check
            //if (nextWindow != default && commandEnd >= nextWindow.Start)
            //{
            //    Console.WriteLine("❌ Command blocked: Will clash with next QueryHistoricDataAction.");
            //    return false;
            //}

            Console.WriteLine("✅ Command allowed.");
            return true;
        }
    }
}
