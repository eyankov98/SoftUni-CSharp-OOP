using System.Collections.Generic;

namespace DetailPrinter;

class Program
{
    static void Main()
    {
        Employee employee = new Employee("John The Employee");
        ICollection<string> documents = new List<string>()
        {
            "Document One",
            "Document Two",
            "Document Three"
        };
        Employee manager = new Manager("Peter The Manager", documents);

        List<Employee> employees = new List<Employee>();
        employees.Add(employee);
        employees.Add(manager);

        DetailsPrinter printer = new DetailsPrinter(employees);
        printer.PrintDetails();
    }
}
