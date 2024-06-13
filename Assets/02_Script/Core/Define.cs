
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
        CheckBox = 16,
        Text = 32,
        SceneChange = 64,
        Exit = 128,
        Selection = 256,
        Setup = 512,
        Init = 1024
    }
}
