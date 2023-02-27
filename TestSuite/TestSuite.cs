using CircularQueue;

namespace TestSuite
{
    public class TestSuite
    {
        private CircularQueue<int?> circleQueue = new CircularQueue<int?>(5);

        [Fact]
        public void AddNewValue()
        {
            circleQueue.Insert(1);

            Assert.NotEmpty(circleQueue.Queue());
            Assert.Equal(1, circleQueue.Queue()[0]);
        }

        [Fact]
        public void RemoveValueShouldDeleteOldestItem()
        {
            circleQueue.Insert(8);
            circleQueue.Insert(2);

            int? firstItem = circleQueue.Queue()[0];
            circleQueue.Remove();
            Assert.DoesNotContain(firstItem, circleQueue.Queue());

            circleQueue.Remove();
            Assert.Empty(circleQueue.Queue());
        }

        [Fact]
        public void AddTwoRemoveOneShouldRemainOne()
        {
            circleQueue.Insert(1);
            circleQueue.Insert(2);
            circleQueue.Remove();

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
            var tempQueue = new CircularQueue<int>(3);
            for (int i = 0; i < tempQueue.Length(); i++)
            {
                tempQueue.Insert(i + 5);
            }

            tempQueue.Insert(4);

            Assert.Equal(4, tempQueue.Queue()[0]);
            Assert.Equal(6, tempQueue.Queue()[1]);
            Assert.Equal(7, tempQueue.Queue()[2]);
        }

        [Fact]
        public void AddNullItemShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => circleQueue.Insert(null));
        }
    }
}