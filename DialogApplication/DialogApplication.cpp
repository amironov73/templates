#include <windows.h>
#include <CommCtrl.h>
#include <tchar.h>
#include <wchar.h>
#include <stdio.h>

#include "resource.h"

// подключаем библиотеку современных контролов
#pragma comment (lib, "ComCtl32.lib")

// включаем поддержку тем
#if defined _M_IX86
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='x86' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_X64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='amd64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#else
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#endif

// инициализация диалога
BOOL InitializeDialog
    (
        HWND hwnd
    )
{
    // загружаем иконку диалога из ресурсов
    HICON hicon = (HICON) LoadImage
        (
            GetModuleHandle (nullptr),
            MAKEINTRESOURCE(IDI_MAIN_ICON),
            IMAGE_ICON,
            0,
            0,
            LR_DEFAULTCOLOR | LR_DEFAULTSIZE
        );
    SendMessage
        (
            hwnd,
            WM_SETICON,
            ICON_BIG,
            (LPARAM) hicon
        );
    SendMessage
        (
            hwnd,
            WM_SETICON,
            ICON_SMALL,
            (LPARAM) hicon
        );

    return FALSE;
}

// простая процедура для окна "О программе"
BOOL CALLBACK AboutProc
    (
        HWND hwnd,
        UINT message,
        WPARAM wParam,
        LPARAM lParam
    )
{
    switch (message)
    {
        case WM_CLOSE:
            EndDialog (hwnd, 0);
            return FALSE;

        case WM_COMMAND:
            if (LOWORD (wParam) == IDCANCEL)
            {
                EndDialog (hwnd, 0);
            }

        default:
            return FALSE;
    }
}

// обработка нажатий кнопок
BOOL HandleCommand
    (
        HWND hwnd,
        UINT message,
        WPARAM wParam,
        LPARAM lParam
    )
{
    switch (LOWORD (wParam))
    {
        case IDOK:
        case IDCANCEL:
            SendMessage (hwnd, WM_CLOSE, 0, 0);
            break;

        default:
            break;
    }

    return FALSE;
}

// процедура-обработчик сообщений для диалогового окна
BOOL CALLBACK DialogProc
    (
        HWND hwnd,
        UINT message,
        WPARAM wParam,
        LPARAM lParam
    )
{
    switch (message)
    {
        case WM_INITDIALOG:
            return InitializeDialog (hwnd);

        case WM_COMMAND:
            return HandleCommand
                (
                    hwnd,
                    message,
                    wParam,
                    lParam
                );

        case WM_CLOSE:
            EndDialog (hwnd, 0);
            return FALSE;

        default:
            return FALSE;
    }
}

// точка входа в программу
int WINAPI WinMain
    (
        HINSTANCE hInstance,     // текущий экземпляр приложения
        HINSTANCE hPrevInstance, // не используется
        LPSTR pCmdLine,          // командная строка
        int iCmdShow             // режим отображения окна
    )
{
    InitCommonControls();

    return (int) DialogBox
        (
            hInstance,
            MAKEINTRESOURCE (IDD_MAIN_DIALOG),
            nullptr,
            (DLGPROC) DialogProc
        );
}
