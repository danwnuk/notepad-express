using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
public partial class Editor : UserControl
{
    [DllImport("User32.dll")]
    public extern static int GetScrollPos(IntPtr hWnd, int nBar);
    public event EventHandler UpdateNotepadExpressEvent;
    [DllImport("User32.dll")]
    public extern static int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    public Editor()
    {
        InitializeComponent();
    }

    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }

    private void textEditor_VScroll(object sender, EventArgs e)
    {
        syncScroll();
    }

    private void textEditor_TextChanged(object sender, EventArgs e)
    {
        String[] numbers = new String[textEditor.Lines.Length + 1];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = (i + 1).ToString();
        }
        lineNumbers.Lines = numbers;
    }

    private void syncScroll()
    {
        int nPos = GetScrollPos(textEditor.Handle, (int)ScrollBarType.SbVert);
        nPos <<= 16;
        uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
        SendMessage(lineNumbers.Handle, 0x115, new IntPtr(wParam), new IntPtr(0));
    }

    public RichTextBox TextEditor
    {
        get
        {
            return this.textEditor;
        }
    }

    private void textEditor_CursorChanged(object sender, EventArgs e)
    {
        if (UpdateNotepadExpressEvent != null)
            UpdateNotepadExpressEvent(sender, e);
    }

}
