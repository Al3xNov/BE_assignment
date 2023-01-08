namespace Common.Interfaces;
public interface IProvider
{
    Task<bool> Connect();
}