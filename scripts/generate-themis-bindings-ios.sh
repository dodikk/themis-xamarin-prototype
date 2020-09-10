sharpie bind                                                         \
    -framework ../lib-native/ios/Carthage/Build/iOS/themis.framework \
    -sdk iphoneos13.6                                                \
    -output ../lib-bindings/themis/themis.ios                        \
    -namespace "Themis.iOS"

#### Sharpie docs
## https://docs.microsoft.com/en-gb/xamarin/cross-platform/macios/binding/objective-sharpie/examples/cocoapod
## https://docs.microsoft.com/en-gb/xamarin/cross-platform/macios/binding/objective-sharpie/examples/advanced
## https://docs.microsoft.com/en-us/xamarin/ios/platform/binding-objective-c/walkthrough


# ===
# [Verify] attributes intentionally cause C# compilation errors 
so that you are forced to verify the binding. 
You should remove the [Verify] attribute 
when you have reviewed (and possibly corrected) the code.
# ===
