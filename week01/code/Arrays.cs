public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        
        /*
        PLAN FOR MultiplesOf FUNCTION:
        1. Create a new array of type double with the specified 'length'
        2. Use a loop to iterate from 0 to 'length' - 1
        3. For each position i in the array, calculate: number * (i + 1)
           Explanation: The first multiple should be the number itself (number * 1), 
           the second should be number * 2, and so on. Since arrays are 0-indexed, 
           we use (i + 1) as the multiplier.
        4. Store each calculated multiple in the array at position i
        5. Return the completed array
        */
        
        // Step 1: Create the array with the specified length
        double[] result = new double[length];
        
        // Steps 2-4: Fill the array with multiples
        for (int i = 0; i < length; i++)
        {
            // Calculate the multiple: number * (position + 1)
            // i+1 gives us 1 for the first position, 2 for the second, etc.
            result[i] = number * (i + 1);
        }
        
        // Step 5: Return the completed array
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        
        /*
        PLAN FOR RotateListRight FUNCTION:
        Approach: Using list slicing technique
        
        1. First, handle edge case: if amount equals the list size, no rotation needed
           (rotating by full list size brings us back to original)
        
        2. Determine the split point:
           - We want to move 'amount' elements from the end to the beginning
           - The split point is at index: data.Count - amount
           Example: For list of size 9 with amount=3, split point is at index 6
           Elements from index 6 to end (3 elements) will move to front
        
        3. Extract the portion that needs to move to the front (the "tail")
           Using GetRange(startIndex, count):
           - startIndex = data.Count - amount
           - count = amount
        
        4. Extract the portion that needs to move to the back (the "head")
           Using GetRange(startIndex, count):
           - startIndex = 0
           - count = data.Count - amount
        
        5. Clear the original list
        
        6. Add the tail (elements that were at the end) to the now-empty list
        
        7. Add the head (elements that were at the beginning) to the list
        
        Alternative simpler approach (which I'll implement):
        1. Use GetRange to get the tail portion (last 'amount' elements)
        2. Remove those elements from the original list using RemoveRange
        3. Insert the tail at the beginning using InsertRange at index 0
        */
        
        // Step 1: Check if rotation is needed (if amount equals list size, we get original list)
        if (amount == data.Count || amount <= 0)
            return; // No rotation needed
        
        // Step 2: Get the last 'amount' elements that need to move to the front
        // These are the elements from position (data.Count - amount) to the end
        List<int> tail = data.GetRange(data.Count - amount, amount);
        
        // Step 3: Remove those elements from their current position at the end
        data.RemoveRange(data.Count - amount, amount);
        
        // Step 4: Insert those elements at the beginning of the list
        data.InsertRange(0, tail);
        
        /*
        Walkthrough with example: data = {1,2,3,4,5,6,7,8,9}, amount = 3
        
        Step 2: tail = data.GetRange(9-3, 3) = data.GetRange(6, 3) = {7,8,9}
        Step 3: data.RemoveRange(6, 3) -> data becomes {1,2,3,4,5,6}
        Step 4: data.InsertRange(0, {7,8,9}) -> data becomes {7,8,9,1,2,3,4,5,6}
        */
    }
}