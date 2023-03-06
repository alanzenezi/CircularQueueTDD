namespace CircularBuffer
{
    public class CircularQueue
    {
        private int?[] items;
        private int writePtr, readPtr;

        private int movPtrNext(int ptr)
        {
            ptr++;
            if (ptr == items.Length)
            {
                return ptr = 0;
            }
            return ptr;
        }

        public int Length()
        {
            return items.Length;
        }

        public CircularQueue(int size)
        {
            items = new int?[size];
            writePtr = 0;
            readPtr = 0;
        }

        public void Insert(int? value)
        {
            try
            {
                new Action(() =>
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("Cannot insert null item");
                    }
                })();

                items[writePtr] = value;
                writePtr = movPtrNext(writePtr);
                
                if (writePtr == readPtr)
                {
                    readPtr = movPtrNext(readPtr);
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public void Remove()
        {
            try
            {
                new Action(() =>
                {
                    bool isAllNull = true;
                    foreach (var item in items)
                    {
                        if (item != null)
                        {
                            isAllNull = false;
                        }
                    }
                    if (isAllNull)
                    {
                        throw new InvalidOperationException("The buffer is already empty");
                    }
                })();

                items[readPtr] = null;
                readPtr = movPtrNext(readPtr);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public int[] Queue()
        {
            int[] convertedOutputQueue;
            int size = 0;
            int readerPtr = readPtr;

            new Action(() =>
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        size++;
                    }
                }
            })();

            convertedOutputQueue = new int[size];

            try
            {
                new Action(() =>
                {
                    for (int i = 0; i < size; i++, movPtrNext(readerPtr))
                    {
                        if (items != null)
                        {
                            convertedOutputQueue[i] = items[readerPtr] ??
                                throw new InvalidOperationException("Demonic fatal error happened to the queue");
                        }
                    }

                })();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return convertedOutputQueue;
        }
    }
}