The project depends on `themis` library binaries.
The version has been pinned to `v0.13.2`.

Other versions can be found in tags and branches
* `v0.13.2`
* `v0.12.0`

### How to build and run

1. Open `app/ThemisDemoXamarin.sln` in VisualSturio (either windows or mac)
2. Select `ThemisDemoXamarin.iOS` or `ThemisDemoXamarin.Android` with `Set as Startup project`
3. Build and run
4. Look into console logs.
5. **Note:** on android search for `[themis demo]` prefix since the system generates a lot of other logs


### Prototype notes:

For convenience the dependencies have been committed into this git repo since it is a prototype and is not supposed to be changed frequently.
If you need later versions, please do the following steps:
1. update `Cartfile` in `lib-native/ios` directory
2. update java `*.aar` package links in `scripts/download-binaries-droid.sh`
3. run `configure.sh` which will download artefacts from the internet

```
No windows scripts yet but please feel free to send a pull request.
```

## If you are planning to build your project on top of this prototype
```
Do not commit binaries and artefacts to your repo
```

* *.framework ios artefacts 
* Carthage directory binary contents
*  *.aar packages

## Known issues

Build artefact optimizer of Xamarin.Forms or Xamarin.Android might strip native Java classes from the binding lib
For example:
```
Java.Lang.IncompatibleClassChangeError: no non-static method "Lcom/cossacklabs/themis/SecureCellSeal;.decrypt([B[B)[B"
[orion.mobile]   at Java.Interop.JniEnvironment+InstanceMethods.GetMethodID (Java.Interop.JniObjectReference type, System.String name, System.String signature) [0x0005b] in <42d2b7086f0a46efb99253c5db1ecca9>:0 
[orion.mobile]   at Android.Runtime.JNIEnv.GetMethodID (System.IntPtr kls, System.String name, System.String signature) [0x00007] in <3080427739614e60a939a88bf3f838d5>:0 
[orion.mobile]   at Com.Cossacklabs.Themis.SecureCell+ISealInvoker.Decrypt (System.Byte[] p0, System.Byte[] p1) [0x00017] in <cd618986d1ce4194b63cdd3366dad291>:0 
[orion.mobile]   at Themis.Droid.CellSealDroid.UnwrapData (Themis.ISecureCellData cipherTextData, System.Byte[] context) [0x0007e] in <a492e7118e094c3296442a386fe5d80e>:0 
[orion.mobile]    --- End of inner exception stack trace ---
```

Most relevant resources for troubleshooting found so far:
* https://gist.github.com/JonDouglas/dda6d8ace7d071b0e8cb
* https://stackoverflow.com/questions/38967851/missing-methods-and-classes-in-jar-after-building-xamarin-app

---

☕ Buy me a coffee <br>
With ETH 🔸: 0xD961220f3C356Bee7E7905377fE9Da95DE6F978E <br>
With BTC ₿: bc1qte94s9smlzy8k0jxlfzp7pzkdx9xzztnngcg4n
