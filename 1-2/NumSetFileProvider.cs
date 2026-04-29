namespace Study.Generate;

public class NumSetFileProvider
{
  private readonly string _filePath;
  private readonly int _setsCount;
  private readonly int _numInSet;
  private readonly int _minValue;
  private readonly int _maxValue;


  public NumSetFileProvider(
    string path,
    int setsCount,
    int numInSet,
    int min,
    int max
  )
  {
    if (string.IsNullOrWhiteSpace(path))
      throw new ArgumentException("Path to file can't be empty");
    if (setsCount <= 0)
      throw new ArgumentException("Sets count can't be <= 0");

    if (numInSet <= 0)
      throw new ArgumentException("Nums in set can't be <= 0");

    if (max < min)
      throw new ArgumentException("Max value can't be < min value");

      _filePath = path;
      _setsCount = setsCount;
      _numInSet = numInSet;
      _minValue = min;
      _maxValue = max;
  }

  public void EnsureFileExists()
  {
    if (File.Exists(_filePath))
    {
      Console.WriteLine($"Fail is created: {_filePath}");
      Console.WriteLine("Generation data dont started");
      return;
    }
    GenerateFile();
  }

  private void GenerateFile()
  {
    Random rand = new Random();

    using StreamWriter writer = new StreamWriter(_filePath);
    for(int i = 0; i < _setsCount; i++)
    {
      int[] nums = new int[_numInSet];
      for(int j = 0; j < _numInSet; j++)
      {
        nums[j] = rand.Next(_minValue, _maxValue + 1);
      }
      string line = string.Join(" ", nums);
      writer.WriteLine(line);
    }
    Console.WriteLine($"File is created: {_filePath}");
  }

  public List<int[]> LoadNumSet()
  {
    string[] lines = File.ReadAllLines(_filePath);

    List<int[]> numberSets = new List<int[]>();

    foreach(string line in lines)
    {
      int[] numbers = line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
        numberSets.Add(numbers);
    }
    return numberSets;
  }

}