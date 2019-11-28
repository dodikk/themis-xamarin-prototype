The project depends on `themis` library binaries.
The version has been pinned to `v0.12.0`

For convenience they have been committed into this git repo since it is a prototype and is not supposed to be changed frequently.
If you need later versions, please do the following steps:
1. update `Cartfile` in `lib-native/ios` directory
2. update java `*.aar` package links in `scripts/download-binaries-droid.sh`
3. run `configure.sh` which will download artefacts from the internet


### If you are planning to build your project on top of this prototype
```
Do not commit binaries and artefacts to your repo
* *.framework ios artefacts 
* Carthage directory binary contents
*  *.aar packages
```
