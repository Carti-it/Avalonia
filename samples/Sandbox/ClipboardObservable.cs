#nullable enable

using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Threading;

namespace Sandbox;

public static class ClipboardObservable
{
    private static readonly TimeSpan s_pollingInterval = TimeSpan.FromMilliseconds(50);

    public static IObservable<string?> ClipboardTextChanged(TopLevel topLevel)
    {
        var clipboard = topLevel.Clipboard
            ?? throw new InvalidOperationException("TopLevel has no Clipboard");

        return Observable
            .Timer(s_pollingInterval)
            .Repeat()
            .Select(_ => Observable.FromAsync(async () =>
            {
                return await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    return await clipboard.TryGetTextAsync();
                });
            })) 
            .Merge(1)
            .DistinctUntilChanged()
            .Publish()
            .RefCount();
    }
}
