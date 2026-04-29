

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


      _filePath = path;
  }


}