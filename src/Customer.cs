using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
1. Create `Customer` class: This class represents a customer 
and should contain properties such as Id, FirstName, 
LastName, Email, and Address. You can decide the suitable data types for each.
*/

public class Customer
{
    public int Id;
    public string FirstName;
    public string LastName;
    public string Email;
    public string Address;
    private static int _id=0;
    public Customer(string firstName, string lastName, string email, string address)
    {
        Id = _id+1;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    
    }
    public void UpdateCustomer(CustomerUpdateDTO updateCustomer)
    {
        FirstName = updateCustomer.FirstName;
        LastName = updateCustomer.LastName;
        Email = updateCustomer.Email;
        Address = updateCustomer.Address;

    }
    public class CustomerUpdateDTO
    {
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public string Email {get;set;}
        public string Address {get;set;}

        public CustomerUpdateDTO(string firstname,string lastName,string email, string address)
        {
            FirstName = firstname;
            LastName = lastName;
            Email = email;
            Address = address;
        }
    }


}
