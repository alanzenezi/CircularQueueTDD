namespace CircularBuffer
{
    public class CircularQueue
    {
        private int?[] items;
        private int writePtr, readPtr;
        private bool movReadPtr;

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
            movReadPtr = false;
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

                if (movReadPtr)
                {
                    readPtr = movPtrNext(readPtr);
                }

                items[writePtr] = value;
                writePtr = movPtrNext(writePtr);
                
                if (writePtr == readPtr)
                {
                    movReadPtr = true;
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
                movReadPtr = false;
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
                    for (int i = 0; i < size; i++)
                    {
                        if (items != null)
                        {
                            convertedOutputQueue[i] = items[readerPtr] ??
                                throw new InvalidOperationException("Demonic fatal error happened to the queue");
                            readerPtr = movPtrNext(readerPtr);
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