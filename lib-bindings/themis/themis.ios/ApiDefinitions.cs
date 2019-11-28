using System;
using Foundation;
using ObjCRuntime;


namespace Themis.iOS
{
	//[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	//partial interface Constants
	//{
	//	// extern double themisVersionNumber;
	//	[Field ("themisVersionNumber", "__Internal")]
	//	double themisVersionNumber { get; }

	//	// extern const unsigned char [] themisVersionString;
	//	[Field ("themisVersionString", "__Internal")]
	//	byte[] themisVersionString { get; }
	//}

	// @interface TSCell : NSObject
	[BaseType (typeof(NSObject))]
	interface TSCell : IDisposable, INativeObject, INSObjectProtocol
    {
		// @property (readonly, nonatomic) NSData * _Nonnull key;
		[Export ("key")]
		NSData Key { get; }

		// -(instancetype _Nullable)initWithKey:(NSData * _Nonnull)key;
		[Export ("initWithKey:")]
		IntPtr Constructor (NSData key);
	}

	// @interface TSCellSeal : TSCell
	[BaseType (typeof(TSCell))]
	interface TSCellSeal: IDisposable, INativeObject, INSObjectProtocol
	{
		// -(instancetype _Nullable)initWithKey:(NSData * _Nonnull)key;
		[Export ("initWithKey:")]
		IntPtr Constructor (NSData key);

