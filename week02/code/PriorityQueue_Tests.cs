using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities (2, 5, 1) and dequeue them.
    // Expected Result: Should dequeue in order: Task2 (priority 5), Task1 (priority 2), Task3 (priority 1)
    // Defect(s) Found: Loop condition was "index < _queue.Count - 1" which skipped last element.
    // Priority comparison used ">=" instead of ">" causing FIFO violation for same priorities.
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Task1", 2);
        priorityQueue.Enqueue("Task2", 5);
        priorityQueue.Enqueue("Task3", 1);
        
        // Should dequeue Task2 first (highest priority 5)
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Task2", result);
        
        // Then Task1 (priority 2)
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task1", result);
        
        // Then Task3 (priority 1)
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task3", result);
        
        Console.WriteLine("Test passed: Items dequeued in correct priority order.");
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with same priority (3, 3).
    // Expected Result: Should dequeue in FIFO order: Task1, then Task2
    // Defect(s) Found: Priority comparison used ">=" which would take later item with same priority.
    // Should use ">" to maintain FIFO for equal priorities.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Task1", 3);
        priorityQueue.Enqueue("Task2", 3);
        priorityQueue.Enqueue("Task3", 3);
        
        // Should dequeue in FIFO order for same priority
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Task1", result);
        
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task2", result);
        
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task3", result);
        
        Console.WriteLine("Test passed: FIFO order maintained for same priorities.");
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: No defects found - exception was already implemented correctly.
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
            Console.WriteLine("Test passed: Correct exception thrown for empty queue.");
        }
    }

    [TestMethod]
    // Scenario: Enqueue items with negative priorities (-5, -1, -10)
    // Expected Result: Should dequeue in order: Task2 (-1), Task1 (-5), Task3 (-10)
    // Defect(s) Found: No defects found - negative priorities work correctly.
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Task1", -5);
        priorityQueue.Enqueue("Task2", -1); // -1 is higher than -5
        priorityQueue.Enqueue("Task3", -10);
        
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Task2", result); // -1 is highest
        
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task1", result); // -5 is higher than -10
        
        result = priorityQueue.Dequeue();
        Assert.AreEqual("Task3", result);
        
        Console.WriteLine("Test passed: Negative priorities handled correctly.");
    }

    [TestMethod]
    // Scenario: Enqueue mixed priorities and verify queue state after each operation
    // Expected Result: Dequeue should always remove highest priority item
    // Defect(s) Found: Item was not being removed from queue after dequeue operation.
    public void TestPriorityQueue_MixedPrioritiesAndState()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 3); // Same priority as B
        
        // Queue should be: A(1), B(3), C(2), D(3)
        Assert.AreEqual("B", priorityQueue.Dequeue()); // Highest priority
        
        // After dequeue B, remaining: A(1), C(2), D(3)
        Assert.AreEqual("D", priorityQueue.Dequeue()); // Next highest (same priority as B was)
        
        // After dequeue D, remaining: A(1), C(2)
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Priority 2
        
        // After dequeue C, remaining: A(1)
        Assert.AreEqual("A", priorityQueue.Dequeue()); // Last item
        
        Console.WriteLine("Test passed: Mixed priorities handled correctly.");
    }

    [TestMethod]
    // Scenario: Single item in queue
    // Expected Result: Should dequeue that single item
    // Defect(s) Found: No defects found
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Single", 10);
        
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Single", result);
        
        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected exception after dequeuing last item.");
        }
        catch (InvalidOperationException)
        {
            // Expected
        }
        
        Console.WriteLine("Test passed: Single item handled correctly.");
    }
}