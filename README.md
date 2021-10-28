<h2>Heatshrink-C#</h2>

A C# implementation of the Heatshrink library by atomicobject.
If you are using it on an embedded application but want to decode the files on a desktop application, you may use it.

<strong>Important note:</strong> You must select the platform target as x86 if you are using the included dlls!

If you want to create your own dll file follow the steps:

1) Download the heatshrink source files from https://github.com/atomicobject/heatshrink and put them in both "Encoder" and "Decoder" directories inside "HS_DLL" directory. Exclude the encoder and decoder ".c" files. Also wrap their headers in extern C blocks.

2) $gcc -shared -o heathsrink.dll dllmain.cpp

Alternatively, you can create a DLL application in Visual Studio and simply build the solution (as detailed here https://github.com/MicrosoftDocs/cpp-docs/blob/master/docs/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp.md)

Afterwards put the generated dlls into your application directory and the magic will happen.