		// -(NSData * _Nullable)wrapData:(NSData * _Nonnull)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:error:")]
		[return: NullAllowed]
		NSData WrapData (NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(NSData * _Nonnull)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:error:")]
		[return: NullAllowed]
		NSData UnwrapData (NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)wrapData:(NSData * _Nonnull)message context:(NSData * _Nullable)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:context:error:")]
		[return: NullAllowed]
		NSData WrapData (NSData message, [NullAllowed] NSData context, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(NSData * _Nonnull)message context:(NSData * _Nullable)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:context:error:")]
		[return: NullAllowed]
		NSData UnwrapData (NSData message, [NullAllowed] NSData context, [NullAllowed] out NSError error);
	}

	// @interface TSCellTokenEncryptedData : NSObject
	[BaseType (typeof(NSObject))]
	interface TSCellTokenEncryptedData
	{
		// @property (nonatomic, strong) NSMutableData * _Nonnull cipherText;
		[Export ("cipherText", ArgumentSemantic.Strong)]
		NSMutableData CipherText { get; set; }

		// @property (nonatomic, strong) NSMutableData * _Nonnull token;
		[Export ("token", ArgumentSemantic.Strong)]
		NSMutableData Token { get; set; }
	}

	// @interface TSCellToken : TSCell
	[BaseType (typeof(TSCell))]
	interface TSCellToken
	{
		// -(instancetype _Nullable)initWithKey:(NSData * _Nonnull)key;
		[Export ("initWithKey:")]
		IntPtr Constructor (NSData key);

		// -(TSCellTokenEncryptedData * _Nullable)wrapData:(NSData * _Nonnull)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:error:")]
		[return: NullAllowed]
		TSCellTokenEncryptedData WrapData (NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(TSCellTokenEncryptedData * _Nonnull)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:error:")]
		[return: NullAllowed]
		NSData UnwrapData (TSCellTokenEncryptedData message, [NullAllowed] out NSError error);

		// -(TSCellTokenEncryptedData * _Nullable)wrapData:(NSData * _Nonnull)message context:(NSData * _Nullable)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:context:error:")]
		[return: NullAllowed]
		TSCellTokenEncryptedData WrapData (NSData message, [NullAllowed] NSData context, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(TSCellTokenEncryptedData * _Nonnull)message context:(NSData * _Nullable)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:context:error:")]
		[return: NullAllowed]
		NSData UnwrapData (TSCellTokenEncryptedData message, [NullAllowed] NSData context, [NullAllowed] out NSError error);
	}

	// @interface TSCellContextImprint : TSCell
	[BaseType (typeof(TSCell))]
	interface TSCellContextImprint
	{
		// -(instancetype _Nullable)initWithKey:(NSData * _Nonnull)key;
		[Export ("initWithKey:")]
		IntPtr Constructor (NSData key);

		// -(NSData * _Nullable)wrapData:(NSData * _Nonnull)message context:(NSData * _Nonnull)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:context:error:")]
		[return: NullAllowed]
		NSData WrapData (NSData message, NSData context, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(NSData * _Nonnull)message context:(NSData * _Nonnull)context error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:context:error:")]
		[return: NullAllowed]
		NSData UnwrapData (NSData message, NSData context, [NullAllowed] out NSError error);
	}

	// @interface TSKeyGen : NSObject
	[BaseType (typeof(NSObject))]
	interface TSKeyGen
	{
		// @property (readonly, nonatomic) NSMutableData * _Nonnull privateKey;
		[Export ("privateKey")]
		NSMutableData PrivateKey { get; }

		// @property (readonly, nonatomic) NSMutableData * _Nonnull publicKey;
		[Export ("publicKey")]
		NSMutableData PublicKey { get; }

		// -(instancetype _Nullable)initWithAlgorithm:(TSKeyGenAsymmetricAlgorithm)algorithm;
		[Export ("initWithAlgorithm:")]
		IntPtr Constructor (Themis.iOS.TSKeyGenAsymmetricAlgorithm algorithm);
	}

	// @interface TSMessage : NSObject
	[BaseType (typeof(NSObject))]
	interface TSMessage
	{
		// @property (readonly, nonatomic) NSData * _Nonnull privateKey;
		[Export ("privateKey")]
		NSData PrivateKey { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull publicKey;
		[Export ("publicKey")]
		NSData PublicKey { get; }

		// @property (readonly, nonatomic) TSMessageMode mode;
		[Export ("mode")]
        Themis.iOS.TSMessageMode Mode { get; }

		// -(instancetype _Nullable)initInEncryptModeWithPrivateKey:(NSData * _Nonnull)privateKey peerPublicKey:(NSData * _Nonnull)peerPublicKey;
		[Export ("initInEncryptModeWithPrivateKey:peerPublicKey:")]
		IntPtr InitInEncryptMode(// Constructor
            NSData privateKey,
            NSData peerPublicKey);

		// -(instancetype _Nullable)initInSignVerifyModeWithPrivateKey:(NSData * _Nullable)privateKey peerPublicKey:(NSData * _Nullable)peerPublicKey;
		[Export ("initInSignVerifyModeWithPrivateKey:peerPublicKey:")]
		IntPtr InitInSignVerifyMode( // Constructor (
            [NullAllowed] NSData privateKey,
            [NullAllowed] NSData peerPublicKey);

		// -(NSData * _Nullable)wrapData:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:error:")]
		[return: NullAllowed]
		NSData WrapData ([NullAllowed] NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:error:")]
		[return: NullAllowed]
		NSData UnwrapData ([NullAllowed] NSData message, [NullAllowed] out NSError error);
	}

	// @interface TSComparator : NSObject
	[BaseType (typeof(NSObject))]
	interface TSComparator
	{
		// -(instancetype _Nullable)initWithMessageToCompare:(NSData * _Nonnull)message;
		[Export ("initWithMessageToCompare:")]
		IntPtr Constructor (NSData message);

		// -(NSData * _Nullable)beginCompare:(NSError * _Nullable * _Nullable)error;
		[Export ("beginCompare:")]
		[return: NullAllowed]
		NSData BeginCompare ([NullAllowed] out NSError error);

		// -(NSData * _Nullable)proceedCompare:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("proceedCompare:error:")]
		[return: NullAllowed]
		NSData ProceedCompare ([NullAllowed] NSData message, [NullAllowed] out NSError error);

		// -(TSComparatorStateType)status;
		[Export ("status")]
        Themis.iOS.TSComparatorStateType Status { get; }
	}

	// @interface TSSessionTransportInterface : NSObject
	[BaseType (typeof(NSObject))]
	interface TSSessionTransportInterface
	{
		// -(void)sendData:(NSData * _Nullable)data error:(NSError * _Nullable * _Nullable)error;
		[Export ("sendData:error:")]
		void SendData ([NullAllowed] NSData data, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)receiveDataWithError:(NSError * _Nullable * _Nullable)error;
		[Export ("receiveDataWithError:")]
		[return: NullAllowed]
		NSData ReceiveDataWithError ([NullAllowed] out NSError error);

		// -(NSData * _Nullable)publicKeyFor:(NSData * _Nullable)binaryId error:(NSError * _Nullable * _Nullable)error;
		[Export ("publicKeyFor:error:")]
		[return: NullAllowed]
		NSData PublicKeyFor ([NullAllowed] NSData binaryId, [NullAllowed] out NSError error);

		// -(secure_session_user_callbacks_t * _Nonnull)callbacks;
		//[Export ("callbacks")]
		//[Verify (MethodToProperty)]
		//unsafe secure_session_user_callbacks_t* Callbacks { get; }
	}

	// @interface TSSession : NSObject
	[BaseType (typeof(NSObject))]
	interface TSSession
	{
		// -(instancetype _Nullable)initWithUserId:(NSData * _Nonnull)userId privateKey:(NSData * _Nonnull)privateKey callbacks:(TSSessionTransportInterface * _Nonnull)callbacks;
		[Export ("initWithUserId:privateKey:callbacks:")]
		IntPtr Constructor (NSData userId, NSData privateKey, TSSessionTransportInterface callbacks);

		// -(NSData * _Nullable)connectRequest:(NSError * _Nullable * _Nullable)error;
		[Export ("connectRequest:")]
		[return: NullAllowed]
		NSData ConnectRequest ([NullAllowed] out NSError error);

		// -(BOOL)connect:(NSError * _Nullable * _Nullable)error;
		[Export ("connect:")]
		bool Connect ([NullAllowed] out NSError error);

		// -(NSData * _Nullable)wrapData:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapData:error:")]
		[return: NullAllowed]
		NSData WrapData ([NullAllowed] NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapData:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapData:error:")]
		[return: NullAllowed]
		NSData UnwrapData ([NullAllowed] NSData message, [NullAllowed] out NSError error);

		// -(BOOL)wrapAndSend:(NSData * _Nullable)message error:(NSError * _Nullable * _Nullable)error;
		[Export ("wrapAndSend:error:")]
		bool WrapAndSend ([NullAllowed] NSData message, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)unwrapAndReceive:(NSUInteger)length error:(NSError * _Nullable * _Nullable)error;
		[Export ("unwrapAndReceive:error:")]
		[return: NullAllowed]
		NSData UnwrapAndReceive (nuint length, [NullAllowed] out NSError error);

		// -(BOOL)isSessionEstablished;
		[Export ("isSessionEstablished")]
		bool IsSessionEstablished { get; }
	}
}
