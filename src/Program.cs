using System.Data.Common;
using System.Reflection.Metadata;
using CustomerDatabase.src.Helper;

class Program
{
    static void Main(string[] args)
    {
        // pass the argu to constructor of Customer_Database, get the populated collection of data.
        string filePath="src/customers.csv";
        Customer_Database database= new Customer_Database(filePath);
        List<Customer> customers= database.GetCustomers;
        // instantiate customer: 
        var newCusotmer1=new Customer("John", "Smith", "smith@email.com", "123 Main Street");
        var newCusotmer2=new Customer("Alice", "Johnson", "alice@email.com", "456 Elm Avenue");
        var newCusotmer3=new Customer("Michael", "Brown", "brown@email.com", "789 Oak Road");
        var newCusotmer4=new Customer("Emily", "Davis", "davis@email.com", "101 Pine Lane");
        var newCusotmer5=new Customer("Robert", "Wilson", "robert@email.com", "222 Cedar Drive");
        var newCusotmer6=new Customer("Sarah", "Lee", "lee@email.com", "987 Main Street");
        var newCusotmer7=new Customer("William", "Anderson", "william@email.com", "444 Maple Way");
        var newCusotmer8=new Customer("Jennifer", "Miller", "jennifer@email.com", " 555 Spruce Street");
        var newCusotmer9=new Customer("David", "Martin", "david.martin@email.com", "666 Fir Avenue");
        var newCusotmer10=new Customer("Jessica", "Taylor", "jessica@email.com", "123 Main Street");
        var newCusotmer12=new Customer("Tracy", "Chang", "tracy@email.com", "123 Main Street");
        // database.AddCustomer(newCusotmer12);
        UndoRedoManager undoredo = new UndoRedoManager();
        var previousState = new Customer(newCusotmer12.FirstName, newCusotmer12.LastName,newCusotmer12.Email, newCusotmer12.Address);
        CustomerAction action = new CustomerAction("Add Customer",newCusotmer12,previousState);
        undoredo.CaptureAction("Add Customer", newCusotmer12, action);
        undoredo.Undo();

        foreach(Customer c in customers)
        {
           // Console.WriteLine(c.FirstName+c.LastName+c.Address+c.Email);
        }

        // call the FindById function from database instantiate, expect to return an object in Customer type.
        var customerFindById=database.FindById(10);
        if(customerFindById is not null){
            //  Console.WriteLine(customerFindById.FirstName+customerFindById.LastName+customerFindById.Address+customerFindById.Email);
        }

       // update Customer action 
        var updateData1 = new Customer.CustomerUpdateDTO("Xu", "Yan", "Xuyan@example.com", "221 Glen St6");
        var updateData2= new Customer.CustomerUpdateDTO("David", "Murdoch","david@hotmail.com","098 Wege St");
        var updateData8 = new Customer.CustomerUpdateDTO("Vera", "Chang","vera@hotmail.com","098 Wege St");
        // database.UpdateCustomer(9,updateData8);

      // remove Customer action 
      // database.RemoveCustomer(2);
      //  database.RemoveCustomer(16);

    }

    


}

