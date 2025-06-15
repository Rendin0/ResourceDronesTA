using System.Runtime.InteropServices;
using System;
using Zenject;
using UnityEngine;
using R3;
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

    static readonly IntPtr HWND_TOPMOST = new(-1);
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

        // ������� ����� � ���������, ������������ ����� WS_POPUP
        uint style = GetWindowLong(hwnd, GWL_STYLE);
        style &= ~WS_CAPTION;
        style &= ~WS_THICKFRAME;
        style |= WS_POPUP | WS_VISIBLE;
        SetWindowLong(hwnd, GWL_STYLE, style);

        WindowOptions.Instance.Position.Subscribe(pos => SetWindowPosition(pos, hwnd));
    }

    public void SetWindowPosition(WindowPosition position, IntPtr hwnd)
    {
        // �������� ������ ������
        int screenHeight = Screen.currentResolution.height;
        int screenWidth = Screen.currentResolution.width;

        int windowHeight = 200; // ������ ����
        int windowWidth = 200; // ������ ����

        switch (position)
        {
            case WindowPosition.Bottom:
                SetWindowPos(hwnd, HWND_TOPMOST, -1, screenHeight - windowHeight, screenWidth + 1, windowHeight, SWP_NOACTIVATE);
                break;
            case WindowPosition.Top:
                SetWindowPos(hwnd, HWND_TOPMOST, -1, 0, screenWidth + 1, windowHeight, SWP_NOACTIVATE);
                break;
            case WindowPosition.Left:
                SetWindowPos(hwnd, HWND_TOPMOST, -1, 0, windowWidth, screenHeight + 1, SWP_NOACTIVATE);
                break;
            case WindowPosition.Right:
                SetWindowPos(hwnd, HWND_TOPMOST, screenWidth - windowWidth, 0, windowWidth, screenHeight + 1, SWP_NOACTIVATE);
                break;
        }


    }
}