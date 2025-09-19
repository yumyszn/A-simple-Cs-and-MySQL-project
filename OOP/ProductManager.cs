public class ProductManager
{
    private string connectionString;
    public ProductManager(string connStr)
    {
        connectionString = connStr;
    }
    public void Add()
    {
        Product product = new Product();

        Console.Write("Enter Name: ");
        product.Name = Console.ReadLine();

        Console.Write("Enter Stock: ");
        product.Stock = int.Parse(Console.ReadLine());

  

        MySqlDB.AddProductToDatabase(product, connectionString);

        

    }

    public void Update()
    {
        Product product = new Product();

        Console.Write("ID of the Product you want to update:  ");
        product.ID = int.Parse(Console.ReadLine());

        Console.Write("Enter New Name: ");
        product.Name = Console.ReadLine();

        Console.Write("Enter New Stock: ");
        product.Stock = int.Parse(Console.ReadLine());
        MySqlDB.UpdateProductToDatabase(product, connectionString);
    }

    public void Remove()
    {
        Product product = new Product();

        Console.Write("Enter the ID of the product you want to delete: ");
        product.ID = int.Parse(Console.ReadLine());

        MySqlDB.RemoveProductToDatabase(product, connectionString);
        
    }
    
        public void Show()
    {
        Product product = new Product();
        MySqlDB.ShowProductToDatabase(product, connectionString);
    }
}


