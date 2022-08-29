using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Projektanker.Icons.Avalonia;

namespace Demo
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = this;
        }

        public IEnumerable<IconAnimation> Animations => Enum.GetValues<IconAnimation>();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}