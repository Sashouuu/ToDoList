using System;

namespace TodoListApp
{
    [Serializable]
    public class TodoItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return (IsCompleted ? "☑ " : "□ ") + Description;
        }
    }
}
