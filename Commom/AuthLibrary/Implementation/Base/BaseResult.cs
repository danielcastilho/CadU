using CadU.Interfaces.Base;

namespace CadU.Implementation.Base
{
  public class BaseResult<T> : IBaseResult<T>
  {
    public string Message { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
  }
}