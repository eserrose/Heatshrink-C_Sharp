#include "../pch.h"
#include "heatshrink_decoder.h"
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#define DECODE_INPUT_BUFFER_SIZE    65535    //64 kb

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
    __declspec(dllexport) uint8_t* HeatshrinkDecode(uint8_t* input, uint32_t input_size, uint32_t* decomp_sz)
    {
        heatshrink_decoder* hsd = heatshrink_decoder_alloc((uint16_t)DECODE_INPUT_BUFFER_SIZE, 8, 4);
        size_t sunk = 0;
        size_t polled = 0;
        size_t count = 0;

        uint8_t *output = (uint8_t * ) malloc(*decomp_sz);
        memset(output, 0, *decomp_sz);

        while (sunk < input_size) {
            heatshrink_decoder_sink(hsd, &input[sunk], input_size - sunk, &count);
            sunk += count;
            printf("^^ sunk %zd\n", count);
            if (sunk == input_size) {
                heatshrink_decoder_finish(hsd);
            }

            HSD_poll_res pres;
            do {
                pres = heatshrink_decoder_poll(hsd, &output[polled],
                    *decomp_sz - polled, &count);
                polled += count;
                if (count <= 0) { printf("count is smaller than 0\n"); break; }
                printf("^^ polled %zd\n", count);
            } while (pres == HSDR_POLL_MORE);

            if (sunk == input_size) {
                heatshrink_decoder_finish(hsd);
            }
        }
        printf("decompressed: %zu\n", polled); 
        *decomp_sz = polled;
        return output;
    }

}