using System.Text.Json;
using System.Diagnostics;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var pairs = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip words with same letters (e.g., "aa")
            if (word[0] == word[1])
                continue;

            // Create the reverse of the word
            var reverse = new string(new char[] { word[1], word[0] });
            
            // If we've seen the reverse before, we found a pair
            if (seen.Contains(reverse))
            {
                // Add to pairs in alphabetical order
                string pair;
                if (string.Compare(word, reverse) < 0)
                    pair = $"{word} & {reverse}";
                else
                    pair = $"{reverse} & {word}";
                
                pairs.Add(pair);
            }
            
            // Add current word to seen set
            seen.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3].Trim();
            
            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();
        
        // If lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;
        
        var charCount = new Dictionary<char, int>();
        
        // Count characters in word1
        foreach (char c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        // Subtract characters from word2
        foreach (char c in word2)
        {
            if (!charCount.ContainsKey(c))
                return false;
            
            charCount[c]--;
            if (charCount[c] == 0)
                charCount.Remove(c);
        }
        
        // If dictionary is empty, all characters matched
        return charCount.Count == 0;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        try
        {
            const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
            using var client = new HttpClient();
            
            // Añadir timeout y User-Agent
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("CSE212/1.0");
            
            using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = client.Send(getRequestMessage);
            
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"HTTP Error: {response.StatusCode}");
                return ["API Error - No data available"];
            }
            
            using var jsonStream = response.Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            // Crear lista de descripciones de terremotos
            var earthquakeDescriptions = new List<string>();
            
            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    if (feature?.Properties != null && feature.Properties.Mag > 0)
                    {
                        string description = $"{feature.Properties.Place} - Mag {feature.Properties.Mag:F1}";
                        earthquakeDescriptions.Add(description);
                    }
                }
            }

            // Si no hay terremotos, devolver un array con un mensaje
            if (earthquakeDescriptions.Count == 0)
            {
                return ["No earthquakes recorded today"];
            }

            return earthquakeDescriptions.ToArray();
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine($"HTTP Request Error: {ex.Message}");
            return ["Network error - Unable to fetch earthquake data"];
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"JSON Error: {ex.Message}");
            return ["Data format error - Unable to parse earthquake data"];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"General Error: {ex.Message}");
            return ["Error fetching earthquake data"];
        }
    }
}