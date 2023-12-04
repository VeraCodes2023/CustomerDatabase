using System.Linq.Expressions;

namespace CustomerDatabase.src.Helper
{
    public class FileHelper
    {
        private string _path;
        private FileInfo _fi;

        public FileHelper(string path)
        {
            _path = path;
            _fi = new FileInfo(path);
        }

        public string[]? GetAll()
        {
            try
            {
                var data = File.ReadAllLines("src/customers.csv");
                return data;
            }          
            catch (Exception e)
            {
                throw ExceptionHandler.FetchDataException(e.Message);
            }
        }

        public void AddNew(string content)
        {
            
            File.AppendAllText(_path, content);
        }

        public async Task AddNewAsync(string content) //naming convention for async method: ended with `Async`
        {
            // await File.AppendAllTextAsync(_path, content);
            try{
                using var sw = _fi.AppendText();
                await sw.WriteLineAsync(content);
            }catch(Exception e)
            {
                Console.WriteLine($"An error occurred: {e}");
            }
           
        }

        public string[] ReadFromDatabase()
        {
            try{
                return File.ReadAllLines("src/customers.csv");

            }catch(Exception e)
            {
                throw ExceptionHandler.FetchDataException(e.Message);
            }
        }
        public void UpdateCustomer(int id, string updatedData)
        {
          var allData=GetAll();
          if(allData is not null){

            for(int i=0;i<allData.Length;i++)
          {
            string[] values= allData[i].Split(',');
            if(values.Length ==5 && int.TryParse(values[0], out int CustomerId) && CustomerId ==id)
            {
                 allData[i] = $"{updatedData}";
                //  break;
            }
          }
          File.WriteAllLines(_path, allData);
          }
         
        }

        public void DeleteCustomer(int id)
        {
           var allData = GetAll();
           var dataAfterDelete = new List<string>();
           if(allData is not null)
           foreach (string line in allData)
           {
                string[] values = line.Split(',');
                if(values.Length ==5 && int.TryParse(values[0], out int CustomerId) && CustomerId ==id)
                {
                    
                    continue;
                }

              dataAfterDelete.Add(line);
            
           }
        
           File.WriteAllLines(_path, dataAfterDelete);
       
          

        }

    }
}