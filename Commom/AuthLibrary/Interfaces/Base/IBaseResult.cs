namespace CadU.Interfaces.Base
{
  public interface IBaseResult<T>
  {
    string Message { get; set; }
    bool Success { get; set; }
    T Data { get; set; }
  }
}