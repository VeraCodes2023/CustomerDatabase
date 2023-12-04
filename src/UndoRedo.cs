public class UndoRedoManager
{
    private Stack<CustomerAction> undoStack = new Stack<CustomerAction>();
    private Stack<CustomerAction> redoStack = new Stack<CustomerAction>();
    private CustomerAction currentAction;

    public UndoRedoManager()
    {
        // 在构造函数中初始化 currentAction，可以是默认操作或根据需要进行初始化
        currentAction = new CustomerAction("Initial", new Customer("","","",""), new Customer("","","",""));
    }
    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            CustomerAction lastAction = undoStack.Pop();
            CustomerAction previousState = currentAction;
            currentAction = lastAction;
            currentAction.PreviousState= lastAction.CurrentCustomer;
            redoStack.Push(lastAction);
        }
        
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            CustomerAction lastRedoAction = redoStack.Pop();
            CustomerAction previousState = currentAction;
            currentAction = lastRedoAction;
            currentAction.PreviousState = previousState.CurrentCustomer;
            undoStack.Push(lastRedoAction);
 
        }
    }

    public void CaptureAction(string description, Customer previousState, CustomerAction action)
    {
        
        undoStack.Push(action);
        redoStack.Clear();
        currentAction=action;
    }
}
