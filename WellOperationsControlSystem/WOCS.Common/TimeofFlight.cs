namespace WOCS.Common
{
    public static class TimeofFlight
    {
        /* Defined space for data in the transmission, 235 bytes (determined as 255 bytes - header/footer requirements) */
        const double DataSpace = 235;
        const int BitsPerByte = 8;
        const int WorstCaseProcessingTime = 3;
        const int PartialMessageHeaderValue = 144;
        const int FullMessageHeaderValue = 152;
        const int RoundToNearestMinutes = 15;

        public static TimeSpan GetTimeofFlight(TimeSpan duration, TimeSpan scheduleDataInterval, int stationLevel, int dFormat, double chirpFrequencyDuration)
        {
            int expectedSamples = GetExpectedSamples(duration, scheduleDataInterval);
            if (expectedSamples == 0)
            {
                return TimeSpan.Zero;
            }

            int bytesPerRecord = GetBytesPerRecord((uint)dFormat);

            (int fullMessages, int partMessages, int remainingSamples) =
                GetMessageCounts(expectedSamples, bytesPerRecord);

            int totalBits = GetTotalBits(fullMessages, partMessages, remainingSamples, bytesPerRecord);

            double totalSeconds = (totalBits * chirpFrequencyDuration * stationLevel) + (WorstCaseProcessingTime * stationLevel);

            // TODO: add DP variance
            return RoundUpToNearest(TimeSpan.FromSeconds(totalSeconds), RoundToNearestMinutes);
        }

        /// <summary>
        /// Number of samples expected over the duration, inclusive of the first and last sample.
        /// </summary>
        private static int GetExpectedSamples(TimeSpan duration, TimeSpan scheduleDataInterval)
        {
            if (duration == TimeSpan.Zero || scheduleDataInterval == TimeSpan.Zero)
            {
                return 0;
            }

            return (int)(duration.TotalMinutes / scheduleDataInterval.TotalMinutes) + 1;
        }

        /// <summary>
        /// Total bytes needed per sample record, based on the temperature/pressure precision
        /// encoded in the low/high nibbles of the data format.
        /// </summary>
        private static int GetBytesPerRecord(uint dataFormat)
        {
            uint temperaturePrecision = dataFormat & 0x0FU;
            uint pressurePrecision = dataFormat >> 4;

            return GetTemperatureBytes(temperaturePrecision) + GetPressureBytes(pressurePrecision);
        }

        private static int GetTemperatureBytes(uint precision)
        {
            if (precision > 4U) return 0; // temperature not included
            return precision == 0 ? 2 : 3;
        }

        private static int GetPressureBytes(uint precision)
        {
            if (precision > 4U) return 0; // pressure not included
            if (precision == 0) return 2;
            return precision <= 2 ? 3 : 4;
        }

        /// <summary>
        /// Splits the expected samples into full messages, whether a partial message is needed,
        /// and how many samples fall into that partial message.
        /// </summary>
        private static (int fullMessages, int partMessages, int remainingSamples) GetMessageCounts(
            int expectedSamples, int bytesPerRecord)
        {
            int samplesPerMessage = (int)(DataSpace / bytesPerRecord);

            int fullMessages = expectedSamples / samplesPerMessage;
            double exactMessages = expectedSamples / (DataSpace / bytesPerRecord);
            int partMessages = (int)Math.Ceiling(exactMessages - fullMessages);

            int remainingSamples = expectedSamples - (samplesPerMessage * fullMessages);

            return (fullMessages, partMessages, remainingSamples);
        }

        /// <summary>
        /// Total bits across all full messages plus the trailing partial message (if any).
        /// </summary>
        private static int GetTotalBits(int fullMessages, int partMessages, int remainingSamples, int bytesPerRecord)
        {
            int fullMessageBits = fullMessages > 0
                ? (FullMessageHeaderValue + ((int)DataSpace * BitsPerByte)) * fullMessages
                : 0;

            int partMessageBits = 0;
            if (partMessages > 0)
            {
                int partialBits = remainingSamples * bytesPerRecord * BitsPerByte;
                partMessageBits = PartialMessageHeaderValue + partialBits;
            }

            return fullMessageBits + partMessageBits;
        }

        private static TimeSpan RoundUpToNearest(TimeSpan value, int roundToMinutes)
        {
            int minutes = (int)Math.Ceiling(value.TotalMinutes);
            int roundedMinutes = ((minutes + (roundToMinutes - 1)) / roundToMinutes) * roundToMinutes;
            return TimeSpan.FromMinutes(roundedMinutes);
        }
    }
}