using System.Collections;

public static class Recursion
{
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }
        
        foreach (char c in letters)
        {
            if (!word.Contains(c))
            {
                PermutationsChoose(results, letters, size, word + c);
            }
        }
    }

    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();
        
        if (remember.ContainsKey(s))
            return remember[s];
        
        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;
        
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);
        
        remember[s] = ways;
        return ways;
    }

    public static void WildcardBinary(string pattern, List<string> results)
    {
        int idx = pattern.IndexOf('*');
        if (idx == -1)
        {
            results.Add(pattern);
            return;
        }
        
        WildcardBinary(pattern.Remove(idx, 1).Insert(idx, "0"), results);
        WildcardBinary(pattern.Remove(idx, 1).Insert(idx, "1"), results);
    }

    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        currPath.Add((x, y));
        
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }
        
        if (maze.IsValidMove(currPath, x + 1, y))
            SolveMaze(results, maze, x + 1, y, currPath);
        
        if (maze.IsValidMove(currPath, x, y + 1))
            SolveMaze(results, maze, x, y + 1, currPath);
        
        if (maze.IsValidMove(currPath, x - 1, y))
            SolveMaze(results, maze, x - 1, y, currPath);
        
        if (maze.IsValidMove(currPath, x, y - 1))
            SolveMaze(results, maze, x, y - 1, currPath);
        
        currPath.RemoveAt(currPath.Count - 1);
    }
}