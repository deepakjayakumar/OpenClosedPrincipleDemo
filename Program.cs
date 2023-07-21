namespace OpenClosedPrincipleDemo
{
    internal class Program
    {
        public enum Color
        {
            Red,Green, Blue
        }
        public enum Size
        {
            Small,Medium,Large
        }

        public class Product
        {
            public string Name;
            public Color Color;
            public Size Size;

            public Product(string name, Color color, Size size)
            {
                  if(name == null)
                    throw new ArgumentNullException("name");

                Name = name;  
                Color = color;  
                Size = size;
            }   
        }

        public class ProductFilter
        {

            public  IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) {

                foreach (Product p in products)
                {
                    if(p.Size == size)
                    yield return p;
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {

                foreach (Product p in products)
                {
                    if (p.Color == color)
                        yield return p;
                }
            }


        }



        static void Main(string[] args)
        {
            var apple = new Product("apple", Color.Red, Size.Small);
            var tree = new Product("tree", Color.Green, Size.Medium);
            var house = new Product("house", Color.Blue, Size.Large);

            Product[] products = new Product[] {apple, tree, house};

            var pf = new ProductFilter();
            foreach(var p in pf.FilterByColor(products,Color.Red)) 
            {
                Console.WriteLine($"{p.Name}" + " is red");
            }
                

            
        }
    }
}