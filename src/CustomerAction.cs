
public class CustomerAction
{
    public string ActionType { get; set; }
    public Customer PreviousState { get; set; }
    public Customer CurrentCustomer { get; set; }
    public CustomerAction(string actionType,Customer previousState, Customer currentCustomer)
    {
        ActionType = actionType;
        PreviousState = previousState;
        CurrentCustomer = currentCustomer;
    }  


}
