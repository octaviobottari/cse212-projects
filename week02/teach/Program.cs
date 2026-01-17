Console.WriteLine("\n======================\nComplex Stack\n======================");
Console.WriteLine("Test 1 (should be True): " + ComplexStack.DoSomethingComplicated("(a == 3 or (b == 5 and c == 6))"));
Console.WriteLine("Test 2 (should be False): " + ComplexStack.DoSomethingComplicated("(students]i].Grade > 80 and students[i].Grade < 90)"));
Console.WriteLine("Test 3 (should be False): " + ComplexStack.DoSomethingComplicated("(robot[id + 1].Execute(.Pass() || (!robot[id * (2 + i)].Alive && stormy) || (robot[id - 1].Alive && lavaFlowing))"));

Console.WriteLine("\n======================\nCustomer Service Auto Test\n======================");
CustomerServiceAutoTest.Run();