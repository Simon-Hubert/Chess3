public interface IDataServices
{
    bool SaveData<T>(string Path, T Data, bool Encrypted);

    T LoadData<T>(string Path, bool Encrypted);
}
