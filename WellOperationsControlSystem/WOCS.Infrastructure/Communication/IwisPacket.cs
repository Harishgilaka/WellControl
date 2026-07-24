namespace WOCS.Infrastructure.Communication
{
    public static class IwisPacket
    {
        public const byte SyncByte = 0xAE;
        public static ushort Crc16(byte[] data)
        {
            ushort crc = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return crc;
        }

        public static byte[] BuildIwisFrame(byte[] payload)
        {
            int length = payload.Length;

            byte LSB = (byte)(length & 0xFF);
            byte MSB = (byte)((length >> 8) & 0xFF);

            byte CompLSB = (byte)~LSB;
            byte CompMSB = (byte)~MSB;

            List<byte> frame = new List<byte>();

            frame.Add(SyncByte);      // sync
            frame.Add(LSB);
            frame.Add(MSB);
            frame.Add(CompLSB);
            frame.Add(CompMSB);

            frame.AddRange(payload);

            ushort crc = Crc16(frame.ToArray());
            frame.Add((byte)(crc & 0xFF));
            frame.Add((byte)(crc >> 8));

            return frame.ToArray();
        }

        public static byte[] ExtractPayload(byte[] frame)
        {
            if (frame.Length < 7) throw new Exception("Frame too short");
            if (frame[0] != SyncByte) throw new Exception("Invalid Sync byte");

            byte LSB = frame[1];
            byte MSB = frame[2];

            byte CompLSB = frame[3];
            byte CompMSB = frame[4];

            if ((byte)~LSB != CompLSB || (byte)~MSB != CompMSB)
                throw new Exception("Length complement error");

            int payloadLen = LSB + (MSB << 8);

            if (frame.Length < (5 + payloadLen + 2))
                throw new Exception("Frame length mismatch");

            // CRC check
            ushort recvCrc = (ushort)(frame[5 + payloadLen] | (frame[6 + payloadLen] << 8));
            ushort calcCrc = Crc16(Sub(frame, 0, 5 + payloadLen));

            if (recvCrc != calcCrc)
                throw new Exception("CRC mismatch");

            return Sub(frame, 5, payloadLen);
        }

        private static byte[] Sub(byte[] src, int index, int len)
        {
            byte[] result = new byte[len];
            Buffer.BlockCopy(src, index, result, 0, len);
            return result;
        }
    }
}
