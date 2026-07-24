namespace WOCS.Common
{
    public static class TimeofFlight
    {
        /* Defined space for data in the transmission, 235 bytes (determined as 255 bytes - header/footer requirements) */
        const double DataSpace = 235;
        const int BitsPerByte = 8;
        const int WorstCaseProcessingTime = 3;
        const int PartialMessageHeaderValue = 144;
        const int FullMesageHeaderValue = 152;

        public static TimeSpan GetTimeofFlight(TimeSpan duration, TimeSpan scheduleDataInterval, int stationLevel, int dFormat, double chirpFrequencyDuration)
        {
            int ExpectedSamples = 0;

            if (duration != TimeSpan.Zero && scheduleDataInterval != TimeSpan.Zero)
            {
                ExpectedSamples = (int)(duration.TotalMinutes / scheduleDataInterval.TotalMinutes);
                ExpectedSamples += 1; // samples include the first and last sample of the period requested duration.
            }
            else
            {
                return TimeSpan.FromSeconds(0);
            }

            /* How many full messages are being sent */

            uint dataFormat = (uint)dFormat;

            var temperaturePrecision = dataFormat & 0x0FU;
            bool includeTemperature = temperaturePrecision <= 4U;
            uint pressurePrecision = dataFormat >> 4;
            bool includePressure = pressurePrecision <= 4U;

            int BytesPerTempVal = 0;
            int BytesPerPressVal = 0;

            if (includeTemperature)
            {
                if (temperaturePrecision == 0)
                {
                    BytesPerTempVal = 2;
                }
                else
                {
                    BytesPerTempVal = 3;
                }
            }

            if (includePressure)
            {
                if (pressurePrecision == 0)
                {
                    BytesPerPressVal = 2;
                }
                else if (pressurePrecision <= 2)
                {
                    BytesPerPressVal = 3;
                }
                else
                {
                    BytesPerPressVal = 4;
                }
            }

            int BytesPerRecord = (BytesPerPressVal + BytesPerTempVal);

            int fullMessages = (int)(ExpectedSamples / (int)(DataSpace / BytesPerRecord));

            double messages = ExpectedSamples / (DataSpace / BytesPerRecord);

            int partMessages = (int)Math.Ceiling(messages - fullMessages);

            /* How many samples in partial the message */

            int samplesPerMessage = (int)(DataSpace / BytesPerRecord);
            int remainingSamples = ExpectedSamples - (samplesPerMessage * fullMessages);

            /* How many bits in partial the message */
            int pBits = remainingSamples * BytesPerRecord * BitsPerByte;

            int fullMessageBits = 0;
            int partMessageBits = 0;

            if (fullMessages > 0)
            {
                fullMessageBits = (FullMesageHeaderValue + ((int)DataSpace * BitsPerByte)) * fullMessages;
            }

            if (partMessages > 0)
            {
                partMessageBits = PartialMessageHeaderValue + pBits;
            }

            double totalDuration = (fullMessageBits + partMessageBits) * chirpFrequencyDuration * stationLevel + (WorstCaseProcessingTime * stationLevel);

            //return TimeSpan.FromSeconds(totalDuration);

            // TODO round up!

            // add DP variance

            TimeSpan originalTimespan = TimeSpan.FromSeconds(totalDuration);

            int roundBy = 15; // Round up to nearest 15 minutes

            int minutes = (int)Math.Ceiling(originalTimespan.TotalMinutes);
            int roundedMinutes = ((minutes + (roundBy - 1)) / roundBy) * roundBy;

            TimeSpan roundedTimespan = TimeSpan.FromMinutes(roundedMinutes);

            return roundedTimespan;
        }
    }
}
