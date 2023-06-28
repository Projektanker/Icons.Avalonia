using System.Collections.Generic;
using System;
using System.ComponentModel;
using Projektanker.Icons.Avalonia;

namespace Demo;

public class DemoViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isEnabled;
    private IEnumerable<IconAnimation> _iconAnimations;

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            PropertyChanged?.Invoke(this, new(nameof(IsEnabled)));
        }
    }

    public IEnumerable<IconAnimation> Animations
        => _iconAnimations ??= Enum.GetValues<IconAnimation>();
}
