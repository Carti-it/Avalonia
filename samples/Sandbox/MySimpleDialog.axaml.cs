#nullable enable

using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;

namespace Sandbox;

public partial class MySimpleDialog : Window
{
    public bool Result { get; private set; } = false;

    public MySimpleDialog()
    {
        InitializeComponent();
    }

    private void Yes_Click(object? sender, RoutedEventArgs e)
    {
        Result = true;
        Close();
    }

    private void No_Click(object? sender, RoutedEventArgs e)
    {
        Result = false;
        Close();
    }

    // Helper: show and await result
    public static async Task<bool> ShowAsync(Window owner, string message = "Are you sure?")
    {
        var dlg = new MySimpleDialog
        {
            // You can make Title / message dynamic if needed
        };

        await dlg.ShowDialog(owner);
        return dlg.Result;
    }
}
