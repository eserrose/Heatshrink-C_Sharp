Heatshrink-C#

You must select the platform target as x86 if you are using the included dll!

If you want to create your own dll file follow the steps:

1) Download the heatshrink source files from https://github.com/atomicobject/heatshrink and put them in the "HS_DLL" directory.

2) $gcc -shared -o heathsrink.dll dllmain.cpp

Alternatively, you can create a DLL application in Visual Studio and simply build the solution (as detailed here https://github.com/MicrosoftDocs/cpp-docs/blob/master/docs/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp.md)

Afterwards put the generated dll into your application directory and the magic will happen.