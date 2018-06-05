using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteDBXF.Model
{
    public class Todo : ReactiveObject
    {
        public string ID { get; set; }
        public string Title { get; set; }
        bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set => this.RaiseAndSetIfChanged(ref _isDone, value);
        }
        public bool IsEnabled => !IsDone;
    }
}
