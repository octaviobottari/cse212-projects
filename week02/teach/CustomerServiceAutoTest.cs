using System.Collections.Generic;

/// <summary>
/// CustomerService con testing automático
/// </summary>
public class CustomerServiceAutoTest {
    public static void Run() {
        Console.WriteLine("Test 1: Add and serve one customer");
        Test1();
        
        Console.WriteLine("\nTest 2: Serve from empty queue");
        Test2();
        
        Console.WriteLine("\nTest 3: Max size enforcement");
        Test3();
        
        Console.WriteLine("\nTest 4: Invalid size defaults to 10");
        Test4();
        
        Console.WriteLine("\nTest 5: Multiple customers order");
        Test5();
    }
    
    static void Test1() {
        var cs = new CustomerServiceAuto(10);
        cs.AddNewCustomer("John", "123", "Login issue");
        cs.ServeCustomer();
    }
    
    static void Test2() {
        var cs = new CustomerServiceAuto(5);
        cs.ServeCustomer();
    }
    
    static void Test3() {
        var cs = new CustomerServiceAuto(2);
        cs.AddNewCustomer("A", "1", "P1");
        cs.AddNewCustomer("B", "2", "P2");
        cs.AddNewCustomer("C", "3", "P3"); // Should show error
        Console.WriteLine(cs);
    }
    
    static void Test4() {
        var cs = new CustomerServiceAuto(0);
        Console.WriteLine($"Size should be 10: {cs}");
    }
    
    static void Test5() {
        var cs = new CustomerServiceAuto(10);
        cs.AddNewCustomer("First", "001", "First problem");
        cs.AddNewCustomer("Second", "002", "Second problem");
        cs.AddNewCustomer("Third", "003", "Third problem");
        
        Console.WriteLine("Serving 3 customers:");
        cs.ServeCustomer();
        cs.ServeCustomer();
        cs.ServeCustomer();
    }
}

public class CustomerServiceAuto {
    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerServiceAuto(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

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
            return $"{Name} ({AccountId}): {Problem}";
        }
    }

    public void AddNewCustomer(string name, string accountId, string problem) {
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
        Console.WriteLine($"Added: {customer}");
    }

    public void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }
        
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine($"Serving: {customer}");
    }

    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}