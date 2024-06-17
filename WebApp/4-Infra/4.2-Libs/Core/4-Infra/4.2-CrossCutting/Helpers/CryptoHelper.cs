using System.Security.Cryptography;
using System.Text;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class CryptoHelper
{
    private static readonly byte[] _salt = Encoding.ASCII.GetBytes("somesalt");
    private const int _iterations = 10000;
    private const int _keySize = 256;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string Encrypt(string plainText)
    {
        string chaveDeAcesso = GetAccessKey();
        byte[] key = GenerateKey(chaveDeAcesso);

        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException(nameof(plainText));

        if (key == null || key.Length != _keySize / 8)
            throw new ArgumentException($"A chave deve ter {_keySize} bits.", nameof(key));

        using var aes = Aes.Create();
        aes.KeySize = _keySize;
        aes.Key = key;
        aes.IV = GenerateRandomBytes(16);

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
            using var streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(plainText);
        }

        var cipherText = memoryStream.ToArray();

        return Convert.ToBase64String(cipherText);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Decrypt(string cipherText)
    {
        string chaveDeAcesso = GetAccessKey();
        byte[] key = GenerateKey(chaveDeAcesso);

        if (string.IsNullOrEmpty(cipherText))
            return string.Empty;

        if (key == null || key.Length != _keySize / 8)
            throw new ArgumentException($"A chave deve ter {_keySize} bits.", nameof(key));

        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.KeySize = _keySize;
        aes.Key = key;

        var iv = new byte[16];
        Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
        aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
        {
            cryptoStream.Write(cipherBytes, iv.Length, cipherBytes.Length - iv.Length);
        }

        var plainBytes = memoryStream.ToArray();

        return Encoding.UTF8.GetString(plainBytes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static byte[] GenerateKey(string password)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, _salt, _iterations, HashAlgorithmName.SHA256);
        return deriveBytes.GetBytes(_keySize / 8);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetAccessKey()
    {
        try
        {
            string caminhoArquivo = "C:\\ChaveDeAcesso\\Chave.txt";

            using StreamReader reader = new(caminhoArquivo);
            string chave = reader.ReadLine();
            return chave;
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        var randomBytes = new byte[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return randomBytes;
    }
}