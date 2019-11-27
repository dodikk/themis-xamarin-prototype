
# ====
# The bindings should be generated automatically
# as part of VisualStudio build step
# via reflection
# ====


### https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/binding-an-aar
# https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/
# https://marketplace.visualstudio.com/items?itemName=EgorBogatov.XamarinGradleBindings
# https://github.com/mattleibow/Xamarin.Android.Bindings.Generators.git


## A binding project can only include one .AAR file. 
## If the .AAR depends on other .AAR, 
## then those dependencies should be contained in their own binding project 
## and then referenced. See Bug 44573.
