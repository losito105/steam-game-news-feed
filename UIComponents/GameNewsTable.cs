namespace steamgamenewsfeed;

using System.Data;
using Terminal.Gui;
using steamgamenewsfeed;
using System.Collections.Generic;
using System.Diagnostics;

public class GameNewsTable : Terminal.Gui.TableView
{
    public GameNewsTable()
    {
        InitializeComponent();
    }

    private async void InitializeComponent()
    {
        List<int> games = new List<int>
        {
            3065800, // Marathon
            1671210, // Deltarune
            2001120, // Split Fiction
        };
        DataTable dt = await SteamAPIWrapper.GetNewsForApp(games, 100);
        this.Table = dt;

        this.Width = Dim.Fill(0);
        this.Height = Dim.Fill(0);
        this.X = 0;
        this.Y = 0;

        foreach (DataColumn column in dt.Columns)
        {
            this.Style.GetOrCreateColumnStyle(column).ColorGetter = (e) =>
            {
                return CreateScheme(Color.White);
            };
        }

        this.KeyPress += (keyEventArgs) =>
        {
            if (keyEventArgs.KeyEvent.Key == Key.Enter)
            {
                int selectedColumn = this.SelectedColumn;
                int selectedRow = this.SelectedRow;

                // Link Column.
                if (selectedColumn == 3)
                {
                    // Open link in default browser (only Windows is supported at the moment).
                    string url = this.Table.Rows[selectedRow].Field<string>("Link");
                    url = url.Replace("&", "^&");
                    System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
            }
        };
    }

    private static ColorScheme CreateScheme(Color foreground)
    {
        return new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(foreground, Color.Blue),
            Focus = new Terminal.Gui.Attribute(Color.Blue, foreground),
            HotNormal = new Terminal.Gui.Attribute(foreground, Color.Blue),
            HotFocus = new Terminal.Gui.Attribute(Color.Blue, foreground),
            Disabled = new Terminal.Gui.Attribute(foreground, Color.Blue)
        };
    }
}