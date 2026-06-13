namespace FinCashly.Application.Utils;

public static class GenericMethodsUtils
{
    public static string ReadFileTypeJson(string localPath)
    {
        string dir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(dir, localPath);

        if (!File.Exists(relativePath))
        {
            throw new FileNotFoundException($"O arquivo de seed não foi encontrado no caminho: {relativePath}");
        }

        string jsonConfig = File.ReadAllText(relativePath);

        return jsonConfig;
    }
}