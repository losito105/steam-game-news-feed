namespace steamgamenewsfeed;

using Terminal.Gui;

public class BackgroundWindow : Terminal.Gui.Window
{
    public BackgroundWindow() {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        this.Title = "STEAM GAME NEWS FEED";
        this.Border.Background = Color.DarkGray;
        this.Add(new GameNewsTable());
    }
}