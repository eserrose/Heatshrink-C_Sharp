#include "../pch.h"
#include "../heatshrink_encoder.h"
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C"
{
    __declspec(dllexport) uint8_t* HeatshrinkEncode(uint8_t* input, uint32_t input_size, uint32_t* decomp_sz)
    {
        heatshrink_encoder* hse = heatshrink_encoder_alloc(8, 4);
        uint32_t sunk = 0;
        uint32_t polled = 0;
        size_t count = 0;

        size_t comp_sz = input_size + (input_size / 2) + 4;
        uint8_t* comp = (uint8_t*) malloc(comp_sz);

        while (sunk < input_size) {
            heatshrink_encoder_sink(hse, &input[sunk], input_size - sunk, &count);
            sunk += count;

            if (sunk == input_size) {
                heatshrink_encoder_finish(hse);
            }

            HSE_poll_res pres;
            do {
                pres = heatshrink_encoder_poll(hse, &comp[polled], comp_sz - polled, &count);
                polled += count;
            } while (pres == HSER_POLL_MORE);

            if (sunk == input_size) {
                heatshrink_encoder_finish(hse);
            }
        }

        printf("compressed: %zu\n", polled); 
        *decomp_sz = polled;
        return comp;
    }
}