using System.Runtime.InteropServices;
using System;
using Zenject;
using UnityEngine;
public class DesktopWindow : IInitializable
{
    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const uint WS_THICKFRAME = 0x00040000;
    const uint WS_CAPTION = 0x00C00000;

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_NOACTIVATE = 0x0010;


    public void Initialize()
    {
#if UNITY_EDITOR
#else
    InitWindow();
#endif
    }

    private void InitWindow()
    {
        Application.runInBackground = true;
        IntPtr hwnd = GetActiveWindow();

        // Убираем рамки и заголовок, устанавливая стиль WS_POPUP
        uint style = GetWindowLong(hwnd, GWL_STYLE);
        style &= ~WS_CAPTION; 
        style &= ~WS_THICKFRAME;
        style |= WS_POPUP | WS_VISIBLE;
        SetWindowLong(hwnd, GWL_STYLE, style);

        // Получаем высоту экрана
        int screenHeight = Screen.currentResolution.height;
        int screenWidth = Screen.currentResolution.width;

        int windowHeight = 200; // Высота окна

        // Устанавливаем позицию и размер окна: ширина 200, высота - полный экран, позиция (0,0)
        SetWindowPos(hwnd, HWND_TOPMOST, 0, screenHeight - windowHeight, screenWidth, windowHeight, SWP_NOZORDER | SWP_NOACTIVATE);
    }
}