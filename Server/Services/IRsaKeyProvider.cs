using System.Security.Cryptography;

namespace QuintrixFullstack.Server.Services;

public interface IRsaKeyProvider
{
    public RSA PublicKey { get; set; }
    public RSA PrivateKey { get; set; }
}