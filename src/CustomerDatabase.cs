using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDatabase.src.Helper;
using Microsoft.VisualBasic;

public class Customer_Database
{
   
    private List<Customer> customers = new List<Customer>();
    // private CustomerAction? currentAction;
    private UndoRedoManager  undoredo = new UndoRedoManager();


   public Customer_Database(string filePath)
   {
     FileHelper fileHelper= new FileHelper(filePath);
     if(fileHelper is not null){
        LoadFromDatabase(fileHelper);
     }
   } 
    private void LoadFromDatabase(FileHelper fileHelper)
    {
        
            string[] csvLines = fileHelper.ReadFromDatabase();
            foreach (string line in csvLines)
            {
            string[] values = line.Split(',');
            if (values.Length == 5)
            {
                int id = int.Parse(values[0]);
                string firstName = values[1];
                string lastName = values[2];
                string email = values[3];
                string address = values[4];
             
                customers.Add(new Customer(firstName, lastName, email, address ) { Id = id });
            }
        }

    }

    public bool AddCustomer(Customer customer)
    {

       if(!customers.Any(c=>c.Email==customer.Email))
       
       {
             Customer previousState = new Customer(customer.FirstName, customer.LastName, customer.Email, customer.Address);
             CustomerAction action = new CustomerAction("Add Customer",customer, previousState);
             customer.Id= GetNextCustomerId();
             customers.Add(customer);
             SaveAddToDatabase();
             Console.WriteLine("Customer added successfully.");
             undoredo.CaptureAction("Add Customer",previousState,action);
            return true;
       }else{
        Console.WriteLine("Customer with the same email already exists.");
        return false;
       }
    }
    public Customer? FindById(int id)
    {
        var targetedCustomer= customers.Find(c =>c.Id == id);
        if(targetedCustomer is not null)
        {
            
            return  targetedCustomer;
        }else{
            return null;
        }
    }
    public  bool UpdateCustomer(int id, Customer.CustomerUpdateDTO update)
    {
        var targetedCustomer= customers.Find(c =>c.Id == id);
        if(targetedCustomer is not null){
            targetedCustomer.UpdateCustomer(update);
            SaveUpdateToDatabase(id);
            Console.WriteLine("update ok");
           return true;
        }else{
             Console.WriteLine("update failed");
             return false;
        }
    }

    public bool RemoveCustomer(int id)
    {
        var targetedCustomer= customers.Find(c =>c.Id == id);
        if(targetedCustomer is not null)
        {
            FileHelper filehelper= new FileHelper("src/customers.csv");
            customers.Remove(targetedCustomer);
            filehelper.DeleteCustomer(id);
            Console.WriteLine("remove ok");
            return true;
        }else
        {
            Console.WriteLine("remove failed");
            return false;
        }
    }


    public List<Customer> GetCustomers
    {
        get
        {
            return new List<Customer>(customers);
        }
    }
  
   private int GetNextCustomerId()
    {
        int maxId = customers.Any() ? customers.Max(c => c.Id) : 0;
        Console.WriteLine($"maxid is: {maxId}");
        return maxId+1;
    }
   
    private void SaveAddToDatabase()
    {
        string scvData = GenerateCsvData(customers.Last());
        FileHelper filehelper= new FileHelper("src/customers.csv");
        filehelper.AddNew(scvData);
    }

    private void SaveUpdateToDatabase(int id)
    {
        var updateCustomer=customers.Find(c =>c.Id == id);
        if(updateCustomer is not null){
            string scvData = GenerateCsvupdate(updateCustomer);
            FileHelper filehelper= new FileHelper("src/customers.csv");
            filehelper.UpdateCustomer(id,scvData);
        }
        
    }

    private string GenerateCsvData(Customer customer)
    {
        return $"{customer.Id}, {customer.FirstName}, {customer.LastName},{customer.Email},{customer.Address}\n";
    }
    private string GenerateCsvupdate(Customer customer)
    {
        return $"{customer.Id}, {customer.FirstName}, {customer.LastName},{customer.Email},{customer.Address}";
    }



}
