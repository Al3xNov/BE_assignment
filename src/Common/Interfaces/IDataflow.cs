namespace Common.Interfaces;
public interface IDataflow
{
    Task<bool> StartPipeline(object obj);
    void DoneProducing();
}