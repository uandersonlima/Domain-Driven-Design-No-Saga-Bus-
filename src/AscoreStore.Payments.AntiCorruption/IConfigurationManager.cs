namespace AscoreStore.Payments.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}