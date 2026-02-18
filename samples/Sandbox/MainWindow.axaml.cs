#nullable enable

using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        private readonly IDisposable? _clipboardSub;

        public MainWindow()
        {
            InitializeComponent();

            // Option A – simplest
            _clipboardSub = ClipboardObservable
                .ClipboardTextChanged(this)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(text =>
                {
                    Console.WriteLine($"Clipboard changed → '{text ?? "<empty>"}'");
                });
        }

        private async void Test_OnClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new MySimpleDialog();
            await dialog.ShowDialog(this);
        }

        protected override void OnClosed(EventArgs e)
        {
            _clipboardSub?.Dispose();
            base.OnClosed(e);
        }
    }
}
