using R3;

public enum WindowPosition
{
    Bottom,
    Left,
    Right,
    Top
}

public class WindowOptions
{
    private static WindowOptions _instance;
    public static WindowOptions Instance
    {
        get
        {
            _instance ??= new WindowOptions();
            return _instance;
        }
    }

    private WindowOptions() {}

    public ReactiveProperty<WindowPosition> Position { get; set; } = new(WindowPosition.Bottom);
}