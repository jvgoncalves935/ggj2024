using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
public class MouseOperations {
    private static Vector2 posicaoMouse;
    public enum MouseEventFlags {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition() {
        SetCursorPos((int)(posicaoMouse.x), (int)(posicaoMouse.y));
    }

    public static void SetCursorPosition(int X, int Y) {
        SetCursorPos(X, Y);
    }

    public static void SetCursorPosition(MousePoint point) {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition() {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if(!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value) {
        MousePoint position = GetCursorPosition();

        mouse_event
            ((int)value,
            position.X,
             position.Y,
             0,
             0)
             ;
    }

    public static void SalvarPosicaoCursorMultiPlat() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        posicaoMouse = new Vector2(Screen.width / 2, Screen.height / 2);
    #endif
    }

    public static void SetPosicaoCursorMultiPlat() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            //Debug.Log("clicou mouse");
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
    #endif
    }

    public static void TravarCursorMultiPlat() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    #endif
    }

    public static void DestravarCursorMultiPlat() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    #endif
    }

    public static void FocarMouseMultiPlat() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        FocarMouseWIN();
    #endif
    }

    private static void FocarMouseWIN() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        TravarCursorMultiPlat();
        SalvarPosicaoCursorMultiPlat();
        SetCursorPosition();
        SetPosicaoCursorMultiPlat();
    #endif
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint {
        public int X;
        public int Y;

        public MousePoint(int x, int y) {
            X = x;
            Y = y;
        }

    }

}
#endif
