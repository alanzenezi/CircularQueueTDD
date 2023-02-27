namespace CircularQueue
{
    public class CircularQueue<T>
    {
        public T?[] items;
        private int writePtr;

        private void movWritePtrNext()
        {
            ++writePtr;
            if (writePtr == items.Length)
            {
                writePtr = 0;
            }
        }

        public int Length()
        {
            return items.Length;
        }

        public CircularQueue(int size)
        {
            items = new T?[size];
            writePtr = 0;
        }

        public void Insert(T? value)
        {
            try
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot insert null item");
                }

                items[writePtr] = value;
                movWritePtrNext();
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

                new Action(() =>
                {
                    var queueMinusOldestItem = new T?[items.Length];

                    for (int i = 1; i < items.Length; i++)
                    {
                        queueMinusOldestItem[i - 1] = items[i];
                        if (i == items.Length - 1)
                        {
                            queueMinusOldestItem[i] = default(T);
                        }
                    }

                    items = queueMinusOldestItem;
                })();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public T[] Queue()
        {
            T[] convertedOutputQueue;
            int size = 0;

            new Action(() =>
            {
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        size++;
                    }
                }
            })();

            convertedOutputQueue = new T[size];
            try
            {
                new Action(() =>
                {
                    int j = 0;
                    for (int i = 0; i < convertedOutputQueue.Length; i++)
                    {
                        if (items != null)
                        {
                            convertedOutputQueue[j] = items[i] ??
                                throw new InvalidOperationException("Demonic fatal error happened to the queue");
                            j++;
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