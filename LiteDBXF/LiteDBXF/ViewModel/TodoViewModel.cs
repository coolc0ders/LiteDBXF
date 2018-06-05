using LiteDBXF.Model;
using LiteDBXF.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace LiteDBXF.ViewModel
{
    public class TodoViewModel : ReactiveObject
    {
        /// <summary>
        /// Reactive List https://reactiveui.net/docs/handbook/collections/reactive-list
        /// </summary>
        ReactiveList<Todo> _todos;
        public ReactiveList<Todo> Todos
        {
            get => _todos;
            set => this.RaiseAndSetIfChanged(ref _todos, value);
        }
        private Todo _selectedTodo;
        public Todo SelectedTodo
        {
            get => _selectedTodo;
            set => this.RaiseAndSetIfChanged(ref _selectedTodo, value);
        }
        
        private string _todoTitl;
        public string TodoTitle
        {
            get { return _todoTitl; }
            set { this.RaiseAndSetIfChanged(ref _todoTitl, value); }
        }

        TodoLiteDBService _liteDBService;

        public ReactiveCommand AddCommand { get; private set; }

        public TodoViewModel()
        {
            _liteDBService = new TodoLiteDBService();
            var todos = _liteDBService.ReadAllItems();
            if (todos.Any())
            {
                Todos = new ReactiveList<Todo>(todos) { ChangeTrackingEnabled = true };
            }
            else { Todos = new ReactiveList<Todo>() { ChangeTrackingEnabled = true }; }

            AddCommand = ReactiveCommand.Create(() =>
            {
                var todo = new Todo() { Title = TodoTitle };
                Todos.Add(todo);
                TodoTitle = string.Empty;
                _liteDBService.CreateItem(todo);

            }, this.WhenAnyValue(x => x.TodoTitle,
                title =>
                !String.IsNullOrEmpty(title)));

            ///Lets detect when ever a todo Item is marked as done 
            ///IF it is, it is sent to the bottom of the list
            ///Else nothing happens
            Todos.ItemChanged.Where(x => x.PropertyName == "IsDone" && x.Sender.IsDone)
                .Select(x => x.Sender)
                .Subscribe(x =>
                {
                    if (x.IsDone)
                    {
                        Todos.Remove(x);
                        Todos.Add(x);
                        _liteDBService.UpdateItem(x);
                    }
                });
        }
    }
}
