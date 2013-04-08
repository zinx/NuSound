using System;

namespace ACT_Plugin
{
    class CircularBuffer
    {
        byte[] data;
        int head, len;
        int Head { get { return head; } }
        int Tail { get { return (head + len) % data.Length; } }

        public int Count { get { return len; } }
        public int Capacity { get { return data.Length; } }
        public int EmptyCount { get { return data.Length - len; } }

        public void Reset()
        {
            head = 0;
            len = 0;
        }

        public CircularBuffer(int size)
        {
            data = new byte[size];
            Reset();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (count < 0) count = 0;
            lock (data)
            {
                int readCount = Math.Min(len, count);
                int toEnd = Math.Min(readCount, data.Length - head);
                Array.Copy(data, head, buffer, offset, toEnd);
                if (toEnd < readCount)
                    Array.Copy(data, 0, buffer, offset + toEnd, readCount - toEnd);
                head = (head + readCount) % data.Length;
                len -= readCount;
                return readCount;
            }
        }

        public int Write(byte[] buffer, int offset, int count)
        {
            if (count < 0) count = 0;
            lock (data)
            {
                int writeCount = Math.Min(EmptyCount, count);
                int toEnd = Math.Min(writeCount, data.Length - Tail);
                Array.Copy(buffer, offset, data, Tail, toEnd);
                if (toEnd < writeCount)
                    Array.Copy(buffer, offset + toEnd, data, 0, writeCount - toEnd);
                len += writeCount;
                return writeCount;
            }
        }
    }
}
