using System.Security.Cryptography;

namespace MBA.Modulo2.Data.Domain.Extensions;

public static class UuidExtensions
{
	public static Guid NewUuidV7(this string? value)
	{
		var uuid = new byte[16];

		var unixTimeMs = (ulong)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() & 0xFFFFFFFFFFFF);
		uuid[0] = (byte)(unixTimeMs >> 40);
		uuid[1] = (byte)(unixTimeMs >> 32);
		uuid[2] = (byte)(unixTimeMs >> 24);
		uuid[3] = (byte)(unixTimeMs >> 16);
		uuid[4] = (byte)(unixTimeMs >> 8);
		uuid[5] = (byte)unixTimeMs;

		var random12 = RandomNumberGenerator.GetInt32(0, 4096);
		uuid[6] = (byte)(0x70 | ((random12 >> 8) & 0x0F));
		uuid[7] = (byte)(random12 & 0xFF);

		var randomBytes = new byte[8];
		RandomNumberGenerator.Fill(randomBytes);
		randomBytes[0] = (byte)((randomBytes[0] & 0x3F) | 0x80);
		Array.Copy(randomBytes, 0, uuid, 8, 8);

		return new Guid(uuid);
	}
}