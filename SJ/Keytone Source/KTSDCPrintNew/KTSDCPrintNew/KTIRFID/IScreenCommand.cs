using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace KTone.KTControls.TouchScreenKeyPad
{
    public interface IScreenCommand
    {

        void HideWindow();
        void ShowWindow();
        void CloseWindow();

        void SetOnTop();
        void ResetOnTop();

        void SetSize(Size sz);
        void SetLocation(Point pt);
        Point GetLocation();
        Size GetSize();

        bool IsHidden { get; }
    }


}
