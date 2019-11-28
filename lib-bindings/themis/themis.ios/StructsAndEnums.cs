using ObjCRuntime;

namespace Themis.iOS
{
	[Native]
	public enum TSKeyGenAsymmetricAlgorithm : long
	{
		Rsa,
		Ec
	}

	[Native]
	public enum TSMessageMode : long
	{
		EncryptDecrypt,
		SignVerify
	}

	[Native]
	public enum TSComparatorStateType : long
	{
		NotReady = 0,
		NotMatch = 22,
		Match = 21
	}

	[Native]
	public enum TSErrorType : long
	{
		Success = 0,
		BufferTooSmall = 14,
		Fail = 11,
		SendAsIs = 1
	}
}
