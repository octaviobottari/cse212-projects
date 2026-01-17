using System.Collections.Generic;

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Add one customer and serve them
        // Expected Result: Should display customer info
        Console.WriteLine("Test 1");
        var cs = new CustomerService(10);
        // Simulate adding customer (in real scenario would use Console.ReadLine)
        // For testing, we'll modify the class to accept parameters
        
        Console.WriteLine("=================");

        // Test 2
        // Scenario: Serve from empty queue
        // Expected Result: Should show error
        Console.WriteLine("Test 2");
        cs = new CustomerService(5);
        cs.ServeCustomer();

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Max size enforcement
        // Expected Result: Should show error on 3rd addition
        Console.WriteLine("Test 3");
        cs = new CustomerService(2);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer(); // Should show error
        Console.WriteLine($"Service Queue: {cs}");

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Invalid size defaults to 10
        // Expected Result: Should show max_size=10
        Console.WriteLine("Test 4");
        cs = new CustomerService(0);
        Console.WriteLine($"Size should be 10: {cs}");

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Multiple customers served in correct order
        // Expected Result: Should serve in FIFO order
        Console.WriteLine("Test 5");
        cs = new CustomerService(10);
        // Add multiple and serve
        Console.WriteLine("Add 3 customers and serve them");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    public void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count > _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    public void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }
        
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}