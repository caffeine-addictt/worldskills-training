using System;
using System.Security.Cryptography;
using System.Text;
using Desktop.Models;

namespace Desktop.Utils
{
  internal static class Auth
  {
    /// <summary>
    /// Generate salt
    /// </summary>
    private static byte[] GenSalt()
    {
      using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
      {
        byte[] salt = new byte[16];
        rng.GetBytes(salt);
        return salt;
      }
    }

    /// <summary>
    /// Compare password
    /// </summary>
    public static bool ComparePassword(User user, string plainTextPassword)
      => ComparePassword(user.Password, user.Salt, plainTextPassword);
    public static bool ComparePassword(string hashedPassword, string salt, string plainTextPassword)
    {
      byte[] saltArray = Convert.FromBase64String(salt);
      return hashedPassword == HashPassword(plainTextPassword, saltArray);
    }


    /// <summary>
    /// Return the hash
    /// </summary>
    public static Tuple<string, byte[]> HashPassword(string plainTextPassword)
    {
      byte[] salt = GenSalt();
      return new Tuple<string, byte[]>(HashPassword(plainTextPassword, salt), salt);
    }
    public static string HashPassword(string plainTextPassword, byte[] salt)
    {
      using (var sha256 = new SHA256Managed())
      {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(plainTextPassword);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        // Concatenate password and salt
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        // Hash the concatenated password and salt
        byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

        // Concatenate the salt and hashed password for storage
        byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
        Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
        Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

        return Convert.ToBase64String(hashedPasswordWithSalt);
      }
    }
  }
}
