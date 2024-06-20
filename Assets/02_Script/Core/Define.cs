
using System;

namespace UIFunction
{
    public enum SceneType
    {
        Sample
    }

    [Flags]
    public enum UIKeyword
    {
        None = 0,
        Button = 1,
        Panel = 2,
        Slider = 4,
        Toggle = 8,
        DropDown = 16,
        CheckBox = 32,
        Label = 64,
        SceneChange = 128,
        Exit = 256,
        Selection = 512,
        Setup = 1024,
        Init = 2048,
        Deco = 4096
    }
}
