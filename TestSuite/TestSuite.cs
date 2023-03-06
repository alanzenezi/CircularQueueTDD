using CircularBuffer;

namespace TestSuite
{
    public class TestSuite
    {
        private CircularQueue circleQueue = new CircularQueue(5);

        [Fact]
        public void AddNewValue()
        {
            circleQueue.Insert(1);

            Assert.NotEmpty(circleQueue.Queue());
            Assert.Equal(1, circleQueue.Queue()[0]);
        }

        [Fact]
        public void AddTwoRemoveOneShouldRemainOne()
        {
            circleQueue.Insert(1);
            circleQueue.Insert(2);
            circleQueue.Remove();

            Assert.NotEmpty(circleQueue.Queue());
            Assert.Equal(2, circleQueue.Queue()[0]);
            Assert.Single(circleQueue.Queue());
        }

        [Fact]
        public void RemoveFromEmptyListShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => circleQueue.Remove());
        }

        [Fact]
        public void AddInFullListShouldReplaceOldestItem()
        {
            var tempQueue = new CircularBuffer.CircularQueue(3);
            for (int i = 0; i < 3; i++)
            {
                tempQueue.Insert(i + 5);
            }

            tempQueue.Insert(4);

            Assert.Equal(6, tempQueue.Queue()[0]);
            Assert.Equal(7, tempQueue.Queue()[1]);
            Assert.Equal(4, tempQueue.Queue()[2]);
        }

        [Fact]
        public void AddNullItemShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => circleQueue.Insert(null));
        }

        [Fact]
        public void RemoveValueShouldDeleteOldestItem()
        {
            for (int i = 0; i < 6; i++)
            {
                circleQueue.Insert(i * 2);
            }

            circleQueue.Remove();

            int[] expectedValues = { 4, 6, 8, 10 };

            for (int i = 0; i < expectedValues.Length; i++)
            {
                Assert.Equal(expectedValues[i], circleQueue.Queue()[i]);
            }
            
        }

        [Fact]
        public void MovedListShouldReturnItemsInOrder()
        {
            int[] expectedValues = {10, 12, 14};

            for (int i = 0; i < 8; i++)
            {
                circleQueue.Insert(i * 2);
            }

            circleQueue.Remove();
            circleQueue.Remove();

            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(expectedValues[i], circleQueue.Queue()[i]);
            }
        }

    }
}