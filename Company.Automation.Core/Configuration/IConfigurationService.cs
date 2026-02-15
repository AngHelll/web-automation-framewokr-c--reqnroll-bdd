namespace Company.Automation.Core.Configuration;

public interface IConfigurationService
{
    string GetString(string key);
    int GetInt(string key, int defaultValue = 0);
    bool GetBool(string key, bool defaultValue = false);
    T Get<T>(string key);
}
